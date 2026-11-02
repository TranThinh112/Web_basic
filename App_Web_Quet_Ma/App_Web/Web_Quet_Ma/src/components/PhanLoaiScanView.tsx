"use client";

import { useState, useEffect, useRef } from "react";
import {
  Package,
  Scan,
  CheckCircle2,
  AlertCircle,
  Camera,
  X,
  Printer,
  Zap,
  RefreshCw,
  Clock,
  Weight,
  Layers,
} from "lucide-react";
import { api } from "@/lib/api";
import { sounds } from "@/lib/sound";
import { TOOrder } from "@/types";
import BarcodeScanner from "@/components/BarcodeScanner";
import BillModal from "@/components/BillModal";

interface ScannedItem {
  orderId: string;
  soKi: number;
  noiNhan?: string;
  thoiGianScan?: string;
}

export default function PhanLoaiScanView({ initialMaTO }: { initialMaTO?: string }) {
  const [currentTO, setCurrentTO] = useState<TOOrder | null>(null);

  const [barcodeInput, setBarcodeInput] = useState("");
  const [scanMessage, setScanMessage] = useState<{
    text: string;
    type: "success" | "error" | "info";
  } | null>(null);
  const [loadingScan, setLoadingScan] = useState(false);
  const [showCamera, setShowCamera] = useState(false);
  const [printTO, setPrintTO] = useState<TOOrder | null>(null);

  const inputRef = useRef<HTMLInputElement>(null);

  // Sinh mã TO: TO + yyMMdd + 5 ký tự ngẫu nhiên
  const generateTOId = () => {
    const d = new Date();
    const yy = String(d.getFullYear()).slice(-2);
    const mm = String(d.getMonth() + 1).padStart(2, "0");
    const dd = String(d.getDate()).padStart(2, "0");
    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    let rand = "";
    for (let i = 0; i < 5; i++) {
      rand += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return `TO${yy}${mm}${dd}${rand}`;
  };

  // Khởi tạo bao TO mới với trạng thái ban đầu là Packing
  const handleKhoiTaoTO = async (silent: boolean = false) => {
    const newMaTO = generateTOId();
    const userStr =
      typeof window !== "undefined" ? localStorage.getItem("spx_user") : null;
    let packerName = "admin";
    if (userStr) {
      try {
        packerName = JSON.parse(userStr).username || packerName;
      } catch {}
    }

    const newTO: TOOrder = {
      maTO: newMaTO,
      danhSachGoiHang: [],
      diaDiemGiaoHang: "",
      trangThai: "Packing", // Trạng thái Packing khi đang đóng hàng
      packer: packerName,
      ngayTao: new Date().toISOString(),
      completeTime: null,
      totalWeight: 0,
    };

    await api.createTO(newTO);
    setCurrentTO(newTO);
    setScanMessage({
      text: `Đã mở bao mới [${newMaTO}]. Nhập hoặc scan đơn hàng để gán Hub đích và chuyển trạng thái Packing!`,
      type: "info",
    });

    if (!silent) {
      sounds.playSuccess();
    }
    setTimeout(() => inputRef.current?.focus(), 100);
  };

  useEffect(() => {
    if (initialMaTO) {
      // Nếu có mã TO từ router params (VD: /TOmanagement/sorting/TO261102M3CIE)
      api.getOneTO(initialMaTO).then((found) => {
        if (found) {
          setCurrentTO({
            ...found,
            trangThai: found.trangThai === "Complete" || found.trangThai === "Packed" ? "Packing" : found.trangThai,
          });
          setScanMessage({
            text: `Đang mở lại bao hàng [${initialMaTO}] để quét tiếp.`,
            type: "info",
          });
        } else {
          handleKhoiTaoTO(true);
        }
      }).catch(() => {
        handleKhoiTaoTO(true);
      });
    } else {
      handleKhoiTaoTO(true);
    }
  }, [initialMaTO]);

  useEffect(() => {
    inputRef.current?.focus();
  }, [currentTO]);

  // Luôn bắt phím quét mã vào input
  useEffect(() => {
    const handleGlobalKeyDown = (e: KeyboardEvent) => {
      if (
        document.activeElement?.tagName !== "INPUT" &&
        document.activeElement?.tagName !== "SELECT"
      ) {
        inputRef.current?.focus();
      }
    };
    window.addEventListener("keydown", handleGlobalKeyDown);
    return () => window.removeEventListener("keydown", handleGlobalKeyDown);
  }, []);

  // Format thời gian hiển thị gọn gàng
  const formatTime = (isoString?: string) => {
    if (!isoString) {
      const now = new Date();
      return `${String(now.getHours()).padStart(2, "0")}:${String(
        now.getMinutes()
      ).padStart(2, "0")}:${String(now.getSeconds()).padStart(2, "0")}`;
    }
    try {
      const d = new Date(isoString);
      if (isNaN(d.getTime())) return isoString;
      return `${String(d.getHours()).padStart(2, "0")}:${String(
        d.getMinutes()
      ).padStart(2, "0")}:${String(d.getSeconds()).padStart(2, "0")}`;
    } catch {
      return isoString;
    }
  };

  // Chuẩn hóa danh sách kiện hàng
  const getScannedList = (): ScannedItem[] => {
    if (!currentTO) return [];
    const raw = currentTO.danhSachGoiHang;
    const arr = Array.isArray(raw)
      ? raw
      : JSON.parse(typeof raw === "string" ? raw : "[]");

    return arr.map((it: any) => {
      if (typeof it === "string") {
        return {
          orderId: it,
          soKi: 0.5,
          noiNhan: currentTO.diaDiemGiaoHang || "SOC",
          thoiGianScan: currentTO.ngayTao,
        };
      }
      return {
        orderId: it.orderId || it.id || "---",
        soKi: Number(it.soKi ?? it.weight ?? 0.5),
        noiNhan: it.noiNhan || currentTO.diaDiemGiaoHang || "SOC",
        thoiGianScan: it.thoiGianScan || it.time || currentTO.ngayTao,
      };
    });
  };

  // Quét / nhập đơn vào bao TO (cập nhật DB và trạng thái Packing ngay lập tức)
  const handleProcessScan = async (code: string) => {
    const cleanCode = code.trim().toUpperCase();
    if (!cleanCode) return;

    if (!currentTO) {
      await handleKhoiTaoTO(true);
    }

    const activeTO = currentTO;
    if (!activeTO) return;

    const currentList = getScannedList();

    // Check trùng mã trong bao
    const isDuplicate = currentList.some((item) => item.orderId === cleanCode);
    if (isDuplicate) {
      setScanMessage({
        text: `Đơn [${cleanCode}] đã có trong bao này! Vui lòng không quét lại.`,
        type: "error",
      });
      sounds.playError();
      setBarcodeInput("");
      return;
    }

    setLoadingScan(true);
    try {
      // Gọi API scan: Backend sẽ cập nhật ngay lập tức vào database:
      // - Đơn hàng: trangThai = 'Inbound', maTO = activeTO.maTO, thoiGianDongBao = now
      // - Bao hàng (TO): thêm đơn vào danhSachGoiHang, tăng SL, cộng weight, trangThai = 'Packing'
      const res = await api.scanOrder(cleanCode, activeTO.maTO);

      const currentTimeStr = new Date().toISOString();

      if (res.success) {
        // Cập nhật state tức thì
        const orderWeight = Number(res.order?.soKi || 0.5);
        const orderDest = res.order?.noiNhan || activeTO.diaDiemGiaoHang || "SOC";

        const newItem: ScannedItem = {
          orderId: cleanCode,
          soKi: orderWeight,
          noiNhan: orderDest,
          thoiGianScan: currentTimeStr,
        };

        const updatedList = [newItem, ...currentList];
        const newTotalWeight = Number(
          (Number(activeTO.totalWeight || 0) + orderWeight).toFixed(2)
        );

        const updatedTOState: TOOrder = {
          ...activeTO,
          trangThai: "Packing",
          diaDiemGiaoHang: activeTO.diaDiemGiaoHang || orderDest,
          danhSachGoiHang: updatedList,
          totalWeight: newTotalWeight,
        };

        setCurrentTO(updatedTOState);

        // Đảm bảo đồng bộ trạng thái Packing về server TO nếu chưa cập nhật
        if (activeTO.trangThai !== "Packing") {
          api.updateTO(updatedTOState).catch(() => {});
        }

        sounds.playSuccess();
        setScanMessage({
          text: `Đã quét đơn [${cleanCode}] vào bao. Trạng thái: Packing (${orderDest} - ${orderWeight}kg)`,
          type: "success",
        });

        // Tải lại TO đầy đủ từ server ngầm để đồng bộ nhất quán
        api.getOneTO(activeTO.maTO).then((serverTO) => {
          if (serverTO) {
            setCurrentTO((prev) => ({
              ...serverTO,
          trangThai: "Packing",
            }));
          }
        });
      } else {
        sounds.playError();
        setScanMessage({
          text: res.message || "Lỗi quét đơn hoặc đơn khác Hub nơi nhận!",
          type: "error",
        });
      }
    } catch {
      sounds.playError();
      setScanMessage({ text: "Lỗi kết nối khi quét đơn", type: "error" });
    } finally {
      setLoadingScan(false);
      setBarcodeInput("");
      inputRef.current?.focus();
    }
  };

  // Xóa đơn khỏi TO (gọi API remove để khôi phục trạng thái đơn trong database)
  const handleRemoveItem = async (pkgCode: string) => {
    if (!currentTO) return;

    if (!confirm(`Bạn có chắc muốn xóa mã đơn [${pkgCode}] khỏi bao này?`)) {
      return;
    }

    try {
      // Gọi API gỡ đơn khỏi TO trong database
      await api.removeOrderFromTO(pkgCode);

      // Cập nhật UI ngay lập tức
      const list = getScannedList();
      const removedItem = list.find((it) => it.orderId === pkgCode);
      const filtered = list.filter((it) => it.orderId !== pkgCode);
      const newWeight = Math.max(
        0,
        Number(
          (
            Number(currentTO.totalWeight || 0) - (removedItem?.soKi || 0)
          ).toFixed(2)
        )
      );

      const nextTO: TOOrder = {
        ...currentTO,
        danhSachGoiHang: filtered,
        totalWeight: newWeight,
        diaDiemGiaoHang: filtered.length === 0 ? "" : currentTO.diaDiemGiaoHang,
      };

      setCurrentTO(nextTO);
      sounds.playSuccess();
      setScanMessage({
        text: `Đã xóa đơn [${pkgCode}] khỏi bao hàng`,
        type: "info",
      });

      // Tải lại dữ liệu chuẩn từ server
      const updated = await api.getOneTO(currentTO.maTO);
      if (updated) {
        setCurrentTO(updated);
      }
    } catch {
      sounds.playError();
      setScanMessage({ text: "Lỗi khi xóa đơn khỏi bao", type: "error" });
    } finally {
      inputRef.current?.focus();
    }
  };

  // Đóng bao & Niêm phong
  const handleCompleteTO = async () => {
    if (!currentTO) return;
    const list = getScannedList();

    if (list.length === 0) {
      alert(
        "Bao hàng chưa có kiện nào! Vui lòng quét ít nhất 1 kiện trước khi đóng bao."
      );
      return;
    }

    const completedTO: TOOrder = {
      ...currentTO,
      trangThai: "Complete",
      completeTime: new Date().toISOString(),
    };

    await api.updateTO(completedTO);
    setCurrentTO(completedTO);
    setPrintTO(completedTO);
    sounds.playSuccess();
  };

  const listItems = getScannedList();

  return (
    <div className="space-y-4">
      {/* KHỐI TRÊN: MỤC QUÉT MÃ (NHẬP TAY + SCAN MÃ) VÀ THÔNG TIN BAO */}
      <div className="bg-white p-5 rounded-2xl border border-orange-100 shadow-sm space-y-4">
        {/* Header thông tin bao */}
        <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3 pb-3 border-b border-orange-100">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-xl bg-orange-600 flex items-center justify-center text-white shadow-sm shadow-orange-600/30">
              <Scan className="w-5 h-5" />
            </div>
            <div>
              <div className="flex items-center gap-2">
                <span className="font-mono font-bold text-base text-slate-900">
                  {currentTO?.maTO || "---"}
                </span>
                <span className={`px-2.5 py-0.5 rounded-full text-[11px] font-bold border ${currentTO?.trangThai === "Complete" ? "bg-emerald-50 text-emerald-700 border-emerald-200" : "bg-rose-50 text-rose-700 border-rose-200 animate-pulse"}`}>
                  {currentTO?.trangThai === "Complete"
                    ? "Packed"
                    : "Packing"}
                </span>
              </div>
              <p className="text-xs text-slate-500 mt-0.5">
                Điểm đến (Sorting):{" "}
                <b className="text-slate-900">
                  {currentTO?.diaDiemGiaoHang || "Chờ quét đơn đầu tiên"}
                </b>{" "}
                | Nhân viên:{" "}
                <span className="text-slate-700 font-medium">
                  {currentTO?.packer || "admin"}
                </span>
              </p>
            </div>
          </div>

          {/* Nút thao tác nhanh */}
          <div className="flex items-center gap-2">
            <button
              onClick={() => handleKhoiTaoTO(false)}
              className="px-3.5 py-2 bg-orange-50 hover:bg-orange-100 text-orange-700 rounded-xl border border-orange-200 text-xs font-bold transition flex items-center gap-1.5"
            >
              <RefreshCw className="w-3.5 h-3.5" /> Mở bao mới
            </button>

            <button
              onClick={() => setShowCamera(!showCamera)}
              className={`px-3.5 py-2 rounded-xl border text-xs font-bold transition flex items-center gap-1.5 ${
                showCamera
                  ? "bg-orange-600 text-white border-orange-600 shadow-sm"
                  : "bg-white text-slate-700 border-orange-200 hover:bg-orange-50"
              }`}
            >
              <Camera className="w-4 h-4 text-orange-600" />
              <span>{showCamera ? "Tắt camera" : "Bật camera scan"}</span>
            </button>
          </div>
        </div>

        {/* Camera Scanner nếu bật */}
        {showCamera && (
          <div className="p-3 bg-orange-50/50 rounded-2xl border border-orange-200">
            <BarcodeScanner
              active={showCamera}
              onScan={(code) => handleProcessScan(code)}
            />
          </div>
        )}

        {/* Ô NHẬP LIỆU: BẮN SÚNG SCAN HOẶC NHẬP TAY */}
        <div className="space-y-2">
          <label className="block text-xs font-bold uppercase tracking-wider text-orange-700 flex items-center justify-between">
            <span className="flex items-center gap-1.5">
              <Zap className="w-4 h-4 fill-orange-600 text-orange-600" />
              Khu vực quét mã (Hỗ trợ súng quét mã vạch & nhập tay)
            </span>
            <span className="text-[11px] font-medium text-emerald-700 bg-emerald-50 px-2 py-0.5 rounded-full border border-emerald-200 flex items-center gap-1">
              <span className="w-1.5 h-1.5 rounded-full bg-emerald-500 animate-ping" />
              Sẵn sàng nhận mã
            </span>
          </label>

          <div className="relative">
            <input
              ref={inputRef}
              type="text"
              value={barcodeInput}
              disabled={!currentTO || currentTO.trangThai === "Complete"}
              onChange={(e) => setBarcodeInput(e.target.value)}
              onKeyDown={(e) => {
                if (e.key === "Enter") {
                  handleProcessScan(barcodeInput);
                }
              }}
              placeholder="🔫 Cầm súng quét bưu kiện bắn vào đây hoặc nhập mã (VD: SPXVN... rồi Enter)"
              className="w-full pl-4 pr-28 py-3.5 bg-orange-50/40 border-2 border-orange-300 focus:border-orange-600 focus:ring-4 focus:ring-orange-100 rounded-xl text-base font-mono font-bold text-slate-900 outline-none transition placeholder:text-slate-400 placeholder:font-normal placeholder:text-xs"
            />

            <button
              disabled={
                !currentTO ||
                !barcodeInput.trim() ||
                loadingScan ||
                currentTO.trangThai === "Complete"
              }
              onClick={() => handleProcessScan(barcodeInput)}
              className="absolute right-2 top-2 bottom-2 px-5 bg-orange-600 hover:bg-orange-700 disabled:bg-slate-200 text-white rounded-lg text-xs font-bold transition active:scale-95 shadow-sm shadow-orange-600/30"
            >
              {loadingScan ? "Đang nạp..." : "Nhập / Scan"}
            </button>
          </div>


        </div>

        {/* Thông báo quét */}
        {scanMessage && (
          <div
            className={`p-3 rounded-xl border text-xs font-semibold flex items-center justify-between gap-2 ${
              scanMessage.type === "success"
                ? "bg-emerald-50 text-emerald-800 border-emerald-200"
                : scanMessage.type === "error"
                ? "bg-rose-50 text-rose-800 border-rose-200"
                : "bg-blue-50 text-blue-800 border-blue-200"
            }`}
          >
            <div className="flex items-center gap-2">
              {scanMessage.type === "success" ? (
                <CheckCircle2 className="w-4 h-4 text-emerald-600 shrink-0" />
              ) : (
                <AlertCircle className="w-4 h-4 text-rose-600 shrink-0" />
              )}
              <span>{scanMessage.text}</span>
            </div>
            <button
              onClick={() => setScanMessage(null)}
              className="text-slate-400 hover:text-slate-600 p-0.5"
            >
              <X className="w-3.5 h-3.5" />
            </button>
          </div>
        )}
      </div>

      {/* KHỐI DƯỚI: BẢNG DANH SÁCH KIỆN TRONG TO VÀ CÁC CHỈ SỐ */}
      <div className="bg-white rounded-2xl border border-orange-100 shadow-sm overflow-hidden space-y-0">
        {/* Thanh tiêu đề bảng + Thống kê */}
        <div className="p-4 bg-orange-50/40 border-b border-orange-100 flex flex-wrap items-center justify-between gap-3">
          <div className="flex items-center gap-4">
            <h3 className="font-bold text-sm text-slate-900 flex items-center gap-2">
              <Package className="w-4 h-4 text-orange-600" />
              Danh sách kiện hàng trong bao (TO)
            </h3>
            <span className="px-2.5 py-0.5 rounded-full text-xs font-bold bg-orange-100 text-orange-800">
              {listItems.length} kiện
            </span>
          </div>

          <div className="flex items-center gap-4 text-xs">
            <div className="flex items-center gap-1.5 font-bold text-slate-700 bg-white px-3 py-1.5 rounded-xl border border-orange-200 shadow-xs">
              <Weight className="w-4 h-4 text-orange-600" />
              <span>
                Tổng khối lượng:{" "}
                <b className="text-orange-600 text-sm">
                  {Number(currentTO?.totalWeight || 0).toFixed(2)}
                </b>{" "}
                kg
              </span>
            </div>

            {currentTO?.trangThai === "Packing" ? (
              <button
                onClick={handleCompleteTO}
                className="px-4 py-2 bg-orange-600 hover:bg-orange-700 text-white rounded-xl font-bold transition shadow-sm shadow-orange-600/30 flex items-center gap-1.5"
              >
                <Package className="w-4 h-4" /> Đóng bao & Niêm phong
              </button>
            ) : (
              <button
                onClick={() => setPrintTO(currentTO)}
                className="px-4 py-2 bg-white hover:bg-orange-50 text-orange-600 border border-orange-300 rounded-xl font-bold transition flex items-center gap-1.5 shadow-xs"
              >
                <Printer className="w-4 h-4" /> In Bill TO
              </button>
            )}
          </div>
        </div>

        {/* Bảng dữ liệu theo đúng các cột yêu cầu */}
        <div className="overflow-x-auto">
          <table className="w-full text-left text-xs">
            <thead className="bg-slate-50 border-b border-slate-200 text-slate-600 uppercase font-bold text-[11px] tracking-wider">
              <tr>
                <th className="px-4 py-3 text-center w-12">#</th>
                <th className="px-4 py-3">Mã đơn</th>
                <th className="px-4 py-3">Sorting (Nơi đến)</th>
                <th className="px-4 py-3 text-center">Khối lượng</th>
                <th className="px-4 py-3">Thời gian scan vào TO</th>
                <th className="px-4 py-3 text-center w-16">Xóa</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100">
              {listItems.length === 0 ? (
                <tr>
                  <td colSpan={6} className="text-center py-12 text-slate-400">
                    <Package className="w-10 h-10 mx-auto text-orange-300 mb-2 stroke-[1.5]" />
                    <p className="font-semibold text-slate-600 text-sm">
                      Bao hàng chưa có kiện nào
                    </p>
                    <p className="text-xs text-slate-400 mt-1">
                      Hãy scan mã đơn hàng ở ô phía trên để bắt đầu đóng bao
                    </p>
                  </td>
                </tr>
              ) : (
                listItems.map((item, idx) => (
                  <tr
                    key={item.orderId}
                    className="hover:bg-orange-50/40 transition group"
                  >
                    {/* STT */}
                    <td className="px-4 py-3 text-center font-bold text-slate-400">
                      {idx + 1}
                    </td>

                    {/* Mã đơn */}
                    <td className="px-4 py-3">
                      <span className="font-mono font-bold text-orange-600 text-sm bg-orange-50 px-2 py-0.5 rounded border border-orange-200">
                        {item.orderId}
                      </span>
                    </td>

                    {/* Sorting (Hub đích) */}
                    <td className="px-4 py-3">
                      <span className="font-semibold text-slate-800 bg-slate-100 px-2 py-0.5 rounded text-[11px] border border-slate-200">
                        {item.noiNhan || currentTO?.diaDiemGiaoHang || "SOC"}
                      </span>
                    </td>

                    {/* Khối lượng */}
                    <td className="px-4 py-3 text-center">
                      <span className="font-bold text-slate-900">
                        {Number(item.soKi).toFixed(2)}{" "}
                        <span className="text-slate-400 font-normal">kg</span>
                      </span>
                    </td>

                    {/* Thời gian scan vào TO */}
                    <td className="px-4 py-3 text-slate-600 font-mono text-[11px] flex items-center gap-1.5 pt-4">
                      <Clock className="w-3.5 h-3.5 text-slate-400" />
                      <span>{formatTime(item.thoiGianScan)}</span>
                    </td>

                    {/* Dấu x cuối cùng để xóa đơn khỏi TO */}
                    <td className="px-4 py-3 text-center">
                      <button
                        onClick={() => handleRemoveItem(item.orderId)}
                        title="Xóa đơn khỏi bao hàng"
                        className="w-7 h-7 inline-flex items-center justify-center rounded-lg text-slate-400 hover:text-white hover:bg-rose-500 border border-slate-200 hover:border-rose-500 transition active:scale-95"
                      >
                        <X className="w-4 h-4" />
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>

      {/* Modal in phiếu Bill TO khi đóng bao hoàn tất */}
      {printTO && <BillModal to={printTO} onClose={() => setPrintTO(null)} />}
    </div>
  );
}





