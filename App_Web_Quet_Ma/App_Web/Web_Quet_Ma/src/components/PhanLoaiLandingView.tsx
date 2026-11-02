"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import {
  PackagePlus,
  Box,
  Layers,
  ArrowRight,
  Clock,
  Scan,
  CheckCircle2,
  ListOrdered,
  Sparkles,
  RefreshCw,
} from "lucide-react";
import { api } from "@/lib/api";
import { sounds } from "@/lib/sound";
import { TOOrder } from "@/types";

export default function PhanLoaiLandingView() {
  const router = useRouter();
  const [loading, setLoading] = useState(false);
  const [recentPackingTOs, setRecentPackingTOs] = useState<TOOrder[]>([]);
  const [loadingList, setLoadingList] = useState(false);

  // Sinh mã TO: TO + yyMMdd + 5 ký tự ngẫu nhiên
  const generateTOId = () => {
    const d = new Date();
    const yy = String(d.getFullYear()).slice(-2);
    const mm = String(d.getMonth() + 1).padStart(2, "0");
    const dd = String(d.getDate()).padStart(2, "0");
    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    let rand = "";
    for (let i = 0; i < 5; i++) {
      rand += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return `TO${yy}${mm}${dd}${rand}`;
  };

  // Tải danh sách các bao đang Packing gần đây để chọn nhanh
  const fetchActiveTOs = async () => {
    setLoadingList(true);
    try {
      const list = await api.getAllTOs();
      const active = list
        .filter((t) => t.trangThai === "Packing" || t.trangThai === "Inbound")
        .sort((a, b) => {
          const tA = a.ngayTao ? new Date(a.ngayTao).getTime() : 0;
          const tB = b.ngayTao ? new Date(b.ngayTao).getTime() : 0;
          return tB - tA;
        })
        .slice(0, 6);
      setRecentPackingTOs(active);
    } catch {
      // ignore
    } finally {
      setLoadingList(false);
    }
  };

  useEffect(() => {
    fetchActiveTOs();
  }, []);

  // Tạo bao mới và điều hướng trực tiếp sang router /TOmanagement/sorting/[maTO]
  const handleTaoBaoMoi = async () => {
    if (loading) return;
    setLoading(true);

    try {
      const newMaTO = generateTOId();
      const userStr =
        typeof window !== "undefined" ? localStorage.getItem("spx_user") : null;
      let packerName = "admin";
      if (userStr) {
        try {
          packerName = JSON.parse(userStr).username || packerName;
        } catch {}
      }

      const newTO: TOOrder = {
        maTO: newMaTO,
        danhSachGoiHang: [],
        diaDiemGiaoHang: "",
        trangThai: "Packing",
        packer: packerName,
        ngayTao: new Date().toISOString(),
        completeTime: null,
        totalWeight: 0,
      };

      await api.createTO(newTO);
      sounds.playSuccess();

      // Dùng mã TO để điều hướng router
      router.push(`/TOmanagement/sorting/${newMaTO}`);
    } catch {
      sounds.playError();
      alert("Lỗi khi tạo bao mới, vui lòng thử lại!");
      setLoading(false);
    }
  };

  return (
    <div className="max-w-4xl mx-auto space-y-6 py-4">
      {/* KHỐI HERO: NÚT TẠO BAO MỚI NỔI BẬT */}
      <div className="relative overflow-hidden bg-gradient-to-br from-orange-500 via-orange-600 to-amber-600 rounded-3xl p-8 sm:p-10 text-white shadow-xl shadow-orange-600/20">
        <div className="absolute top-0 right-0 -mr-16 -mt-16 w-64 h-64 bg-white/10 rounded-full blur-2xl pointer-events-none" />
        <div className="absolute bottom-0 right-20 -mb-10 w-48 h-48 bg-amber-400/20 rounded-full blur-xl pointer-events-none" />

        <div className="relative z-10 max-w-xl space-y-4">
          <div className="inline-flex items-center gap-2 px-3.5 py-1.5 rounded-full bg-white/15 backdrop-blur-md border border-white/20 text-xs font-semibold text-orange-100">
            <Sparkles className="w-3.5 h-3.5 text-amber-300" />
            <span>Phân loại & Gom bao trung chuyển SPX</span>
          </div>

          <h1 className="text-2xl sm:text-3xl font-extrabold tracking-tight text-white leading-tight">
            Sẵn sàng đóng bao hàng mới?
          </h1>

          <p className="text-orange-100 text-sm leading-relaxed">
            Nhấn nút bên dưới để khởi tạo bao hàng mới. Sau khi tạo xong, hệ thống sẽ tự động gán mã TO và điều hướng sang trang quét mã chuyên biệt cho bao đó.
          </p>

          <div className="pt-2 flex flex-col sm:flex-row items-stretch sm:items-center gap-3">
            <button
              onClick={handleTaoBaoMoi}
              disabled={loading}
              className="px-6 py-4 bg-white text-orange-700 hover:bg-orange-50 active:scale-95 transition rounded-2xl font-black text-sm shadow-lg shadow-black/15 flex items-center justify-center gap-2.5 cursor-pointer disabled:opacity-70"
            >
              <PackagePlus className="w-5 h-5 text-orange-600" />
              <span>{loading ? "Đang khởi tạo bao..." : "Tạo bao mới ngay"}</span>
              <ArrowRight className="w-4 h-4 text-orange-600" />
            </button>

            <button
              onClick={() => router.push("/TOmanagement/listTO")}
              className="px-5 py-4 bg-white/10 hover:bg-white/20 text-white rounded-2xl font-bold text-xs border border-white/20 transition flex items-center justify-center gap-2 cursor-pointer"
            >
              <ListOrdered className="w-4 h-4" />
              <span>Xem danh sách bao hàng</span>
            </button>
          </div>
        </div>
      </div>

      {/* KHỐI 2: CÁC BAO ĐANG PACKING (NẾU CÓ THÌ BẤM VÀO TIẾP TỤC QUÉT) */}
      <div className="bg-white rounded-2xl border border-orange-100 p-6 shadow-xs space-y-4">
        <div className="flex items-center justify-between pb-3 border-b border-orange-100">
          <div className="flex items-center gap-2.5">
            <div className="w-8 h-8 rounded-xl bg-orange-100 text-orange-600 flex items-center justify-center font-bold">
              <Box className="w-4 h-4" />
            </div>
            <div>
              <h2 className="text-sm font-bold text-slate-900">
                Bao hàng đang mở (Packing)
              </h2>
              <p className="text-xs text-slate-500">
                Các bao hàng chưa hoàn tất, click vào để tiếp tục quét vào bao đó
              </p>
            </div>
          </div>

          <div className="flex items-center gap-2">
            <button
              onClick={fetchActiveTOs}
              title="Tải lại danh sách"
              className="p-1.5 text-slate-400 hover:text-orange-600 hover:bg-orange-50 rounded-lg transition cursor-pointer"
            >
              <RefreshCw className={`w-3.5 h-3.5 ${loadingList ? "animate-spin text-orange-600" : ""}`} />
            </button>
            <button
              onClick={() => router.push("/TOmanagement/listTO")}
              className="text-xs font-semibold text-orange-600 hover:text-orange-700 hover:underline flex items-center gap-1 cursor-pointer"
            >
              <span>Xem tất cả</span>
              <ArrowRight className="w-3.5 h-3.5" />
            </button>
          </div>
        </div>

        {loadingList ? (
          <div className="py-8 text-center text-xs text-slate-400">
            Đang kiểm tra danh sách bao hàng...
          </div>
        ) : recentPackingTOs.length === 0 ? (
          <div className="py-8 text-center space-y-1">
            <CheckCircle2 className="w-8 h-8 text-emerald-500 mx-auto" />
            <p className="text-xs font-semibold text-slate-700">
              Hiện không có bao nào đang mở dở dang
            </p>
            <p className="text-[11px] text-slate-400">
              Bấm "Tạo bao mới ngay" ở trên để bắt đầu ca quét đóng bao
            </p>
          </div>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-3">
            {recentPackingTOs.map((item) => {
              const pkgs: any[] = Array.isArray(item.danhSachGoiHang)
                ? item.danhSachGoiHang
                : JSON.parse(
                    typeof item.danhSachGoiHang === "string"
                      ? item.danhSachGoiHang
                      : "[]"
                  );
              return (
                <div
                  key={item.maTO}
                  onClick={() => router.push(`/TOmanagement/sorting/${item.maTO}`)}
                  className="group p-4 rounded-xl border border-orange-100 hover:border-orange-300 hover:shadow-md hover:bg-orange-50/30 transition cursor-pointer flex flex-col justify-between"
                >
                  <div className="space-y-2">
                    <div className="flex items-center justify-between">
                      <span className="font-mono font-bold text-xs text-slate-900 group-hover:text-orange-600 transition">
                        {item.maTO}
                      </span>
                      <span className="text-[10px] font-bold px-2 py-0.5 rounded-full bg-rose-50 text-rose-700 border border-rose-200">
                        Packing
                      </span>
                    </div>

                    <div className="text-xs text-slate-600">
                      Điểm đến:{" "}
                      <span className="font-semibold text-slate-800">
                        {item.diaDiemGiaoHang || "Chưa có"}
                      </span>
                    </div>
                  </div>

                  <div className="pt-3 mt-3 border-t border-slate-100 flex items-center justify-between text-[11px] text-slate-400">
                    <span className="flex items-center gap-1">
                      <Layers className="w-3.5 h-3.5 text-orange-500" />
                      <b className="text-slate-700">{pkgs.length}</b> kiện
                    </span>
                    <span className="font-medium text-orange-600 group-hover:translate-x-0.5 transition flex items-center gap-0.5">
                      Quét tiếp <ArrowRight className="w-3 h-3" />
                    </span>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>

      {/* KHỐI 3: HƯỚNG DẪN QUY TRÌNH NHANH */}
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
        <div className="p-4 bg-white rounded-2xl border border-orange-100 space-y-1.5">
          <div className="w-7 h-7 rounded-lg bg-orange-100 text-orange-700 font-black text-xs flex items-center justify-center">
            1
          </div>
          <h3 className="text-xs font-bold text-slate-800">Tạo mã bao TO</h3>
          <p className="text-[11px] text-slate-500 leading-relaxed">
            Nhấn tạo bao, hệ thống sinh mã TO và điều hướng sang router /TOmanagement/sorting/[maTO].
          </p>
        </div>

        <div className="p-4 bg-white rounded-2xl border border-orange-100 space-y-1.5">
          <div className="w-7 h-7 rounded-lg bg-orange-100 text-orange-700 font-black text-xs flex items-center justify-center">
            2
          </div>
          <h3 className="text-xs font-bold text-slate-800">Bắn súng quét mã</h3>
          <p className="text-[11px] text-slate-500 leading-relaxed">
            Quét mã bưu kiện hoặc nhập tay. Đơn tự động nạp vào bao và lưu database tức thì.
          </p>
        </div>

        <div className="p-4 bg-white rounded-2xl border border-orange-100 space-y-1.5">
          <div className="w-7 h-7 rounded-lg bg-orange-100 text-orange-700 font-black text-xs flex items-center justify-center">
            3
          </div>
          <h3 className="text-xs font-bold text-slate-800">Đóng bao & In Bill</h3>
          <p className="text-[11px] text-slate-500 leading-relaxed">
            Chốt niêm phong bao sang trạng thái Packed màu xanh lá và in phiếu Bill TO.
          </p>
        </div>
      </div>
    </div>
  );
}
