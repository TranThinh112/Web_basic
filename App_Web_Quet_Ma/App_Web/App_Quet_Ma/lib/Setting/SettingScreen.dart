import 'package:appqr1/Login/LoginScreen.dart';
import 'package:flutter/material.dart';
import '../main.dart';

class SettingsScreen extends StatefulWidget {
  final Map<String, dynamic> user;

  const SettingsScreen({super.key, required this.user});

  @override
  State<SettingsScreen> createState() => _SettingsScreenState();
}

class _SettingsScreenState extends State<SettingsScreen> {
  @override
  Widget build(BuildContext context) {
    final isDarkMode = MyApp.of(context)?.isDarkMode ?? false;
    return Scaffold(
      appBar: AppBar(
        title: const Text("Cài đặt"),
        backgroundColor: Colors.orange[700],
        foregroundColor: Colors.white,
      ),
      body: ListView(
        children: [
          // ================= TÀI KHOẢN =================
          const Padding(
            padding: EdgeInsets.all(12),
            child: Text(
              "Tài khoản",
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
            ),
          ),

          ListTile(
            leading: const Icon(Icons.person),
            title: const Text("Thông tin người dùng"),
            onTap: () {
              final username = widget.user['username'] ?? 'Unknown';
              final createdAt = widget.user['created_at'] ?? '';

              showDialog(
                context: context,
                builder: (context) => AlertDialog(
                  title: const Text("Thông tin"),
                  content: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Text("Tên: $username"),
                      Text("Ngày tạo: $createdAt"),
                    ],
                  ),
                  actions: [
                    TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: const Text("Đóng"),
                    )
                  ],
                ),
              );
            },
          ),

          ListTile(
            leading: const Icon(Icons.logout, color: Colors.red),
            title: const Text("Đăng xuất", style: TextStyle(color: Colors.red)),
            onTap: () {
              showDialog(
                context: context,
                builder: (ctx) => AlertDialog(
                  title: const Text("Xác nhận"),
                  content: const Text("Bạn có chắc chắn muốn đăng xuất không?"),
                  actions: [
                    TextButton(
                      onPressed: () => Navigator.pop(ctx),
                      child: const Text("Hủy"),
                    ),
                    ElevatedButton(
                      style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
                      onPressed: () {
                        Navigator.pop(ctx);
                        Navigator.pushAndRemoveUntil(
                          context,
                          MaterialPageRoute(
                            builder: (context) => const LoginPage(),
                          ),
                          (route) => false,
                        );
                      },
                      child: const Text("Đăng xuất", style: TextStyle(color: Colors.white)),
                    ),
                  ],
                ),
              );
            },
          ),

          const Divider(),

          // ================= GIAO DIỆN =================
          const Padding(
            padding: EdgeInsets.all(12),
            child: Text(
              "Giao diện",
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
            ),
          ),

          SwitchListTile(
            secondary: isDarkMode
                ? const Icon(Icons.light_mode)
                : const Icon(Icons.dark_mode),
            title: Text(isDarkMode ? "Light Mode" : "Dark Mode"),
            value: isDarkMode,
            onChanged: (value) {
              MyApp.of(context)?.toggleTheme(value);
              setState(() {});
            },
          ),
          const ListTile(
            leading: Icon(Icons.language),
            title: Text("Ngôn ngữ"),
            subtitle: Text("Tiếng Việt"),
          ),
        ],
      ),
    );
  }
}
