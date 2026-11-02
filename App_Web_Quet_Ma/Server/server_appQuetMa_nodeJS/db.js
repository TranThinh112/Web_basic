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

// Tự động khởi tạo bảng nếu chạy trên MySQL mới (như Railway Cloud)
async function autoInitDatabase(pool) {
  try {
    const promisePool = pool.promise();
    await promisePool.query(`
      CREATE TABLE IF NOT EXISTS \`orders\` (
        \`id\` varchar(50) NOT NULL,
        \`nguoiGui\` varchar(255) DEFAULT NULL,
        \`nguoiNhan\` varchar(255) DEFAULT NULL,
        \`diaChiGui\` text DEFAULT NULL,
        \`diaChiNhan\` text DEFAULT NULL,
        \`noiGui\` varchar(255) DEFAULT NULL,
        \`noiNhan\` varchar(255) DEFAULT NULL,
        \`sanPham\` varchar(500) DEFAULT NULL,
        \`soKi\` decimal(10,2) DEFAULT NULL,
        \`giaTien\` int(11) DEFAULT NULL,
        \`trangThai\` varchar(50) DEFAULT NULL,
        \`thoiGianTao\` timestamp NULL DEFAULT current_timestamp(),
        \`thoiGianDongBao\` timestamp NULL DEFAULT NULL,
        \`maTO\` varchar(255) DEFAULT NULL,
        PRIMARY KEY (\`id\`)
      ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
    `);

    await promisePool.query(`
      CREATE TABLE IF NOT EXISTS \`to_orders\` (
        \`maTO\` varchar(50) NOT NULL,
        \`danhSachGoiHang\` text DEFAULT NULL,
        \`diaDiemGiaoHang\` text DEFAULT NULL,
        \`trangThai\` text DEFAULT NULL,
        \`packer\` text DEFAULT NULL,
        \`ngayTao\` timestamp NOT NULL DEFAULT current_timestamp(),
        \`completeTime\` text DEFAULT NULL,
        \`totalWeight\` double DEFAULT 0,
        \`SL\` int(11) DEFAULT 0,
        PRIMARY KEY (\`maTO\`)
      ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
    `);

    await promisePool.query(`
      CREATE TABLE IF NOT EXISTS \`users\` (
        \`id\` int(11) NOT NULL AUTO_INCREMENT,
        \`username\` varchar(50) NOT NULL,
        \`password\` varchar(100) NOT NULL,
        \`created_at\` timestamp NULL DEFAULT current_timestamp(),
        PRIMARY KEY (\`id\`)
      ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
    `);

    const [userRows] = await promisePool.query("SELECT COUNT(*) as cnt FROM users");
    if (userRows[0].cnt === 0) {
      await promisePool.query("INSERT INTO users (username, password) VALUES ('admin', '123')");
      console.log("[Database] Created default user 'admin'");
    }

    // Tự động chuẩn hóa trạng thái các đơn NULL cũ trong database về Outbound (nếu chưa vào bao) hoặc Inbound (nếu đã vào bao)
    await promisePool.query(`
      UPDATE orders
      SET trangThai = 'Inbound'
      WHERE (trangThai IS NULL OR trangThai = '') AND maTO IS NOT NULL AND maTO != ''
    `);
    await promisePool.query(`
      UPDATE orders
      SET trangThai = 'Outbound'
      WHERE (trangThai IS NULL OR trangThai = '') AND (maTO IS NULL OR maTO = '')
    `);
    console.log("[Database] Chuẩn hóa trạng thái đơn hàng (Outbound/Inbound) hoàn tất.");
    console.log("[Database] Tables verified & ready.");
  } catch (err) {
    console.error("[Database] Auto-init tables error:", err.message);
  }
}

autoInitDatabase(db);


console.log(`[Database] Đang kết nối tới: ${isRailway ? "Railway Cloud MySQL (" + process.env.MYSQLHOST + ")" : "Localhost MySQL (appqm)"}`);

module.exports = db;
