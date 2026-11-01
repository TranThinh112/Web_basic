const mysql = require("mysql2");

// Tự động nhận cấu hình Railway (hoặc Localhost nếu không có biến môi trường)
const isRailway = !!(process.env.MYSQLHOST || process.env.MYSQL_URL || process.env.DATABASE_URL);

let poolConfig = {};

if (process.env.MYSQL_URL || process.env.DATABASE_URL) {
  // Trường hợp Railway cấp 1 chuỗi URL duy nhất
  poolConfig = {
    uri: process.env.MYSQL_URL || process.env.DATABASE_URL,
    waitForConnections: true,
    connectionLimit: 10,
    queueLimit: 0,
  };
} else if (isRailway) {
  // Trường hợp Railway cấp từng biến riêng lẻ
  poolConfig = {
    host: process.env.MYSQLHOST,
    user: process.env.MYSQLUSER,
    password: process.env.MYSQLPASSWORD,
    database: process.env.MYSQLDATABASE,
    port: Number(process.env.MYSQLPORT) || 3306,
    waitForConnections: true,
    connectionLimit: 10,
    queueLimit: 0,
  };
} else {
  // Môi trường Localhost
  poolConfig = {
    host: "localhost",
    user: "root",
    password: "",
    database: "appqm",
    port: 3306,
    waitForConnections: true,
    connectionLimit: 10,
  };
}

const db = mysql.createPool(poolConfig);

console.log(`[Database] Đang kết nối tới: ${isRailway ? "Railway Cloud MySQL (" + process.env.MYSQLHOST + ")" : "Localhost MySQL (appqm)"}`);

module.exports = db;
