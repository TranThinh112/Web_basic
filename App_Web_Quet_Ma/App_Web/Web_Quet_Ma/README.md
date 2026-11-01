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
4. **Tra cứu vận đơn (Order Lookup):**
   - Tra cứu chi tiết hành trình, địa chỉ người nhận, khối lượng, tiền COD.
5. **Tạo mới đơn hàng (Create Order):**
   - Form đăng ký đơn mới, có nút sinh mã ngẫu nhiên dạng `SPXVN...` giống script Python `random_SPX.py`.
6. **Cài đặt thiết bị & Kết nối Backend:**
   - Đổi URL backend (Localhost / Railway online), bật/tắt âm thanh, kiểm tra tiếng chuông phản hồi.

## ⚙️ Hướng dẫn Khởi chạy Web
```bash
cd App_Web/Web_Quet_Ma
npm run dev
```
Mở trình duyệt tại: `http://localhost:3000` (hoặc cổng do Next.js cung cấp).
