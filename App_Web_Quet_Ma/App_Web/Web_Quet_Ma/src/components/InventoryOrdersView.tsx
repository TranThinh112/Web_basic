"use client";

import { useState, useEffect, useCallback } from "react";
import {
  Package,
  Search,
  RefreshCw,
  Filter,
  Eye,
  ChevronLeft,
  ChevronRight,
  Boxes,
  CheckCircle2,
  XCircle,
} from "lucide-react";
import { api } from "@/lib/api";
import { Order } from "@/types";
import OrderBillModal from "@/components/OrderBillModal";

export default function InventoryOrdersView() {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(true);
  const [totalOrders, setTotalOrders] = useState(0);
  const [inboundCount, setInboundCount] = useState(0);

  // Bộ lọc & phân trang
  const [page, setPage] = useState(1);
  const [limit, setLimit] = useState(15);
  const [keyword, setKeyword] = useState("");
  const [searchInput, setSearchInput] = useState("");
  const [statusFilter, setStatusFilter] = useState<string>("ALL");

  const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);

  // Lấy giá trị linh hoạt hỗ trợ cả camelCase lẫn lowercase từ database/API
  const getOrderField = (o: any, ...keys: string[]) => {
    if (!o) return "";
    for (const k of keys) {
      if (o[k] !== undefined && o[k] !== null && String(o[k]).trim() !== "") {
        return o[k];
      }
    }
    return "";
  };

  const fetchOrders = useCallback(async () => {
    setLoading(true);
    try {
      const res = await api.getOrders({
        page,
        limit,
        keyword: keyword || undefined,
        trangThai: statusFilter === "ALL" ? undefined : statusFilter,
      });

      const rawList = res.data || [];

      // Sort ưu tiên:
      // 1. Đơn Inbound lên trước: sắp xếp theo thời gian đóng bao (thoiGianDongBao) hoặc ngày tạo mới nhất
      // 2. Đơn Outbound xuống sau: sắp xếp theo thời gian tạo (thoiGianTao) mới nhất
      const isOrderInbound = (o: any) => {
        const st = String(getOrderField(o, "trangThai", "trangthai") || "").trim().toLowerCase();
        const maTO = getOrderField(o, "maTO", "mato");
        return st === "inbound" || Boolean(maTO);
      };

      const sortedList = [...rawList].sort((a: any, b: any) => {
        const isInboundA = isOrderInbound(a);
        const isInboundB = isOrderInbound(b);

        if (isInboundA && !isInboundB) return -1;
        if (!isInboundA && isInboundB) return 1;

        if (isInboundA && isInboundB) {
          const timeA = new Date(getOrderField(a, "thoiGianDongBao", "thoigiandongbao") || getOrderField(a, "thoiGianTao", "thoigiantao", "createdAt") || 0).getTime();
          const timeB = new Date(getOrderField(b, "thoiGianDongBao", "thoigiandongbao") || getOrderField(b, "thoiGianTao", "thoigiantao", "createdAt") || 0).getTime();
          return timeB - timeA;
        }

        const timeA = new Date(getOrderField(a, "thoiGianTao", "thoigiantao", "createdAt") || 0).getTime();
        const timeB = new Date(getOrderField(b, "thoiGianTao", "thoigiantao", "createdAt") || 0).getTime();
        return timeB - timeA;
      });

      setOrders(sortedList);
      setTotalOrders(res.total || 0);
      setInboundCount(res.inbound || 0);
    } catch (err) {
      console.error("Lỗi tải danh sách kho:", err);
      setOrders([]);
    } finally {
      setLoading(false);
    }
  }, [page, limit, keyword, statusFilter]);

  useEffect(() => {
    fetchOrders();
  }, [fetchOrders]);

  const handleSearchSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setPage(1);
    setKeyword(searchInput.trim());
  };

  const handleClearSearch = () => {
    setSearchInput("");
    setKeyword("");
    setPage(1);
  };

  const totalPages = Math.ceil(totalOrders / limit) || 1;

  // Hiển thị nhãn chỉ gồm: "Inbound" hoặc "Outbound"
  const getStatusBadge = (status?: string | null, maTO?: string | null) => {
    const s = (status || "").trim().toLowerCase();
    const isInbound = s === "inbound" || Boolean(maTO);
    if (isInbound) {
      return (
        <span className="inline-flex items-center gap-1 px-2 py-0.5 rounded-full text-[11px] font-bold bg-emerald-50 text-emerald-700 border border-emerald-300">
          <CheckCircle2 className="w-3 h-3 text-emerald-600" />
          Inbound
        </span>
      );
    }
    return (
      <span className="inline-flex items-center gap-1 px-2 py-0.5 rounded-full text-[11px] font-bold bg-red-50 text-red-700 border border-red-300">
        <XCircle className="w-3 h-3 text-red-600" />
        Outbound
      </span>
    );
  };

  // Format chi tiết ngày giờ DD/MM/YYYY HH:mm:ss
  const formatDateTime = (dateStr?: string | null) => {
    if (!dateStr) return "—";
    const d = new Date(dateStr);
    if (isNaN(d.getTime())) return "—";
    const pad = (n: number) => String(n).padStart(2, "0");
    const date = `${pad(d.getDate())}/${pad(d.getMonth() + 1)}/${d.getFullYear()}`;
    const time = `${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
    return { date, time };
  };

  return (
    <div className="space-y-4 w-full">
      {/* HEADER & THỐNG KÊ NHANH TẠI KHO */}
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-3">
        <div className="bg-white p-3.5 rounded-xl border border-orange-100 shadow-xs flex items-center gap-3">
          <div className="w-10 h-10 rounded-lg bg-orange-50 text-orange-600 flex items-center justify-center font-bold shrink-0">
            <Boxes className="w-5 h-5" />
          </div>
          <div>
            <p className="text-[10px] text-slate-500 font-semibold uppercase tracking-wider">Tổng đơn trong kho</p>
            <p className="text-lg font-black text-slate-900">{totalOrders.toLocaleString()} <span className="text-xs font-normal text-slate-500">kiện</span></p>
          </div>
        </div>

        <div className="bg-white p-3.5 rounded-xl border border-orange-100 shadow-xs flex items-center gap-3">
          <div className="w-10 h-10 rounded-lg bg-emerald-50 text-emerald-600 flex items-center justify-center font-bold shrink-0">
            <CheckCircle2 className="w-5 h-5" />
          </div>
          <div>
            <p className="text-[10px] text-slate-500 font-semibold uppercase tracking-wider">Inbound (Đã đóng bao)</p>
            <p className="text-lg font-black text-emerald-700">{inboundCount.toLocaleString()} <span className="text-xs font-normal text-slate-500">kiện</span></p>
          </div>
        </div>

        <div className="bg-white p-3.5 rounded-xl border border-orange-100 shadow-xs flex items-center gap-3">
          <div className="w-10 h-10 rounded-lg bg-red-50 text-red-600 flex items-center justify-center font-bold shrink-0">
            <XCircle className="w-5 h-5" />
          </div>
          <div>
            <p className="text-[10px] text-slate-500 font-semibold uppercase tracking-wider">Outbound (Chưa vào bao)</p>
            <p className="text-lg font-black text-red-700">
              {Math.max(0, totalOrders - inboundCount).toLocaleString()} <span className="text-xs font-normal text-slate-500">kiện</span>
            </p>
          </div>
        </div>
      </div>

      {/* THANH CÔNG CỤ TÌM KIẾM VÀ LỌC */}
      <div className="bg-white p-3.5 rounded-xl border border-orange-100 shadow-xs">
        <div className="flex flex-col md:flex-row items-stretch md:items-center justify-between gap-2.5">
          {/* Form tìm kiếm */}
          <form onSubmit={handleSearchSubmit} className="flex-1 flex items-center gap-2">
            <div className="relative flex-1">
              <Search className="w-4 h-4 text-slate-400 absolute left-3 top-1/2 -translate-y-1/2" />
              <input
                type="text"
                placeholder="Tìm mã SPX, người nhận, người gửi, nơi đến..."
                value={searchInput}
                onChange={(e) => setSearchInput(e.target.value)}
                className="w-full pl-8.5 pr-8 py-1.5 bg-slate-50 hover:bg-slate-100/70 focus:bg-white border border-slate-200 rounded-lg text-xs font-medium focus:outline-none focus:border-orange-500 focus:ring-1 focus:ring-orange-500/20 transition-all"
              />
              {searchInput && (
                <button
                  type="button"
                  onClick={handleClearSearch}
                  className="absolute right-2.5 top-1/2 -translate-y-1/2 text-slate-400 hover:text-slate-600 text-xs cursor-pointer"
                >
                  ✕
                </button>
              )}
            </div>
            <button
              type="submit"
              className="px-3.5 py-1.5 bg-orange-600 hover:bg-orange-700 text-white rounded-lg text-xs font-bold transition shadow-xs cursor-pointer shrink-0"
            >
              Tìm kiếm
            </button>
          </form>

          {/* Lọc trạng thái & Reload */}
          <div className="flex items-center gap-2 shrink-0">
            <div className="flex items-center gap-1.5 bg-slate-50 border border-slate-200 px-2.5 py-1.5 rounded-lg text-xs">
              <Filter className="w-3.5 h-3.5 text-slate-500" />
              <span className="text-slate-500 font-semibold text-[11px]">Trạng thái:</span>
              <select
                value={statusFilter}
                onChange={(e) => {
                  setStatusFilter(e.target.value);
                  setPage(1);
                }}
                className="bg-transparent font-bold text-slate-800 outline-none cursor-pointer text-xs"
              >
                <option value="ALL">Tất cả ({totalOrders})</option>
                <option value="Inbound">Inbound</option>
                <option value="Outbound">Outbound</option>
              </select>
            </div>

            <button
              onClick={() => fetchOrders()}
              disabled={loading}
              title="Làm mới dữ liệu"
              className="p-1.5 rounded-lg border border-slate-200 hover:bg-orange-50 hover:text-orange-600 text-slate-600 transition cursor-pointer"
            >
              <RefreshCw className={`w-4 h-4 ${loading ? "animate-spin text-orange-600" : ""}`} />
            </button>
          </div>
        </div>
      </div>

      {/* BẢNG DỮ LIỆU ĐƠN HÀNG - THIẾT KẾ ĐẦY ĐỦ TRÊN MỘT MÀN HÌNH KHÔNG CẦN CUỘN NGANG */}
      <div className="bg-white rounded-xl border border-orange-100 shadow-xs overflow-hidden">
        <table className="w-full text-left border-collapse text-[11px] table-fixed">
          <thead>
            <tr className="bg-orange-50/70 border-b border-orange-100 text-slate-700 font-bold uppercase tracking-wider text-[10px]">
              <th className="py-2.5 px-2.5 w-[38px] text-center">STT</th>
              <th className="py-2.5 px-2.5 w-[140px]">MÃ VẬN ĐƠN</th>
              <th className="py-2.5 px-2 w-[95px] text-center">TRẠNG THÁI</th>
              <th className="py-2.5 px-2 w-[90px] text-center">MÃ TO</th>
              <th className="py-2.5 px-2.5 w-[140px]">NƠI ĐI / NƠI ĐẾN</th>
              <th className="py-2.5 px-2.5 w-[180px]">NGƯỜI GỬI / NHẬN</th>
              <th className="py-2.5 px-2 w-[65px] text-right">KG</th>
              <th className="py-2.5 px-2 w-[85px] text-right">COST</th>
              <th className="py-2.5 px-2 w-[125px] text-center">TIME CREATE</th>
              <th className="py-2.5 px-2 w-[46px] text-center">XEM</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-100">
            {loading ? (
              <tr>
                <td colSpan={10} className="py-10 text-center text-slate-400">
                  <RefreshCw className="w-5 h-5 animate-spin mx-auto text-orange-500 mb-2" />
                  Đang tải dữ liệu đơn hàng từ kho...
                </td>
              </tr>
            ) : orders.length === 0 ? (
              <tr>
                <td colSpan={10} className="py-10 text-center text-slate-400">
                  <Package className="w-6 h-6 mx-auto text-slate-300 mb-1" />
                  Không tìm thấy đơn hàng nào phù hợp.
                </td>
              </tr>
            ) : (
              orders.map((order, idx) => {
                const maDon = getOrderField(order, "id");
                const trangThai = getOrderField(order, "trangThai", "trangthai");
                const maTO = getOrderField(order, "maTO", "mato");
                const noiGui = getOrderField(order, "noiGui", "noigui") || "HCM";
                const noiNhan = getOrderField(order, "noiNhan", "noinhan") || "---";
                const nguoiGui = getOrderField(order, "nguoiGui", "nguoigui") || "---";
                const nguoiNhan = getOrderField(order, "nguoiNhan", "nguoinhan") || "---";
                const diaChiNhan = getOrderField(order, "diaChiNhan", "diachinhan");
                const soKi = Number(getOrderField(order, "soKi", "soki")) || 0;
                const giaTien = Number(getOrderField(order, "giaTien", "giatien", "cod")) || 0;
                const thoiGianTaoRaw = getOrderField(order, "thoiGianTao", "thoigiantao", "createdAt");
                const dt = formatDateTime(thoiGianTaoRaw);

                return (
                  <tr key={maDon || idx} className="hover:bg-orange-50/40 transition">
                    {/* STT */}
                    <td className="py-2.5 px-2.5 text-center text-slate-400 font-medium">
                      {(page - 1) * limit + idx + 1}
                    </td>

                    {/* MÃ VẬN ĐƠN */}
                    <td className="py-2.5 px-2.5 font-mono font-bold text-orange-600 truncate" title={maDon}>
                      {maDon}
                    </td>

                    {/* TRẠNG THÁI */}
                    <td className="py-2.5 px-2 text-center">
                      {getStatusBadge(trangThai, maTO)}
                    </td>

                    {/* MÃ TO: CHỈ HIỂN THỊ VỚI ĐƠN ĐÃ VÀO BAO */}
                    <td className="py-2.5 px-2 text-center">
                      {maTO ? (
                        <span className="font-mono font-bold text-slate-800 bg-slate-100 px-1.5 py-0.5 rounded text-[10px] border border-slate-200">
                          {maTO}
                        </span>
                      ) : (
                        <span className="text-slate-300 font-light">—</span>
                      )}
                    </td>

                    {/* NƠI ĐI / NƠI ĐẾN */}
                    <td className="py-2.5 px-2.5">
                      <div className="flex items-center gap-1 font-bold text-slate-900 leading-tight">
                        <span className="px-1 py-0.2 bg-orange-100 text-orange-700 rounded text-[9.5px] font-mono shrink-0">
                          {noiGui}
                        </span>
                        <span className="text-slate-400 text-[9px]">➔</span>
                        <span className="px-1 py-0.2 bg-emerald-100 text-emerald-700 rounded text-[9.5px] font-mono shrink-0">
                          {noiNhan}
                        </span>
                      </div>
                      {diaChiNhan && (
                        <div className="text-[9.5px] text-slate-400 truncate mt-0.5" title={diaChiNhan}>
                          {diaChiNhan}
                        </div>
                      )}
                    </td>

                    {/* NGƯỜI GỬI / NGƯỜI NHẬN NẰM CÙNG HÀNG */}
                    <td className="py-2.5 px-2.5 truncate" title={`Gửi: ${nguoiGui} | Nhận: ${nguoiNhan}`}>
                      <span className="text-slate-500 font-medium">{nguoiGui}</span>
                      <span className="text-slate-400 mx-1">/</span>
                      <span className="font-bold text-slate-900">{nguoiNhan}</span>
                    </td>

                    {/* KG */}
                    <td className="py-2.5 px-2 text-right font-semibold text-slate-700">
                      {soKi.toFixed(2)}
                    </td>

                    {/* COST */}
                    <td className="py-2.5 px-2 text-right font-bold text-slate-900">
                      {giaTien.toLocaleString()} đ
                    </td>

                    {/* TIME CREATE (CHI TIẾT NGÀY GIỜ) */}
                    <td className="py-2.5 px-2 text-center text-slate-600 font-mono text-[10.5px]">
                      {typeof dt === "object" ? (
                        <div>
                          <div className="font-semibold text-slate-800">{dt.date}</div>
                          <div className="text-[9.5px] text-slate-400">{dt.time}</div>
                        </div>
                      ) : (
                        "—"
                      )}
                    </td>

                    {/* XEM MÃ ĐƠN (CHỈ HIỆN ICON MẮT) */}
                    <td className="py-2.5 px-2 text-center">
                      <button
                        onClick={() => setSelectedOrder(order)}
                        title="Xem chi tiết mã đơn"
                        className="inline-flex items-center justify-center w-7 h-7 rounded-lg bg-orange-100 text-orange-700 hover:bg-orange-600 hover:text-white transition shadow-2xs cursor-pointer"
                      >
                        <Eye className="w-3.5 h-3.5" />
                      </button>
                    </td>
                  </tr>
                );
              })
            )}
          </tbody>
        </table>

        {/* PHÂN TRANG GỌN GÀNG */}
        <div className="p-3 border-t border-slate-100 flex flex-col sm:flex-row items-center justify-between gap-2.5 text-xs text-slate-500">
          <div className="flex items-center gap-1.5 text-[11px]">
            <span>Hiển thị</span>
            <select
              value={limit}
              onChange={(e) => {
                setLimit(Number(e.target.value));
                setPage(1);
              }}
              className="bg-slate-50 border border-slate-200 rounded px-1.5 py-0.5 font-bold text-slate-700 cursor-pointer text-xs"
            >
              <option value={10}>10</option>
              <option value={15}>15</option>
              <option value={30}>30</option>
              <option value={50}>50</option>
            </select>
            <span>/ trang • Tổng cộng {totalOrders} đơn</span>
          </div>

          <div className="flex items-center gap-1">
            <button
              onClick={() => setPage((p) => Math.max(1, p - 1))}
              disabled={page <= 1}
              className="p-1 rounded border border-slate-200 disabled:opacity-30 disabled:cursor-not-allowed hover:bg-orange-50 hover:text-orange-600 cursor-pointer"
            >
              <ChevronLeft className="w-3.5 h-3.5" />
            </button>
            <span className="px-2 py-0.5 font-bold text-slate-800 bg-orange-50 text-orange-700 rounded text-xs border border-orange-200">
              {page} / {totalPages}
            </span>
            <button
              onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
              disabled={page >= totalPages}
              className="p-1 rounded border border-slate-200 disabled:opacity-30 disabled:cursor-not-allowed hover:bg-orange-50 hover:text-orange-600 cursor-pointer"
            >
              <ChevronRight className="w-3.5 h-3.5" />
            </button>
          </div>
        </div>
      </div>

      {/* MODAL XEM CHI TIẾT PHIẾU ĐƠN */}
      {selectedOrder && (
        <OrderBillModal
          order={selectedOrder}
          onClose={() => setSelectedOrder(null)}
        />
      )}
    </div>
  );
}
