/// =============================================================
/// File: danh_sach_to_screen.dart
/// Mô tả: Màn hình Quản Lý đơn hàng (Hiển thị bảng đơn, phân trang, lọc).
/// =============================================================
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'dart:async';
import '../data/api_service.dart';
import '../models/to_model.dart';
import '../models/Oders_model.dart';
import '../QuanLy/QuangLy_logic.dart';
import '../BILL/BillTo.dart';
import '../models/appbar_logo.dart';

class QuanLyScreen extends StatefulWidget {
  const QuanLyScreen({super.key});

  @override
  State<QuanLyScreen> createState() => QuanLyScreenState();
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
      child: SelectableText(
        text,
        textAlign: TextAlign.center,
      ),
    );
  }
}

class QuanLyScreenState extends State<QuanLyScreen> {
  final TextEditingController searchController = TextEditingController();
  int currentPage = 1;
  bool isLoading = false;
  bool hasMore = true;
  String keyword = "";
  int tongDon = 0;
  int donInbound = 0;

  List<OrderModel> orders = [];
  Timer? _debounce;

  @override
  void initState() {
    super.initState();
    loadPage(1);
  }

  Future<void> loadPage(int page) async {
    if (isLoading) return;
    setState(() => isLoading = true);

    try {
      final res = await ApiService.getOrderQL(page, keyword);
      if (!mounted) return;
      setState(() {
        orders = res['orders'];
        currentPage = page;
        isLoading = false;
        hasMore = orders.length == 10;
        if (keyword.isEmpty) {
          tongDon = res['total'];
          donInbound = res['inbound'];
        }
      });
    } catch (e) {
      if (mounted) setState(() => isLoading = false);
    }
  }

  String formatTime(DateTime? time) {
    if (time == null) return '';
    final year = (time.year % 100).toString().padLeft(2, '0');
    return "$year/${time.month}/${time.day} ${time.hour}:${time.minute.toString().padLeft(2, '0')}";
  }

  Future<void> scanToSearch() async {
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
          title: const Text('Quét mã đơn hàng'),
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
                    final keywordScan = code.toUpperCase();
                    searchController.text = keywordScan;
                    keyword = keywordScan;
                    Navigator.pop(ctx);
                    loadPage(1);
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

  @override
  void dispose() {
    _debounce?.cancel();
    searchController.dispose();
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
            title: 'Quản Lý Đơn Hàng',
            onBack: () => Navigator.pop(context),
          ),
          Padding(
            padding: const EdgeInsets.fromLTRB(16, 12, 16, 8),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: searchController,
                    onChanged: (value) {
                      if (_debounce?.isActive ?? false) _debounce!.cancel();
                      _debounce = Timer(const Duration(milliseconds: 300), () {
                        keyword = value.trim();
                        loadPage(1);
                      });
                    },
                    decoration: InputDecoration(
                      hintText: 'Tìm mã vận đơn...',
                      hintStyle: TextStyle(color: Colors.grey[400]),
                      prefixIcon: const Icon(Icons.search),
                      suffixIcon: searchController.text.isNotEmpty
                          ? IconButton(
                              icon: const Icon(Icons.clear),
                              onPressed: () {
                                searchController.clear();
                                keyword = "";
                                loadPage(1);
                              },
                            )
                          : null,
                      contentPadding: const EdgeInsets.symmetric(horizontal: 14, vertical: 12),
                      border: OutlineInputBorder(borderRadius: BorderRadius.circular(10)),
                      filled: true,
                      fillColor: isDark ? Colors.grey[850] : Colors.grey[50],
                    ),
                  ),
                ),
                const SizedBox(width: 8),
                Container(
                  height: 48,
                  width: 48,
                  decoration: BoxDecoration(
                    color: Theme.of(context).colorScheme.primary,
                    borderRadius: BorderRadius.circular(10),
                  ),
                  child: IconButton(
                    onPressed: scanToSearch,
                    icon: const Icon(Icons.qr_code_scanner, color: Colors.white),
                  ),
                ),
              ],
            ),
          ),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 6),
            child: Row(
              children: [
                Text("Tổng đơn: $tongDon", style: const TextStyle(fontWeight: FontWeight.bold)),
                const Spacer(),
                Text("Đã đóng bao: $donInbound/$tongDon",
                    style: const TextStyle(fontWeight: FontWeight.bold, color: Colors.green)),
              ],
            ),
          ),
          Expanded(
            child: RefreshIndicator(
              onRefresh: () async => await loadPage(1),
              child: SingleChildScrollView(
                physics: const AlwaysScrollableScrollPhysics(),
                padding: const EdgeInsets.all(16),
                child: SingleChildScrollView(
                  scrollDirection: Axis.horizontal,
                  child: Table(
                    border: TableBorder.all(
                      color: isDark ? Colors.grey[700]! : Colors.grey[300]!,
                      width: 1,
                    ),
                    columnWidths: const {
                      0: FixedColumnWidth(175),
                      1: FixedColumnWidth(80),
                      2: FixedColumnWidth(55),
                      3: FixedColumnWidth(110),
                      4: FixedColumnWidth(125),
                      5: FixedColumnWidth(150),
                      6: FixedColumnWidth(60),
                    },
                    children: [
                      TableRow(
                        decoration: BoxDecoration(
                          color: isDark ? Colors.orange[800] : Colors.orange[200],
                        ),
                        children: const [
                          _Header('Mã Đơn'),
                          _Header('Nơi đến'),
                          _Header('KG'),
                          _Header('Trạng Thái'),
                          _Header('T/Gian Đóng'),
                          _Header('Mã TO'),
                          _Header('View'),
                        ],
                      ),
                      ...orders.map((o) {
                        final bool hasTO = o.maTO != null && o.maTO!.isNotEmpty;
                        return TableRow(
                          children: [
                            _Cell(o.id),
                            _Cell(o.noinhan),
                            _Cell(o.soKi.toStringAsFixed(1)),
                            Padding(
                              padding: const EdgeInsets.all(6),
                              child: o.trangthai == null || o.trangthai!.isEmpty
                                  ? const SizedBox()
                                  : Container(
                                      alignment: Alignment.center,
                                      padding: const EdgeInsets.symmetric(vertical: 4),
                                      decoration: BoxDecoration(
                                        color: o.trangthai == 'Inbound'
                                            ? Colors.green
                                            : Colors.orange,
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
                            _Cell(o.thoigiandongbao != null
                                ? formatTime(o.thoigiandongbao!)
                                : '—'),
                            _Cell(o.maTO ?? '—'),
                            Center(
                              child: IconButton(
                                icon: Icon(
                                  Icons.visibility,
                                  color: hasTO ? Colors.blue : Colors.grey[400],
                                ),
                                onPressed: hasTO
                                    ? () {
                                        final to = TOModel(
                                          maTO: o.maTO!,
                                          diaDiemGiaoHang: "",
                                          packer: "",
                                          totalWeight: o.soKi,
                                          ngayTao: DateTime.now(),
                                          completeTime: o.thoigiandongbao,
                                          danhSachGoiHang: [],
                                        );
                                        Navigator.push(
                                          context,
                                          MaterialPageRoute(
                                            builder: (_) => BillTO(TO: to),
                                          ),
                                        );
                                      }
                                    : null,
                              ),
                            ),
                          ],
                        );
                      }),
                    ],
                  ),
                ),
              ),
            ),
          ),
          Container(
            padding: const EdgeInsets.symmetric(vertical: 6),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                IconButton(
                  icon: const Icon(Icons.arrow_back),
                  onPressed: currentPage > 1 ? () => loadPage(currentPage - 1) : null,
                ),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 12),
                  child: Text(
                    "Trang $currentPage",
                    style: const TextStyle(fontWeight: FontWeight.bold),
                  ),
                ),
                IconButton(
                  icon: const Icon(Icons.arrow_forward),
                  onPressed: hasMore ? () => loadPage(currentPage + 1) : null,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
