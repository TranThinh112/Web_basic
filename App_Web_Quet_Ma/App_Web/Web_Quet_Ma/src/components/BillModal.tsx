"use client";

import { useEffect } from "react";
import { TOOrder } from "@/types";
import { Printer, X, Share2 } from "lucide-react";
import { QRCodeSVG } from "qrcode.react";

interface BillModalProps {
  to: TOOrder;
  onClose: () => void;
}

function BarcodeWidget({ value, width = 250, height = 65 }: { value: string; width?: number; height?: number }) {
  const bars = [2, 1, 3, 1, 2, 2, 1, 3, 1, 1, 2, 3, 2, 1, 1, 2, 3, 1, 2, 1, 3, 2, 1, 1, 2, 2, 1, 3, 1, 2, 1, 3, 2];
  return (
    <div className="flex justify-center items-center py-1">
      <svg width={width} height={height} viewBox="0 0 250 65" preserveAspectRatio="none">
        <rect width="250" height="65" fill="white" />
        {bars.map((w, i) => {
          const x = i * 7.5 + 4;
          return <rect key={i} x={x} y="4" width={w * 1.6} height="57" fill="black" />;
        })}
      </svg>
    </div>
  );
}

export default function BillModal({ to, onClose }: BillModalProps) {
  // Lắng nghe sự kiện phím Escape để đóng modal
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Escape") {
        onClose();
      }
    };
    window.addEventListener("keydown", handleKeyDown);
    return () => window.removeEventListener("keydown", handleKeyDown);
  }, [onClose]);

  const listPackages: string[] = Array.isArray(to.danhSachGoiHang)
    ? to.danhSachGoiHang
    : typeof to.danhSachGoiHang === "string"
    ? JSON.parse(to.danhSachGoiHang || "[]")
    : [];

  const handlePrint = () => {
    window.print();
  };

  const handleShare = async () => {
    if (typeof navigator !== "undefined" && navigator.share) {
      try {
        await navigator.share({
          title: "Bill TO - " + to.maTO,
          text: `Mã TO: ${to.maTO} | Đến: ${to.diaDiemGiaoHang} | Số kiện: ${listPackages.length}`,
          url: window.location.href,
        });
      } catch (err) {
        console.log("Share cancelled", err);
      }
    } else if (typeof navigator !== "undefined" && navigator.clipboard) {
      navigator.clipboard.writeText(to.maTO);
      alert("Đã sao chép mã TO: " + to.maTO);
    }
  };

  const formatTime = (timeStr?: string | null) => {
    if (!timeStr) return "-";
    try {
      const d = new Date(timeStr);
      if (isNaN(d.getTime())) return timeStr;
      const day = d.getDate();
      const month = d.getMonth() + 1;
      const year = d.getFullYear();
      const hour = d.getHours();
      const min = d.getMinutes().toString().padStart(2, "0");
      return `${day}-${month}-${year} ${hour}:${min}`;
    } catch {
      return timeStr;
    }
  };

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/60 backdrop-blur-xs p-4"
      onClick={(e) => {
        if (e.target === e.currentTarget) onClose();
      }}
    >
      <div className="bg-white text-slate-800 rounded-2xl w-full max-w-lg shadow-2xl overflow-hidden border border-slate-200 animate-in fade-in zoom-in-95 duration-150">
        {/* HEADER MODAL */}
        <div className="flex items-center justify-between px-5 py-3.5 bg-orange-600 text-white print:hidden">
          <div className="flex items-center gap-2">
            <span className="font-bold text-sm">Tem Bao Hàng (Bill TO) - {to.maTO}</span>
            <span className="text-[10px] bg-white/20 px-1.5 py-0.5 rounded text-white/90">ESC để đóng</span>
          </div>
          <div className="flex items-center gap-2">
            <button
              onClick={handleShare}
              title="Chia sẻ hoặc sao chép"
              className="p-1 rounded-lg hover:bg-orange-700 transition"
            >
              <Share2 className="w-4 h-4" />
            </button>
            <button
              onClick={onClose}
              title="Đóng (Esc)"
              className="p-1 rounded-lg hover:bg-orange-700 transition"
            >
              <X className="w-5 h-5" />
            </button>
          </div>
        </div>

        {/* NỘI DUNG TEM BILL TO CHUẨN THEO BillTo.dart CỦA APP */}
        <div className="p-6 flex justify-center bg-slate-50/50 print:bg-white print:p-0">
          <div
            id="printable-bill-to"
            className="w-full max-w-[460px] bg-white rounded-lg border-2 border-black p-3.5 text-black font-sans shadow-md print:border-black print:shadow-none print:m-0"
          >
            <div className="flex items-start">
              {/* CỘT TRÁI (flex: 2) */}
              <div className="flex-[2] pr-3 text-xs leading-relaxed space-y-1.5 font-bold">
                {/* Barcode code128 */}
                <div className="mb-2">
                  <BarcodeWidget value={to.maTO} width={240} height={60} />
                </div>

                <div>Mã TO: {to.maTO}</div>
                <div>Đến: {to.diaDiemGiaoHang || "-"}</div>
                <div>Packer: {to.packer || "admin"}</div>
                <div>Số lượng: {listPackages.length}</div>
                <div>Số kí: {Number(to.totalWeight || 0).toFixed(1)}kg</div>
                <div>Ngày tạo: {formatTime(to.ngayTao)}</div>
                <div>Ngày Đóng: {formatTime(to.completeTime)}</div>
              </div>

              {/* ĐƯỜNG KẺ DỌC (VerticalDivider thickness: 2, color: black) */}
              <div className="w-[2px] bg-black self-stretch min-h-[220px]"></div>

              {/* CỘT PHẢI (flex: 1) */}
              <div className="flex-1 pl-3 flex flex-col items-center justify-center text-center">
                <div className="font-bold text-xs uppercase pt-2">
                  Đến: {to.diaDiemGiaoHang || "-"}
                </div>

                <div className="mt-6 mb-2 p-1.5 bg-white border border-slate-200 rounded">
                  <QRCodeSVG value={to.maTO} size={95} level="M" />
                </div>

                <span className="text-[10px] text-slate-500 font-mono">Quét mã TO</span>
              </div>
            </div>
          </div>
        </div>

        {/* FOOTER ACTIONS */}
        <div className="flex items-center justify-end gap-3 px-6 py-3.5 bg-white border-t border-slate-100 print:hidden">
          <button
            onClick={onClose}
            className="px-4 py-2 rounded-xl text-xs font-semibold text-slate-600 hover:bg-slate-100 transition"
          >
            Đóng (Esc)
          </button>
          <button
            onClick={handlePrint}
            className="px-5 py-2 rounded-xl text-xs font-bold bg-orange-600 hover:bg-orange-700 text-white flex items-center gap-2 shadow-md shadow-orange-500/20 transition active:scale-95"
          >
            <Printer className="w-4 h-4" />
            In phiếu TO
          </button>
        </div>
      </div>
    </div>
  );
}
