const db = require("../../db");
const express = require('express');
const router = express.Router();
const INSTANCE_ID = Math.random().toString(36).substring(2, 8);

// Quét đóng đơn vào bao TO: Kiểm tra hợp lệ & cùng nơi nhận TRƯỚC, chỉ UPDATE khi hoàn toàn hợp lệ
router.post('/scan/:id', (req, res) => {
  const { id } = req.params;
  const { maTO } = req.body;

  console.log("SCAN ID:", id, "into TO:", maTO);

  // CHECK FORMAT SPXVN (chấp nhận cả 11 chữ số hoặc 10 chữ số + ký tự A, B, C cho tháng 10, 11, 12)
  const validIdPattern = /^SPXVN\d{10}[\dA-C]$/i;
  if (!validIdPattern.test(id)) {
    return res.status(400).json({ message: "Sai định dạng" });
  }

  // Chuẩn hóa giờ theo múi giờ Việt Nam
  const now = new Date().toLocaleString("sv-SE", {
    timeZone: "Asia/Ho_Chi_Minh"
  });

  // BƯỚC 1: Lấy thông tin đơn hàng và kiểm tra xem đơn có tồn tại không
  db.query(
    "SELECT id, soKi, noiNhan, trangThai FROM orders WHERE id = ?",
    [id],
    (errOrder, orderRows) => {
      if (errOrder) return res.status(500).json(errOrder);
      if (!orderRows.length) {
        return res.status(404).json({ message: "Đơn hàng không tồn tại" });
      }

      const orderItem = orderRows[0];
      const currentTrangThai = String(orderItem.trangThai || "").toLowerCase();

      // Chỉ cho phép quét đơn đang ở trạng thái Outbound (chưa vào bao)
      if (currentTrangThai === "inbound") {
        return res.status(400).json({ message: "Đơn không hợp lệ hoặc đã scan" });
      }

      const soKi = Number(orderItem.soKi) || 0;
      const noiNhan = (orderItem.noiNhan || "").trim().toLowerCase();

      // BƯỚC 2: Lấy thông tin bao TO để kiểm tra nơi nhận & danh sách hiện tại
      db.query(
        "SELECT maTO, diaDiemGiaoHang, danhSachGoiHang FROM TO_orders WHERE maTO = ?",
        [maTO],
        (errTO, toRows) => {
          if (errTO) return res.status(500).json(errTO);
          if (!toRows.length) {
            return res.status(404).json({ message: "TO không tồn tại" });
          }

          let list = [];
          const raw = toRows[0].danhSachGoiHang;
          if (raw && raw.length > 5) {
            try {
              const parsed = typeof raw === "string" ? JSON.parse(raw) : raw;
              list = Array.isArray(parsed) ? parsed : [];
            } catch {
              list = [];
            }
          }

          // Kiểm tra xem đơn đã nằm trong bao này chưa
          const isExistedInTO = list.some(item => item.orderId === id);
          if (isExistedInTO) {
            return res.json({
              success: false,
              message: "Đơn đã Scan"
            });
          }

          // BƯỚC 3: Kiểm tra tính hợp lệ về NƠI NHẬN (chỉ cho phép vào bao nếu khớp nơi đến)
          const isFirstOrder = list.length === 0;
          if (!isFirstOrder) {
            const currentDestInTO = (list[0]?.noiNhan || toRows[0].diaDiemGiaoHang || "").trim().toLowerCase();
            if (currentDestInTO && currentDestInTO !== noiNhan) {
              // KHÁC NƠI NHẬN: DỪNG LẠI NGAY, tuyệt đối không chỉnh sửa gì trong Database!
              return res.status(400).json({
                message: "Khác nơi nhận"
              });
            }
          }

          // BƯỚC 4: Tất cả điều kiện đã thỏa mãn -> Thêm đơn vào danh sách bao TO
          list.push({
            orderId: id,
            soKi: soKi,
            noiNhan: noiNhan,
            thoiGianScan: now
          });

          // Cập nhật bao TO (TO_orders)
          db.query(
            `UPDATE TO_orders 
             SET danhSachGoiHang = ?, 
                 totalWeight = totalWeight + ?,
                 SL = SL + 1,
                 diaDiemGiaoHang = CASE 
                   WHEN ? THEN ? 
                   ELSE diaDiemGiaoHang 
                 END
             WHERE maTO = ?`,
            [
              JSON.stringify(list),
              soKi,
              isFirstOrder,
              orderItem.noiNhan,
              maTO
            ],
            (errUpdateTO) => {
              if (errUpdateTO) return res.status(500).json(errUpdateTO);

              // BƯỚC 5: Cập nhật trạng thái đơn hàng sang Inbound và gắn mã TO
              db.query(
                `UPDATE orders 
                 SET trangThai = 'Inbound', maTO = ?, thoiGianDongBao = ?
                 WHERE id = ?`,
                [maTO, now, id],
                (errUpdateOrder) => {
                  if (errUpdateOrder) return res.status(500).json(errUpdateOrder);

                  // Phản hồi thành công
                  return res.json({
                    success: true,
                    instance: INSTANCE_ID,
                    addedWeight: soKi,
                    order: {
                      id: id,
                      soKi: soKi,
                      noiNhan: orderItem.noiNhan
                    }
                  });
                }
              );
            }
          );
        }
      );
    }
  );
});

//xoa don khoi TO (trangThai = 'Outbound' + maTO = null + thoiGianDongBao = null)
router.post('/remove/:id', (req, res) => {
  const { id } = req.params;

  //  GỘP: lấy maTO + đảm bảo đúng trạng thái
  db.query(
    "SELECT maTO FROM orders WHERE id = ? AND maTO IS NOT NULL",
    [id],
    (err, rows) => {
      if (err) return res.status(500).json(err);
      if (!rows.length)
        return res.status(400).json({ message: "Đơn không thuộc TO" });

      const maTO = rows[0].maTO;

      // 2. LẤY TO (bỏ TRIM → dùng index)
      db.query(
        "SELECT danhSachGoiHang FROM TO_orders WHERE maTO = ?",
        [maTO],
        (err2, rows2) => {
          if (err2) return res.status(500).json(err2);
          if (!rows2.length)
            return res.status(404).json({ message: "TO không tồn tại" });

          let list = [];
          let raw = rows2[0].danhSachGoiHang;

          // parse nhanh + an toàn
          if (raw && raw.length > 5) {
            try {
              if (Buffer.isBuffer(raw)) raw = raw.toString();
              list = JSON.parse(raw);
              if (!Array.isArray(list)) list = [];
            } catch {
              list = [];
            }
          }

          //  tìm index (giữ nguyên O(n) vì cần index)
          const index = list.findIndex(item => item.orderId === id);

          if (index === -1) {
            return res.status(400).json({
              message: "Đơn không nằm trong TO"
            });
          }

          const isFirstOrder = index === 0;
          const removed = list[index];
          const removedWeight = removed?.soKi || 0;

          //  xoá trực tiếp (không filter lại lần 2)
          list.splice(index, 1);

          // ===== UPDATE =====
          function updateTO(newDiaDiem) {
            //  tính nhanh hơn (tránh reduce nặng nếu list lớn)
            let totalWeight = 0;
            for (let i = 0; i < list.length; i++) {
              totalWeight += list[i].soKi || 0;
            }

            const SL = list.length;

            let sql = `
              UPDATE TO_orders 
              SET danhSachGoiHang = ?, 
                  totalWeight = ?,
                  SL = ?`;

            let params = [JSON.stringify(list), totalWeight, SL];

            if (newDiaDiem !== undefined) {
              sql += ", diaDiemGiaoHang = ?";
              params.push(newDiaDiem);
            }

            sql += " WHERE maTO = ?";
            params.push(maTO);

            db.query(sql, params, (err4) => {
              if (err4) return res.status(500).json(err4);

              //  update order (nhanh, không check lại)
              db.query(
                `UPDATE orders 
                 SET trangThai = 'Outbound', maTO = NULL, thoiGianDongBao = NULL
                 WHERE id = ?`,
                [id],
                (err5) => {
                  if (err5) return res.status(500).json(err5);

                  res.json({
                    success: true,
                    removedWeight
                    //  bỏ list → giảm response
                  });
                }
              );
            });
          }

          //  xử lý first order (KHÔNG QUERY nếu có sẵn)
          if (isFirstOrder) {
            if (list.length === 0) {
              return updateTO(null);
            } else {
              //  dùng luôn noiNhan nếu có
              const newDiaDiem = list[0]?.noiNhan;

              if (newDiaDiem) {
                return updateTO(newDiaDiem);
              }

              // fallback mới query
              const newFirstOrderId = list[0].orderId;

              return db.query(
                "SELECT noiNhan FROM orders WHERE id = ?",
                [newFirstOrderId],
                (err3, result3) => {
                  if (err3) return res.status(500).json(err3);

                  updateTO(result3[0]?.noiNhan || null);
                }
              );
            }
          } else {
            return updateTO(undefined);
          }
        }
      );
    }
  );
});
// Hàm sinh mã SPX đúng chuẩn SPXVN{yearCode}{8 số ngẫu nhiên}{monthCode}
function generateUniqueSPXCode() {
  const now = new Date();
  const year = now.getFullYear();
  const yearCodeNum = year % 10 === 0 ? year % 100 : year % 10;
  const yearCode = String(yearCodeNum).padStart(2, "0");

  const month = now.getMonth() + 1;
  let monthCode = String(month);
  if (month === 10) monthCode = "A";
  if (month === 11) monthCode = "B";
  if (month === 12) monthCode = "C";

  let rand = "";
  for (let i = 0; i < 8; i++) {
    rand += Math.floor(Math.random() * 10).toString();
  }
  return 'SPXVN' + yearCode + rand + monthCode;
}

// Hàm insert đơn hàng có cơ chế auto-retry chống trùng mã id
function insertOrderWithRetry(orderData, maxRetries, callback) {
  let attempts = 0;

  function tryInsert(currentId) {
    attempts++;
    const sql = 'INSERT INTO orders(id, nguoiGui, nguoiNhan, diaChiGui, diaChiNhan, noiGui, noiNhan, sanPham, soKi, giaTien, trangThai, thoiGianTao) VALUES(?,?,?,?,?,?,?,?,?,?,?,?)';
    const params = [
      currentId,
      orderData.nguoiGui,
      orderData.nguoiNhan,
      orderData.diaChiGui,
      orderData.diaChiNhan,
      orderData.noiGui,
      orderData.noiNhan,
      orderData.sanPham,
      orderData.soKi,
      orderData.giaTien,
      orderData.trangThai,
      orderData.thoiGianTao
    ];

    db.query(sql, params, (err, result) => {
      if (err) {
        // Nếu trùng khóa chính (duplicate key) và chưa vượt quá số lần thử lại
        if ((err.code === 'ER_DUP_ENTRY' || err.errno === 1062) && attempts < maxRetries) {
          const nextId = generateUniqueSPXCode();
          console.warn('[Auto-Retry] Trùng mã ' + currentId + ', tự động sinh mã mới: ' + nextId + ' (thử lần ' + attempts + ')');
          return tryInsert(nextId);
        }
        return callback(err);
      }
      return callback(null, { ...orderData, id: currentId });
    });
  }

  const initialId = (orderData.id && String(orderData.id).trim() !== "")
    ? String(orderData.id).trim()
    : generateUniqueSPXCode();

  tryInsert(initialId);
}

//tao order moi tu form (co co che tu dong check trung & retry 5 lan)
router.post("/", (req, res) => {
  const { id, nguoiGui, nguoiNhan, diaChiGui, diaChiNhan, noiGui, noiNhan, sanPham, soKi, giaTien, trangThai } = req.body;
  const thoiGianTao = new Date();
  const status = trangThai || 'Outbound';

  const orderData = {
    id,
    nguoiGui,
    nguoiNhan,
    diaChiGui,
    diaChiNhan,
    noiGui,
    noiNhan,
    sanPham,
    soKi,
    giaTien,
    trangThai: status,
    thoiGianTao,
  };

  insertOrderWithRetry(orderData, 5, (err, savedOrder) => {
    if (err) {
      console.log("DB error:", err);
      return res.status(500).json({ error: err.message });
    }
    return res.json(savedOrder);
  });
});

module.exports = router;
