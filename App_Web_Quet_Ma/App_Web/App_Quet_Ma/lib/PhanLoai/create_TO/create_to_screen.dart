/// =============================================================
/// File: create_to_screen.dart
/// Mô tả: UI (giao diện) cho màn hình "Create TO" - Tạo bao hàng.
///        Logic xử lý nằm trong create_to_logic.dart.
/// =============================================================
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'package:audioplayers/audioplayers.dart';
import 'package:intl/intl.dart';
import '../../models/to_model.dart';
import 'create_to_logic.dart';
import '../ketqua_to_Screen.dart';
import '../../data/api_service.dart';
import '../../models/appbar_logo.dart';

class CreateTOScreen extends StatefulWidget {
  final TOModel? editTO;
  final Map<String, dynamic> user;
  const CreateTOScreen({super.key, this.editTO, required this.user});

  @override
  State<CreateTOScreen> createState() => _CreateTOScreenState();
}

class _CreateTOScreenState extends State<CreateTOScreen>
    with SingleTickerProviderStateMixin {
  late CreateTOLogic logic;

  late AnimationController animationController;
  late Animation<double> animation;

  final ScrollController listScrollController = ScrollController();
  final FocusNode inputFocus = FocusNode();
  final TextEditingController inputController = TextEditingController();
  final AudioPlayer beepPlayer = AudioPlayer();
  final AudioPlayer errorPlayer = AudioPlayer();

  // Scanner tối ưu quét nhanh, nhạy
  final MobileScannerController controller = MobileScannerController(
    detectionSpeed: DetectionSpeed.normal,
    facing: CameraFacing.back,
    torchEnabled: false,
    returnImage: false,
    formats: const [
      BarcodeFormat.qrCode,
      BarcodeFormat.code128,
      BarcodeFormat.code39,
      BarcodeFormat.ean13,
      BarcodeFormat.ean8,
    ],
  );

  String? centerMessage;
  Color? centerMessageColor;
  Timer? _messageTimer;

  bool _isProcessing = false;
  String? _lastScannedCode;
  DateTime _lastScannedAt = DateTime.fromMillisecondsSinceEpoch(0);
  bool _torchOn = false;

  Future<void> _initTO() async {
    await logic.initNewTO();
    if (mounted) setState(() {});
  }

  @override
  void initState() {
    super.initState();
    logic = CreateTOLogic(user: widget.user, editTO: widget.editTO);

    beepPlayer.setReleaseMode(ReleaseMode.stop);
    errorPlayer.setReleaseMode(ReleaseMode.stop);
    beepPlayer.setSource(AssetSource('beep.mp3'));
    errorPlayer.setSource(AssetSource('error.mp3'));

    if (widget.editTO == null) {
      _initTO();
    }

    animationController = AnimationController(
      vsync: this,
      duration: const Duration(seconds: 2),
    )..repeat(reverse: true);
    animation = Tween<double>(begin: 0, end: 1).animate(animationController);
  }

  void _playBeep() {
    try {
      beepPlayer.stop();
      beepPlayer.play(AssetSource('beep.mp3'));
    } catch (_) {}
  }

  void _playErrorSound() {
    try {
      errorPlayer.stop();
      errorPlayer.play(AssetSource('error.mp3'));
    } catch (_) {}
  }

  void _showCenterMessage(String text, Color color,
      {Duration duration = const Duration(milliseconds: 1000)}) {
    _messageTimer?.cancel();
    if (!mounted) return;
    setState(() {
      centerMessage = text;
      centerMessageColor = color;
    });
    _messageTimer = Timer(duration, () {
      if (!mounted) return;
      setState(() => centerMessage = null);
    });
  }

  // Quét camera
  Future<void> _handleBarcode(BarcodeCapture capture) async {
    if (_isProcessing) return;
    if (capture.barcodes.isEmpty) return;

    final code = capture.barcodes.first.rawValue?.trim() ?? "";
    if (code.isEmpty) return;

    final now = DateTime.now();
    // Chặn trùng liên tiếp trong 1.5s
    if (code == _lastScannedCode &&
        now.difference(_lastScannedAt).inMilliseconds < 1500) {
      return;
    }

    _isProcessing = true;
    _lastScannedCode = code;
    _lastScannedAt = now;
    inputController.text = code;

    await _processCode(code);

    await Future.delayed(const Duration(milliseconds: 250));
    _isProcessing = false;
  }

  // Xử lý gửi mã tới Server
  Future<void> _processCode(String code) async {
    try {
      final response = await ApiService.scanOrder(
        id: code,
        maTO: logic.toId,
      );

      if (response['success'] == true) {
        final to = await ApiService.getOneTO(logic.toId);
        if (to != null && mounted) {
          setState(() {
            logic.loadFromServer(to);
          });
          _playBeep();
          _showCenterMessage('Thêm mã thành công', Colors.green);
          inputController.clear();

          // Tự cuộn xuống cuối danh sách
          if (listScrollController.hasClients) {
            listScrollController.animateTo(
              listScrollController.position.maxScrollExtent + 40,
              duration: const Duration(milliseconds: 300),
              curve: Curves.easeOut,
            );
          }
        } else {
          _playErrorSound();
          _showCenterMessage('Không load được TO', Colors.red);
        }
      } else {
        _playErrorSound();
        _showCenterMessage(
          response['message'] ?? 'Scan thất bại',
          Colors.red,
        );
        inputController.clear();
      }
    } catch (e) {
      _playErrorSound();
      _showCenterMessage('Lỗi kết nối máy chủ', Colors.red);
    }
  }

  // Nhập tay
  Future<void> _onManualInput() async {
    final text = inputController.text.replaceAll(RegExp(r'\s+'), '').toUpperCase();
    if (text.isEmpty) {
      _showCenterMessage('Vui lòng nhập mã vào ô trống', Theme.of(context).colorScheme.primary);
      return;
    }

    FocusScope.of(context).unfocus();
    _isProcessing = true;
    await _processCode(text);
    _isProcessing = false;
  }

  // Hoàn tất TO
  Future<void> _completeAndOpenResult() async {
    if (logic.scannedCodes.isEmpty) {
      _showCenterMessage(
        'Chưa quét gói hàng nào vào bao',
        Theme.of(context).colorScheme.primary,
      );
      return;
    }

    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (_) => const Center(child: CircularProgressIndicator()),
    );

    await logic.completeTO();
    final to = await ApiService.getOneTO(logic.toId);

    if (!mounted) return;
    Navigator.of(context).pop(); // đóng loading dialog

    if (to != null) {
      // Thay thế màn hình đóng bao hiện tại bằng trang kết quả để không bị đè stack
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(
          builder: (_) => TOResultScreen(
            to: to,
            user: widget.user,
          ),
        ),
      );
    } else {
      _showCenterMessage('Hoàn tất thất bại, vui lòng thử lại', Colors.red);
    }
  }

  @override
  void dispose() {
    _messageTimer?.cancel();
    controller.dispose();
    animationController.dispose();
    inputController.dispose();
    inputFocus.dispose();
    listScrollController.dispose();
    beepPlayer.dispose();
    errorPlayer.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final isDark = theme.brightness == Brightness.dark;

    return Scaffold(
      backgroundColor: theme.scaffoldBackgroundColor,
      body: Stack(
        children: [
          Column(
            children: [
              HeaderWidget(
                title: widget.editTO != null ? 'Sửa Bao Hàng' : 'Tạo Bao Hàng (TO)',
                onBack: () => Navigator.pop(context),
                trailing: IconButton(
                  icon: Icon(
                    _torchOn ? Icons.flash_on : Icons.flash_off,
                    color: Colors.white,
                  ),
                  onPressed: () async {
                    await controller.toggleTorch();
                    setState(() => _torchOn = !_torchOn);
                  },
                ),
              ),
              Expanded(
                child: SingleChildScrollView(
                  padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                  child: ConstrainedBox(
                    constraints: const BoxConstraints(maxWidth: 480),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        _buildInfoCard(isDark),
                        const SizedBox(height: 12),
                        _buildInputRow(isDark),
                        const SizedBox(height: 14),
                        _buildCameraPreview(),
                        const SizedBox(height: 14),
                        _buildScannedListCard(isDark),
                        const SizedBox(height: 16),
                        _buildCompleteButton(),
                      ],
                    ),
                  ),
                ),
              ),
            ],
          ),
          _buildCenterMessageOverlay(),
        ],
      ),
    );
  }

  Widget _buildInfoCard(bool isDark) {
    final textColor = Theme.of(context).textTheme.bodyMedium?.color;
    final style = TextStyle(fontSize: 14, fontWeight: FontWeight.bold, color: textColor);

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 14, vertical: 10),
      decoration: BoxDecoration(
        color: Theme.of(context).cardColor,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: Colors.orange.withValues(alpha: 0.3)),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withValues(alpha: 0.04),
            blurRadius: 6,
            offset: const Offset(0, 2),
          ),
        ],
      ),
      child: Column(
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text('TO ID: ${logic.toId}', style: style),
              Text('Số lượng: ${logic.scannedCodes.length}/${TOModel.maxGoiHang}', style: style),
            ],
          ),
          const SizedBox(height: 4),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text('Station: ${logic.station}', style: style),
              Text(
                'Khối lượng: ${logic.tongKhoiLuong.toStringAsFixed(2)}/${TOModel.maxWeight} kg',
                style: style,
              ),
            ],
          ),
          const SizedBox(height: 4),
          Row(
            children: [
              Text('Packer: ${logic.packer}', style: style),
            ],
          ),
        ],
      ),
    );
  }

  Widget _buildInputRow(bool isDark) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Expanded(
          child: TextField(
            controller: inputController,
            focusNode: inputFocus,
            textCapitalization: TextCapitalization.characters,
            decoration: InputDecoration(
              hintText: 'Nhập hoặc quét mã bưu kiện...',
              hintStyle: TextStyle(color: Colors.grey[400], fontSize: 14),
              contentPadding: const EdgeInsets.symmetric(horizontal: 14, vertical: 12),
              border: OutlineInputBorder(borderRadius: BorderRadius.circular(10)),
              filled: true,
              fillColor: isDark ? Colors.grey[850] : Colors.grey[50],
            ),
            onSubmitted: (_) => _onManualInput(),
          ),
        ),
        const SizedBox(width: 8),
        ElevatedButton(
          onPressed: _onManualInput,
          style: ElevatedButton.styleFrom(
            backgroundColor: Theme.of(context).colorScheme.primary,
            foregroundColor: Colors.white,
            padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 13),
            shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
            elevation: 3,
          ),
          child: const Text('Confirm', style: TextStyle(fontWeight: FontWeight.bold)),
        ),
      ],
    );
  }

  Widget _buildCameraPreview() {
    return Center(
      child: Container(
        width: 250,
        height: 250,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(16),
          border: Border.all(color: Theme.of(context).colorScheme.primary, width: 3),
          boxShadow: [
            BoxShadow(
              color: Theme.of(context).colorScheme.primary.withValues(alpha: 0.15),
              blurRadius: 12,
              spreadRadius: 2,
            ),
          ],
        ),
        child: ClipRRect(
          borderRadius: BorderRadius.circular(13),
          child: Stack(
            children: [
              MobileScanner(
                controller: controller,
                onDetect: _handleBarcode,
              ),
              AnimatedBuilder(
                animation: animation,
                builder: (context, child) {
                  return Positioned(
                    top: animation.value * 238,
                    left: 0,
                    right: 0,
                    child: Container(
                      height: 3,
                      decoration: BoxDecoration(
                        gradient: LinearGradient(
                          colors: [
                            Colors.orange.withValues(alpha: 0),
                            Colors.orange,
                            Colors.orange.withValues(alpha: 0),
                          ],
                        ),
                      ),
                    ),
                  );
                },
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildScannedListCard(bool isDark) {
    return Container(
      height: 240,
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Theme.of(context).cardColor,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: Colors.grey.withValues(alpha: 0.25)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 6),
            decoration: BoxDecoration(
              color: Colors.orange.withValues(alpha: 0.1),
              borderRadius: BorderRadius.circular(8),
            ),
            child: const Row(
              children: [
                Expanded(
                  flex: 4,
                  child: Text('Mã Đơn',
                      style: TextStyle(
                          color: Colors.orange, fontWeight: FontWeight.bold, fontSize: 13)),
                ),
                Expanded(
                  flex: 2,
                  child: Text('Giờ',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          color: Colors.orange, fontWeight: FontWeight.bold, fontSize: 13)),
                ),
                Expanded(
                  flex: 2,
                  child: Text('KG',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          color: Colors.orange, fontWeight: FontWeight.bold, fontSize: 13)),
                ),
                SizedBox(width: 24),
              ],
            ),
          ),
          const SizedBox(height: 6),
          Expanded(
            child: logic.scannedCodes.isEmpty
                ? Center(
                    child: Text(
                      'Chưa có đơn hàng trong bao',
                      style: TextStyle(color: Colors.grey[400], fontSize: 13),
                    ),
                  )
                : ListView.builder(
                    controller: listScrollController,
                    itemCount: logic.scannedCodes.length,
                    itemBuilder: (context, index) {
                      final item = logic.scannedCodes[index];
                      final timeStr = DateFormat('HH:mm:ss').format(item.timestamp);
                      return Padding(
                        padding: const EdgeInsets.symmetric(vertical: 4),
                        child: Row(
                          children: [
                            Expanded(
                              flex: 4,
                              child: SelectableText(
                                item.code,
                                style: const TextStyle(fontSize: 12.5),
                              ),
                            ),
                            Expanded(
                              flex: 2,
                              child: Text(
                                timeStr,
                                textAlign: TextAlign.center,
                                style: const TextStyle(fontSize: 12),
                              ),
                            ),
                            Expanded(
                              flex: 2,
                              child: Text(
                                '${item.weight.toStringAsFixed(2)} kg',
                                textAlign: TextAlign.center,
                                style: const TextStyle(fontSize: 12, fontWeight: FontWeight.w600),
                              ),
                            ),
                            GestureDetector(
                              onTap: () async {
                                final code = logic.scannedCodes[index].code;
                                setState(() {
                                  logic.scannedCodes.removeAt(index);
                                });
                                await ApiService.removeOrder(
                                  id: code,
                                  maTO: logic.toId,
                                );
                              },
                              child: const Padding(
                                padding: EdgeInsets.only(left: 6),
                                child: Icon(Icons.close, size: 18, color: Colors.red),
                              ),
                            ),
                          ],
                        ),
                      );
                    },
                  ),
          ),
        ],
      ),
    );
  }

  Widget _buildCompleteButton() {
    return SizedBox(
      width: double.infinity,
      height: 50,
      child: ElevatedButton(
        onPressed: _completeAndOpenResult,
        style: ElevatedButton.styleFrom(
          backgroundColor: Theme.of(context).colorScheme.primary,
          foregroundColor: Colors.white,
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(25)),
          elevation: 4,
        ),
        child: const Text('Complete (Đóng Bao)',
            style: TextStyle(fontSize: 17, fontWeight: FontWeight.bold, letterSpacing: 0.5)),
      ),
    );
  }

  Widget _buildCenterMessageOverlay() {
    return Positioned.fill(
      child: IgnorePointer(
        ignoring: centerMessage == null,
        child: AnimatedOpacity(
          duration: const Duration(milliseconds: 200),
          opacity: centerMessage == null ? 0.0 : 1.0,
          child: Center(
            child: Container(
              padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 12),
              decoration: BoxDecoration(
                color: (centerMessageColor ?? Colors.black87).withValues(alpha: 0.9),
                borderRadius: BorderRadius.circular(12),
              ),
              child: Text(
                centerMessage ?? '',
                textAlign: TextAlign.center,
                style: const TextStyle(
                  color: Colors.white,
                  fontSize: 15,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
