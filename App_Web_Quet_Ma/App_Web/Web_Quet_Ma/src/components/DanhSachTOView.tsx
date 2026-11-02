"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import {
  Package,
  Search,
  Printer,
  RotateCcw,
  Trash2,
  Scan,
  RefreshCw,
} from "lucide-react";
import { api } from "@/lib/api";
import { TOOrder } from "@/types";
import BillModal from "@/components/BillModal";

export default function DanhSachTOView() {
  const router = useRouter();
  const [listTO, setListTO] = useState<TOOrder[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [statusFilter, setStatusFilter] = useState("ALL");
  const [selectedTOForBill, setSelectedTOForBill] = useState<TOOrder | null>(null);

  const fetchTOs = async () => {
    setLoading(true);
    try {
      const data = await api.getAllTOs();
      setListTO(data);
    } catch {
      // ignore
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTOs();
  }, []);

  // Nút ReOpen đối với bao đã Packed (hoặc Complete): vào thẳng không hiện confirm alert
  const handleReopen = async (maTO: string) => {
    // Gọi reopen ngầm và điều hướng trực tiếp vào trang quét mã của TO
    api.reopenTO(maTO).catch(() => {});
    router.push(`/TOmanagement/sorting/${encodeURIComponent(maTO)}`);
  };

  // Nút mở lại trang quét mã đối với bao đang ở trạng thái Packing
  const handleContinuePacking = (maTO: string) => {
    router.push(`/TOmanagement/sorting/${encodeURIComponent(maTO)}`);
  };

  const handleDelete = async (maTO: string) => {
    if (confirm(`Xác nhận xóa mã bao ${maTO}?`)) {
      const ok = await api.deleteTO(maTO);
      if (ok) fetchTOs();
    }
  };

  // Hàm format thời gian chi tiết chuẩn múi giờ Việt Nam (Asia/Ho_Chi_Minh - GMT+7)
  // Hàm format thời gian chi tiết chuẩn giờ Việt Nam
  const formatDateTimeFull = (isoString?: string | null) => {
    if (!isoString) return "---";
    try {
      const str = String(isoString).trim();
      // Nếu chuỗi chứa ngày và giờ dạng YYYY-MM-DDTHH:mm:ss hoặc YYYY-MM-DD HH:mm:ss
      const match = str.match(/^(\d{4})-(\d{2})-(\d{2})[T ](\d{2}:\d{2}:\d{2})/);
      if (match) {
        const [, yyyy, mm, dd, timePart] = match;
        return `${timePart} ${dd}/${mm}/${yyyy}`;
      }

      const d = new Date(str);
      if (isNaN(d.getTime())) return str;
      const hh = String(d.getHours()).padStart(2, "0");
      const mm = String(d.getMinutes()).padStart(2, "0");
      const ss = String(d.getSeconds()).padStart(2, "0");
      const dd = String(d.getDate()).padStart(2, "0");
      const MM = String(d.getMonth() + 1).padStart(2, "0");
      const yyyy = d.getFullYear();
      return `${hh}:${mm}:${ss} ${dd}/${MM}/${yyyy}`;
    } catch {
      return String(isoString);
    }
  };

  const filtered = listTO
    .filter((item) => {
      const isPacked = item.trangThai === "Packed" || item.trangThai === "Complete";
      const itemStatus = isPacked ? "Packed" : "Packing";
      const matchStatus = statusFilter === "ALL" || itemStatus === statusFilter;

      const matchSearch =
        item.maTO.toLowerCase().includes(searchTerm.toLowerCase()) ||
        (item.diaDiemGiaoHang || "").toLowerCase().includes(searchTerm.toLowerCase()) ||
        (item.packer || "").toLowerCase().includes(searchTerm.toLowerCase());
      return matchStatus && matchSearch;
    })
    .sort((a, b) => {
      const isPackedA = a.trangThai === "Packed" || a.trangThai === "Complete";
      const isPackedB = b.trangThai === "Packed" || b.trangThai === "Complete";

      // Lấy mốc thời gian so sánh theo từng loại TO
      // Packed: lấy thời gian completeTime (nếu không có thì dùng ngayTao)
      // Packing: lấy thời gian ngayTao
      const getTimeOfItem = (item: typeof a, isPacked: boolean) => {
        if (isPacked) {
          const t = item.completeTime ? new Date(item.completeTime).getTime() : 0;
          return t > 0 ? t : (item.ngayTao ? new Date(item.ngayTao).getTime() : 0);
        }
        return item.ngayTao ? new Date(item.ngayTao).getTime() : 0;
      };

      const timeA = getTimeOfItem(a, isPackedA);
      const timeB = getTimeOfItem(b, isPackedB);

      // Sort từ trên xuống (Mới nhất nằm ở dòng đầu tiên của bảng)
      return timeB - timeA;
    });

  return (
    <div className="space-y-4">
      {/* Top Filter */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
        <div className="flex items-center gap-1.5">
          {["ALL", "Packing", "Packed"].map((st) => (
            <button
              key={st}
              onClick={() => setStatusFilter(st)}
              className={`px-3 py-1.5 rounded-lg text-xs transition ${
                statusFilter === st
                  ? "bg-slate-900 text-white font-medium"
                  : "bg-white text-slate-600 hover:bg-slate-100 border border-slate-200"
              }`}
            >
              {st === "ALL" ? "Tất cả" : st}
            </button>
          ))}
        </div>

        <div className="relative w-full sm:w-64">
          <Search className="w-4 h-4 text-slate-400 absolute left-3 top-2.5" />
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Tìm mã TO, điểm đến..."
            className="w-full pl-9 pr-3 py-1.5 bg-white rounded-lg border border-slate-200 text-xs focus:border-slate-400 outline-none"
          />
        </div>
      </div>

      {/* Table TO */}
      <div className="bg-white rounded-xl border border-slate-200 overflow-hidden shadow-xs">
        <div className="overflow-x-auto">
          <table className="w-full text-left text-xs">
            <thead className="bg-slate-50 text-slate-500 font-semibold border-b border-slate-200">
              <tr>
                <th className="px-4 py-3">Mã bao</th>
                <th className="px-4 py-3">Điểm đến (Hub)</th>
                <th className="px-4 py-3 text-center">Số kiện</th>
                <th className="px-4 py-3 text-right">Khối lượng (kg)</th>
                <th className="px-4 py-3">Trạng thái</th>
                <th className="px-4 py-3">Nhân viên</th>
                <th className="px-4 py-3">Thời gian tạo</th>
                <th className="px-4 py-3 text-right">Thao tác</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-100 text-slate-700">
              {filtered.length === 0 ? (
                <tr>
                  <td colSpan={8} className="text-center py-10 text-slate-400">
                    {loading ? "Đang tải dữ liệu..." : "Chưa có dữ liệu bao hàng"}
                  </td>
                </tr>
              ) : (
                filtered.map((item) => {
                  const pkgs: any[] = Array.isArray(item.danhSachGoiHang)
                    ? item.danhSachGoiHang
                    : JSON.parse(
                        typeof item.danhSachGoiHang === "string"
                          ? item.danhSachGoiHang
                          : "[]"
                      );

                  const isPacked =
                    item.trangThai === "Complete" || item.trangThai === "Packed";

                  return (
                    <tr key={item.maTO} className="hover:bg-slate-50/70 transition">
                      {/* Mã bao: click để chuyển thẳng tới router của bao */}
                      <td className="px-4 py-3 font-mono font-bold text-slate-900">
                        <button
                          onClick={() => handleContinuePacking(item.maTO)}
                          title="Mở màn hình quét mã của TO này"
                          className="hover:text-orange-600 hover:underline flex items-center gap-1.5"
                        >
                          {item.maTO}
                        </button>
                      </td>

                      <td className="px-4 py-3 font-medium text-slate-800">
                        {item.diaDiemGiaoHang || "---"}
                      </td>
                      <td className="px-4 py-3 text-center font-bold">{pkgs.length}</td>
                      <td className="px-4 py-3 text-right font-mono">
                        {Number(item.totalWeight || 0).toFixed(2)}
                      </td>

                      {/* Trạng thái: Packing đỏ, Packed xanh lá */}
                      <td className="px-4 py-3">
                        <span
                          className={`text-[11px] px-2.5 py-0.5 rounded-full font-bold border ${
                            isPacked
                              ? "bg-emerald-50 text-emerald-700 border-emerald-200"
                              : "bg-rose-50 text-rose-700 border-rose-200"
                          }`}
                        >
                          {isPacked ? "Packed" : "Packing"}
                        </span>
                      </td>

                      <td className="px-4 py-3 text-slate-500">{item.packer}</td>
                      <td className="px-4 py-3 font-mono text-[11px] text-slate-600">
                        <div>
                          <span className="font-semibold text-slate-800">{formatDateTimeFull(isPacked && item.completeTime ? item.completeTime : item.ngayTao)}</span>
                          {isPacked && item.completeTime && (
                            <span className="block text-[10px] text-emerald-600 font-sans font-medium">(Hoàn tất)</span>
                          )}
                        </div>
                      </td>

                      {/* Thao tác */}
                      <td className="px-4 py-3 text-right">
                        <div className="flex items-center justify-end gap-1.5">
                          {/* Nút ReOpen đối với bao đã Packed */}
                          {isPacked && (
                            <button
                              onClick={() => handleReopen(item.maTO)}
                              title="ReOpen: Mở lại bao để tiếp tục thêm / xóa đơn"
                              className="px-2.5 py-1 rounded-lg text-[11px] font-bold bg-amber-50 text-amber-800 hover:bg-amber-100 border border-amber-200 transition flex items-center gap-1"
                            >
                              <RotateCcw className="w-3 h-3 text-amber-700" />
                              <span>ReOpen</span>
                            </button>
                          )}

                          {/* Nút mở lại trang quét mã đối với bao đang Packing */}
                          {!isPacked && (
                            <button
                              onClick={() => handleContinuePacking(item.maTO)}
                              title="Mở trang quét mã đơn cho bao này"
                              className="px-2.5 py-1 rounded-lg text-[11px] font-bold bg-orange-50 text-orange-700 hover:bg-orange-100 border border-orange-200 transition flex items-center gap-1"
                            >
                              <Scan className="w-3 h-3 text-orange-600" />
                              <span>Quét tiếp</span>
                            </button>
                          )}

                          {/* In phiếu Bill */}
                          <button
                            onClick={() => setSelectedTOForBill(item)}
                            title="In phiếu Bill TO"
                            className="p-1.5 rounded-lg text-slate-500 hover:text-slate-900 hover:bg-slate-100 border border-transparent hover:border-slate-200 transition"
                          >
                            <Printer className="w-4 h-4" />
                          </button>

                          {/* Xóa bao */}
                          <button
                            onClick={() => handleDelete(item.maTO)}
                            title="Xóa bao"
                            className="p-1.5 rounded-lg text-slate-400 hover:text-rose-600 hover:bg-rose-50 border border-transparent hover:border-rose-200 transition"
                          >
                            <Trash2 className="w-4 h-4" />
                          </button>
                        </div>
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>
        </div>
      </div>

      {selectedTOForBill && (
        <BillModal
          to={selectedTOForBill}
          onClose={() => setSelectedTOForBill(null)}
        />
      )}
    </div>
  );
}