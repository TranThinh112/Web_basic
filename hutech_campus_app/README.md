# HUTECH Campus App

Ứng dụng di động được xây dựng bằng **Flutter**, dùng để giới thiệu các cơ sở đào tạo của **Trường Đại học Công nghệ TP.HCM (HUTECH)**.

---

## 📌 Tính Năng Nổi Bật

- **Danh sách cơ sở trực quan**: Hiển thị danh sách các campus với bảng màu nhận diện thương hiệu riêng biệt.
  - **Saigon Campus**: 475A Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM
  - **Ung Van Khiem Campus**: 31/36 Ung Văn Khiêm, P.25, Q.Bình Thạnh, TP.HCM
  - **Thu Duc Campus**: Khu Công nghệ cao TP.HCM, Xa lộ Hà Nội, TP.Thủ Đức
  - **Hitech Park Campus**: Khu Công nghệ cao TP.HCM, P.Long Thạnh Mỹ, TP.Thủ Đức
- **Trang chi tiết cơ sở (Details Screen)**: Điều hướng và truyền tham số (`Route Arguments`) hiển thị địa chỉ chi tiết và hình ảnh thực tế chất lượng cao được tải trực tiếp từ internet (`Image.network`).
- **Hỗ trợ đa nền tảng**: Tương thích tốt trên Android, iOS, Web và Desktop (Windows/macOS/Linux).

---

## 🛠 Công Nghệ Sử Dụng

- **Ngôn ngữ**: Dart (SDK `^3.10.7`)
- **Framework**: Flutter
- **Thiết kế giao diện**: Material Design 3, Cupertino Icons

---

## 📂 Cấu Trúc Thư Mục

```text
hutech_campus_app/
├── lib/
│   ├── models/
│   │   └── campus.dart          # Lớp mô hình Campus (name, address, image, color)
│   ├── screens/
│   │   ├── home_screen.dart     # Giao diện danh sách các cơ sở (Card layout)
│   │   └── details_screen.dart  # Giao diện xem chi tiết cơ sở & hình ảnh
│   └── main.dart                # Điểm khởi chạy app và cấu hình Routes
├── android/                     # Mã nguồn nền tảng Android
├── ios/                         # Mã nguồn nền tảng iOS
├── pubspec.yaml                 # Quản lý dependencies và cấu hình ứng dụng
└── README.md
```

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Ứng Dụng

### Yêu cầu tiên quyết:
- Đã cài đặt [Flutter SDK](https://docs.flutter.dev/get-started/install).
- Trình giả lập (Android Emulator / iOS Simulator) hoặc thiết bị thật đã bật USB Debugging.

### Các bước thực hiện:

1. **Di chuyển vào thư mục dự án**:
   ```bash
   cd hutech_campus_app
   ```

2. **Cài đặt các gói phụ thuộc (Dependencies)**:
   ```bash
   flutter pub get
   ```

3. **Chạy ứng dụng**:
   ```bash
   flutter run
   ```

4. **Chạy trên trình duyệt Web (tùy chọn)**:
   ```bash
   flutter run -d chrome
   ```