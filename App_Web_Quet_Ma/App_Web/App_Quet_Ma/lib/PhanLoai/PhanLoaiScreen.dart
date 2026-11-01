/// =============================================================
/// File: phan_loai_screen.dart
/// Mô tả: Màn hình menu "Phân Loại" - điều hướng đến các chức năng:
///        1. Create TO  → Tạo bao hàng mới (đóng bao)
///        2. Scan TO    → Quét mã TO để kiểm tra
///        3. Table TO   → Xem danh sách bao hàng đã tạo
/// =============================================================
import 'package:flutter/material.dart';
import 'Scan_to/scan_to_screen.dart';
import 'danhsach-TO/table_to_screen.dart';
import 'create_TO/create_to_screen.dart';
import '../models/appbar_logo.dart';

class PhanLoaiScreen extends StatelessWidget {
  final Map<String, dynamic> user;

  const PhanLoaiScreen({super.key, required this.user});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Theme.of(context).scaffoldBackgroundColor,
      body: Column(
        children: [
          const HeaderWidget(title: 'Phân Loại Bưu Kiện'),
          Expanded(
            child: Center(
              child: SingleChildScrollView(
                padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 20),
                child: ConstrainedBox(
                  constraints: const BoxConstraints(maxWidth: 420),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      _buildMenuButton(
                        context: context,
                        icon: Icons.add_circle_outline,
                        label: 'Create TO',
                        subtitle: 'Tạo bao hàng mới và quét gom kiện',
                        color: Colors.orange[600]!,
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => CreateTOScreen(user: user),
                            ),
                          );
                        },
                      ),
                      const SizedBox(height: 16),
                      _buildMenuButton(
                        context: context,
                        icon: Icons.qr_code_scanner,
                        label: 'Scan TO',
                        subtitle: 'Quét nhanh mã kiểm tra bao hàng',
                        color: Colors.deepOrange[400]!,
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => const ScanTOScreen(),
                            ),
                          );
                        },
                      ),
                      const SizedBox(height: 16),
                      _buildMenuButton(
                        context: context,
                        icon: Icons.table_chart_outlined,
                        label: 'Table TO',
                        subtitle: 'Danh sách và trạng thái toàn bộ bao hàng',
                        color: Colors.amber[800]!,
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => TableTOScreen(user: user),
                            ),
                          );
                        },
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildMenuButton({
    required BuildContext context,
    required IconData icon,
    required String label,
    required String subtitle,
    required Color color,
    required VoidCallback onTap,
  }) {
    return Material(
      color: Colors.transparent,
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(18),
        child: Container(
          width: double.infinity,
          padding: const EdgeInsets.symmetric(vertical: 18, horizontal: 20),
          decoration: BoxDecoration(
            color: color,
            borderRadius: BorderRadius.circular(18),
            boxShadow: [
              BoxShadow(
                color: color.withValues(alpha: 0.35),
                blurRadius: 10,
                offset: const Offset(0, 5),
              ),
            ],
          ),
          child: Row(
            children: [
              Container(
                padding: const EdgeInsets.all(12),
                decoration: BoxDecoration(
                  color: Colors.white.withValues(alpha: 0.22),
                  borderRadius: BorderRadius.circular(14),
                ),
                child: Icon(icon, color: Colors.white, size: 28),
              ),
              const SizedBox(width: 16),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      label,
                      style: const TextStyle(
                        color: Colors.white,
                        fontSize: 19,
                        fontWeight: FontWeight.bold,
                        letterSpacing: 0.3,
                      ),
                    ),
                    const SizedBox(height: 2),
                    Text(
                      subtitle,
                      style: TextStyle(
                        color: Colors.white.withValues(alpha: 0.9),
                        fontSize: 12.5,
                      ),
                    ),
                  ],
                ),
              ),
              const Icon(Icons.arrow_forward_ios, color: Colors.white70, size: 18),
            ],
          ),
        ),
      ),
    );
  }
}
