"use client";

import { useState, useEffect, useRef } from "react";
import { Package, Scan, CheckCircle2, AlertCircle, Camera, Trash2, Printer, Zap } from "lucide-react";
import { api } from "@/lib/api";
import { sounds } from "@/lib/sound";
import { TOOrder } from "@/types";
import BarcodeScanner from "@/components/BarcodeScanner";
import BillModal from "@/components/BillModal";

export default function PhanLoaiScanView() {
  const [maTO, setMaTO] = useState("");
  const [hubDich, setHubDich] = useState("Hub Hà Nội");
  const [packer, setPacker] = useState("Staff_01");
  const [currentTO, setCurrentTO] = useState<TOOrder | null>(null);

  const [barcodeInput, setBarcodeInput] = useState("");
  const [scanMessage, setScanMessage] = useState<{ text: string; type: "success" | "error" | "info" } | null>(null);
  const [loadingScan, setLoadingScan] = useState(false);
  const [showCamera, setShowCamera] = useState(false);
  const [printTO, setPrintTO] = useState<TOOrder | null>(null);

  const inputRef = useRef<HTMLInputElement>(null);

  // Tự động focus liên tục vào ô quét để sẵn sàng nhận tín hiệu súng bắn
  useEffect(() => {
    inputRef.current?.focus();
  }, [currentTO]);

  // Lắng nghe phím toàn trang: Nếu đang ở màn hình này mà bấm súng quét, tự động focus vào ô quét
  useEffect(() => {
    const handleGlobalKeyDown = (e: KeyboardEvent) => {
      // Nếu không phải đang gõ ở các input khác thì luôn chuyển focus về ô quét mã
      if (document.activeElement?.tagName !== "INPUT" && document.activeElement?.tagName !== "SELECT") {
        inputRef.current?.focus();
      }
    };
    window.addEventListener("keydown", handleGlobalKeyDown);
    return () => window.removeEventListener("keydown", handleGlobalKeyDown);
  }, []);

  const handleKhoiTaoTO = async () => {
    const randomCode = "TO_" + Math.floor(100000 + Math.random() * 900000);
    const newTO: TOOrder = {
      maTO: randomCode,
      danhSachGoiHang: [],
      diaDiemGiaoHang: hubDich,
      trangThai: "Packing",
      packer: packer,
      ngayTao: new Date().toISOString(),
      completeTime: null,
      totalWeight: 0,
    };

    await api.createTO(newTO);
    setCurrentTO(newTO);
    setMaTO(randomCode);
    setScanMessage({ text: `Đã mở bao mới: ${randomCode} (${hubDich})`, type: "success" });
    sounds.playSuccess();
    setTimeout(() => inputRef.current?.focus(), 100);
  };

  const handleProcessScan = async (code: string) => {
    const cleanCode = code.trim().toUpperCase();
    if (!cleanCode) return;
    if (!currentTO) {
      setScanMessage({ text: "Vui lòng bấm 'Khởi tạo bao mới' trước khi quét kiện hàng", type: "error" });
      sounds.playError();
      return;
    }

    const currentList: string[] = Array.isArray(currentTO.danhSachGoiHang)
      ? currentTO.danhSachGoiHang
      : JSON.parse(typeof currentTO.danhSachGoiHang === "string" ? currentTO.danhSachGoiHang : "[]");

    if (currentList.includes(cleanCode)) {
      setScanMessage({ text: `Mã [${cleanCode}] đã quét vào bao này rồi!`, type: "error" });
      sounds.playError();
      setBarcodeInput("");
      return;
    }

    setLoadingScan(true);
    try {
      const res = await api.scanOrder(cleanCode, currentTO.maTO);
      const updatedList = [cleanCode, ...currentList];
      const updatedWeight = Number(currentTO.totalWeight || 0) + 0.45;
      const updatedTO: TOOrder = {
        ...currentTO,
        danhSachGoiHang: updatedList,
        totalWeight: updatedWeight,
      };
      setCurrentTO(updatedTO);
      sounds.playSuccess();
      setScanMessage({ text: `Đã quét thành công: ${cleanCode}`, type: "success" });
    } catch {
      sounds.playError();
      setScanMessage({ text: "Lỗi kết nối khi quét đơn", type: "error" });
    } finally {
      setLoadingScan(false);
      setBarcodeInput("");
      inputRef.current?.focus();
    }
  };

  const handleCompleteTO = async () => {
    if (!currentTO) return;
    const currentList: string[] = Array.isArray(currentTO.danhSachGoiHang)
      ? currentTO.danhSachGoiHang
      : JSON.parse(typeof currentTO.danhSachGoiHang === "string" ? currentTO.danhSachGoiHang : "[]");

    if (currentList.length === 0) {
      alert("Bao hàng chưa có kiện nào!");
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

  const handleRemoveItem = async (pkgCode: string) => {
    if (!currentTO) return;
    const currentList: string[] = Array.isArray(currentTO.danhSachGoiHang)
      ? currentTO.danhSachGoiHang
      : JSON.parse(typeof currentTO.danhSachGoiHang === "string" ? currentTO.danhSachGoiHang : "[]");

    await api.removeOrderFromTO(pkgCode);
    const updatedList = currentList.filter((item) => item !== pkgCode);
    setCurrentTO({ ...currentTO, danhSachGoiHang: updatedList });
  };

  const listGoiHang: string[] = currentTO
    ? Array.isArray(currentTO.danhSachGoiHang)
      ? currentTO.danhSachGoiHang
      : JSON.parse(typeof currentTO.danhSachGoiHang === "string" ? currentTO.danhSachGoiHang : "[]")
    : [];

  return (
    <div className="space-y-5">
      {/* Top Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3 bg-white p-5 rounded-2xl border border-orange-100 shadow-xs">
        <div>
          <h2 className="text-base font-bold text-slate-900 flex items-center gap-2">
            <Scan className="w-5 h-5 text-orange-600" />
            Trạm phân loại & gom bao hàng (Transfer Order)
          </h2>
          <p className="text-xs text-slate-500 mt-0.5">
            Quét mã bưu kiện gom vào bao trung chuyển theo từng Hub đích
          </p>
        </div>

        <button
          onClick={() => setShowCamera(!showCamera)}
          className={`px-3 py-2 rounded-xl border text-xs font-semibold transition flex items-center gap-1.5 self-start sm:self-auto ${
            showCamera
              ? "bg-orange-600 text-white border-orange-600 shadow-sm shadow-orange-600/30"
              : "bg-white text-slate-700 border-orange-200 hover:bg-orange-50"
          }`}
        >
          <Camera className="w-4 h-4 text-orange-600" />
          <span>{showCamera ? "Tắt camera" : "Quét bằng Camera laptop"}</span>
        </button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-12 gap-5">
        {/* CỘT TRÁI */}
        <div className="lg:col-span-7 space-y-4">
          <div className="bg-white p-5 rounded-2xl border border-orange-100 space-y-4 shadow-xs">
            <h3 className="font-bold text-xs text-slate-900 uppercase tracking-wide">
              1. Khởi tạo bao hàng
            </h3>

            <div className="grid grid-cols-1 sm:grid-cols-3 gap-3">
              <div>
                <label className="block text-[11px] text-slate-500 mb-1 font-medium">Hub đích</label>
                <select
                  disabled={!!currentTO && currentTO.trangThai === "Packing"}
                  value={hubDich}
                  onChange={(e) => setHubDich(e.target.value)}
                  className="w-full px-3 py-2 bg-white border border-slate-200 rounded-xl text-xs font-semibold text-slate-800"
                >
                  <option value="Hub Hà Nội">Hub Hà Nội</option>
                  <option value="Hub Đà Nẵng">Hub Đà Nẵng</option>
                  <option value="Hub Cần Thơ">Hub Cần Thơ</option>
                  <option value="Hub Hải Phòng">Hub Hải Phòng</option>
                  <option value="Hub Tân Bình">Hub Tân Bình</option>
                </select>
              </div>

              <div>
                <label className="block text-[11px] text-slate-500 mb-1 font-medium">Nhân viên</label>
                <input
                  type="text"
                  value={packer}
                  onChange={(e) => setPacker(e.target.value)}
                  className="w-full px-3 py-2 bg-white border border-slate-200 rounded-xl text-xs font-semibold text-slate-800"
                />
              </div>

              <div className="flex items-end">
                {!currentTO || currentTO.trangThai !== "Packing" ? (
                  <button
                    onClick={handleKhoiTaoTO}
                    className="w-full py-2 bg-orange-600 hover:bg-orange-700 text-white rounded-xl text-xs font-bold transition shadow-sm shadow-orange-600/20 active:scale-95"
                  >
                    + Mở bao mới
                  </button>
                ) : (
                  <button
                    onClick={() => {
                      if (confirm("Đổi phiên quét khác?")) setCurrentTO(null);
                    }}
                    className="w-full py-2 bg-orange-50 hover:bg-orange-100 text-orange-700 rounded-xl text-xs font-bold transition border border-orange-200"
                  >
                    Đổi bao khác
                  </button>
                )}
              </div>
            </div>

            {currentTO && (
              <div className="bg-orange-50/60 p-3.5 rounded-xl border border-orange-200/80 flex items-center justify-between text-xs">
                <div>
                  <span className="text-[11px] text-orange-700 font-semibold">Mã bao đang mở:</span>
                  <div className="font-mono font-bold text-slate-900 text-sm">{currentTO.maTO}</div>
                  <div className="text-slate-600 text-[11px]">Đích: <b className="text-slate-800">{currentTO.diaDiemGiaoHang}</b></div>
                </div>
                <span className="text-[11px] bg-white border border-orange-200 text-orange-700 px-2.5 py-1 rounded-lg font-bold">
                  {currentTO.trangThai === "Packing" ? "Đang gom kiện" : "Đã đóng bao"}
                </span>
              </div>
            )}
          </div>

          {showCamera && (
            <div className="bg-white p-3 rounded-2xl border border-orange-100 shadow-xs">
              <BarcodeScanner active={showCamera} onScan={(code) => handleProcessScan(code)} />
            </div>
          )}

          {/* Ô BẮN MÃ VẠCH VÀ SÚNG SCANNER (NỔI BẬT VỚI HƯỚNG DẪN RÕ RÀNG) */}
          <div className="bg-white p-5 rounded-2xl border-2 border-orange-200 space-y-3 shadow-xs">
            <div className="flex items-center justify-between">
              <h3 className="font-bold text-xs text-orange-600 uppercase tracking-wide flex items-center gap-1.5">
                <Zap className="w-4 h-4 fill-orange-600 text-orange-600" />
                2. Vị trí quét súng mã vạch (Barcode Scanner Gun)
              </h3>
              <span className="text-[10px] text-emerald-700 font-bold bg-emerald-50 border border-emerald-200 px-2 py-0.5 rounded-full flex items-center gap-1">
                <span className="w-1.5 h-1.5 rounded-full bg-emerald-500 animate-pulse" />
                Súng quét: Sẵn sàng nhận
              </span>
            </div>

            {/* Ô INPUT CHÍNH ĐỂ SÚNG BẮN VÀO */}
            <div className="relative">
              <input
                ref={inputRef}
                type="text"
                value={barcodeInput}
                disabled={!currentTO || currentTO.trangThai !== "Packing"}
                onChange={(e) => setBarcodeInput(e.target.value)}
                onKeyDown={(e) => {
                  if (e.key === "Enter") {
                    handleProcessScan(barcodeInput);
                  }
                }}
                placeholder={
                  currentTO
                    ? "🔫 Cầm súng quét bưu kiện bắn vào đây (hoặc gõ SPXVN... rồi Enter)"
                    : "⚠️ Vui lòng bấm '+ Mở bao mới' ở bước 1 để bắt đầu bắn mã"
                }
                className="w-full pl-4 pr-24 py-3 bg-orange-50/30 border-2 border-orange-300 focus:border-orange-600 focus:ring-2 focus:ring-orange-100 rounded-xl text-sm font-mono font-bold text-slate-900 outline-none transition placeholder:text-slate-400 placeholder:font-normal placeholder:text-xs"
              />

              <button
                disabled={!currentTO || !barcodeInput.trim() || loadingScan}
                onClick={() => handleProcessScan(barcodeInput)}
                className="absolute right-1.5 top-1.5 bottom-1.5 px-4 bg-orange-600 hover:bg-orange-700 disabled:bg-slate-200 text-white rounded-lg text-xs font-bold transition active:scale-95"
              >
                {loadingScan ? "Đang xử lý..." : "Nhập"}
              </button>
            </div>

            {/* Chú thích cách súng hoạt động */}
            <div className="flex flex-wrap items-center justify-between text-xs text-slate-500 pt-1">
              <span className="text-[11px] text-slate-400">
                💡 <b>Mẹo:</b> Khi súng bắn mã kêu <i>&quot;tít&quot;</i>, mã sẽ tự động nạp vào bao và phát tiếng Beep thành công.
              </span>

              <div className="flex items-center gap-1 text-[11px]">
                <span>Thử nghiệm nhanh:</span>
                <button
                  type="button"
                  onClick={() => setBarcodeInput("SPXVN01" + Math.floor(100000000 + Math.random() * 900000000))}
                  className="text-orange-600 font-bold hover:underline font-mono"
                >
                  + Sinh mã mẫu
                </button>
              </div>
            </div>

            {scanMessage && (
              <div
                className={`p-3 rounded-xl border text-xs font-semibold flex items-center gap-2 ${
                  scanMessage.type === "success"
                    ? "bg-emerald-50 text-emerald-800 border-emerald-200"
                    : "bg-rose-50 text-rose-800 border-rose-200"
                }`}
              >
                {scanMessage.type === "success" ? (
                  <CheckCircle2 className="w-4 h-4 text-emerald-600 shrink-0" />
                ) : (
                  <AlertCircle className="w-4 h-4 text-rose-600 shrink-0" />
                )}
                <span>{scanMessage.text}</span>
              </div>
            )}
          </div>
        </div>

        {/* CỘT PHẢI: BỘ ĐẾM & DANH SÁCH */}
        <div className="lg:col-span-5 space-y-4">
          <div className="grid grid-cols-2 gap-3">
            <div className="bg-white p-4 rounded-2xl border border-orange-100 shadow-xs text-center">
              <span className="text-[11px] text-slate-400 font-bold uppercase">Tổng kiện trong bao</span>
              <div className="text-2xl font-black text-orange-600 mt-0.5">{listGoiHang.length}</div>
            </div>
            <div className="bg-white p-4 rounded-2xl border border-orange-100 shadow-xs text-center">
              <span className="text-[11px] text-slate-400 font-bold uppercase">Khối lượng</span>
              <div className="text-2xl font-black text-slate-900 mt-0.5">
                {Number(currentTO?.totalWeight || 0).toFixed(2)} <span className="text-xs font-normal text-slate-400">kg</span>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-2xl border border-orange-100 shadow-xs overflow-hidden flex flex-col h-[350px]">
            <div className="px-4 py-3 bg-orange-50/50 border-b border-orange-100 flex justify-between text-xs font-bold text-slate-800">
              <span>Danh sách kiện đã quét ({listGoiHang.length})</span>
              <span className="font-mono text-orange-600">{currentTO?.maTO || "---"}</span>
            </div>

            <div className="flex-1 overflow-y-auto p-2.5 space-y-1.5">
              {listGoiHang.length === 0 ? (
                <div className="h-full flex flex-col items-center justify-center text-slate-400 text-xs text-center p-4">
                  <Package className="w-8 h-8 text-orange-300 mb-1" />
                  <span>Bao hàng chưa có kiện nào</span>
                  <span className="text-[11px] text-slate-400 mt-0.5">Dùng súng quét bưu kiện để gom hàng vào bao</span>
                </div>
              ) : (
                listGoiHang.map((pkg, idx) => (
                  <div
                    key={pkg}
                    className="flex items-center justify-between p-2.5 bg-orange-50/30 hover:bg-orange-50 rounded-xl border border-orange-100 text-xs transition"
                  >
                    <span className="font-mono font-bold text-slate-800">
                      <span className="text-orange-400 mr-2 font-normal">#{listGoiHang.length - idx}</span>
                      {pkg}
                    </span>
                    {currentTO?.trangThai === "Packing" && (
                      <button
                        onClick={() => handleRemoveItem(pkg)}
                        title="Xóa kiện"
                        className="text-slate-400 hover:text-rose-600 transition"
                      >
                        <Trash2 className="w-3.5 h-3.5" />
                      </button>
                    )}
                  </div>
                ))
              )}
            </div>

            <div className="p-3 bg-white border-t border-orange-100">
              {currentTO?.trangThai === "Packing" ? (
                <button
                  onClick={handleCompleteTO}
                  className="w-full py-2.5 bg-orange-600 hover:bg-orange-700 text-white rounded-xl text-xs font-bold transition shadow-sm shadow-orange-600/20 active:scale-95"
                >
                  Đóng bao & Niêm phong
                </button>
              ) : currentTO?.trangThai === "Complete" ? (
                <button
                  onClick={() => setPrintTO(currentTO)}
                  className="w-full py-2.5 bg-white hover:bg-orange-50 text-orange-600 border border-orange-300 rounded-xl text-xs font-bold transition flex items-center justify-center gap-1.5"
                >
                  <Printer className="w-4 h-4" /> In phiếu bao (Bill TO)
                </button>
              ) : null}
            </div>
          </div>
        </div>
      </div>

      {printTO && <BillModal to={printTO} onClose={() => setPrintTO(null)} />}
    </div>
  );
}
