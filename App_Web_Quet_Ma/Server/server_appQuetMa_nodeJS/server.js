const express = require("express");
const cors = require("cors");

const app = express();

app.use(cors({
  origin: "*",
  methods: ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
  allowedHeaders: ["Content-Type", "Authorization"]
}));

app.use(express.json());

app.get("/", (req, res) => {
  res.json({
    status: "online",
    message: "SPX Express Backend API is running 🚀",
    timestamp: new Date().toISOString()
  });
});

const ordersRouter = require("./routers/orders/index_Orders");
app.use('/orders', ordersRouter);

const TO_ordersRouter = require("./routers/TO_Orders/index_TO");
app.use('/TO', TO_ordersRouter);

const usersRouter = require("./routers/users/index_Users");
app.use('/', usersRouter);

app.use((req, res, next) => {
  console.log("👉", req.method, req.url);
  next();
});


// Hàm chạy ngầm định kỳ mỗi 1 giờ dọn dẹp các TO rỗng quá 1 ngày (24 giờ)
const db = require("./db");
function runPeriodicTOCleanup() {
  const sql = `
    DELETE FROM TO_orders 
    WHERE (SL = 0 OR danhSachGoiHang = '[]' OR danhSachGoiHang IS NULL)
      AND ngayTao < DATE_SUB(NOW(), INTERVAL 1 DAY)
  `;
  db.query(sql, (err, result) => {
    if (err) {
      console.error("[Scheduled Cleanup] Lỗi quét dọn TO rỗng:", err.message);
    } else if (result && result.affectedRows > 0) {
      console.log(`[Scheduled Cleanup] Đã tự động dọn dẹp ${result.affectedRows} bao TO rỗng`);
    }
  });
}
// Chạy ngay khi start server và lặp lại mỗi 1 giờ
setTimeout(runPeriodicTOCleanup, 5000);
setInterval(runPeriodicTOCleanup, 60 * 60 * 1000);

// Chạy port 5000 ở local (tránh đụng cổng 3000 của app khác) hoặc PORT do Railway cấp
const PORT = process.env.PORT || 5000;
app.listen(PORT, "0.0.0.0", () => {
  console.log(`🚀 Server SPX Express running on port ${PORT}`);
});
