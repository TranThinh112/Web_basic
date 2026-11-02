const express = require("express");
const router = express.Router();
const db = require("../../db");

///////////////////////////////// lay all TO va lay TO theo trang thai /////////////////////////
router.get("/", (req, res) => {
  const { maTO, trangThai } = req.query;

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
    if (!result || result.length === 0) return res.json(result);

    // Neu query 1 maTO cu the, dong bo danhSachGoiHang tu bang orders neu can
    if (maTO && result.length === 1) {
      const toItem = result[0];
      let currentList = [];
      try {
        currentList = typeof toItem.danhSachGoiHang === "string"
          ? JSON.parse(toItem.danhSachGoiHang)
          : (toItem.danhSachGoiHang || []);
      } catch {
        currentList = [];
      }

      // Neu danhSachGoiHang trong TO_orders rong nhung trong bang orders van con don thuoc maTO nay
      db.query(
        "SELECT id, soKi, noiNhan, thoiGianDongBao FROM orders WHERE maTO = ?",
        [maTO],
        (err2, orderRows) => {
          if (!err2 && orderRows && orderRows.length > 0) {
            const formatted = orderRows.map(o => ({
              orderId: o.id,
              soKi: Number(o.soKi) || 0,
              noiNhan: o.noiNhan,
              thoiGianScan: o.thoiGianDongBao
            }));

            // Tinh lai tong can nang chuan neu can
            const calcWeight = formatted.reduce((acc, it) => acc + it.soKi, 0);

            toItem.danhSachGoiHang = JSON.stringify(formatted);
            if (formatted.length > currentList.length) {
              toItem.SL = formatted.length;
              if (!toItem.totalWeight || toItem.totalWeight === 0) {
                toItem.totalWeight = Number(calcWeight.toFixed(2));
              }

              // Dong bo nguoc lai vao TO_orders de cac lan sau nhanh hon
              db.query(
                "UPDATE TO_orders SET danhSachGoiHang = ?, SL = ? WHERE maTO = ?",
                [toItem.danhSachGoiHang, toItem.SL, maTO],
                () => {}
              );
            }
          }
          return res.json([toItem]);
        }
      );
      return;
    }

    res.json(result);
  });
});

module.exports = router;
