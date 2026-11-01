/// =============================================================
/// File: scan_to_screen.dart
/// Mô tả: UI (giao diện) cho màn hình "Scan TO" - Quét mã bao hàng.
///        Logic xử lý nằm trong scan_to_logic.dart.
/// =============================================================
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'package:audioplayers/audioplayers.dart';
import 'scan_to_logic.dart';
import 'package:appqr1/models/appbar_logo.dart';

class ScanTOScreen extends StatefulWidget {
  const ScanTOScreen({super.key});

  @override
  State<ScanTOScreen> createState() => _ScanTOScreenState();
}

class _ScanTOScreenState extends State<ScanTOScreen>
    with SingleTickerProviderStateMixin {
  final ScanTOLogic logic = ScanTOLogic();

  late AnimationController animationController;
  late Animation<double> animation;

  final TextEditingController inputController = TextEditingController();
  final AudioPlayer beepPlayer = AudioPlayer();
  final AudioPlayer errorPlayer = AudioPlayer();

  // Scanner tối ưu quét nhanh & mượt
  final MobileScannerController controller = MobileScannerController(
    detectionSpeed: DetectionSpeed.normal,
    facing: CameraFacing.back,
    returnImage: false,
    formats: const [
      BarcodeFormat.qrCode,
      BarcodeFormat.code128,
      BarcodeFormat.ean13,
      BarcodeFormat.code39,
    ],
  );

  bool _isProcessing = false;
  String? _lastCode;
  DateTime _lastScanTime = DateTime.fromMillisecondsSinceEpoch(0);
  bool _torchOn = false;

  @override
  void initState() {
    super.initState();
    beepPlayer.setReleaseMode(ReleaseMode.stop);
    errorPlayer.setReleaseMode(ReleaseMode.stop);
    beepPlayer.setSource(AssetSource('beep.mp3'));
    errorPlayer.setSource(AssetSource('error.mp3'));

    animationController = AnimationController(
      vsync: this,
      duration: const Duration(seconds: 2),
    )..repeat(reverse: true);
    animation = Tween<double>(begin: 0, end: 1).animate(animationController);
  }

  // ── Audio ──
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

  // ── Xử lý kết quả quét ──
  void _handleResult(ScanTOResult result) {
    switch (result) {
      case ScanTOResult.success:
        _playBeep();
        setState(() {});
        break;

      case ScanTOResult.duplicate:
        _playErrorSound();
        _showSnackBar("Mã vừa được quét", Colors.orange);
        break;

      case ScanTOResult.invalidFormat:
        _playErrorSound();
        _showSnackBar("Mã không đúng định dạng TO (VD: TO240101ABCD)", Colors.red);
        break;

      case ScanTOResult.empty:
        break;
    }
  }

  void _showSnackBar(String text, Color color) {
    if (!mounted) return;
    ScaffoldMessenger.of(context).clearSnackBars();
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(text, style: const TextStyle(fontWeight: FontWeight.bold)),
        backgroundColor: color,
        duration: const Duration(milliseconds: 1200),
      ),
    );
  }

  // ── Camera barcode callback với debouncing chống đơ ──
  void _handleBarcode(BarcodeCapture capture) {
    if (_isProcessing) return;
    if (capture.barcodes.isEmpty) return;

    final barcode = capture.barcodes.first;
    final newValue = barcode.rawValue?.trim() ?? "";
    if (newValue.isEmpty) return;

    final now = DateTime.now();
    // Chặn trùng mã liên tiếp trong 1.5 giây
    if (newValue == _lastCode && now.difference(_lastScanTime).inMilliseconds < 1500) {
      return;
    }

    _isProcessing = true;
    _lastCode = newValue;
    _lastScanTime = now;

    inputController.text = newValue;
    final result = logic.processCode(newValue, barcode.format.name);
    _handleResult(result);

    Future.delayed(const Duration(milliseconds: 250), () {
      if (mounted) _isProcessing = false;
    });
  }

  void _confirmManualInput() {
    final text = inputController.text.trim().toUpperCase();
    if (text.isEmpty) {
      _showSnackBar('Vui lòng nhập mã vào ô trống', Colors.orange);
      return;
    }
    FocusScope.of(context).unfocus();
    final result = logic.processCode(text, "MANUAL");
    _handleResult(result);
  }

  @override
  void dispose() {
    controller.dispose();
    animationController.dispose();
    inputController.dispose();
    beepPlayer.dispose();
    errorPlayer.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final bool isDark = Theme.of(context).brightness == Brightness.dark;

    return Scaffold(
      backgroundColor: Theme.of(context).scaffoldBackgroundColor,
      body: Column(
        children: [
          HeaderWidget(
            title: 'Scan TO',
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
              padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
              child: ConstrainedBox(
                constraints: const BoxConstraints(maxWidth: 450),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    _buildInputSection(isDark),
                    const SizedBox(height: 20),
                    _buildCameraPreview(isDark),
                    const SizedBox(height: 20),
                    _buildResultSection(isDark),
                    const SizedBox(height: 24),
                    _buildCompleteButton(isDark),
                  ],
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildInputSection(bool isDark) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          'Dữ liệu input:',
          style: TextStyle(fontSize: 15, fontWeight: FontWeight.bold),
        ),
        const SizedBox(height: 8),
        Row(
          children: [
            Expanded(
              child: TextField(
                controller: inputController,
                textCapitalization: TextCapitalization.characters,
                decoration: InputDecoration(
                  hintText: 'Nhập hoặc quét mã TO...',
                  hintStyle: TextStyle(color: Colors.grey[400]),
                  contentPadding: const EdgeInsets.symmetric(horizontal: 14, vertical: 12),
                  border: OutlineInputBorder(borderRadius: BorderRadius.circular(10)),
                  filled: true,
                  fillColor: isDark ? Colors.grey[850] : Colors.grey[50],
                ),
                onSubmitted: (_) => _confirmManualInput(),
              ),
            ),
            const SizedBox(width: 10),
            ElevatedButton(
              onPressed: _confirmManualInput,
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.orange[600],
                foregroundColor: Colors.white,
                padding: const EdgeInsets.symmetric(horizontal: 18, vertical: 14),
                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
                elevation: 4,
              ),
              child: const Text('Xác nhận', style: TextStyle(fontWeight: FontWeight.bold)),
            ),
          ],
        ),
      ],
    );
  }

  Widget _buildCameraPreview(bool isDark) {
    return Center(
      child: Container(
        width: 270,
        height: 270,
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(16),
          border: Border.all(color: Colors.orange[600]!, width: 3),
          boxShadow: [
            BoxShadow(
              color: Colors.orange.withValues(alpha: 0.2),
              blurRadius: 16,
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
                    top: animation.value * 256,
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

  Widget _buildResultSection(bool isDark) {
    final hasResult = logic.result.isNotEmpty;
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 14),
      decoration: BoxDecoration(
        color: isDark ? Colors.grey[900] : Colors.grey[50],
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: hasResult ? Colors.green : Colors.grey[300]!),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withValues(alpha: 0.04),
            blurRadius: 8,
            offset: const Offset(0, 2),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Text(
                'Dữ liệu quét:',
                style: TextStyle(
                  fontSize: 13,
                  fontWeight: FontWeight.w500,
                  color: isDark ? Colors.white70 : Colors.grey[600],
                ),
              ),
              const Spacer(),
              if (hasResult)
                const Row(
                  children: [
                    Icon(Icons.check_circle, color: Colors.green, size: 16),
                    SizedBox(width: 4),
                    Text('Hợp lệ', style: TextStyle(color: Colors.green, fontSize: 12, fontWeight: FontWeight.bold)),
                  ],
                ),
            ],
          ),
          const SizedBox(height: 6),
          SelectableText(
            hasResult ? logic.result : 'Đưa camera vào mã bao hàng để quét...',
            style: TextStyle(
              fontSize: 18,
              fontWeight: hasResult ? FontWeight.bold : FontWeight.normal,
              color: hasResult
                  ? (isDark ? Colors.orange[300] : Colors.orange[800])
                  : (isDark ? Colors.grey[500] : Colors.grey[400]),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildCompleteButton(bool isDark) {
    return SizedBox(
      width: double.infinity,
      height: 50,
      child: ElevatedButton(
        onPressed: () => Navigator.pop(context),
        style: ElevatedButton.styleFrom(
          backgroundColor: Colors.orange[600],
          foregroundColor: Colors.white,
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(25)),
          elevation: 4,
        ),
        child: const Text('Hoàn tất',
            style: TextStyle(fontSize: 17, fontWeight: FontWeight.bold, letterSpacing: 0.5)),
      ),
    );
  }
}
