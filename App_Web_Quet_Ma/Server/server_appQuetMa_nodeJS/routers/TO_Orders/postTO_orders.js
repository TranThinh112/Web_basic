const express = require('express');
const router = express.Router();
const db = require("../../db");


//////////////////////////////////////////////////////////////////////////////////// TO_orders ///////////////////////////////////////////////////////////////////////
//gửi dữ liệu TO moi khi vua tao len server 
router.post("/", (req, res) => {
  const {
    maTO,
    danhSachGoiHang,
    diaDiemGiaoHang,
    trangThai,
    packer,
    totalWeight,
    ngayTao,
    completeTime
  } = req.body;
  const safeList = Array.isArray(danhSachGoiHang)
    ? danhSachGoiHang
    : [];
  const SL = safeList.length;
  db.query(
    `INSERT INTO TO_orders 
    (maTO, danhSachGoiHang, diaDiemGiaoHang, trangThai, packer, totalWeight, SL, ngayTao, completeTime)
    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)`,
    [
      maTO,
      JSON.stringify(safeList),
      diaDiemGiaoHang,
      trangThai,
      packer,
      totalWeight,
      SL,
      ngayTao,
      completeTime
    ],
    (err, result) => {
      if (err) {
        console.log("DB ERROR:", err);
        return res.status(500).json(err);
      }
      res.json({ success: true});
    }
  );
});

//api upload TO sau khi bam compete
router.put("/:maTO", (req, res) => {
  const maTO = req.params.maTO;
  const {
    danhSachGoiHang,
    diaDiemGiaoHang,
    trangThai,
    totalWeight,
    completeTime
  } = req.body;
  const safeList = Array.isArray(danhSachGoiHang)
  ? danhSachGoiHang
  : [];

  const SL = safeList.length;
  db.query(
    `UPDATE TO_orders SET 
      danhSachGoiHang=?,
      diaDiemGiaoHang=?,
      trangThai=?,
      totalWeight=?,
      SL=?,
      completeTime=?
     WHERE maTO=?`,
    [
      JSON.stringify(safeList),
      diaDiemGiaoHang,
      trangThai,
      totalWeight,
      SL,
      completeTime,
      maTO
    ],
    (err) => {
      if (err) return res.status(500).json(err);
      res.json({ success: true });
    }
  );
});

//cap nhat trang tahi khi reopen TO
router.put("/:maTO/reopen", (req, res) => {
  const maTO = req.params.maTO;

  db.query(
    `UPDATE TO_orders 
     SET trangThai = 'Packing',
         completeTime = NULL
     WHERE maTO = ?`,
    [maTO],
    (err) => {
      if (err) return res.status(500).json(err);
      res.json({ success: true });
    }
  );
});

// Xóa thủ công bao hàng TO (Hỗ trợ nút thùng rác ở Web / App)
router.delete("/:maTO", (req, res) => {
  const maTO = req.params.maTO;

  // Giải phóng các đơn hàng nếu bao TO này đang giữ đơn
  db.query(
    "UPDATE orders SET trangThai = 'Outbound', maTO = NULL, thoiGianDongBao = NULL WHERE maTO = ?",
    [maTO],
    (errOrder) => {
      if (errOrder) console.error("[Delete TO] Lỗi reset đơn hàng:", errOrder.message);

      // Xóa bao hàng khỏi bảng TO_orders
      db.query("DELETE FROM TO_orders WHERE maTO = ?", [maTO], (errDelete, result) => {
        if (errDelete) return res.status(500).json({ error: errDelete.message });
        return res.json({ success: true, message: `Đã xóa bao ${maTO}` });
      });
    }
  );
});

module.exports = router;