# Mobile Scanner App

Ứng dụng quét mã vạch và mã QR thời gian thực trên nền tảng **Flutter**, tích hợp giao diện ngắm quét chuyên nghiệp, nút điều khiển flash/camera và âm thanh phản hồi thông minh.

---

## 📸 Giao Diện Ứng Dụng (UI Screenshot)

<p align="center">
  <img src="./screenshots/scanner_screen.png" width="340" alt="Scanner Screen" />
</p>

---

## 📌 Tính Năng Nổi Bật

- **Quét đa định dạng**: Nhận diện tức thì nhiều định dạng mã phổ biến:
  - `QR Code`
  - `EAN-13` (Mã số thương phẩm quốc tế)
  - `Code 128` (Mã vạch công nghiệp)
  - `Code 39`, `UPC`, v.v.
- **Giao diện quét chuyên nghiệp (Custom UI Viewfinder)**:
  - Khung ngắm quét trung tâm với các góc căn chỉnh màu xanh lá nổi bật.
  - Thông báo hướng dẫn trực quan: *"Hướng camera về phía mã vạch để quét"*.
  - Điều khiển thao tác nhanh dưới đáy màn hình:
    - **Bật/Tắt Flash**: Hỗ trợ chiếu sáng khi quét trong môi trường thiếu sáng.
    - **Đổi Camera**: Chuyển đổi linh hoạt giữa camera trước và camera sau.
- **Phản hồi âm thanh tương tác**:
  - Âm **tít** (`beep.mp3`): Báo hiệu nhận diện thành công mã mới.
  - Âm **è** (`error.mp3`): Cảnh báo khi người dùng quét lại mã vừa quét trước đó.
- **Cơ chế chống quét trùng lặp (Anti-Spam / Debounce)**: Tự động khóa quét tạm thời để tránh nhận diện trùng lặp nhiều lần mà vẫn giữ khung hình camera mở liên tục.
- **Bảng kết quả nhận diện**: Hiển thị rõ ràng loại mã (`Type`) và nội dung giá trị (`Result`) giải mã được.

---

## 🛠 Công Nghệ & Thư Viện

- **Ngôn ngữ**: Dart (SDK `>=3.0.0 <4.0.0`)
- **Framework**: Flutter
- **Thư viện chính**:
  - [`mobile_scanner: ^7.2.0`](https://pub.dev/packages/mobile_scanner) - Nhận diện và xử lý luồng camera quét mã vạch tốc độ cao.
  - [`audioplayers: ^6.0.0`](https://pub.dev/packages/audioplayers) - Phát âm thanh phản hồi từ thư mục assets.

---

## 📂 Cấu Trúc Thư Mục

```text
mobile_Scanner_app/
├── assets/
│   ├── beep.mp3             # Âm thanh khi quét mã mới thành công
│   └── error.mp3            # Âm thanh cảnh báo quét trùng mã
├── lib/
│   └── main.dart            # Toàn bộ mã nguồn: MobileScanner, Sound logic, Viewfinder & Controls
├── pubspec.yaml             # Khai báo packages và tài nguyên assets audio
└── README.md
```

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Ứng Dụng

### Yêu cầu tiên quyết:
- Đã cài đặt [Flutter SDK](https://docs.flutter.dev/get-started/install).
- Thiết bị thật (Android / iOS) có camera hoạt động. *(Lưu ý: Camera trên máy ảo emulator cần được cấu hình Webcam để có thể quét mã)*.

### Các bước thực hiện:

1. **Di chuyển vào thư mục dự án**:
   ```bash
   cd mobile_Scanner_app
   ```

2. **Cài đặt các dependencies**:
   ```bash
   flutter pub get
   ```

3. **Chạy trên thiết bị di động**:
   ```bash
   flutter run
   ```

### ⚙️ Lưu ý cấp quyền Camera:
- **Android**: Quyền camera đã được cấu hình tự động thông qua plugin `mobile_scanner`.
- **iOS**: Hãy đảm bảo khóa `NSCameraUsageDescription` đã được khai báo trong `ios/Runner/Info.plist`.
