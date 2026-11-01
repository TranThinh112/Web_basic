"use client";

import { useState, useEffect } from "react";
import { Package, Search, Printer, RotateCcw, Trash2, RefreshCw } from "lucide-react";
import { api } from "@/lib/api";
import { TOOrder } from "@/types";
import BillModal from "@/components/BillModal";

export default function DanhSachTOView() {
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

  const handleReopen = async (maTO: string) => {
    if (confirm(`Mở lại bao ${maTO} để tiếp tục gom kiện?`)) {
      const ok = await api.reopenTO(maTO);
      if (ok) fetchTOs();
    }
  };

  const handleDelete = async (maTO: string) => {
    if (confirm(`Xác nhận xóa mã bao ${maTO}?`)) {
      const ok = await api.deleteTO(maTO);
      if (ok) fetchTOs();
    }
  };

  const filtered = listTO.filter((item) => {
    const matchStatus = statusFilter === "ALL" || item.trangThai === statusFilter;
    const matchSearch =
      item.maTO.toLowerCase().includes(searchTerm.toLowerCase()) ||
      item.diaDiemGiaoHang.toLowerCase().includes(searchTerm.toLowerCase()) ||
      item.packer.toLowerCase().includes(searchTerm.toLowerCase());
    return matchStatus && matchSearch;
  });

  return (
    <div className="space-y-5">
      {/* Top Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3 bg-white p-5 rounded-xl border border-slate-200">
        <div>
          <h2 className="text-base font-semibold text-slate-900 flex items-center gap-2">
            <Package className="w-5 h-5 text-slate-700" />
            Danh sách bao hàng (Transfer Orders)
          </h2>
          <p className="text-xs text-slate-500 mt-0.5">
            Quản lý toàn bộ danh sách bao hàng TO tạo tại trạm Hub
          </p>
        </div>

        <button
          onClick={fetchTOs}
          className="px-3 py-1.5 bg-white hover:bg-slate-50 border border-slate-200 text-slate-700 text-xs font-medium rounded-lg transition flex items-center gap-1.5 self-start sm:self-auto"
        >
          <RefreshCw className={`w-3.5 h-3.5 ${loading ? "animate-spin" : ""}`} />
          Làm mới
        </button>
      </div>

      {/* Filter & Search */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
        <div className="flex items-center gap-1.5">
          {["ALL", "Packing", "Complete"].map((st) => (
            <button
              key={st}
              onClick={() => setStatusFilter(st)}
              className={`px-3 py-1.5 rounded-lg text-xs transition ${
                statusFilter === st
                  ? "bg-slate-900 text-white font-medium"
                  : "bg-white text-slate-600 hover:bg-slate-100 border border-slate-200"
              }`}
            >
              {st === "ALL" ? "Tất cả" : st === "Packing" ? "Đang gom" : "Đã đóng bao"}
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
      <div className="bg-white rounded-xl border border-slate-200 overflow-hidden">
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
                  const pkgs: string[] = Array.isArray(item.danhSachGoiHang)
                    ? item.danhSachGoiHang
                    : JSON.parse(typeof item.danhSachGoiHang === "string" ? item.danhSachGoiHang : "[]");

                  return (
                    <tr key={item.maTO} className="hover:bg-slate-50/70">
                      <td className="px-4 py-3 font-mono font-semibold text-slate-900">{item.maTO}</td>
                      <td className="px-4 py-3 font-medium text-slate-800">{item.diaDiemGiaoHang}</td>
                      <td className="px-4 py-3 text-center">{pkgs.length}</td>
                      <td className="px-4 py-3 text-right font-mono">{Number(item.totalWeight || 0).toFixed(2)}</td>
                      <td className="px-4 py-3">
                        <span className="text-[11px] bg-slate-100 border border-slate-200 text-slate-700 px-2 py-0.5 rounded">
                          {item.trangThai === "Complete" ? "Đã đóng bao" : "Đang gom hàng"}
                        </span>
                      </td>
                      <td className="px-4 py-3 text-slate-500">{item.packer}</td>
                      <td className="px-4 py-3 text-[11px] text-slate-400">
                        {item.ngayTao ? new Date(item.ngayTao).toLocaleDateString("vi-VN") : "---"}
                      </td>
                      <td className="px-4 py-3 text-right">
                        <div className="flex items-center justify-end gap-1">
                          <button
                            onClick={() => setSelectedTOForBill(item)}
                            title="In phiếu Bill"
                            className="p-1 rounded text-slate-500 hover:text-slate-900 hover:bg-slate-100"
                          >
                            <Printer className="w-4 h-4" />
                          </button>
                          {item.trangThai === "Complete" && (
                            <button
                              onClick={() => handleReopen(item.maTO)}
                              title="Mở lại bao"
                              className="p-1 rounded text-slate-500 hover:text-slate-900 hover:bg-slate-100"
                            >
                              <RotateCcw className="w-4 h-4" />
                            </button>
                          )}
                          <button
                            onClick={() => handleDelete(item.maTO)}
                            title="Xóa bao"
                            className="p-1 rounded text-slate-400 hover:text-slate-700 hover:bg-slate-100"
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
        <BillModal to={selectedTOForBill} onClose={() => setSelectedTOForBill(null)} />
      )}
    </div>
  );
}
