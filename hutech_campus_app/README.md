# HUTECH Campus App

Ứng dụng di động được xây dựng bằng **Flutter**, dùng để giới thiệu các cơ sở đào tạo của **Trường Đại học Công nghệ TP.HCM (HUTECH)**.

---

## 📸 Giao Diện Ứng Dụng (UI Screenshots)

| Màn hình danh sách cơ sở (Home) | Màn hình chi tiết cơ sở (Detail) |
| :---: | :---: |
| <img src="./screenshots/home_screen.png" width="300" alt="Home Screen" /> | <img src="./screenshots/detail_screen.png" width="300" alt="Detail Screen" /> |

---

## 📌 Tính Năng Nổi Bật

- **Danh sách cơ sở trực quan**: Hiển thị danh sách các campus với bảng màu nhận diện thương hiệu riêng biệt.
  - **Saigon Campus**: 475A Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM
  - **Ung Van Khiem Campus**: 31/36 Ung Văn Khiêm, P.25, Q.Bình Thạnh, TP.HCM
  - **Thu Duc Campus**: Khu Công nghệ cao TP.HCM, Xa lộ Hà Nội, TP.Thủ Đức
  - **Hitech Park Campus**: Khu Công nghệ cao TP.HCM, P.Long Thạnh Mỹ, TP.Thủ Đức
- **Trang chi tiết cơ sở (Details Screen)**:
  - Hiển thị hình ảnh thực tế chất lượng cao của từng campus.
  - Thông tin liên hệ đầy đủ: địa chỉ chi tiết, hotline liên hệ (ví dụ: `028 5445 7777`).
  - Danh sách các ngành nghề đào tạo tại cơ sở (Công nghệ thông tin, Tự động hóa, Công nghệ sinh học, Thiết kế đồ họa...).
  - Tích hợp nút hành động **"Mở trên Google Maps"** để dẫn đường trực tiếp.
- **Hỗ trợ đa nền tảng**: Tương thích tốt trên Android, iOS, Web và Desktop (Windows/macOS/Linux).

---

## 🛠 Công Nghệ Sử Dụng

- **Ngôn ngữ**: Dart (SDK `^3.10.7`)
- **Framework**: Flutter
- **Thiết kế giao diện**: Material Design 3, Cupertino Icons
- **Tiện ích**: `url_launcher` (mở chỉ đường Google Maps)

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
