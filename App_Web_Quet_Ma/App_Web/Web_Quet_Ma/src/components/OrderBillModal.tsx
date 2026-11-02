"use client";

import { useRef, useEffect } from "react";
import { QRCodeSVG } from "qrcode.react";
import { Order } from "@/types";
import { X, Printer } from "lucide-react";

interface BillModalProps {
  order: any;
  onClose: () => void;
}

// Giả lập vạch mã Barcode Code 128 bằng SVG vector kích thước chuẩn A6 / Bill kho
function BarcodeWidget({ value, width = 420, height = 95 }: { value: string; width?: number; height?: number }) {
  const bars = [2, 1, 3, 1, 2, 2, 1, 3, 1, 1, 2, 3, 2, 1, 1, 2, 3, 1, 2, 1, 3, 2, 1, 1, 2, 2, 1, 3, 1, 2, 1, 3, 2, 1, 2, 3, 1, 2];
  return (
    <div className="flex flex-col items-center justify-center py-1">
      <svg width={width} height={height} viewBox="0 0 420 95" className="max-w-full">
        <rect width="420" height="95" fill="white" />
        {bars.map((w, i) => {
          const x = i * 10.8 + 6;
          return <rect key={i} x={x} y="4" width={w * 2.2} height="86" fill="black" />;
        })}
      </svg>
      <div className="font-mono text-base font-extrabold text-black tracking-widest mt-1">
        Mã Vận Đơn: {value}
      </div>
    </div>
  );
}

export default function OrderBillModal({ order, onClose }: BillModalProps) {
  const billRef = useRef<HTMLDivElement>(null);

  // Helper đọc linh hoạt cả camelCase và lowercase
  const get = (...keys: string[]) => {
    for (const k of keys) {
      if (order[k] !== undefined && order[k] !== null && String(order[k]).trim() !== "") {
        return order[k];
      }
    }
    return "";
  };

  const id = get("id") || "SPXVN00000000";
  const noiNhan = get("noiNhan", "noinhan") || "SOC HCM";
  const noiGui = get("noiGui", "noigui") || "HN";
  const nguoiGui = get("nguoiGui", "nguoigui") || "Người gửi";
  const diaChiGui = get("diaChiGui", "diachigui") || "---";
  const nguoiNhan = get("nguoiNhan", "nguoinhan") || "Người nhận";
  const diaChiNhan = get("diaChiNhan", "diachinhan") || "---";
  const sanPham = get("sanPham", "sanpham") || "Hàng hóa SPX Express";
  const soKi = Number(get("soKi", "soki")) || 0.5;
  const giaTien = Number(get("giaTien", "giatien", "cod")) || 0;
  const thoiGianTao = get("thoiGianTao", "thoigiantao", "createdAt");

  // Lắng nghe phím ESC để đóng Modal
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Escape" || e.keyCode === 27) {
        onClose();
      }
    };

    window.addEventListener("keydown", handleKeyDown);
    return () => {
      window.removeEventListener("keydown", handleKeyDown);
    };
  }, [onClose]);

  const formatPrice = (val?: number) => {
    if (!val) return "0 VND";
    return new Intl.NumberFormat("vi-VN").format(val) + " VND";
  };

  const formatDate = (dateStr?: string | null) => {
    if (!dateStr) {
      const now = new Date();
      return `${now.getDate()}-${now.getMonth() + 1}-${now.getFullYear()} ${now.getHours()}:${String(now.getMinutes()).padStart(2, "0")}`;
    }
    const d = new Date(dateStr);
    return `${d.getDate()}-${d.getMonth() + 1}-${d.getFullYear()} ${d.getHours()}:${String(d.getMinutes()).padStart(2, "0")}`;
  };

  const handlePrint = () => {
    window.print();
  };

  return (
    <div
      onClick={onClose}
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/65 p-4 backdrop-blur-xs animate-in fade-in duration-150 overflow-y-auto cursor-pointer"
    >
      {/* Khung Modal - Click bên trong không bị đóng (stopPropagation) */}
      <div
        onClick={(e) => e.stopPropagation()}
        className="relative w-full max-w-[540px] bg-white rounded-2xl shadow-2xl overflow-hidden border border-slate-300 my-auto cursor-default"
      >
        {/* Header Modal */}
        <div className="flex items-center justify-between px-5 py-3 bg-slate-100 border-b border-slate-200">
          <span></span>
          <div className="flex items-center gap-2">
            <button
              onClick={handlePrint}
              className="p-1.5 hover:bg-slate-200 rounded-lg text-slate-700 transition cursor-pointer flex items-center gap-1 text-xs font-semibold"
              title="In phiếu"
            >
              <Printer className="w-4 h-4" />
              <span>In phiếu</span>
            </button>
            <button
              onClick={onClose}
              className="p-1.5 hover:bg-slate-200 rounded-lg text-slate-700 transition cursor-pointer"
              title="Đóng (ESC)"
            >
              <X className="w-4 h-4" />
            </button>
          </div>
        </div>

        {/* Khung Hóa Đơn Khổ Lớn Rõ Nét */}
        <div ref={billRef} className="p-6 bg-white text-black font-sans text-sm select-none">
          {/* 1. Barcode Code 128 To Rõ */}
          <div className="pb-3 border-b-2 border-black flex justify-center">
            <BarcodeWidget value={id} width={440} height={90} />
          </div>

          {/* 2. Nơi nhận (Hub trung chuyển) */}
          <div className="py-3 text-center text-2xl font-black border-b-2 border-black uppercase tracking-wider">
            {noiNhan}
          </div>

          {/* 3. Từ (Người gửi) & Đến (Người nhận) chia cột có vạch kẻ dày ngăn cách */}
          <div className="grid grid-cols-2 divide-x-2 divide-black border-b-2 border-black">
            <div className="p-3.5 space-y-1.5">
              <div className="font-black text-sm">Từ: {nguoiGui}</div>
              <div className="text-xs text-slate-800 leading-relaxed">
                Địa chỉ: {diaChiGui}
              </div>
            </div>
            <div className="p-3.5 space-y-1.5">
              <div className="font-black text-sm">Đến: {nguoiNhan}</div>
              <div className="text-xs text-slate-800 leading-relaxed">
                Địa chỉ: {diaChiNhan}
              </div>
            </div>
          </div>

          {/* 4. Nội dung hàng hóa & QR Code */}
          <div className="grid grid-cols-12 divide-x-2 divide-black border-b-2 border-black min-h-[190px]">
            {/* Cột trái: Nội dung */}
            <div className="col-span-7 p-3.5">
              <div className="font-black text-sm">Nội dung:</div>
              <div className="text-sm font-semibold text-slate-900 mt-1.5 leading-relaxed">
                {sanPham}
              </div>
            </div>

            {/* Cột phải: QR code + Nơi gửi + Ngày đặt */}
            <div className="col-span-5 p-3 flex flex-col items-center justify-between text-center">
              <QRCodeSVG value={id} size={110} />
              <div className="w-full border-t-2 border-black my-1.5" />
              <div className="font-black text-sm uppercase">{noiGui}</div>
              <div className="w-full border-t border-black my-1" />
              <div className="text-[11px] text-slate-600">Ngày đặt:</div>
              <div className="font-bold text-xs">{formatDate(thoiGianTao)}</div>
            </div>
          </div>

          {/* 5. Giá tiền COD & Khối lượng */}
          <div className="grid grid-cols-12 divide-x-2 divide-black border-b-2 border-black py-3 items-center">
            <div className="col-span-7 px-3 flex items-center justify-between">
              <span className="font-black text-base">{formatPrice(giaTien)}</span>
              {giaTien > 3000000 && (
                <span className="px-2 py-0.5 bg-black text-white text-[11px] font-black rounded">
                  GTC
                </span>
              )}
            </div>
            <div className="col-span-5 px-3 text-right">
              <span className="font-black text-sm">Khối lượng: {soKi.toFixed(2)}kg</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

