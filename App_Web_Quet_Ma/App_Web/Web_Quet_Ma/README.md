# SPX Express Web Portal (Next.js & React)

Hệ thống Web điều phối & phân loại bưu kiện SPX Express dành cho máy tính để bàn (Desktop / Tablet) tại các Hub / Sorting Center bưu cục.

Được chuyển thể toàn diện từ ứng dụng di động Flutter (`App_Quet_Ma`), dùng chung Backend Node.js Express sẵn có.

---

## 🌐 Đường Dẫn Truy Cập Hệ Thống (Live Deployment)

| Dịch vụ | Nền tảng | URL Trực tuyến |
| :--- | :--- | :--- |
| **SPX Web Portal** | Next.js 14 / Railway | **https://webbasic-production-c870.up.railway.app** |
| **Backend API** | Node.js Express / Railway | **https://webbasic-production-47cd.up.railway.app** |

---

## 📦 Danh Sách Mã Đơn Mẫu & Phiếu In Vận Đơn (Dùng Để Test Hệ Thống)

Người dùng có thể sử dụng các mã vận đơn mẫu và hình ảnh phiếu in dưới đây để thử nghiệm các tính năng **Quét mã vạch (Barcode/QR)**, **Phân loại vào bao TO**, **Tra cứu đơn hàng** và **Kiểm tra luồng Inbound / Outbound**:

| Mã vận đơn (Tracking ID) | Nơi gửi | Nơi nhận (Hub đích) | Khối lượng | Trạng thái mẫu | Phiếu in mẫu |
| :--- | :---: | :---: | :---: | :---: | :---: |
| **`SPXVN06721210393`** | HN | **CT** (Cần Thơ) | 2.64 kg | Inbound | [Xem phiếu](public/sample_bills/SPXVN06721210393.jpg) |
| **`SPXVN06620767933`** | HN | **DN** (Đà Nẵng) | 2.31 kg | Inbound | [Xem phiếu](public/sample_bills/SPXVN06620767933.jpg) |
| **`SPXVN06733932163`** | DN | **MT** (Miền Trung) | 0.34 kg | Outbound | [Xem phiếu](public/sample_bills/SPXVN06733932163.jpg) |
| **`SPXVN06715297463`** | DN | **HN** (Hà Nội) | 2.72 kg | Outbound | [Xem phiếu](public/sample_bills/SPXVN06715297463.jpg) |
| **`SPXVN06687683833`** | HN | **MT** (Miền Trung) | 1.63 kg | Outbound | [Xem phiếu](public/sample_bills/SPXVN06687683833.jpg) |
| **`SPXVN06679685553`** | MT | **CT** (Cần Thơ) | 2.96 kg | Inbound | [Xem phiếu](public/sample_bills/SPXVN06679685553.jpg) |
| **`SPXVN06670867033`** | MT | **HCM** (TP.HCM) | 0.43 kg | Outbound | [Xem phiếu](public/sample_bills/SPXVN06670867033.jpg) |

### 🖼️ Hình Ảnh Phiếu Vận Đơn Mẫu Trực Quan (Scan Test)

<p align="center">
  <img src="public/sample_bills/SPXVN06721210393.jpg" width="280" alt="Bill SPXVN06721210393" />
  <img src="public/sample_bills/SPXVN06620767933.jpg" width="280" alt="Bill SPXVN06620767933" />
  <img src="public/sample_bills/SPXVN06733932163.jpg" width="280" alt="Bill SPXVN06733932163" />
</p>
<p align="center">
  <img src="public/sample_bills/SPXVN06715297463.jpg" width="280" alt="Bill SPXVN06715297463" />
  <img src="public/sample_bills/SPXVN06687683833.jpg" width="280" alt="Bill SPXVN06687683833" />
  <img src="public/sample_bills/SPXVN06679685553.jpg" width="280" alt="Bill SPXVN06679685553" />
</p>
<p align="center">
  <img src="public/sample_bills/SPXVN06670867033.jpg" width="280" alt="Bill SPXVN06670867033" />
</p>

---

## 📸 Giao Diện Cổng Quản Trị Web Portal (Web UI Screenshots)

Các màn hình chức năng chính của phiên bản Web Portal (Desktop / Tablet) được thiết kế hiện đại, hỗ trợ Dark Mode và trực quan hóa toàn bộ quy trình kho bãi:

### 1. Quản Lý Chuyến Xe Trung Chuyển & Xuất Bến (/dispatch)
Giao diện điều phối vận tải theo dõi hành trình xe từ Hub, bốc dỡ hàng và cấp lệnh xuất bến:
<p align="center">
  <img src="public/screenshots/tao_chuyen_xe.png" width="850" alt="Quản lý chuyến xe và xuất bến" />
</p>

---

### 2. Trạm Phân Loại & Gom Kiện Hàng Vào Bao TO (/TOmanagement/sorting)
Hỗ trợ cả súng bắn mã vạch chuyên dụng và quét trực tiếp qua Webcam/Camera, cảnh báo âm thanh tức thì:
<p align="center">
  <img src="public/screenshots/phan_loai_bao.png" width="850" alt="Trạm phân loại và quét kiện vào bao TO" />
</p>

---

### 3. Danh Sách Quản Lý Bao Hàng & Chi Tiết Kiện Trong Bao (/TOmanagement/listTO)
Theo dõi tình trạng niêm phong, tải trọng bao hàng và xem chi tiết danh sách bưu kiện bên trong:
<p align="center">
  <img src="public/screenshots/danh_sach_to.png" width="850" alt="Danh sách quản lý bao hàng TO" />
</p>
<p align="center">
  <img src="public/screenshots/chi_tiet_to.png" width="850" alt="Chi tiết bưu kiện trong bao hàng" />
</p>

---

### 4. Tra Cứu Vận Đơn Nâng Cao & Xem Tem Vận Đơn Chi Tiết (/orders/search)
Tra cứu đơn lẻ hoặc hàng loạt với kiểm tra cơ sở dữ liệu nghiêm ngặt, hiển thị tem Bill chuẩn SPX:
<p align="center">
  <img src="public/screenshots/tra_cuu_don.png" width="850" alt="Tra cứu vận đơn và xem bill" />
</p>

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
