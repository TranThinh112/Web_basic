# Showroom Web - GF Furniture

Website giới thiệu showroom và các giải pháp thiết kế nội thất cao cấp của thương hiệu **GF Furniture / GF Interior Design**.

---

## 📸 Giao Diện Trang Web (UI Screenshot)

<p align="center">
  <img src="./screenshots/showroom_ui.png" width="850" alt="Showroom UI" />
</p>

---

## 📌 Tổng Quan Dự Án

Trang web được thiết kế với giao diện tĩnh chuẩn mực, giới thiệu các mẫu thiết kế không gian sống hiện đại (phòng khách, penthouse, văn phòng làm việc) và các sản phẩm nội thất cao cấp.

### 🌟 Các Khối Giao Diện Chính

1. **Header & Thanh Menu Điều Hướng**:
   - Logo thương hiệu GF Furniture.
   - Menu chính gồm: *Trang chủ*, *Giới thiệu*, *Dự án*, *Liên hệ*.
2. **Banner / Slider**:
   - Hình ảnh trình diễn không gian nội thất sang trọng, bắt mắt ngay đầu trang.
3. **Thanh bên (Sidebar / Aside)**:
   - Danh mục **Thiết kế mới**: Văn phòng, Phòng khách, Phòng ngủ, Phòng sách, Nhà vườn.
   - Danh mục **Nội thất cao cấp**: Phân loại theo từng khu vực chức năng.
4. **Khu vực nội dung chính (Main Content)**:
   - **Mẫu thiết kế mới**: Giới thiệu các mẫu thiết kế phòng khách cho căn hộ Penthouse (`TKPK 001`, `TKPK 002`, `TKPK 003`).
   - **Nội thất cao cấp**: Trưng bày danh mục dòng sản phẩm `SFD0234` cho văn phòng và nhà ở kèm nút xem chi tiết.
5. **Chân trang (Footer)**:
   - Thông tin liên hệ showroom: *131 Ngô Gia Tự, P.4, Q.10, TP. Hồ Chí Minh*.
   - Số điện thoại hỗ trợ và hệ thống icon mạng xã hội/kênh tư vấn.

---

## 🛠 Công Nghệ Sử Dụng

- **HTML / XHTML**: Cấu trúc ngữ nghĩa trang web (`index_showrom.html`).
- **CSS**: Định kiểu bố cục dạng khối, sử dụng kỹ thuật `float` và `clear` để chia cột (`index.css`).
- **Tài liệu đính kèm**: Báo cáo đồ án/bài tập cá nhân (`Trần Tăng Trường Thịnh_webshowroom.pdf`).

---

## 📂 Cấu Trúc Thư Mục

```text
showroom_web/
├── images/                               # Thư mục hình ảnh sản phẩm, slider, logo, icons
│   ├── logo.jpg                          # Logo GF Furniture
│   ├── slider_1.jpg                      # Ảnh banner lớn đầu trang
│   ├── san_pham_1.jpg -> san_pham_9.jpg  # Hình ảnh sản phẩm thiết kế và nội thất
│   └── icon_1.jpg -> icon_3.jpg          # Icon liên kết mạng xã hội
├── index_showrom.html                    # File mã nguồn HTML chính của trang
├── index.css                             # File định kiểu stylesheet CSS
├── Trần Tăng Trường Thịnh_webshowroom.pdf # Tài liệu báo cáo dự án
└── README.md
```

---

## 🚀 Hướng Dẫn Mở & Xem Trang Web

Không cần cài đặt web server phức tạp, bạn có thể xem trang web trực tiếp trên bất kỳ trình duyệt nào:

1. **Cách 1: Nhấp đúp chuột** vào file `index_showrom.html` để mở bằng trình duyệt mặc định (Chrome, Edge, Firefox, Brave,...).
2. **Cách 2: Mở nhanh qua Terminal (Windows PowerShell)**:
   ```powershell
   cd showroom_web
   Start-Process index_showrom.html
   ```
3. **Cách 3: Sử dụng Live Server trong VS Code**:
   - Cài đặt extension **Live Server**.
   - Chuột phải vào file `index_showrom.html` và chọn **Open with Live Server**.