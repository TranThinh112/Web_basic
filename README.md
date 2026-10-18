# Tổng Hợp Dự Án Web & Mobile (Web_basic)

Kho lưu trữ bao gồm các dự án thực hành và bài tập phát triển ứng dụng di động (Flutter) cùng với thiết kế giao diện web cơ bản (HTML/CSS).

---

## 📂 Danh Sách Dự Án

| Tên Thư Mục | Loại Dự Án | Công Nghệ Chính | Mô Tả Ngắn |
| :--- | :--- | :--- | :--- |
| [`hutech_campus_app`](#1-hutech-campus-app) | Mobile / Multi-platform | Flutter, Dart | Giới thiệu các cơ sở đào tạo của Đại học HUTECH |
| [`mobile_Scanner_app`](#2-mobile-scanner-app) | Mobile App | Flutter, `mobile_scanner`, `audioplayers` | Quét mã QR, mã vạch với hiệu ứng laser và âm thanh |
| [`showroom_web`](#3-showroom-web-gf-furniture) | Website | HTML5 / XHTML, CSS | Trang giới thiệu showroom thiết kế nội thất cao cấp |

---

## 1. HUTECH Campus App (`hutech_campus_app`)

Ứng dụng Flutter hiển thị danh sách và thông tin chi tiết về các cơ sở (campuses) của Trường Đại học Công nghệ TP.HCM (HUTECH).

#### 📸 Hình ảnh giao diện:
<p align="center">
  <img src="./hutech_campus_app/screenshots/home_screen.png" width="260" alt="Home Screen" />
  &nbsp;&nbsp;&nbsp;&nbsp;
  <img src="./hutech_campus_app/screenshots/detail_screen.png" width="260" alt="Detail Screen" />
</p>

### ✨ Tính Năng
- **Danh sách cơ sở**: Hiển thị các cơ sở kèm màu sắc nhận diện riêng biệt (Saigon Campus, Ung Văn Khiêm Campus, Thu Duc Campus, Hitech Park Campus).
- **Xem chi tiết cơ sở**: Điều hướng qua trang chi tiết hiển thị địa chỉ và hình ảnh phối cảnh của từng cơ sở qua kết nối mạng.
- **Hỗ trợ đa nền tảng**: Android, iOS, Web, Windows, macOS, Linux.

### 🛠 Cấu Trúc Mã Nguồn
```text
hutech_campus_app/
├── lib/
│   ├── models/
│   │   └── campus.dart         # Định nghĩa model Campus và danh sách mẫu
│   ├── screens/
│   │   ├── home_screen.dart    # Màn hình danh sách cơ sở
│   │   └── details_screen.dart # Màn hình chi tiết cơ sở
│   └── main.dart               # Cấu hình routes và khởi chạy app
└── pubspec.yaml                # Khai báo cấu hình và dependencies
```

### 🚀 Cách Chạy Dự Án
```bash
cd hutech_campus_app
flutter pub get
flutter run
```

---

## 2. Mobile Scanner App (`mobile_Scanner_app`)

Ứng dụng quét mã thời gian thực trên nền tảng Flutter, hỗ trợ nhận diện nhanh các loại mã vạch thông dụng và mã QR.

#### 📸 Hình ảnh giao diện:
<p align="center">
  <img src="./mobile_Scanner_app/screenshots/scanner_screen.png" width="280" alt="Scanner Screen" />
</p>

### ✨ Tính Năng
- **Quét đa định dạng**: Hỗ trợ QR Code, EAN-13, Code 128.
- **Giao diện quét trực quan**: Khung ngắm bo góc nổi bật kèm tia laser đỏ chuyển động quét (`AnimationController`).
- **Phản hồi âm thanh thông minh**:
  - Âm báo **tít** (`beep.mp3`) khi quét thành công mã mới.
  - Âm báo **è** (`error.mp3`) cảnh báo khi quét trùng mã vừa quét.
- **Chống spam quét (Debounce)**: Khóa xử lý tạm thời (~0.9s) tránh gọi hàm lặp lại liên tục mà vẫn duy trì luồng camera mượt mà.
- **Hiển thị thông tin**: Xem định dạng mã và nội dung dữ liệu vừa giải mã ở thanh dưới cùng.

### 🛠 Cấu Trúc Mã Nguồn
```text
mobile_Scanner_app/
├── assets/
│   ├── beep.mp3                # Âm thanh khi quét mã hợp lệ
│   └── error.mp3               # Âm thanh cảnh báo trùng mã
├── lib/
│   └── main.dart               # Toàn bộ logic camera, overlay painter, âm thanh và UI
└── pubspec.yaml                # Thư viện: mobile_scanner, audioplayers
```

### 🚀 Cách Chạy Dự Án
```bash
cd mobile_Scanner_app
flutter pub get
flutter run
```
> *Lưu ý: Ứng dụng yêu cầu quyền truy cập Camera trên thiết bị thật hoặc máy ảo có hỗ trợ camera.*

---

## 3. Showroom Web - GF Furniture (`showroom_web`)

Trang web giới thiệu showroom và các mẫu thiết kế không gian nội thất của thương hiệu **GF Furniture / GF Interior Design**.

#### 📸 Hình ảnh giao diện:
<p align="center">
  <img src="./showroom_web/screenshots/showroom_ui.png" width="750" alt="Showroom Web UI" />
</p>

### ✨ Tính Năng & Thành Phần Giao Diện
- **Header & Navigation Bar**: Logo thương hiệu cùng thanh menu liên kết (Trang chủ, Giới thiệu, Dự án, Liên hệ).
- **Banner Slider**: Trình diễn không gian trưng bày nội thất hiện đại.
- **Danh mục Sidebar**: Menu phân loại theo không gian (Văn phòng, Phòng khách, Phòng ngủ, Phòng sách, Nhà vườn) và danh mục Nội thất cao cấp.
- **Khu vực trưng bày sản phẩm (Content)**:
  - **Mẫu thiết kế mới**: Các mẫu thiết kế phòng khách cao cấp cho căn hộ Penthouse (`TKPK 001`, `TKPK 002`, `TKPK 003`).
  - **Nội thất cao cấp**: Dòng sản phẩm nội thất văn phòng và nhà ở cao cấp (`SFD0234`) kèm hình ảnh và nút xem chi tiết.
- **Footer**: Thông tin liên hệ showroom tại 131 Ngô Gia Tự, P.4, Q.10, TP.HCM, số điện thoại và các icon liên kết mạng xã hội.
- Kèm theo tài liệu báo cáo đồ án PDF.

### 🛠 Cấu Trúc Mã Nguồn
```text
showroom_web/
├── images/                     # Bộ sưu tập hình ảnh sản phẩm, banner, logo, icons
├── index_showrom.html          # Cấu trúc trang web HTML
├── index.css                   # Định kiểu giao diện CSS
└── Trần Tăng Trường Thịnh_webshowroom.pdf # Tài liệu báo cáo
```

### 🚀 Cách Mở Website
Mở trực tiếp file `index_showrom.html` bằng bất kỳ trình duyệt web nào (Chrome, Edge, Firefox,...) hoặc dùng Live Server trong VS Code:
```bash
cd showroom_web
start index_showrom.html
```

---

## 📌 Tác Giả / Sinh Viên Thực Hiện
- Dự án học tập và thực hành phát triển Web & Ứng dụng di động.