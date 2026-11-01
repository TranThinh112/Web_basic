/// =============================================================
/// File: table_to_screen.dart
/// Mô tả: UI (giao diện) cho màn hình "Table TO" - Danh sách bao hàng.
///        Logic xử lý nằm trong table_to_logic.dart.
/// =============================================================
import 'package:appqr1/data/api_service.dart';
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import '../../models/to_model.dart';
import '../create_TO/create_to_screen.dart';
import 'table_to_logic.dart';
import '../../BILL/BillTo.dart';
import '../../models/appbar_logo.dart';

class TableTOScreen extends StatefulWidget {
  final Map<String, dynamic> user;
  const TableTOScreen({super.key, required this.user});

  @override
  State<TableTOScreen> createState() => _TableTOScreenState();
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
      padding: const EdgeInsets.all(12),
      child: SelectableText(
        text,
        textAlign: TextAlign.center,
      ),
    );
  }
}

class _TableTOScreenState extends State<TableTOScreen> {
  final TableTOLogic logic = TableTOLogic();
  final TextEditingController searchController = TextEditingController();
  int tongTO = 0;
  int toPacked = 0;

  @override
  void initState() {
    super.initState();
    loadData();
  }

  Future<void> loadData() async {
    await logic.refreshList();
    final total = await sumTO();
    final packed = await packedTO();

    if (mounted) {
      setState(() {
        tongTO = total;
        toPacked = packed;
      });
    }
  }

  Future<void> _refreshList() async {
    await logic.refreshList();
    logic.search(searchController.text);
    if (mounted) setState(() {});
  }

  void _search(String keyword) {
    logic.search(keyword);
    setState(() {});
  }

  String formatTime(DateTime? time) {
    if (time == null) return '';
    final year = (time.year % 100).toString().padLeft(2, '0');
    return "$year/${time.month}/${time.day} ${time.hour}:${time.minute.toString().padLeft(2, '0')}";
  }

  void _editTO(TOModel to) async {
    final fullTO = await ApiService.getOneTO(to.maTO);
    if (fullTO == null || !mounted) return;

    await Navigator.push(
      context,
      MaterialPageRoute(
        builder: (_) => CreateTOScreen(
          editTO: fullTO,
          user: widget.user,
        ),
      ),
    );
    _refreshList();
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
          title: const Text('Quét mã bao hàng'),
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
                    searchController.text = code.toUpperCase();
                    _search(code.toUpperCase());
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

  @override
  void dispose() {
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
            title: 'Danh Sách Bao Hàng',
            onBack: () => Navigator.pop(context),
            trailing: widget.user['username'] == 'admin'
                ? IconButton(
                    icon: Icon(
                      logic.isDeleteMode ? Icons.close : Icons.delete_outline,
                      color: Colors.white,
                    ),
                    onPressed: () {
                      setState(() => logic.toggleDeleteMode());
                    },
                  )
                : null,
          ),
          _buildSearchBar(isDark),
          _buildTextTong(),
          Expanded(child: _buildTable()),
          if (logic.isDeleteMode) _buildDeleteActions(),
        ],
      ),
    );
  }

  Widget _buildSearchBar(bool isDark) {
    return Padding(
      padding: const EdgeInsets.fromLTRB(16, 12, 16, 8),
      child: Row(
        children: [
          Expanded(
            child: TextField(
              controller: searchController,
              onChanged: _search,
              decoration: InputDecoration(
                hintText: 'Tìm kiếm mã TO...',
                hintStyle: TextStyle(color: Colors.grey[400]),
                prefixIcon: Icon(Icons.search, color: Theme.of(context).colorScheme.primary),
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
              onPressed: _scanToSearch,
              icon: const Icon(Icons.qr_code_scanner, color: Colors.white),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildTextTong() {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 6),
      child: Row(
        children: [
          Text(
            "Tổng bao: $tongTO",
            style: const TextStyle(fontWeight: FontWeight.bold),
          ),
          const Spacer(),
          Text(
            "Đã đóng (Packed): $toPacked/$tongTO",
            style: const TextStyle(fontWeight: FontWeight.bold, color: Colors.green),
          ),
        ],
      ),
    );
  }

  Widget _buildTable() {
    final bool isDark = Theme.of(context).brightness == Brightness.dark;
    return RefreshIndicator(
      onRefresh: _refreshList,
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
            columnWidths: {
              if (logic.isDeleteMode) 0: const FixedColumnWidth(40),
              (logic.isDeleteMode ? 1 : 0): const FixedColumnWidth(160),
              (logic.isDeleteMode ? 2 : 1): const FixedColumnWidth(55),
              (logic.isDeleteMode ? 3 : 2): const FixedColumnWidth(100),
              (logic.isDeleteMode ? 4 : 3): const FixedColumnWidth(90),
              (logic.isDeleteMode ? 5 : 4): const FixedColumnWidth(60),
              (logic.isDeleteMode ? 6 : 5): const FixedColumnWidth(80),
              (logic.isDeleteMode ? 7 : 6): const FixedColumnWidth(125),
              (logic.isDeleteMode ? 8 : 7): const FixedColumnWidth(125),
              (logic.isDeleteMode ? 9 : 8): const FixedColumnWidth(55),
              (logic.isDeleteMode ? 10 : 9): const FixedColumnWidth(55),
            },
            children: [
              _buildHeaderRow(),
              ...logic.filteredList.map((t) => _buildTableRow(t)),
            ],
          ),
        ),
      ),
    );
  }

  TableRow _buildHeaderRow() {
    final bool isDark = Theme.of(context).brightness == Brightness.dark;
    return TableRow(
      decoration: BoxDecoration(
        color: isDark ? Colors.orange[800] : Colors.orange[200],
      ),
      children: [
        if (logic.isDeleteMode) const SizedBox(),
        const _Header('Mã TO'),
        const _Header('SL'),
        const _Header('Đến'),
        const _Header('Status'),
        const _Header('KG'),
        const _Header('Packer'),
        const _Header('Thời Gian Tạo'),
        const _Header('T/Gian Đóng'),
        const _Header('View'),
        const _Header('Sửa'),
      ],
    );
  }

  TableRow _buildTableRow(TOModel t) {
    return TableRow(
      children: [
        if (logic.isDeleteMode)
          Padding(
            padding: const EdgeInsets.all(8),
            child: InkWell(
              onTap: () {
                setState(() => logic.toggleSelect(t.maTO));
              },
              child: Container(
                width: 22,
                height: 22,
                decoration: BoxDecoration(
                  shape: BoxShape.circle,
                  border: Border.all(
                    color: logic.selectedTOs.contains(t.maTO) ? Colors.red : Colors.grey,
                  ),
                  color: logic.selectedTOs.contains(t.maTO) ? Colors.red : Colors.transparent,
                ),
                child: logic.selectedTOs.contains(t.maTO)
                    ? const Icon(Icons.check, size: 14, color: Colors.white)
                    : null,
              ),
            ),
          ),
        _Cell(t.maTO),
        _Cell('${t.soLuongDonHang}'),
        _Cell(t.diaDiemGiaoHang),
        Padding(
          padding: const EdgeInsets.all(6),
          child: t.trangThai.isEmpty
              ? const SizedBox()
              : Container(
                  alignment: Alignment.center,
                  padding: const EdgeInsets.symmetric(vertical: 4),
                  decoration: BoxDecoration(
                    color: t.trangThai == 'Packed' ? Colors.green : Colors.red,
                    borderRadius: BorderRadius.circular(6),
                  ),
                  child: Text(
                    t.trangThai,
                    style: const TextStyle(color: Colors.white, fontSize: 12, fontWeight: FontWeight.bold),
                  ),
                ),
        ),
        _Cell(t.totalWeight.toStringAsFixed(1)),
        _Cell(t.packer.isEmpty ? '—' : t.packer),
        _Cell(formatTime(t.ngayTao)),
        _Cell(t.completeTime != null ? formatTime(t.completeTime!) : '—'),
        Center(
          child: IconButton(
            icon: const Icon(Icons.visibility, color: Colors.blue),
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (_) => BillTO(TO: t),
                ),
              );
            },
          ),
        ),
        Center(
          child: IconButton(
            icon: const Icon(Icons.edit, color: Colors.orange),
            onPressed: () => _editTO(t),
          ),
        ),
      ],
    );
  }

  Widget _buildDeleteActions() {
    return Container(
      padding: const EdgeInsets.fromLTRB(16, 8, 16, 16),
      child: Column(
        children: [
          SizedBox(
            width: double.infinity,
            height: 44,
            child: ElevatedButton(
              onPressed: logic.selectedTOs.isEmpty
                  ? null
                  : () async {
                      await logic.deleteSelectedTOs();
                      setState(() {});
                    },
              style: ElevatedButton.styleFrom(
                backgroundColor: Colors.red,
                foregroundColor: Colors.white,
                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
              ),
              child: Text(
                'Xác nhận xóa (${logic.selectedTOs.length})',
                style: const TextStyle(fontSize: 15, fontWeight: FontWeight.bold),
              ),
            ),
          ),
          const SizedBox(height: 8),
          SizedBox(
            width: double.infinity,
            height: 44,
            child: OutlinedButton(
              onPressed: () {
                setState(() => logic.cancelDeleteMode());
              },
              style: OutlinedButton.styleFrom(
                shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
              ),
              child: const Text('Hủy', style: TextStyle(fontSize: 15, fontWeight: FontWeight.bold)),
            ),
          ),
        ],
      ),
    );
  }
}
