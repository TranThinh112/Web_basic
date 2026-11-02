# SPX Express Web Portal (Next.js & React)

Hệ thống Web điều phối & phân loại bưu kiện SPX Express dành cho máy tính để bàn (Desktop / Tablet) tại các Hub / Sorting Center bưu cục.

Được chuyển thể toàn diện từ ứng dụng di động Flutter (`App_Quet_Ma`), dùng chung Backend Node.js Express sẵn có.

---

## 🚀 Các Tính Năng Đã Triển Khai & Cập Nhật Mới Nhất

### 1. Quản lý Điều phối & Menu Điều hướng (TO Management & Orders Router)
- **Gộp nhóm TO Management:** Tối ưu thanh điều hướng Menu bên trái; gom hai chức năng chính vào dropdown **TO Management**:
  - `/TOmanagement/sorting`: Trạm phân loại & Gom bao hàng (Landing View chọn mã TO trước khi vào giao diện quét).
  - `/TOmanagement/listTO`: Danh sách quản lý các bao hàng trong kho.
- **Phân hệ Quản lý Đơn hàng (/orders):**
  - `/orders`: Kho toàn bộ đơn hàng tồn kho (Orders Inventory).
  - `/orders/search`: Tra cứu vận đơn nhanh và tra cứu hàng loạt.
  - `/orders/create`: Tạo mới đơn hàng thông minh.

---

### 2. Quản lý Toàn bộ Đơn hàng Trong Kho (/orders - Inventory View)
- **Chuẩn hóa Trạng thái Vận đơn (Inbound vs Outbound):**
  - **`Inbound` (Nhãn xanh lá):** Các đơn đã được quét phân loại và đóng vào bao hàng TO. Cột mã TO hiển thị mã bao tương ứng kèm thời gian đóng bao.
  - **`Outbound` (Nhãn đỏ):** Các đơn hàng vừa tạo mới hoặc chưa đóng vào bao hàng. Cột mã TO để trống.
- **Thuật toán Sắp xếp (Sorting) chuẩn xác tuyệt đối:**
  - **Ưu tiên 1:** Tất cả đơn **`Inbound`** hiển thị lên đầu bảng, sắp xếp theo thời gian đóng bao (`thoiGianDongBao`) mới nhất.
  - **Ưu tiên 2:** Tất cả đơn **`Outbound`** hiển thị ngay sau, sắp xếp theo thời gian tạo đơn (`thoiGianTao`) mới nhất. Các đơn vừa tạo sẽ lập tức hiển thị trên cùng nhóm Outbound ở trang 1.
- **Tối ưu Bảng dữ liệu (Responsive - Không cần scroll ngang):**
  - Gộp cột người gửi và người nhận vào cùng dòng trực quan.
  - Tên các cột ngắn gọn, dễ nhìn: `Khối lượng` -> `kg`, `Tiền thu` -> `cost`, `Thời gian` -> `Time Create`.
  - Thay thế nút "Xem bill" cồng kềnh bằng biểu tượng con mắt (`Eye icon`) sắc nét mở Modal chi tiết.
- **Bộ lọc & Phân trang:** Lọc theo trạng thái Tất cả / Inbound / Outbound, tìm kiếm theo từ khóa theo thời gian thực và phân trang linh hoạt.

---

### 3. Tạo Đơn hàng Thông minh & Cơ chế Chống Trùng Mã (Zero-Collision)
- **Quy chuẩn mã vận đơn SPX Express:**
  - Tự động sinh mã chuẩn: `SPXVN` + `{2 số năm}` + `{8 số ngẫu nhiên}` + `{ký tự tháng: 1-9, A, B, C}`.
- **Cơ chế Tự động Thử lại tại Backend (Backend Auto-Retry):**
  - Tích hợp kiểm soát trùng khóa chính (`ER_DUP_ENTRY` / Error 1062) trực tiếp trên Backend.
  - Nếu mã bị trùng trong Database, Backend tự động sinh mã mới và lưu lại ngay lập tức (tối đa 5 lần lặp ngầm).
  - **Người dùng chỉ cần ấn Tạo đơn 1 lần duy nhất**, loại bỏ hoàn toàn tình trạng báo lỗi bắt tạo lại đơn, triệt tiêu xác suất trùng mã đối với trải nghiệm người dùng về xấp xỉ 0%.
- **Tự động định tuyến Hub vùng (Auto Region Mapping):**
  - Tự động phân tích địa chỉ người nhận bằng thuật toán so khớp Levenshtein (Fuzzy Matching) để ánh xạ về 5 Hub chính: `HN`, `HCM`, `DN`, `CT`, `MT`.

---

### 4. Trạm phân loại & Gom bao Transfer Order (TO Station)
- **Hỗ trợ thiết bị chuyên dụng:**
  - Tương thích tối đa với súng bắn mã vạch Barcode Scanner (bắt sự kiện phím Enter tốc độ cao).
  - Tích hợp quét trực tiếp qua Webcam/Camera máy tính (`html5-qrcode`).
- **Phản hồi âm thanh chuẩn:** Tiếng Beep nhẹ khi quét đúng và âm Buzz cảnh báo lỗi (sai nơi nhận, đơn đã quét, đơn không hợp lệ) qua Web Audio API.
- **Đóng bao & In nhãn:** Niêm phong bao hàng, chốt tải trọng và in nhãn Bill TO tiêu chuẩn.

---

### 5. Danh sách Bao hàng (TO Management - listTO)
- Bảng quản lý tổng thể các bao TO trong kho.
- Xem danh sách chi tiết các gói hàng bên trong bao (đồng bộ real-time từ Database).
- Mở lại bao để quét thêm gói hàng (Reopen TO), xóa bao và in lại nhãn bao hàng.

---

### 6. Tra cứu Vận đơn Hàng loạt (Multi-Order Tracking)
- **Tra cứu hàng loạt (Batch Search):** Hỗ trợ dán đồng thời hàng chục mã vận đơn phân cách bằng dấu cách, phẩy, chấm phẩy hoặc xuống dòng từ Excel/Notepad.
- **Kiểm tra định dạng & tồn tại nghiêm ngặt:** Cảnh báo ngay lập tức nếu có mã sai quy chuẩn hoặc không tồn tại trong hệ thống.
- **Xem Bill theo yêu cầu:** Popup tem bưu kiện chuẩn khổ lớn, hỗ trợ đóng nhanh bằng phím ESC.

---

## ⚙️ Hướng dẫn Khởi chạy Web

### Cài đặt môi trường & Thư viện
```bash
cd App_Web/Web_Quet_Ma
npm install
```

### Chạy ở môi trường Development
```bash
npm run dev
```
Mở trình duyệt tại: `http://localhost:3000`

### Môi trường Production & Kết nối Backend
- Backend API mặc định có thể trỏ tới Localhost hoặc Backend Online Railway:
  `https://webbasic-production-47cd.up.railway.app`
- Cấu hình linh hoạt qua trang **Cài đặt thiết bị** hoặc file `.env.local`:
  ```env
  NEXT_PUBLIC_API_URL=https://webbasic-production-47cd.up.railway.app
  ```


---

## 📌 LỊCH SỬ CẬP NHẬT & TỐI ƯU HÓA HỆ THỐNG WEB (CHANGELOG)

### 1. Tái cấu trúc Menu & Điều hướng TO Management
- **Gộp Menu Dropdown:** Gộp 2 màn hình *Phân loại* và *Danh sách bao hàng* vào mục menu chính **`TO management`**. Khi click sẽ xổ ra 2 mục con:
  - `/TOmanagement/sorting`: Trạm phân loại & quét TO.
  - `/TOmanagement/listTO`: Quản lý danh sách toàn bộ bao hàng.
- **Màn hình Landing tại `/TOmanagement/sorting`:**
  - Không tự động sinh bao và không vào thẳng màn quét mã ngay từ đầu.
  - Hiển thị giao diện thẻ chào đón nổi bật với nút **"Tạo bao mới ngay"**, nút truy cập nhanh danh sách bao hàng, và danh sách các bao đang dở dang (`Packing`) để nhân viên bấm vào quét tiếp.
  - Khi bấm **"Tạo bao mới ngay"**: hệ thống sinh mã TO chuẩn (`TOyyMMddXXXXX`), lưu vào database với trạng thái `Packing` và tự động điều hướng sang router động:
    ```
    /TOmanagement/sorting/[maTO]
    ```

### 2. Thiết kế lại Trạm Quét Mã Phân Loại (`PhanLoaiScanView.tsx`)
- **Bố cục giao diện 2 phần trực quan:**
  - **Phía trên (Scan Area):** Ô nhập liệu hỗ trợ đồng thời cả súng bắn mã vạch (bắt sự kiện `Enter`) và nhập tay, nút bật/tắt camera quét mã, mã TO, trạng thái badge, điểm đến Sorting Hub.
  - **Phía dưới (DataTable):** Bảng hiển thị danh sách toàn bộ kiện hàng trong bao gồm các cột:
    1. `#` (STT)
    2. `Mã đơn` (VD: `SPXVN06679685553`)
    3. `Sorting (Nơi đến)` (VD: `HCM`, `CT`, `HN`...)
    4. `Khối lượng` (kg)
    5. `Thời gian scan vào TO` (Giờ:Phút:Giây)
    6. `Xóa` (Nút đỏ dấu `X` cuối hàng để gỡ đơn khỏi bao)
- **Chuẩn hóa hiển thị trạng thái (Status Badges):**
  - **`Packing`**: Hiển thị chữ màu đỏ trên nền hồng nhạt (`bg-rose-50 text-rose-700 border-rose-200`).
  - **`Packed`**: Hiển thị chữ màu xanh lá trên nền xanh nhạt (`bg-emerald-50 text-emerald-700 border-emerald-200`).
  - Bỏ toàn bộ các hậu tố mở ngoặc dài dòng, chỉ giữ chuẩn gọn gàng `Packing` / `Packed`.
- **Loại bỏ các thành phần mẫu thừa:** Đã gỡ bỏ nút *+ Sinh mã mẫu SPXVN* và dòng chú thích dài dòng theo đúng yêu cầu.

### 3. Đồng bộ hóa chuẩn xác dữ liệu với bảng `TO_orders` (Database)
- **Tập trung hóa vào bảng `TO_orders`:**
  - Khi quét đơn hoặc xóa đơn, toàn bộ mảng `danhSachGoiHang`, số lượng `SL`, khối lượng `totalWeight`, trạng thái `Packing` và điểm đến `diaDiemGiaoHang` đều được lưu ngay lập tức vào database qua `PUT /TO/:maTO`.
  - Khắc phục triệt để lỗi khi thoát ra ngoài rồi vào lại bị mất danh sách kiện trong bảng bên dưới.
- **Sửa lỗi điểm đến hiển thị "SOC":**
  - API `POST /orders/scan/:id` được bổ sung trả về thông tin `noiNhan` của đơn vừa quét.
  - Khi quét đơn đầu tiên vào bao, hệ thống tự động gán chính xác `diaDiemGiaoHang` của bao theo điểm đến của đơn (VD: `HCM`, `CT`...) thay vì fallback nhầm thành `SOC`.

### 4. Nâng cấp Danh Sách Bao Hàng (`DanhSachTOView.tsx`)
- **Nút Thao Tác Chuyên Biệt Theo Trạng Thái:**
  - Đối với bao **`Packed`**: Hiển thị nút **`ReOpen`**. Khi click sẽ mở lại bao và chuyển thẳng vào router `/TOmanagement/sorting/[maTO]` ngay lập tức (không hiển thị popup alert/confirm làm gián đoạn).
  - Đối với bao **`Packing`**: Hiển thị nút **`Quét tiếp`** để điều hướng trực tiếp tới router của bao đó.
- **Sắp xếp thứ tự danh sách (Sort Descending):**
  - Sắp xếp từ trên xuống dưới (mới nhất luôn nằm ở dòng đầu tiên của bảng).
  - Đối với bao `Packed`: sắp xếp theo thời gian hoàn tất mới nhất (`completeTime`).
  - Đối với bao `Packing`: sắp xếp theo thời gian khởi tạo mới nhất (`ngayTao`).
- **Hiển thị Thời Gian Đầy Đủ Giờ Việt Nam:**
  - Cột thời gian hiển thị chi tiết cả Giờ : Phút : Giây Ngày/Tháng/Năm (`HH:mm:ss dd/MM/yyyy`).
  - Chuẩn hóa theo đúng múi giờ Việt Nam (`Asia/Ho_Chi_Minh` - GMT+7), xử lý triệt để lỗi lệch giờ UTC +7.
