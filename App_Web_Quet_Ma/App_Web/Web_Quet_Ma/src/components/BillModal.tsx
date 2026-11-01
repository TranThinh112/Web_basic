"use client";

import { TOOrder } from "@/types";
import { Printer, X, CheckCircle } from "lucide-react";

interface BillModalProps {
  to: TOOrder;
  onClose: () => void;
}

export default function BillModal({ to, onClose }: BillModalProps) {
  const listPackages: string[] = Array.isArray(to.danhSachGoiHang)
    ? to.danhSachGoiHang
    : typeof to.danhSachGoiHang === "string"
    ? JSON.parse(to.danhSachGoiHang || "[]")
    : [];

  const handlePrint = () => {
    window.print();
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/60 backdrop-blur-sm p-4">
      <div className="bg-white text-slate-800 rounded-2xl w-full max-w-xl shadow-2xl overflow-hidden border border-slate-200 animate-in fade-in zoom-in-95 duration-200">
        {/* Header Modal */}
        <div className="flex items-center justify-between px-6 py-4 bg-orange-600 text-white print:hidden">
          <div className="flex items-center gap-2">
            <CheckCircle className="w-5 h-5 text-white" />
            <h3 className="font-bold text-lg">Phiếu Bao Hàng Vận Chuyển (Bill TO)</h3>
          </div>
          <button
            onClick={onClose}
            className="p-1 rounded-full hover:bg-orange-700 transition"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Nội dung in (Printable Content) */}
        <div id="printable-bill" className="p-8 font-sans">
          {/* Header Bill */}
          <div className="border-b-2 border-dashed border-slate-300 pb-5 mb-5 flex justify-between items-start">
            <div>
              <div className="flex items-center gap-2">
                <span className="text-2xl font-black text-orange-600 tracking-wider">SPX</span>
                <span className="text-xs bg-orange-100 text-orange-700 font-bold px-2 py-0.5 rounded">EXPRESS</span>
              </div>
              <p className="text-xs text-slate-500 mt-1">Hệ Thống Phân Loại & Điều Phối Bưu Cục</p>
            </div>
            <div className="text-right">
              <p className="text-xs font-semibold text-slate-500 uppercase tracking-wider">Mã Bao Hàng (TO Code)</p>
              <p className="text-xl font-black text-slate-900 tracking-wider font-mono">{to.maTO}</p>
            </div>
          </div>

          {/* Grid thông tin */}
          <div className="grid grid-cols-2 gap-4 bg-slate-50 p-4 rounded-xl border border-slate-100 mb-5">
            <div>
              <span className="text-xs text-slate-500 uppercase font-semibold">Điểm Đến (Hub Đích):</span>
              <p className="text-base font-bold text-slate-900">{to.diaDiemGiaoHang}</p>
            </div>
            <div>
              <span className="text-xs text-slate-500 uppercase font-semibold">Nhân Viên Đóng Bao:</span>
              <p className="text-base font-bold text-slate-900">{to.packer}</p>
            </div>
            <div>
              <span className="text-xs text-slate-500 uppercase font-semibold">Tổng Số Kiện:</span>
              <p className="text-2xl font-black text-orange-600">{listPackages.length} <span className="text-sm font-normal text-slate-500">kiện</span></p>
            </div>
            <div>
              <span className="text-xs text-slate-500 uppercase font-semibold">Tổng Khối Lượng:</span>
              <p className="text-2xl font-black text-slate-900">{Number(to.totalWeight || 0).toFixed(2)} <span className="text-sm font-normal text-slate-500">kg</span></p>
            </div>
            <div>
              <span className="text-xs text-slate-500 uppercase font-semibold">Thời Gian Tạo:</span>
              <p className="text-xs font-medium text-slate-700">{new Date(to.ngayTao).toLocaleString("vi-VN")}</p>
            </div>
            <div>
              <span className="text-xs text-slate-500 uppercase font-semibold">Thời Gian Đóng Bao:</span>
              <p className="text-xs font-medium text-slate-700">
                {to.completeTime ? new Date(to.completeTime).toLocaleString("vi-VN") : "Đang xử lý"}
              </p>
            </div>
          </div>

          {/* Danh sách kiện trong bao */}
          <div>
            <h4 className="text-xs font-bold text-slate-700 uppercase tracking-wider mb-2">
              Danh sách kiện hàng trong bao ({listPackages.length}):
            </h4>
            <div className="max-h-48 overflow-y-auto border border-slate-200 rounded-lg p-3 bg-white space-y-1">
              {listPackages.length === 0 ? (
                <p className="text-xs text-slate-400 italic">Bao chưa có kiện hàng nào</p>
              ) : (
                listPackages.map((pkg, idx) => (
                  <div key={idx} className="flex items-center justify-between text-xs py-1 px-2 border-b border-slate-100 last:border-none">
                    <span className="font-mono text-slate-600">#{idx + 1}</span>
                    <span className="font-mono font-bold text-slate-900">{pkg}</span>
                    <span className="text-[10px] text-emerald-600 bg-emerald-50 px-1.5 py-0.5 rounded font-medium">Inbound</span>
                  </div>
                ))
              )}
            </div>
          </div>

          {/* Chữ ký */}
          <div className="mt-8 pt-4 border-t border-dashed border-slate-300 grid grid-cols-2 text-center text-xs">
            <div>
              <p className="font-bold text-slate-700">Nhân viên đóng bao</p>
              <div className="h-16" />
              <p className="italic text-slate-500">{to.packer}</p>
            </div>
            <div>
              <p className="font-bold text-slate-700">Tài xế nhận hàng</p>
              <div className="h-16" />
              <p className="italic text-slate-400">(Ký và ghi rõ họ tên)</p>
            </div>
          </div>
        </div>

        {/* Footer actions */}
        <div className="flex items-center justify-end gap-3 px-6 py-4 bg-slate-50 border-t border-slate-100 print:hidden">
          <button
            onClick={onClose}
            className="px-4 py-2 rounded-xl text-sm font-semibold text-slate-600 hover:bg-slate-200 transition"
          >
            Đóng
          </button>
          <button
            onClick={handlePrint}
            className="px-5 py-2 rounded-xl text-sm font-bold bg-orange-600 hover:bg-orange-700 text-white flex items-center gap-2 shadow-lg shadow-orange-500/20 transition active:scale-95"
          >
            <Printer className="w-4 h-4" />
            In Phiếu (Print Bill)
          </button>
        </div>
      </div>
    </div>
  );
}
