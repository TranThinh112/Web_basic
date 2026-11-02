"use client";

import { useState } from "react";
import { PlusCircle, CheckCircle2, User, Send, Package, Eye, RefreshCw, Copy, Check } from "lucide-react";
import { api } from "@/lib/api";
import { Order } from "@/types";
import { sounds } from "@/lib/sound";
import { HUB_MAP, getRegionFromAddress, generateSPXCode } from "@/lib/sorting";
import OrderBillModal from "@/components/OrderBillModal";

export default function TaoDonView() {
  // Thông tin người nhận
  const [nguoiNhan, setNguoiNhan] = useState("Nguyễn Văn A");
  const [sdtNhan, setSdtNhan] = useState("0912345678");
  const [diaChiNhan, setDiaChiNhan] = useState("123 Cầu Giấy, Hà Nội");
  const [hubDich, setHubDich] = useState("HN"); // Tự động mapping

  // Thông tin người gửi
  const [nguoiGui, setNguoiGui] = useState("Kho Tổng SPX");
  const [sdtGui, setSdtGui] = useState("0987654321");
  const [diaChiGui, setDiaChiGui] = useState("Kho Phân Loại Tân Bình, TP.HCM");

  // Hàng hóa & Cước
  const [sanPham, setSanPham] = useState("Quần áo thời trang");
  const [soKi, setSoKi] = useState("0.5");
  const [giaTien, setGiaTien] = useState("50000");

  const [loading, setLoading] = useState(false);
  const [createdOrder, setCreatedOrder] = useState<Order | null>(null);
  const [showBillModal, setShowBillModal] = useState(false);
  const [copied, setCopied] = useState(false);

  // Tự động phân tích Hub khi gõ địa chỉ
  const handleAddressChange = (address: string) => {
    setDiaChiNhan(address);
    if (address.trim().length >= 2) {
      const detectedHub = getRegionFromAddress(address);
      if (HUB_MAP[detectedHub]) {
        setHubDich(detectedHub);
      }
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!nguoiNhan.trim() || !diaChiNhan.trim()) {
      alert("Vui lòng điền đầy đủ tên và địa chỉ người nhận!");
      return;
    }

    setLoading(true);

    // Tự động sinh mã SPX ngẫu nhiên cho đơn hàng mới
    const newId = generateSPXCode();

    // Tự động định tuyến vùng gửi và nhận
    const noiGui = getRegionFromAddress(diaChiGui) || "HCM";
    const noiNhan = getRegionFromAddress(diaChiNhan) || hubDich;

    const diaChiNhanFull = sdtNhan.trim()
      ? `[SĐT: ${sdtNhan.trim()}] ${diaChiNhan.trim()}`
      : diaChiNhan.trim();

    const diaChiGuiFull = sdtGui.trim()
      ? `[SĐT: ${sdtGui.trim()}] ${diaChiGui.trim()}`
      : diaChiGui.trim();

    const numericGiaTien = parseInt(giaTien.replace(/\D/g, "")) || 0;
    const numericSoKi = parseFloat(soKi) || 0.5;

    const newOrder: Partial<Order> = {
      id: newId,
      noiGui,
      noiNhan,
      nguoiGui: nguoiGui.trim(),
      nguoiNhan: nguoiNhan.trim(),
      diaChiGui: diaChiGuiFull,
      diaChiNhan: diaChiNhanFull,
      sanPham: sanPham.trim(),
      soKi: numericSoKi,
      giaTien: numericGiaTien,
      cod: numericGiaTien,
      trangThai: "Outbound",
      thoiGianTao: new Date().toISOString(),
    };

    try {
      const created = await api.createOrder(newOrder);
      const finalOrder = created || (newOrder as Order);
      setCreatedOrder(finalOrder);
      setCopied(false);
      sounds.playSuccess();
    } catch {
      sounds.playError();
      alert("Lỗi khi kết nối Backend tạo đơn!");
    } finally {
      setLoading(false);
    }
  };

  // Copy mã đơn vào Clipboard
  const handleCopyCode = () => {
    if (!createdOrder) return;
    navigator.clipboard.writeText(createdOrder.id);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  // Quay lại tạo đơn mới
  const handleCreateAnother = () => {
    setCreatedOrder(null);
    setCopied(false);
    setNguoiNhan("");
    setSdtNhan("");
    setDiaChiNhan("");
    setSanPham("");
    setGiaTien("");
  };

  return (
    <div className="max-w-4xl mx-auto space-y-3">
      {/* Header */}
      <div className="bg-white px-4 py-3 rounded-xl border border-slate-200/80 shadow-xs flex items-center justify-between">
        <div>
          <h2 className="text-base font-bold text-slate-900 flex items-center gap-2">
            <PlusCircle className="w-4 h-4 text-orange-600" />
            Tạo mới đơn hàng
          </h2>
          <p className="text-[11px] text-slate-500">
            Hệ thống tự động sinh mã SPX và tự động phân loại Hub theo địa chỉ giao nhận
          </p>
        </div>
        {!createdOrder && (
          <div className="flex items-center gap-2">
            <span className="text-xs text-slate-500">Hub đích tự động:</span>
            <span className="px-2.5 py-0.5 bg-orange-100 text-orange-800 text-xs font-bold rounded-md border border-orange-200">
              {HUB_MAP[hubDich] || hubDich}
            </span>
          </div>
        )}
      </div>

      {/* Trạng thái 1: Khi ĐÃ TẠO XONG -> Ẩn form, chỉ hiển thị mã đơn + nhãn copy + icon hình mắt */}
      {createdOrder ? (
        <div className="bg-white p-6 rounded-2xl border border-slate-200/80 shadow-sm text-center space-y-4 animate-in fade-in duration-200">
          <div className="w-12 h-12 bg-emerald-100 text-emerald-600 rounded-full flex items-center justify-center mx-auto">
            <CheckCircle2 className="w-6 h-6" />
          </div>

          <div>
            <h3 className="text-sm font-semibold text-slate-600">Đơn hàng đã được tạo thành công!</h3>
            <div className="mt-2 inline-flex items-center gap-2.5 px-4 py-2 bg-slate-50 border border-slate-200 rounded-xl">
              <span className="text-xs text-slate-400 font-medium">Mã vận đơn:</span>
              <span className="font-mono text-base font-extrabold text-slate-900 tracking-wider">
                {createdOrder.id}
              </span>

              {/* Nhãn nút Copy thay thế chữ Hub */}
              <button
                type="button"
                onClick={handleCopyCode}
                className={`px-2.5 py-1 rounded-md text-xs font-semibold flex items-center gap-1.5 transition cursor-pointer border ${
                  copied
                    ? "bg-emerald-100 text-emerald-700 border-emerald-300"
                    : "bg-orange-50 hover:bg-orange-100 text-orange-700 border-orange-200"
                }`}
                title="Sao chép mã đơn"
              >
                {copied ? (
                  <>
                    <Check className="w-3.5 h-3.5" />
                    <span>Đã copy</span>
                  </>
                ) : (
                  <>
                    <Copy className="w-3.5 h-3.5" />
                    <span>Copy</span>
                  </>
                )}
              </button>

              {/* Nút con mắt xem UI phiếu đơn */}
              <button
                type="button"
                onClick={() => setShowBillModal(true)}
                className="p-1.5 bg-white hover:bg-orange-50 text-slate-700 hover:text-orange-600 rounded-lg border border-slate-200 hover:border-orange-300 transition shadow-xs flex items-center gap-1.5 text-xs font-semibold cursor-pointer"
                title="Xem phiếu vận đơn"
              >
                <Eye className="w-4 h-4 text-orange-600" />
                <span>Xem phiếu</span>
              </button>
            </div>
          </div>

          <p className="text-xs text-slate-500">
            Bấm vào nút <b>Copy</b> để sao chép mã đơn hoặc <b>Xem phiếu</b> để kiểm tra layout hóa đơn bưu kiện chuẩn SPX
          </p>

          <div className="pt-2">
            <button
              onClick={handleCreateAnother}
              className="px-5 py-2.5 bg-slate-900 hover:bg-slate-800 text-white rounded-xl text-xs font-bold transition inline-flex items-center gap-2 cursor-pointer"
            >
              <RefreshCw className="w-3.5 h-3.5" />
              <span>Tạo thêm đơn khác</span>
            </button>
          </div>
        </div>
      ) : (
        /* Trạng thái 2: Form nhập thông tin nhỏ gọn */
        <form onSubmit={handleSubmit} className="bg-white p-4 rounded-xl border border-slate-200/80 shadow-xs space-y-3 text-xs">
          {/* Khối 1: Thông tin người nhận */}
          <div className="space-y-2">
            <div className="flex items-center justify-between">
              <span className="font-bold text-slate-700 flex items-center gap-1.5 uppercase text-[11px] tracking-wide">
                <User className="w-3.5 h-3.5 text-orange-600" />
                1. Thông tin người nhận
              </span>
              <span className="text-[11px] text-orange-600 font-medium">
                Định tuyến: <b>{HUB_MAP[hubDich] || hubDich}</b>
              </span>
            </div>

            <div className="grid grid-cols-1 sm:grid-cols-12 gap-2">
              <div className="sm:col-span-4">
                <input
                  type="text"
                  value={nguoiNhan}
                  onChange={(e) => setNguoiNhan(e.target.value)}
                  placeholder="Tên người nhận *"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                  required
                />
              </div>
              <div className="sm:col-span-3">
                <input
                  type="tel"
                  value={sdtNhan}
                  onChange={(e) => setSdtNhan(e.target.value)}
                  placeholder="Số điện thoại nhận *"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                  required
                />
              </div>
              <div className="sm:col-span-5">
                <input
                  type="text"
                  value={diaChiNhan}
                  onChange={(e) => handleAddressChange(e.target.value)}
                  placeholder="Địa chỉ nhận (VD: 123 Cầu Giấy, Hà Nội) *"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                  required
                />
              </div>
            </div>
          </div>

          <div className="h-px bg-slate-100" />

          {/* Khối 2: Thông tin người gửi */}
          <div className="space-y-2">
            <span className="font-bold text-slate-700 flex items-center gap-1.5 uppercase text-[11px] tracking-wide">
              <Send className="w-3.5 h-3.5 text-slate-500" />
              2. Thông tin người gửi
            </span>

            <div className="grid grid-cols-1 sm:grid-cols-12 gap-2">
              <div className="sm:col-span-4">
                <input
                  type="text"
                  value={nguoiGui}
                  onChange={(e) => setNguoiGui(e.target.value)}
                  placeholder="Tên người gửi"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                />
              </div>
              <div className="sm:col-span-3">
                <input
                  type="tel"
                  value={sdtGui}
                  onChange={(e) => setSdtGui(e.target.value)}
                  placeholder="Số điện thoại gửi"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                />
              </div>
              <div className="sm:col-span-5">
                <input
                  type="text"
                  value={diaChiGui}
                  onChange={(e) => setDiaChiGui(e.target.value)}
                  placeholder="Địa chỉ gửi (Kho phân loại...)"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                />
              </div>
            </div>
          </div>

          <div className="h-px bg-slate-100" />

          {/* Khối 3: Hàng hóa & Cước */}
          <div className="space-y-2">
            <span className="font-bold text-slate-700 flex items-center gap-1.5 uppercase text-[11px] tracking-wide">
              <Package className="w-3.5 h-3.5 text-slate-500" />
              3. Hàng hóa & Cước COD
            </span>

            <div className="grid grid-cols-1 sm:grid-cols-12 gap-2">
              <div className="sm:col-span-6">
                <input
                  type="text"
                  value={sanPham}
                  onChange={(e) => setSanPham(e.target.value)}
                  placeholder="Tên kiện hàng / Sản phẩm *"
                  className="w-full px-3 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                  required
                />
              </div>
              <div className="sm:col-span-3">
                <div className="relative">
                  <input
                    type="number"
                    step="0.1"
                    min="0.1"
                    value={soKi}
                    onChange={(e) => setSoKi(e.target.value)}
                    placeholder="Khối lượng"
                    className="w-full pl-3 pr-7 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                    required
                  />
                  <span className="absolute right-2.5 top-1.5 text-slate-400 font-medium">kg</span>
                </div>
              </div>
              <div className="sm:col-span-3">
                <div className="relative">
                  <input
                    type="number"
                    step="1000"
                    value={giaTien}
                    onChange={(e) => setGiaTien(e.target.value)}
                    placeholder="Tiền COD"
                    className="w-full pl-3 pr-9 py-1.5 border border-slate-200 rounded-lg focus:outline-none focus:border-orange-500"
                  />
                  <span className="absolute right-2 top-1.5 text-slate-400 font-medium text-[10px]">VNĐ</span>
                </div>
              </div>
            </div>
          </div>

          {/* Nút xác nhận */}
          <div className="pt-1">
            <button
              type="submit"
              disabled={loading}
              className="w-full py-2.5 bg-slate-900 hover:bg-slate-800 active:scale-[0.99] text-white rounded-lg font-semibold text-xs transition flex items-center justify-center gap-2 cursor-pointer shadow-xs"
            >
              {loading ? (
                <span>Đang ghi nhận đơn...</span>
              ) : (
                <>
                  <CheckCircle2 className="w-4 h-4 text-emerald-400" />
                  <span>Xác nhận tạo đơn</span>
                </>
              )}
            </button>
          </div>
        </form>
      )}

      {/* Popup Modal vẽ giao diện phiếu vận đơn chuẩn BillScreen.dart */}
      {showBillModal && createdOrder && (
        <OrderBillModal
          order={createdOrder}
          onClose={() => setShowBillModal(false)}
        />
      )}
    </div>
  );
}
