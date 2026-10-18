# Tuyển Dụng TPD - Dashboard Quản Lý Tuyển Dụng & Telegram Bot

Hệ thống bảng điều khiển (Dashboard) quản lý tuyển dụng toàn diện, hỗ trợ quản lý đơn hàng tuyển dụng, ứng viên, mạng lưới cộng tác viên (CTV) và khách hàng doanh nghiệp. Tích hợp bot Telegram tự động bóc tách thông tin ứng viên từ tin nhắn để đồng bộ dữ liệu vào hệ thống theo thời gian thực.

---

## 📸 Giao Diện Ứng Dụng (UI Screenshots)

| Bảng Điều Khiển Quản Lý Tuyển Dụng TPD |
| :---: |
| ![Bảng Điều Khiển Đơn Tuyển Dụng](screenshots/dashboard_orders.png) |

---

## 🚀 Tính Năng Chính

1. **Quản lý đơn tuyển dụng (Job Orders Management):**
   - Theo dõi toàn diện các đơn hàng tuyển dụng theo mã đơn, công ty, vị trí ứng tuyển.
   - Bộ lọc đa năng theo ngành nghề: Công nghệ thông tin, Sản xuất, Bán lẻ, Logistics, Dịch vụ...
   - Cập nhật số lượng chỉ tiêu cần tuyển, số lượng ứng viên đã nộp và hạn kết thúc tuyển dụng.
2. **Quản lý hồ sơ ứng viên (Candidate Tracking):**
   - Theo dõi chu trình phỏng vấn theo các giai đoạn (Stage Pipeline): Ứng tuyển -> Phỏng vấn -> Đạt yêu cầu -> Nhận việc -> Thử việc.
   - Lưu trữ và xem nhanh CV ứng viên với bộ nhớ đệm nhị phân (Binary Cache) tối ưu tốc độ tải.
3. **Quản lý mạng lưới Cộng tác viên (CTV / Headhunters):**
   - Phân loại nguồn ứng viên theo từng cộng tác viên.
   - Theo dõi hiệu suất tuyển dụng và tính toán chi phí hoa hồng giới thiệu.
4. **Tự động hóa thông minh với Telegram Bot (`telegram_bot.py`):**
   - Lắng nghe tin nhắn từ các nhóm chat Telegram của đội ngũ cộng tác viên.
   - Tự động bóc tách (regex parsing) các trường thông tin: Họ tên, Số điện thoại, Mã đơn ứng tuyển, Tên CTV, Ghi chú.
   - Tự động tạo mới ứng viên vào MongoDB và đồng bộ trực tiếp lên giao diện Dashboard mà không cần nhập liệu thủ công.
5. **Hiệu năng cao & Tối ưu hóa bộ nhớ đệm (In-Memory Cache):**
   - Tích hợp Response Caching và ThreadPool xử lý đồng thời, thời gian phản hồi API dưới 50ms.
   - Cơ chế tự động giải phóng dữ liệu không còn sử dụng.
6. **Sẵn sàng triển khai Đám mây (Cloud Ready):**
   - Cấu hình sẵn `Procfile`, `railway.json`, `requirements.txt` để triển khai tức thì lên Railway hoặc Heroku.

---

## 🛠️ Công Nghệ Sử Dụng

- **Backend:** Python 3.11, `ThreadingHTTPServer`, `SimpleHTTPRequestHandler`, `pymongo`.
- **Cơ sở dữ liệu:** MongoDB Atlas Cloud Database.
- **Tự động hóa:** Telegram Bot API (`telegram_bot.py`), Regular Expressions parser.
- **Frontend:** Single Page Application (SPA) HTML5, CSS3 hiện đại, Vanilla JavaScript.
- **Triển khai:** Railway Cloud Platform (`railway.json`, `Procfile`).

---

## 📂 Cấu Trúc Thư Mục

```
dashboardTKT/
├── server.py                  # Máy chủ HTTP Backend xử lý REST API & Caching
├── telegram_bot.py            # Bot Telegram tự động bóc tách tin nhắn & đồng bộ ứng viên
├── index.html                 # Giao diện Dashboard SPA chính
├── styles.css                 # Định dạng giao diện hiện đại & responsive
├── script.js                  # Logic gọi API, lọc dữ liệu và tương tác giao diện
├── screenshots/               # Hình ảnh giao diện Dashboard
│   └── dashboard_orders.png
├── requirements.txt           # Danh sách thư viện Python cần thiết
├── Procfile                   # Cấu hình tiến trình chạy trên Railway / Heroku
├── railway.json               # Cấu hình triển khai nền tảng Railway
├── DEPLOY_RAILWAY.md          # Hướng dẫn chi tiết triển khai lên Railway
└── README.md
```

---

## ⚙️ Hướng Dẫn Cài Đặt & Chạy

### 1. Cài đặt thư viện Python
```bash
pip install -r requirements.txt
```

### 2. Cấu hình biến môi trường
Tạo tệp `.env` với cấu hình:
```env
MONGODB_URI=mongodb+srv://<username>:<password>@cluster0.mongodb.net/
MONGODB_DB_NAME=recruitment_dashboard
SERVER_HOST=0.0.0.0
SERVER_PORT=5173
TELEGRAM_BOT_TOKEN=your_telegram_bot_token_here
```

### 3. Khởi chạy máy chủ Dashboard
```bash
python server.py
```
Mở trình duyệt và truy cập: `http://localhost:5173`

### 4. Khởi chạy Bot Telegram (Tự động đồng bộ)
```bash
python telegram_bot.py
```
