"use client";

import { useState } from "react";
import { Search, Package } from "lucide-react";
import { api } from "@/lib/api";
import { Order } from "@/types";

export default function TraCuuView() {
  const [trackingId, setTrackingId] = useState("");
  const [order, setOrder] = useState<Order | null>(null);
  const [searched, setSearched] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSearch = async () => {
    const query = trackingId.trim();
    if (!query) return;

    setLoading(true);
    setSearched(true);
    try {
      const result = await api.getOrderById(query);
      setOrder(result);
    } catch {
      setOrder(null);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="space-y-5 max-w-2xl mx-auto">
      <div className="bg-white p-5 rounded-xl border border-slate-200">
        <h2 className="text-base font-semibold text-slate-900 flex items-center gap-2">
          <Search className="w-5 h-5 text-slate-700" />
          Tra cứu vận đơn
        </h2>
        <p className="text-xs text-slate-500 mt-0.5">
          Nhập mã bưu kiện SPX để kiểm tra thông tin và bao hàng chứa đơn
        </p>

        <div className="mt-4 flex gap-2">
          <input
            type="text"
            value={trackingId}
            onChange={(e) => setTrackingId(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && handleSearch()}
            placeholder="Nhập mã SPXVN..."
            className="flex-1 px-3.5 py-2 rounded-lg border border-slate-200 text-xs font-mono font-medium outline-none focus:border-slate-400 uppercase"
          />
          <button
            onClick={handleSearch}
            disabled={loading || !trackingId.trim()}
            className="px-4 py-2 bg-slate-900 hover:bg-slate-800 disabled:bg-slate-200 text-white rounded-lg text-xs font-medium transition"
          >
            {loading ? "Đang tìm..." : "Tra cứu"}
          </button>
        </div>
      </div>

      {searched && (
        <div>
          {order ? (
            <div className="bg-white rounded-xl p-5 border border-slate-200 space-y-4 text-xs">
              <div className="flex justify-between items-center pb-3 border-b border-slate-100">
                <div>
                  <span className="text-[11px] text-slate-400">Mã đơn:</span>
                  <div className="text-base font-bold font-mono text-slate-900">{order.id}</div>
                </div>
                <span className="px-2 py-0.5 rounded bg-slate-100 border border-slate-200 text-slate-700 font-medium">
                  {order.trangThai}
                </span>
              </div>

              <div className="grid grid-cols-2 gap-3 text-slate-600">
                <div>
                  <span className="text-slate-400 block">Nơi nhận:</span>
                  <span className="font-medium text-slate-800">{order.noiNhan || "---"}</span>
                </div>
                <div>
                  <span className="text-slate-400 block">Bao hàng gán (TO):</span>
                  <span className="font-mono font-medium text-slate-800">{order.maTO || "Chưa đóng bao"}</span>
                </div>
                <div>
                  <span className="text-slate-400 block">Khối lượng:</span>
                  <span className="font-medium text-slate-800">{order.soKi || "0.5"} kg</span>
                </div>
                <div>
                  <span className="text-slate-400 block">Tiền thu hộ COD:</span>
                  <span className="font-medium text-slate-800">{order.cod ? `${order.cod} đ` : "0 đ"}</span>
                </div>
              </div>
            </div>
          ) : (
            <div className="bg-white rounded-xl p-8 text-center border border-slate-200 text-slate-400 text-xs">
              Không tìm thấy vận đơn &quot;{trackingId}&quot;
            </div>
          )}
        </div>
      )}
    </div>
  );
}
