"use client";

import { useState } from "react";
import { PlusCircle, Wand2, Package } from "lucide-react";
import { api } from "@/lib/api";
import { Order } from "@/types";
import { sounds } from "@/lib/sound";

export default function TaoDonView() {
  const [id, setId] = useState("");
  const [noiNhan, setNoiNhan] = useState("Hub Hà Nội");
  const [soKi, setSoKi] = useState("0.5");
  const [cod, setCod] = useState("0");
  const [nguoiNhan, setNguoiNhan] = useState("Nguyễn Văn A");
  const [sdtNhan, setSdtNhan] = useState("0912345678");

  const [loading, setLoading] = useState(false);
  const [successOrder, setSuccessOrder] = useState<Order | null>(null);

  const generateRandomSPX = () => {
    const random11Digits = Math.floor(10000000000 + Math.random() * 90000000000);
    setId("SPXVN" + random11Digits);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    const newOrder: Partial<Order> = {
      id: id.trim() || ("SPXVN" + Math.floor(10000000000 + Math.random() * 90000000000)),
      noiNhan,
      soKi: parseFloat(soKi) || 0.5,
      cod: parseFloat(cod) || 0,
      trangThai: "Outbound",
    };

    try {
      const created = await api.createOrder(newOrder);
      setSuccessOrder(created || (newOrder as Order));
      sounds.playSuccess();
      generateRandomSPX();
    } catch {
      sounds.playError();
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-2xl mx-auto space-y-5">
      <div className="bg-white p-5 rounded-xl border border-slate-200">
        <h2 className="text-base font-semibold text-slate-900 flex items-center gap-2">
          <PlusCircle className="w-5 h-5 text-slate-700" />
          Tạo mới đơn hàng
        </h2>
        <p className="text-xs text-slate-500 mt-0.5">
          Nhập thông tin bưu kiện mới vào cơ sở dữ liệu
        </p>
      </div>

      <form onSubmit={handleSubmit} className="bg-white p-5 rounded-xl border border-slate-200 space-y-4 text-xs">
        <div>
          <label className="block text-slate-600 mb-1 font-medium">Mã vận đơn</label>
          <div className="flex gap-2">
            <input
              type="text"
              value={id}
              onChange={(e) => setId(e.target.value.toUpperCase())}
              placeholder="Nhập SPXVN... hoặc bấm sinh mã"
              className="flex-1 px-3 py-2 border border-slate-200 rounded-lg font-mono"
            />
            <button
              type="button"
              onClick={generateRandomSPX}
              className="px-3 py-2 bg-slate-100 hover:bg-slate-200 text-slate-700 rounded-lg font-medium"
            >
              Sinh mã mẫu
            </button>
          </div>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-3 gap-3">
          <div>
            <label className="block text-slate-600 mb-1 font-medium">Hub đích</label>
            <select
              value={noiNhan}
              onChange={(e) => setNoiNhan(e.target.value)}
              className="w-full px-3 py-2 border border-slate-200 rounded-lg font-medium"
            >
              <option value="Hub Hà Nội">Hub Hà Nội</option>
              <option value="Hub Đà Nẵng">Hub Đà Nẵng</option>
              <option value="Hub Cần Thơ">Hub Cần Thơ</option>
              <option value="Hub Tân Bình">Hub Tân Bình</option>
            </select>
          </div>

          <div>
            <label className="block text-slate-600 mb-1 font-medium">Khối lượng (kg)</label>
            <input
              type="number"
              step="0.1"
              value={soKi}
              onChange={(e) => setSoKi(e.target.value)}
              className="w-full px-3 py-2 border border-slate-200 rounded-lg"
            />
          </div>

          <div>
            <label className="block text-slate-600 mb-1 font-medium">COD (VNĐ)</label>
            <input
              type="number"
              value={cod}
              onChange={(e) => setCod(e.target.value)}
              className="w-full px-3 py-2 border border-slate-200 rounded-lg"
            />
          </div>
        </div>

        <div className="grid grid-cols-2 gap-3 pt-2">
          <div>
            <label className="block text-slate-600 mb-1 font-medium">Tên người nhận</label>
            <input
              type="text"
              value={nguoiNhan}
              onChange={(e) => setNguoiNhan(e.target.value)}
              className="w-full px-3 py-2 border border-slate-200 rounded-lg"
            />
          </div>
          <div>
            <label className="block text-slate-600 mb-1 font-medium">Số điện thoại</label>
            <input
              type="text"
              value={sdtNhan}
              onChange={(e) => setSdtNhan(e.target.value)}
              className="w-full px-3 py-2 border border-slate-200 rounded-lg"
            />
          </div>
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full py-2.5 bg-slate-900 hover:bg-slate-800 text-white rounded-lg font-medium transition"
        >
          {loading ? "Đang lưu..." : "Xác nhận tạo đơn"}
        </button>
      </form>

      {successOrder && (
        <div className="bg-slate-50 border border-slate-200 p-3.5 rounded-xl text-xs text-slate-700">
          ✓ Đã tạo thành công đơn: <b className="font-mono">{successOrder.id}</b> ({successOrder.noiNhan})
        </div>
      )}
    </div>
  );
}
