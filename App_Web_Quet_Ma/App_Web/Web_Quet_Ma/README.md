# SPX Express Web Portal (Next.js & React)

Hệ thống Web điều phối & phân loại bưu kiện SPX Express dành cho máy tính để bàn (Desktop / Tablet) tại các Hub / Sorting Center bưu cục.

Được chuyển thể toàn diện từ ứng dụng di động Flutter (`App_Quet_Ma`), dùng chung Backend Node.js Express sẵn có.

## 🚀 Các Tính Năng Đã Triển Khai
1. **Quản lý & Điều phối chuyến xe trung chuyển (Dashboard):**
   - Theo dõi trạng thái: Chờ bốc hàng ➔ Đang tải hàng ➔ Đang trung chuyển ➔ Hoàn thành.
   - Wizard tạo chuyến xe qua 4 bước chuẩn hóa (Tuyến đường -> Xe & Tài xế -> Gán bao TO -> Xác nhận).
2. **Trạm phân loại & Gom bao Transfer Order (TO Station):**
   - Hỗ trợ súng bắn mã vạch Barcode Scanner (nhận diện phím Enter).
   - Tích hợp quét webcam/camera trực tiếp (`html5-qrcode`).
   - Phản hồi âm thanh chuẩn (Beep thành công, Buzz cảnh báo lỗi) qua Web Audio API.
   - Đóng bao niêm phong & In nhãn phiếu bao hàng (Bill TO Print view).
3. **Danh sách bao hàng (TO Table):**
   - Bảng quản lý TO, mở lại bao để quét thêm (Reopen), xóa bao, in lại Bill TO.
4. **Tra c\u1ee9u v\u1eadn \u0111\u01a1n n\u00e2ng cao (Multi-Order Tracking & Verification):**
   - **Tra c\u1ee9u h\u00e0ng lo\u1ea1t (Batch Search):** H\u1ed7 tr\u1ee3 tra c\u1ee9u \u0111\u1ed3ng th\u1eddi nhi\u1ec1u m\u00e3 v\u1eadn \u0111\u01a1n, ch\u1ea5p nh\u1eadn m\u1ecdi \u0111\u1ecbnh d\u1ea1ng ph\u00e2n c\u00e1ch linh ho\u1ea1t (d\u1ea5u c\u00e1ch, ph\u1ea9y, ch\u1ea5m ph\u1ea9y, xu\u1ed1ng d\u00f2ng t\u1eeb Excel/Notepad). T\u1ef1 \u0111\u1ed9ng l\u1ecdc tr\u00f9ng m\u00e3.
   - **C\u01a1 ch\u1ebf x\u00e1c th\u1ef1c \u0111\u1ecbnh d\u1ea1ng (Format Validation):** Ki\u1ec3m tra c\u1ea5u tr\u00fac chu\u1ea9n SPX (SPXVN...), ph\u00e1t hi\u1ec7n v\u00e0 b\u00e1o l\u1ed7i ngay n\u1ebfu c\u00f3 m\u00e3 sai quy chu\u1ea9n.
   - **Ki\u1ec3m tra t\u1ed3n t\u1ea1i Database nghi\u00eam ng\u1eb7t (Strict Existence Check):** Truy v\u1ea5n song song; n\u1ebfu ph\u00e1t hi\u1ec7n d\u00f9 ch\u1ec9 1 m\u00e3 kh\u00f4ng t\u1ed3n t\u1ea1i tr\u00ean h\u1ec7 th\u1ed1ng, m\u00e0n h\u00ecnh l\u1eadp t\u1ee9c th\u00f4ng b\u00e1o danh s\u00e1ch m\u00e3 l\u1ed7i v\u00e0 **t\u1eeb ch\u1ed1i hi\u1ec3n th\u1ecb k\u1ebft qu\u1ea3** nh\u1eb1m \u0111\u1ea3m b\u1ea3o t\u00ednh to\u00e0n v\u1eb9n d\u1eef li\u1ec7u.
   - **B\u1ea3ng t\u1ed5ng h\u1ee3p chi ti\u1ebft:** Hi\u1ec3n th\u1ecb tr\u1ef1c quan STT, m\u00e3 \u0111\u01a1n, tr\u1ea1ng th\u00e1i, ng\u01b0\u1eddi g\u1eedi/nh\u1eadn, \u0111\u1ecba ch\u1ec9, Hub ph\u00e2n lo\u1ea1i (Sort-send/receive), t\u00ean h\u00e0ng.
   - **Xem Bill theo y\u00eau c\u1ea7u (Order Bill Modal):** C\u1ed9t h\u00e0nh \u0111\u1ed9ng trang b\u1ecb icon con m\u1eaft (Eye) cho t\u1eebng \u0111\u01a1n; click m\u1edf Popup tem b\u01b0u ki\u1ec7n chu\u1ea9n kh\u1ed5 l\u1edbn s\u1eafc n\u00e9t, h\u1ed7 tr\u1ee3 \u0111\u00f3ng nhanh b\u1eb1ng ph\u00edm ESC ho\u1eb7c click ra ngo\u00e0i.
5. **T\u1ea1o m\u1edbi \u0111\u01a1n h\u00e0ng th\u00f4ng minh (Smart Create Order):**
   - Form t\u1ea1o \u0111\u01a1n tinh g\u1ecdn: th\u00f4ng tin ng\u01b0\u1eddi g\u1eedi, ng\u01b0\u1eddi nh\u1eadn, s\u1ed1 \u0111i\u1ec7n tho\u1ea1i, \u0111\u1ecba ch\u1ec9, t\u00ean h\u00e0ng, kh\u1ed1i l\u01b0\u1ee3ng, ti\u1ec1n COD.
   - **T\u1ef1 \u0111\u1ed9ng sinh m\u00e3 SPX:** T\u1ef1 \u0111\u1ed9ng t\u1ea1o m\u00e3 ng\u1eabu nhi\u00ean chu\u1ea9n thu\u1eadt to\u00e1n SPX (SPXVN + m\u00e3 n\u0103m + 8 s\u1ed1 ng\u1eabu nhi\u00ean + m\u00e3 th\u00e1ng).
   - **T\u1ef1 \u0111\u1ed9ng \u00e1nh x\u1ea1 Hub ph\u00e2n lo\u1ea1i (Auto Sorting Mapping):** T\u00edch h\u1ee3p thu\u1eadt to\u00e1n \u0111\u1ed1i s\u00e1nh t\u1eeb kh\u00f3a v\u00e0 m\u1edd (Levenshtein) theo TaoDon_logic.dart, t\u1ef1 \u0111\u1ed9ng nh\u1eadn di\u1ec7n Hub v\u00f9ng g\u1eedi v\u00e0 Hub v\u00f9ng nh\u1eadn (HN, HCM, DN, CT, MT) t\u1eeb \u0111\u1ecba ch\u1ec9.
   - **Giao di\u1ec7n sau khi t\u1ea1o:** \u1ea8n form nh\u1eadp li\u1ec7u, hi\u1ec3n th\u1ecb k\u1ebft qu\u1ea3 m\u00e3 v\u1eeba t\u1ea1o k\u00e8m n\u00fat Sao ch\u00e9p v\u00e0 n\u00fat xem Popup phi\u1ebfu b\u01b0u ki\u1ec7n ho\u00e0n ch\u1ec9nh.
6. **Cài đặt thiết bị & Kết nối Backend:**
   - Đổi URL backend (Localhost / Railway online), bật/tắt âm thanh, kiểm tra tiếng chuông phản hồi.

## ⚙️ Hướng dẫn Khởi chạy Web
```bash
cd App_Web/Web_Quet_Ma
npm run dev
```
Mở trình duyệt tại: `http://localhost:3000` (hoặc cổng do Next.js cung cấp).
