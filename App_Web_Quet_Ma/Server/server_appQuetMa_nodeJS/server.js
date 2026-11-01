const express = require("express");
const cors = require("cors");

const app = express();

// Cho phép CORS từ mọi domain (để Web Frontend gọi được vào)
app.use(cors({
  origin: "*",
  methods: ["GET", "POST", "PUT", "DELETE", "OPTIONS"],
  allowedHeaders: ["Content-Type", "Authorization"]
}));

app.use(express.json());

// Endpoint kiểm tra server online
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

// Port linh hoạt: Railway tự cấp process.env.PORT
const PORT = process.env.PORT || 3000;
app.listen(PORT, "0.0.0.0", () => {
  console.log(`🚀 Server SPX Express running on port ${PORT}`);
});
