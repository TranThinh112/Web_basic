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


// Chạy port 5000 ở local (tránh đụng cổng 3000 của app khác) hoặc PORT do Railway cấp
const PORT = process.env.PORT || 5000;
app.listen(PORT, "0.0.0.0", () => {
  console.log(`🚀 Server SPX Express running on port ${PORT}`);
});
