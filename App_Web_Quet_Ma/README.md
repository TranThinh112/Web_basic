# Hệ Thống Quản Lý & Phân Loại Bưu Kiện (SPX Express System)

Hệ thống quản lý, điều phối và phân loại bưu kiện chuyên dụng cho bưu cục và trung tâm phân loại (Hub / Sorting Center) theo quy trình logistics tiêu chuẩn SPX Express. 

Hệ thống bao gồm đầy đủ **Ứng dụng di động (Mobile App Flutter)**, **Cổng quản trị web (Web Portal Next.js)** và **Máy chủ dịch vụ (Backend API Node.js)** kết nối cơ sở dữ liệu tập trung.

---

## 📸 Giao Diện Hệ Thống (UI Screenshots)

| 1. Quản Lý Chuyến Xe Trung Chuyển (Dashboard) | 2. Tạo Chuyến Xe Trung Chuyển |
| :---: | :---: |
| ![Quản Lý Chuyến Xe](screenshots/home_menu.png) | ![Tạo Chuyến Xe](screenshots/tao_chuyen_xe.png) |

| 3. Phân Loại Bao Hàng (TO) | 4. Quản Lý Danh Sách Bao Hàng |
| :---: | :---: |
| ![Phân Loại Bao](screenshots/phan_loai_bao.png) | ![Danh Sách TO](screenshots/danh_sach_to.png) |

| 5. Chi Tiết Kiện Hàng Trong Bao | 6. Tra Cứu Vận Đơn Bưu Kiện |
| :---: | :---: |
| ![Chi Tiết TO](screenshots/chi_tiet_to.png) | ![Tra Cứu Đơn](screenshots/tra_cuu_don.png) |

---

## 🚀 Các Thành Phần Trong Hệ Thống

### 1. Ứng dụng Di động Mobile (`App_Web/App_Quet_Ma` - Flutter)
Được thiết kế tối ưu cho điện thoại thông minh (Android/iOS) và thiết bị PDA quét mã cầm tay chuyên dụng:
- **Tối ưu hóa Scanner:** Sử dụng `mobile_scanner` quét liên tục với các định dạng mã bưu kiện phổ biến (`Code128`, `QRCode`, `EAN13`, `Code39`). Tắt xử lý frame ảnh thừa (`returnImage: false`), giảm tải CPU/RAM, tăng tốc độ nhận diện.
- **Chống quét trùng & chống lag:** Tích hợp bộ đệm thời gian thực (Debounce 1.5s) và cờ khóa xử lý API, ngăn chặn gọi request dồn dập khi camera chĩa lâu vào mã vạch.
- **Đèn Flash:** Bật/tắt đèn chiếu sáng tức thì ngay trên Header màn hình quét mã trong kho tối.
- **Phản hồi âm thanh không độ trễ:** Sử dụng 2 instance `AudioPlayer` chạy nền với âm thanh beep (thành công) và error buzzer (<50ms).
- **Điều hướng mượt mà, chống đè Stack:**
  - Chuẩn hóa thanh điều hướng `HeaderWidget` hiển thị tiêu đề ngữ cảnh rõ ràng và nút quay lại an toàn với `Navigator.canPop`.
  - Cơ chế `pushReplacement` khi hoàn tất đóng bao (`CreateTOScreen` ➔ `TOResultScreen`) hoặc tạo mới TO, loại bỏ triệt để hiện tượng kẹt trang cũ hay các trang bị chồng đè lên nhau.
  - Đăng xuất an toàn dọn sạch stack với xác nhận người dùng.
- **Tính năng đầy đủ:** Quét gom bao TO, kiểm tra trạng thái TO, tra cứu đơn hàng, tạo đơn bưu kiện mới, xem/chia sẻ phiếu in niêm phong (Bill TO, Bill đơn), chế độ Dark Mode / Light Mode.

### 2. Cổng Quản Trị Web Portal (`App_Web/Web_Quet_Ma` - Next.js & React)
Dành cho máy tính văn phòng, trạm máy bàn phân loại (Desktop) hoặc máy tính bảng tại Hub:
- **Công nghệ hiện đại:** Xây dựng trên Next.js 16 (App Router), React 19, TypeScript và Tailwind CSS v4.
- **Điều phối & Quản lý chuyến xe trung chuyển:** Theo dõi trực quan trạng thái xe (*Chờ bốc hàng ➔ Đang tải hàng ➔ Đang trung chuyển ➔ Hoàn thành*). Hỗ trợ Wizard tạo chuyến 4 bước chuẩn hóa.
- **Trạm quét mã phân loại (TO Station):**
  - Tích hợp linh hoạt cả súng bắn mã vạch phần cứng (nhận diện tự động qua phím Enter) lẫn quét trực tiếp qua Webcam/Camera máy tính (`html5-qrcode`).
  - Âm thanh phản hồi trực tiếp bằng Web Audio API không phụ thuộc file ngoài.
  - Hiệu ứng hoàn tất đóng bao sinh động (Confetti) và hỗ trợ in ấn phiếu TO (Print View) trực tiếp từ trình duyệt.
- **Tra cứu và quản lý:** Bảng danh sách bao hàng, mở lại bao (Reopen), tra cứu hành trình vận đơn và tạo đơn hàng mẫu.
- **Cấu hình trạm làm việc:** Dễ dàng đổi URL Backend linh hoạt giữa Localhost và Cloud Railway.

### 3. Máy Chủ Backend API (`Server/server_appQuetMa_nodeJS` - Express)
Cung cấp toàn bộ RESTful API dùng chung cho cả App Mobile và Web:
- **Người dùng (`/`):** Đăng nhập tài khoản nhân viên bưu cục, xác thực, khôi phục mật khẩu.
- **Bưu kiện (`/orders`):** Tạo bưu kiện, tra cứu chi tiết, lấy danh sách phân trang (Pagination 10 đơn/trang), tìm kiếm theo từ khóa/mã đơn, quét đơn vào bao TO (`/orders/scan/:id`), xóa đơn khỏi bao (`/orders/remove/:id`).
- **Bao hàng Transfer Order (`/TO`):** Tạo bao mới, lấy danh sách, kiểm tra chi tiết bao, cập nhật khối lượng/số lượng kiện, đóng bao (`complete`), mở lại bao (`reopen`) và xóa bao.

### 4. Cơ Sở Dữ Liệu (`Database/`)
- Lưu trữ tập trung bằng MySQL/MariaDB (`orders.sql`, `TO_orders.sql`, `users.sql`).
- Hỗ trợ lưu trữ offline SQLite cục bộ (`orders_db.db`) trên ứng dụng di động khi mất kết nối mạng.

---

## 🛠️ Công Nghệ Sử Dụng

| Thành phần | Công nghệ chính |
| :--- | :--- |
| **Mobile App** | Flutter (Dart 3), Material Design, `mobile_scanner`, `audioplayers`, `sqflite`, `http`, `shared_preferences` |
| **Web Portal** | Next.js 16 (App Router), React 19, TypeScript, Tailwind CSS v4, `html5-qrcode`, `lucide-react`, Web Audio API |
| **Backend API** | Node.js, Express.js (v5), MySQL2, CORS |
| **Database** | MySQL / MariaDB, SQLite offline cache |
| **Testing Tools**| Python 3 (`random_SPX.py` sinh mã vận đơn kiểm thử ngẫu nhiên) |

---

## 📂 Cấu Trúc Thư Mục

```
App_Web_Quet_Ma/
├── App_Web/
│   ├── App_Quet_Ma/               # Mã nguồn ứng dụng Flutter (Mobile Android/iOS)
│   │   ├── assets/                # Âm thanh phản hồi (beep/error), SQLite db, icon
│   │   └── lib/
│   │       ├── BILL/              # Màn hình hóa đơn bao hàng (BillScreen, BillTo)
│   │       ├── data/              # Service gọi API Backend (api_service.dart)
│   │       ├── Login/             # Giao diện đăng nhập hệ thống
│   │       ├── models/            # Model dữ liệu (Orders, TO, AppBar header logo)
│   │       ├── PhanLoai/          # Phân loại bao hàng (Create TO, Scan TO, Danh sách TO)
│   │       ├── QuanLy/            # Module quản lý đơn hàng
│   │       ├── Setting/           # Cấu hình giao diện và tài khoản
│   │       ├── TaoDon/            # Tạo đơn bưu kiện mới
│   │       ├── TraCuu/            # Tra cứu thông tin vận đơn
│   │       └── main.dart          # Entry point của ứng dụng Mobile
│   └── Web_Quet_Ma/               # Mã nguồn cổng quản trị Web Portal (Next.js & React)
│       ├── public/                # Tài nguyên tĩnh
│       └── src/
│           ├── app/               # Định tuyến trang (Dashboard, Sorting, Orders, Tracking...)
│           ├── components/        # UI components (Header, Sidebar, CameraScanner, Modals...)
│           ├── context/           # State quản trị và cấu hình
│           ├── lib/               # Service gọi API và Web Audio
│           └── types/             # Định nghĩa kiểu dữ liệu TypeScript
├── Database/                      # Kịch bản cơ sở dữ liệu MySQL (orders.sql, TO_orders.sql...)
├── Server/
│   └── server_appQuetMa_nodeJS/   # Máy chủ Backend Node.js Express
│       ├── routers/               # Định tuyến API (orders, TO_Orders, users)
│       ├── db.js                  # Cấu hình kết nối MySQL
│       └── server.js              # Entry point Express Server
├── screenshots/                   # Hình ảnh giao diện ứng dụng
├── random_SPX.py                  # Script Python sinh dữ liệu kiểm thử
└── README.md                      # Tài liệu tổng quan dự án
```

---

## ⚙️ Hướng Dẫn Cài Đặt & Khởi Chạy

### 1. Khởi động Backend Node.js
```bash
cd Server/server_appQuetMa_nodeJS
npm install
npm start
```
*Server chạy mặc định tại:* `http://localhost:3000`

### 2. Khởi chạy Cổng Web Portal (Next.js)
```bash
cd App_Web/Web_Quet_Ma
npm install
npm run dev
```
*Truy cập giao diện Web tại:* `http://localhost:3001` (hoặc cổng được hiển thị trong terminal)

### 3. Khởi chạy Ứng dụng Di Động (Flutter)
```bash
cd App_Web/App_Quet_Ma
flutter pub get

# Chạy trên thiết bị kết nối / máy ảo:
flutter run

# Hoặc xuất file cài đặt APK cho điện thoại Android:
flutter build apk --release
```

### 4. Sinh dữ liệu bưu kiện kiểm thử (Tùy chọn)
```bash
python random_SPX.py
```
