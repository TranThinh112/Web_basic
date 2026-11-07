const express = require("express");
const router = express.Router();
const db = require("../../db");

// Hàm dọn dẹp các TO rỗng không có đơn hàng sau 1 ngày (24 giờ)
function cleanupEmptyTOs(callback) {
  const sql = `
    DELETE FROM TO_orders 
    WHERE (SL = 0 OR danhSachGoiHang = '[]' OR danhSachGoiHang IS NULL)
      AND ngayTao < DATE_SUB(NOW(), INTERVAL 1 DAY)
  `;
  db.query(sql, (err, result) => {
    if (err) {
      console.error("[Cleanup TO] Lỗi khi dọn dẹp TO rỗng:", err.message);
    } else if (result && result.affectedRows > 0) {
      console.log(`[Cleanup TO] Đã tự động dọn dẹp ${result.affectedRows} bao TO rỗng sau 1 ngày`);
    }
    if (callback) callback();
  });
}

///////////////////////////////// lay all TO va lay TO theo trang thai /////////////////////////
router.get("/", (req, res) => {
  const { maTO, trangThai } = req.query;

  // Tự động dọn dẹp các bao TO rỗng quá 1 ngày trước khi trả về dữ liệu danh sách
  cleanupEmptyTOs(() => {
    let sql = "SELECT * FROM TO_orders";
    let values = [];
    if (maTO) {
      sql += " WHERE maTO = ?";
      values.push(maTO);
    } else if (trangThai) {
      sql += " WHERE LOWER(trangThai) = LOWER(?)";
      values.push(trangThai);
    }

    db.query(sql, values, (err, result) => {
      if (err) return res.status(500).json({ error: err.message });
      res.json(result);
    });
  });
});

module.exports = router;