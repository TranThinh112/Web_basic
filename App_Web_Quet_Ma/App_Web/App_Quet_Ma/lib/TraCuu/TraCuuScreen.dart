import 'dart:async';
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'package:audioplayers/audioplayers.dart';
import '../models/Oders_model.dart';
import '../BILL/BillScreen.dart';
import '../models/appbar_logo.dart';
import './TraCuu_logic.dart';

class TraCuuScreen extends StatefulWidget {
  const TraCuuScreen({super.key});

  @override
  State<TraCuuScreen> createState() => _TraCuuScreenState();
}

class _Header extends StatelessWidget {
  final String text;
  const _Header(this.text);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(10),
      child: Text(
        text,
        textAlign: TextAlign.center,
        style: const TextStyle(fontWeight: FontWeight.bold),
      ),
    );
  }
}

class _Cell extends StatelessWidget {
  final String text;
  const _Cell(this.text);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(10),
      child: Text(
        text,
        textAlign: TextAlign.center,
        overflow: TextOverflow.ellipsis,
      ),
    );
  }
}

class _TraCuuScreenState extends State<TraCuuScreen> {
  final TextEditingController searchController = TextEditingController();
  List<OrderModel> allOrders = [];
  List<OrderModel> filteredList = [];
  final AudioPlayer beepPlayer = AudioPlayer();
  final AudioPlayer errorPlayer = AudioPlayer();
  bool isProcessing = false;
  final logic = TraCuuLogic();

  String? centerMessage;
  Color? centerMessageColor;
  Timer? _messageTimer;

  @override
  void initState() {
    super.initState();
    beepPlayer.setReleaseMode(ReleaseMode.stop);
    errorPlayer.setReleaseMode(ReleaseMode.stop);
    beepPlayer.setSource(AssetSource('beep.mp3'));
    errorPlayer.setSource(AssetSource('error.mp3'));
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
      {Duration duration = const Duration(milliseconds: 1200)}) {
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

  List<OrderModel> uniqueList(List<OrderModel> list) {
    final seen = <String>{};
    return list.where((item) {
      if (seen.contains(item.id)) {
        return false;
      } else {
        seen.add(item.id);
        return true;
      }
    }).toList();
  }

  Future<void> _onManualInput() async {
    final input = searchController.text.trim();
    if (input.isEmpty) {
      _showCenterMessage("Vui lòng nhập mã vận đơn", Theme.of(context).colorScheme.primary);
      setState(() {
        allOrders.clear();
        filteredList.clear();
      });
      return;
    }

    FocusScope.of(context).unfocus();

    final codes = input
        .split(RegExp(r'[\s,]+'))
        .map((e) => e.trim().toUpperCase())
        .where((e) => e.isNotEmpty)
        .toSet()
        .toList();

    List<String> invalidCodes = [];
    for (final code in codes) {
      if (!logic.isValidSPX(code)) {
        invalidCodes.add(code);
      }
    }

    if (invalidCodes.isNotEmpty) {
      _showCenterMessage(
        "Sai định dạng:\n${invalidCodes.join('\n')}",
        Colors.red,
      );
      _playErrorSound();
      return;
    }

    setState(() => isProcessing = true);

    List<String> notFoundCodes = [];
    final results = await Future.wait(codes.map((code) async {
      final result = await logic.processCode(code);
      if (result == ScanResult.notFound) {
        notFoundCodes.add(code);
        return null;
      }
      if (result == ScanResult.success) {
        final data = await logic.refreshList(code);
        if (data != null && data['order'] != null) {
          return data['order'] as OrderModel;
        }
      }
      return null;
    }));

    if (!mounted) return;

    if (notFoundCodes.isNotEmpty) {
      setState(() => isProcessing = false);
      _showCenterMessage(
        "Không tìm thấy:\n${notFoundCodes.join('\n')}",
        Colors.red,
      );
      _playErrorSound();
      return;
    }

    List<OrderModel> tempOrders = results.whereType<OrderModel>().toList();
    final uniqueResult = uniqueList(tempOrders);

    setState(() {
      allOrders = tempOrders;
      filteredList = uniqueResult;
      isProcessing = false;
    });

    _showCenterMessage('Tìm thấy ${uniqueResult.length} đơn hàng', Colors.green);
    _playBeep();
  }

  String formatTime(DateTime? time) {
    if (time == null) return '';
    final year = (time.year % 100).toString().padLeft(2, '0');
    return "$year/${time.month}/${time.day} ${time.hour}:${time.minute.toString().padLeft(2, '0')}";
  }

  Future<void> _scanToSearch() async {
    final scanController = MobileScannerController(
      detectionSpeed: DetectionSpeed.normal,
      facing: CameraFacing.back,
      returnImage: false,
      formats: const [BarcodeFormat.qrCode, BarcodeFormat.code128],
    );

    bool hasScanned = false;

    await showDialog(
      context: context,
      builder: (ctx) {
        return AlertDialog(
          title: const Text('Quét mã vận đơn'),
          content: SizedBox(
            width: 270,
            height: 270,
            child: ClipRRect(
              borderRadius: BorderRadius.circular(12),
              child: MobileScanner(
                controller: scanController,
                onDetect: (capture) {
                  if (hasScanned || capture.barcodes.isEmpty) return;
                  final code = capture.barcodes.first.rawValue?.trim() ?? '';
                  if (code.isNotEmpty) {
                    hasScanned = true;
                    final keyword = code.toUpperCase();
                    final current = searchController.text.trim();
                    final newValue = current.isEmpty ? keyword : "$current $keyword";

                    setState(() {
                      searchController.value = TextEditingValue(
                        text: newValue,
                        selection: TextSelection.collapsed(offset: newValue.length),
                      );
                    });
                    Navigator.pop(ctx);
                  }
                },
              ),
            ),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(ctx),
              child: const Text('Đóng'),
            ),
          ],
        );
      },
    );

    scanController.dispose();
  }

  void _showBillDialog(OrderModel order) {
    showDialog(
      context: context,
      builder: (ctx) => Dialog(
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
        child: SingleChildScrollView(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              BillScreen(order: order),
              Padding(
                padding: const EdgeInsets.only(bottom: 12),
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Theme.of(context).colorScheme.primary,
                    foregroundColor: Colors.white,
                    shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
                  ),
                  onPressed: () => Navigator.pop(ctx),
                  child: const Text("Đóng"),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    _messageTimer?.cancel();
    searchController.dispose();
    beepPlayer.dispose();
    errorPlayer.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final bool isDark = Theme.of(context).brightness == Brightness.dark;

    return Scaffold(
      backgroundColor: Theme.of(context).scaffoldBackgroundColor,
      body: Stack(
        children: [
          Column(
            children: [
              HeaderWidget(
                title: 'Tra Cứu Bưu Kiện',
                onBack: () => Navigator.pop(context),
              ),
              Expanded(
                child: Column(
                  children: [
                    Padding(
                      padding: const EdgeInsets.fromLTRB(16, 12, 16, 8),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              const Text(
                                'Nhập mã đơn bưu kiện:',
                                style: TextStyle(fontSize: 15, fontWeight: FontWeight.bold),
                              ),
                              const Spacer(),
                              Container(
                                height: 42,
                                width: 42,
                                decoration: BoxDecoration(
                                  color: Theme.of(context).colorScheme.primary,
                                  borderRadius: BorderRadius.circular(10),
                                ),
                                child: IconButton(
                                  padding: EdgeInsets.zero,
                                  onPressed: _scanToSearch,
                                  icon: const Icon(Icons.qr_code_scanner, color: Colors.white, size: 24),
                                ),
                              ),
                            ],
                          ),
                          const SizedBox(height: 8),
                          TextField(
                            controller: searchController,
                            maxLines: 3,
                            textCapitalization: TextCapitalization.characters,
                            decoration: InputDecoration(
                              hintText: "Nhập một hoặc nhiều mã (cách nhau bởi khoảng trắng hoặc dấu phẩy)...",
                              hintStyle: TextStyle(color: Colors.grey[400], fontSize: 13),
                              filled: true,
                              fillColor: isDark ? Colors.grey[850] : Colors.grey[50],
                              contentPadding: const EdgeInsets.all(12),
                              border: OutlineInputBorder(borderRadius: BorderRadius.circular(10)),
                            ),
                          ),
                          const SizedBox(height: 10),
                          Row(
                            children: [
                              if (filteredList.isNotEmpty)
                                ElevatedButton.icon(
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: Colors.blue[600],
                                    foregroundColor: Colors.white,
                                    shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(8)),
                                  ),
                                  onPressed: () => _showBillDialog(filteredList.first),
                                  icon: const Icon(Icons.receipt_long, size: 18),
                                  label: const Text("Xem Phiếu"),
                                ),
                              const Spacer(),
                              ElevatedButton(
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: Theme.of(context).colorScheme.primary,
                                  foregroundColor: Colors.white,
                                  padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 12),
                                  shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
                                ),
                                onPressed: isProcessing ? null : _onManualInput,
                                child: isProcessing
                                    ? const SizedBox(
                                        width: 18,
                                        height: 18,
                                        child: CircularProgressIndicator(strokeWidth: 2, color: Colors.white),
                                      )
                                    : const Text(
                                        "Tra Cứu",
                                        style: TextStyle(fontWeight: FontWeight.bold),
                                      ),
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                    const Divider(height: 1),
                    Expanded(child: _buildIn4Orders()),
                  ],
                ),
              ),
            ],
          ),
          _buildCenterMessageOverlay(),
        ],
      ),
    );
  }

  Widget _buildIn4Orders() {
    final bool isDark = Theme.of(context).brightness == Brightness.dark;

    if (filteredList.isEmpty) {
      return Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(Icons.search_off, size: 48, color: Colors.grey[400]),
            const SizedBox(height: 8),
            Text(
              "Chưa có dữ liệu tra cứu",
              style: TextStyle(color: Colors.grey[500], fontSize: 14),
            ),
          ],
        ),
      );
    }

    return SingleChildScrollView(
      padding: const EdgeInsets.all(12),
      child: SingleChildScrollView(
        scrollDirection: Axis.horizontal,
        child: Table(
          border: TableBorder.all(
            color: isDark ? Colors.grey[700]! : Colors.grey[300]!,
            width: 1,
          ),
          columnWidths: const {
            0: FixedColumnWidth(160),
            1: FixedColumnWidth(75),
            2: FixedColumnWidth(80),
            3: FixedColumnWidth(110),
            4: FixedColumnWidth(55),
            5: FixedColumnWidth(90),
            6: FixedColumnWidth(120),
            7: FixedColumnWidth(120),
            8: FixedColumnWidth(130),
            9: FixedColumnWidth(130),
          },
          children: [
            TableRow(
              decoration: BoxDecoration(
                color: isDark ? Colors.orange[800] : Colors.orange[200],
              ),
              children: const [
                _Header('Mã Đơn'),
                _Header('Nơi đi'),
                _Header('Nơi đến'),
                _Header('Sản Phẩm'),
                _Header('KG'),
                _Header('Status'),
                _Header('Thời Gian Tạo'),
                _Header('T/Gian Đóng'),
                _Header('Người gửi'),
                _Header('Người nhận'),
              ],
            ),
            ...filteredList.map((o) {
              return TableRow(
                children: [
                  _Cell(o.id),
                  _Cell(o.noigui),
                  _Cell(o.noinhan),
                  _Cell(o.sanpham),
                  _Cell(o.soKi.toStringAsFixed(1)),
                  Padding(
                    padding: const EdgeInsets.all(6),
                    child: (o.trangthai == null || o.trangthai!.isEmpty)
                        ? const SizedBox()
                        : Container(
                            alignment: Alignment.center,
                            padding: const EdgeInsets.symmetric(vertical: 4),
                            decoration: BoxDecoration(
                              color: o.trangthai == 'Inbound' ? Colors.green : Colors.red,
                              borderRadius: BorderRadius.circular(6),
                            ),
                            child: Text(
                              o.trangthai!,
                              style: const TextStyle(
                                color: Colors.white,
                                fontSize: 12,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                  ),
                  _Cell(formatTime(o.thoigiantao)),
                  _Cell(o.thoigiandongbao != null ? formatTime(o.thoigiandongbao!) : '—'),
                  _Cell(o.nguoigui),
                  _Cell(o.nguoinhan),
                ],
              );
            }),
          ],
        ),
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
