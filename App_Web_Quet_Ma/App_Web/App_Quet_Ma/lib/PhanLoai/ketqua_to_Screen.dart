import 'package:flutter/material.dart';
import '../models/to_model.dart';
import '../PhanLoai/create_TO/create_to_screen.dart';
import '../BILL/BillTo.dart';
import '../data/api_service.dart';
import '../models/appbar_logo.dart';

class TOResultScreen extends StatelessWidget {
  final TOModel to;
  final Map<String, dynamic> user;

  const TOResultScreen({super.key, required this.to, required this.user});

  @override
  Widget build(BuildContext context) {
    final textColor = Theme.of(context).textTheme.bodyMedium?.color;
    final style = TextStyle(
        fontSize: 15, fontWeight: FontWeight.bold, color: textColor);

    return Scaffold(
      backgroundColor: Theme.of(context).scaffoldBackgroundColor,
      body: Column(
        children: [
          HeaderWidget(
            title: 'Chi Tiết Bao Hàng',
            onBack: () => Navigator.pop(context),
          ),
          Expanded(
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  Container(
                    padding: const EdgeInsets.all(16),
                    decoration: BoxDecoration(
                      color: Theme.of(context).cardColor,
                      borderRadius: BorderRadius.circular(16),
                      boxShadow: [
                        BoxShadow(
                          color: Colors.black.withValues(alpha: 0.05),
                          blurRadius: 10,
                          offset: const Offset(0, 3),
                        ),
                      ],
                    ),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Text('TO ID: ${to.maTO}', style: style),
                            Text('Số lượng: ${to.soLuongDonHang}/${TOModel.maxGoiHang}', style: style),
                          ],
                        ),
                        const SizedBox(height: 8),
                        Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            Text('Station: ${to.diaDiemGiaoHang}', style: style),
                            Text(
                              'Khối lượng: ${to.totalWeight.toStringAsFixed(2)}/${TOModel.maxWeight} kg',
                              style: style,
                            ),
                          ],
                        ),
                        const SizedBox(height: 8),
                        Text('Packer: ${to.packer}', style: style),
                      ],
                    ),
                  ),
                  const SizedBox(height: 16),
                  Row(
                    children: [
                      Expanded(
                        child: OutlinedButton(
                          style: OutlinedButton.styleFrom(
                            padding: const EdgeInsets.symmetric(vertical: 12),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10),
                            ),
                          ),
                          onPressed: () async {
                            await ApiService.reopenTO(to.maTO);
                            if (context.mounted) {
                              Navigator.pop(context, true);
                            }
                          },
                          child: const Text(
                            "Mở lại (ReOpen)",
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ),
                      ),
                      const SizedBox(width: 10),
                      Expanded(
                        child: OutlinedButton(
                          style: OutlinedButton.styleFrom(
                            padding: const EdgeInsets.symmetric(vertical: 12),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10),
                            ),
                          ),
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => BillTO(TO: to),
                              ),
                            );
                          },
                          child: const Text(
                            "In Phiếu (View)",
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ),
                      ),
                      const SizedBox(width: 10),
                      Expanded(
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            backgroundColor: Colors.orange,
                            foregroundColor: Colors.white,
                            padding: const EdgeInsets.symmetric(vertical: 12),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10),
                            ),
                          ),
                          onPressed: () {
                            // Tạo TO mới: thay thế màn hình kết quả này bằng màn hình tạo TO mới
                            Navigator.pushReplacement(
                              context,
                              MaterialPageRoute(
                                builder: (_) => CreateTOScreen(user: user),
                              ),
                            );
                          },
                          child: const Text(
                            "TO Mới",
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),
                  Container(
                    height: 320,
                    padding: const EdgeInsets.all(14),
                    decoration: BoxDecoration(
                      color: Theme.of(context).cardColor,
                      borderRadius: BorderRadius.circular(16),
                      border: Border.all(color: Colors.grey.withValues(alpha: 0.2)),
                      boxShadow: [
                        BoxShadow(
                          color: Colors.black.withValues(alpha: 0.05),
                          blurRadius: 8,
                          offset: const Offset(0, 2),
                        ),
                      ],
                    ),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Container(
                          padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 8),
                          decoration: BoxDecoration(
                            color: Colors.orange.withValues(alpha: 0.12),
                            borderRadius: BorderRadius.circular(8),
                          ),
                          child: const Row(
                            children: [
                              Expanded(
                                flex: 4,
                                child: Text('Mã đơn hàng',
                                    style: TextStyle(
                                        color: Colors.orange,
                                        fontWeight: FontWeight.bold,
                                        fontSize: 13)),
                              ),
                              Expanded(
                                flex: 2,
                                child: Text('Khối lượng',
                                    textAlign: TextAlign.center,
                                    style: TextStyle(
                                        color: Colors.orange,
                                        fontWeight: FontWeight.bold,
                                        fontSize: 13)),
                              ),
                            ],
                          ),
                        ),
                        const SizedBox(height: 8),
                        Expanded(
                          child: to.danhSachGoiHang.isEmpty
                              ? const Center(child: Text("Bao hàng chưa có kiện nào"))
                              : ListView.separated(
                                  itemCount: to.danhSachGoiHang.length,
                                  separatorBuilder: (_, __) => Divider(
                                    height: 1,
                                    color: Colors.grey.withValues(alpha: 0.2),
                                  ),
                                  itemBuilder: (_, index) {
                                    final item = to.danhSachGoiHang[index];
                                    final orderId = item['orderId'] ?? item['id'] ?? '';
                                    final kg = item['soKi'] ?? item['weight'] ?? 0;
                                    return Padding(
                                      padding: const EdgeInsets.symmetric(vertical: 8, horizontal: 4),
                                      child: Row(
                                        children: [
                                          Expanded(
                                            flex: 4,
                                            child: SelectableText(
                                              orderId.toString(),
                                              style: const TextStyle(fontWeight: FontWeight.w500),
                                            ),
                                          ),
                                          Expanded(
                                            flex: 2,
                                            child: Text(
                                              "$kg kg",
                                              textAlign: TextAlign.center,
                                              style: const TextStyle(fontWeight: FontWeight.w600),
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
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
