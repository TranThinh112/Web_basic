"use client";

import { useState } from "react";
import { Search, AlertCircle, PackageCheck, Eye, XCircle } from "lucide-react";
import { api } from "@/lib/api";
import { Order } from "@/types";
import OrderBillModal from "@/components/OrderBillModal";

export default function TraCuuView() {
  const [inputText, setInputText] = useState("");
  const [orders, setOrders] = useState<Order[]>([]);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);
  const [searched, setSearched] = useState(false);
  const [loading, setLoading] = useState(false);
  const [selectedOrderForBill, setSelectedOrderForBill] = useState<Order | null>(null);

  const getStatusBadge = (order: Order) => {
    const st = String(order.trangThai || "").trim().toLowerCase();
    const isInbound = st === "inbound" || Boolean(order.maTO);
    if (isInbound) {
      return (
        <span className="inline-flex items-center px-2.5 py-0.5 rounded text-[10px] font-bold border bg-emerald-50 text-emerald-700 border-emerald-300">
          Inbound
        </span>
      );
    }
    return (
      <span className="inline-flex items-center px-2.5 py-0.5 rounded text-[10px] font-bold border bg-red-50 text-red-700 border-red-300">
        Outbound
      </span>
    );
  };
  // Validate format mã đơn hàng SPX: SPXVN + 10-11 ký tự chữ/số (VD: SPXVN06131459353, SPXVN06262273943)
  const isValidSPXFormat = (code: string) => {
    return /^SPXVN[0-9A-Z]{9,15}$/i.test(code);
  };

  const handleSearch = async () => {
    setErrorMsg(null);
    setOrders([]);

    // Tách các mã theo khoảng trắng, dấu phẩy, dấu chấm phẩy hoặc xuống dòng
    const rawTokens = inputText
      .split(/[\s,;]+/)
      .map((t) => t.trim().toUpperCase())
      .filter((t) => t.length > 0);

    if (rawTokens.length === 0) {
      setErrorMsg("Vui lòng nhập ít nhất một mã vận đơn để tra cứu.");
      setSearched(true);
      return;
    }

    // Loại bỏ các mã trùng lặp trong input
    const uniqueCodes = Array.from(new Set(rawTokens));

    // 1. Kiểm tra format hợp lệ
    const invalidFormatCodes = uniqueCodes.filter((code) => !isValidSPXFormat(code));
    if (invalidFormatCodes.length > 0) {
      setErrorMsg(
        "Phát hiện " + invalidFormatCodes.length + " mã đơn sai định dạng SPX: " + invalidFormatCodes.join(", ") + ". Định dạng hợp lệ phải bắt đầu bằng SPXVN..."

      );
      setSearched(true);
      return;
    }

    // 2. Tra cứu dữ liệu từ database qua API
    setLoading(true);
    setSearched(true);

    try {
      // Gọi song song lấy dữ liệu các mã đơn
      const results = await Promise.all(
        uniqueCodes.map(async (code) => {
          try {
            const data = await api.getOrderById(code);
            return { code, order: data };
          } catch {
            return { code, order: null };
          }
        })
      );

      // 3. Kiểm tra xem có mã nào không tồn tại trong database không
      const notFoundCodes = results
        .filter((item) => !item.order)
        .map((item) => item.code);

      if (notFoundCodes.length > 0) {
        setErrorMsg(
          "Không tìm thấy dữ liệu cho " + notFoundCodes.length + " mãđơn trong hệ thống: " + notFoundCodes.join(", ") + ". Yêu cầu tất cả các mã phải tồn tại mới hiển thị kết quả."

        );
        setOrders([]);
        return;
      }

      // 4. Tất cả đều hợp lệ và tồn tại -> Hiển thị danh sách kết quả
      const validOrders = results
        .map((item) => item.order)
        .filter((order): order is Order => order !== null);

      setOrders(validOrders);
      setErrorMsg(null);
    } catch {
      setErrorMsg("Đã xảy ra lỗi trong quá trình tra cứu. Vui lòng thử lại!");
      setOrders([]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="space-y-6 max-w-6xl mx-auto pb-12 font-sans">
      {/* SEARCH BOX */}
      <div className="bg-white p-5 rounded-2xl border border-slate-200/80 shadow-sm print:hidden">
        <div className="flex items-center gap-2.5 mb-2">
          <div className="w-8 h-8 rounded-lg bg-orange-50 border border-orange-200 flex items-center justify-center text-orange-600">
            <Search className="w-4 h-4" />
          </div>
          <div>
            <h2 className="text-sm font-semibold text-slate-900">Tra cứu Đơn hàng SPX</h2>
            <p className="text-xs text-slate-500">
              Nhập một hoặc nhiều mã vận đơn (cách nhau bởi khoảng trắng, phẩy hoặc xuống dòng).
            </p>
          </div>
        </div>

        <div className="mt-4 flex flex-col sm:flex-row gap-2.5">
          <textarea
            rows={2}
            value={inputText}
            onChange={(e) => setInputText(e.target.value)}
            onKeyDown={(e) => {
              if (e.key === "Enter" && !e.shiftKey) {
                e.preventDefault();
                handleSearch();
              }
            }}
            placeholder="Nhập các mã SPXVN... (VD: SPXVN06131459353 SPXVN06137534403)"
            className="flex-1 px-4 py-2.5 rounded-xl border border-slate-200 text-xs font-mono font-medium outline-none focus:border-orange-500 focus:ring-2 focus:ring-orange-100 uppercase transition bg-slate-50/50 resize-none"
          />
          <button
            onClick={handleSearch}
            disabled={loading || !inputText.trim()}
            className="sm:w-32 px-6 py-2.5 bg-orange-600 hover:bg-orange-700 disabled:bg-slate-200 text-white rounded-xl text-xs font-medium transition shadow-sm flex items-center justify-center gap-1.5 self-stretch sm:self-auto"
          >
            {loading ? "Đang tìm..." : "Tra cứu"}
          </button>
        </div>
      </div>

      {searched && (
        <div>
          {/* TRƯỜNG HỢP CÓ LỖI (MÃ SAI FORMAT HOẶC CÓ MÃ KHÔNG TỒN TẠI) */}
          {errorMsg && (
            <div className="bg-white rounded-2xl p-8 text-center border border-red-200/90 shadow-sm max-w-2xl mx-auto space-y-3">
              <div className="w-12 h-12 rounded-full bg-red-50 text-red-500 flex items-center justify-center mx-auto">
                <XCircle className="w-6 h-6" />
              </div>
              <h3 className="text-sm font-bold text-red-600">Tra cứu không thành công</h3>
              <p className="text-xs text-slate-600 leading-relaxed font-medium">
                {errorMsg}
              </p>
              <div className="text-[11px] text-slate-400 italic">
                * Lưu ý: Hệ thống chỉ hiển thị kết quả khi tất cả các mã đơn đều đúng định dạng và có trong cơ sở dữ liệu.
              </div>
            </div>
          )}

          {/* TRƯỜNG HỢP TẤT CẢ CÁC MÃ ĐỀU HỢP LỆ VÀ CÓ TRONG DATABASE */}
          {!errorMsg && orders.length > 0 && (
            <div className="space-y-6">
              <div className="bg-white rounded-2xl border border-orange-200/70 shadow-sm overflow-hidden print:hidden">
                <div className="px-5 py-3.5 bg-orange-50/60 border-b border-orange-100 flex items-center justify-between">
                  <div className="flex items-center gap-2">
                    <PackageCheck className="w-4 h-4 text-orange-600" />
                    <span className="text-xs font-bold text-slate-900">
                      Kết quả tra cứu ({orders.length} đơn hàng)
                    </span>
                  </div>
                  <span className="text-[11px] font-semibold text-emerald-700 bg-emerald-100 px-2.5 py-0.5 rounded-full border border-emerald-200">
                    Tất cả mã hợp lệ & tồn tại
                  </span>
                </div>

                <div className="overflow-x-auto">
                  <table className="w-full text-left text-xs text-slate-700">
                    <thead className="bg-slate-50 border-b border-slate-200 text-slate-600 font-semibold uppercase text-[11px] tracking-wider">
                      <tr>
                        <th className="px-4 py-3 whitespace-nowrap">STT</th>
                        <th className="px-4 py-3 whitespace-nowrap">Mã đơn</th>
                        <th className="px-4 py-3 whitespace-nowrap">Trạng thái</th>
                        <th className="px-4 py-3 whitespace-nowrap">Sender</th>
                        <th className="px-4 py-3 whitespace-nowrap">Receiver</th>
                        <th className="px-4 py-3 min-w-[180px]">Địa chỉ gửi</th>
                        <th className="px-4 py-3 min-w-[180px]">Địa chỉ nhận</th>
                        <th className="px-4 py-3 whitespace-nowrap text-center">Sort-send</th>
                        <th className="px-4 py-3 whitespace-nowrap text-center">Sort-receive</th>
                        <th className="px-4 py-3 min-w-[150px]">Tên hàng</th>
                        <th className="px-4 py-3 whitespace-nowrap text-center">Xem Bill</th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-slate-100">
                      {orders.map((item, idx) => (
                        <tr key={item.id} className="hover:bg-orange-50/30 transition">
                          {/* STT */}
                          <td className="px-4 py-3.5 font-medium text-slate-400 text-center whitespace-nowrap">
                            {idx + 1}
                          </td>
                          {/* mã đơn */}
                          <td className="px-4 py-3.5 font-mono font-bold text-orange-600 whitespace-nowrap">
                            {item.id}
                          </td>
                          {/* trạng thái */}
                          <td className="px-4 py-3.5 whitespace-nowrap">
                            {getStatusBadge(item)}
                          </td>
                          {/* sender */}
                          <td className="px-4 py-3.5 font-medium text-slate-900 whitespace-nowrap">
                            {item.nguoiGui || "-"}
                          </td>
                          {/* receiver */}
                          <td className="px-4 py-3.5 font-medium text-slate-900 whitespace-nowrap">
                            {item.nguoiNhan || "-"}
                          </td>
                          {/* địa chỉ gửi */}
                          <td className="px-4 py-3.5 text-slate-600 leading-relaxed">
                            {item.diaChiGui || "-"}
                          </td>
                          {/* địa chỉ nhận */}
                          <td className="px-4 py-3.5 text-slate-600 leading-relaxed">
                            {item.diaChiNhan || "-"}
                          </td>
                          {/* sort-send */}
                          <td className="px-4 py-3.5 text-center whitespace-nowrap">
                            <span className="px-2.5 py-1 rounded-md font-bold text-[11px] bg-slate-100 text-slate-700 border border-slate-200">
                              {item.noiGui || "-"}
                            </span>
                          </td>
                          {/* sort-receive */}
                          <td className="px-4 py-3.5 text-center whitespace-nowrap">
                            <span className="px-2.5 py-1 rounded-md font-bold text-[11px] bg-orange-100 text-orange-700 border border-orange-200">
                              {item.noiNhan || "-"}
                            </span>
                          </td>
                          {/* tên hàng */}
                          <td className="px-4 py-3.5 font-medium text-slate-800">
                            {item.sanPham || "-"}
                          </td>
                          {/* hành động / xem bill */}
                          <td className="px-4 py-3.5 text-center whitespace-nowrap">
                            <button
                              type="button"
                              onClick={() => setSelectedOrderForBill(item)}
                              title="Xem bill đơn hàng"
                              className="inline-flex items-center justify-center p-2 rounded-lg bg-orange-50 hover:bg-orange-100 text-orange-600 border border-orange-200 transition shadow-sm"
                            >
                              <Eye className="w-4 h-4" />
                            </button>
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>

              {/* POPUP XEM BILL CỦA ĐƠN ĐƯỢC CHỌN */}
              {selectedOrderForBill && (
                <OrderBillModal
                  order={selectedOrderForBill}
                  onClose={() => setSelectedOrderForBill(null)}
                />
              )}
            </div>
          )}
        </div>
      )}
    </div>
  );
}


