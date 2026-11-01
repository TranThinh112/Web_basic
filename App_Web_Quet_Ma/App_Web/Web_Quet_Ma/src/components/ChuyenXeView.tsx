"use client";

import { useState, useEffect } from "react";
import { Truck, Search, Plus, Play, CheckCircle, Ban, Box, Calendar } from "lucide-react";
import { ChuyenXe } from "@/types";
import TripWizardModal from "@/components/TripWizardModal";

const DEFAULT_TRIPS: ChuyenXe[] = [
  {
    id: "1",
    maChuyen: "TRIP_789102",
    tuyenDuong: "SOC HCM -> Hub Hà Nội",
    bienSo: "29C-554.12",
    taiXe: "Nguyễn Văn Tuấn",
    sdtTaiXe: "0901.234.567",
    trangThai: "ChoBocHang",
    thoiGianXuatBen: new Date(Date.now() - 3600000).toISOString(),
    soLuongTO: 8,
    soLuongKien: 120,
    dsMaTO: ["TO_982111", "TO_982112", "TO_982113"],
  },
  {
    id: "2",
    maChuyen: "TRIP_443190",
    tuyenDuong: "SOC HCM -> Hub Đà Nẵng",
    bienSo: "43C-112.90",
    taiXe: "Lê Hoàng Nam",
    sdtTaiXe: "0912.888.999",
    trangThai: "DangTaiHang",
    thoiGianXuatBen: new Date(Date.now() - 7200000).toISOString(),
    soLuongTO: 14,
    soLuongKien: 215,
    dsMaTO: ["TO_772001", "TO_772002"],
  },
  {
    id: "3",
    maChuyen: "TRIP_998124",
    tuyenDuong: "Hub Tân Bình -> SOC HCM",
    bienSo: "51D-891.22",
    taiXe: "Phạm Quốc Bảo",
    sdtTaiXe: "0934.555.666",
    trangThai: "DangTrungChuyen",
    thoiGianXuatBen: new Date(Date.now() - 14400000).toISOString(),
    soLuongTO: 5,
    soLuongKien: 74,
    dsMaTO: ["TO_123456"],
  },
];

interface ChuyenXeViewProps {
  availableTOs?: string[];
}

export default function ChuyenXeView({ availableTOs = [] }: ChuyenXeViewProps) {
  const [trips, setTrips] = useState<ChuyenXe[]>([]);
  const [activeTab, setActiveTab] = useState<string>("ALL");
  const [searchTerm, setSearchTerm] = useState("");
  const [showWizard, setShowWizard] = useState(false);

  useEffect(() => {
    const saved = localStorage.getItem("spx_trips_data");
    if (saved) {
      try {
        setTrips(JSON.parse(saved));
      } catch {
        setTrips(DEFAULT_TRIPS);
      }
    } else {
      setTrips(DEFAULT_TRIPS);
    }
  }, []);

  const saveTrips = (data: ChuyenXe[]) => {
    setTrips(data);
    localStorage.setItem("spx_trips_data", JSON.stringify(data));
  };

  const handleCreateTrip = (newTrip: ChuyenXe) => {
    const updated = [newTrip, ...trips];
    saveTrips(updated);
    setShowWizard(false);
  };

  const handleUpdateStatus = (id: string, status: ChuyenXe["trangThai"]) => {
    const updated = trips.map((t) => (t.id === id ? { ...t, trangThai: status } : t));
    saveTrips(updated);
  };

  const handleDeleteTrip = (id: string) => {
    if (confirm("Bạn có chắc chắn muốn hủy chuyến xe này?")) {
      const updated = trips.filter((t) => t.id !== id);
      saveTrips(updated);
    }
  };

  const filteredTrips = trips.filter((t) => {
    const matchTab = activeTab === "ALL" || t.trangThai === activeTab;
    const matchSearch =
      t.maChuyen.toLowerCase().includes(searchTerm.toLowerCase()) ||
      t.tuyenDuong.toLowerCase().includes(searchTerm.toLowerCase()) ||
      t.bienSo.toLowerCase().includes(searchTerm.toLowerCase()) ||
      t.taiXe.toLowerCase().includes(searchTerm.toLowerCase());
    return matchTab && matchSearch;
  });

  const getStatusText = (status: ChuyenXe["trangThai"]) => {
    switch (status) {
      case "ChoBocHang":
        return "Chờ bốc hàng";
      case "DangTaiHang":
        return "Đang tải hàng";
      case "DangTrungChuyen":
        return "Đang trung chuyển";
      case "HoanThanh":
        return "Hoàn thành";
    }
  };

  return (
    <div className="space-y-5">
      {/* Top Banner Cam & Trắng */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3 bg-white p-5 rounded-2xl border border-orange-100 shadow-xs">
        <div>
          <h2 className="text-base font-bold text-slate-900 flex items-center gap-2">
            <Truck className="w-5 h-5 text-orange-600" />
            Điều phối chuyến xe trung chuyển
          </h2>
          <p className="text-xs text-slate-500 mt-0.5">
            Quản lý tuyến đường, thông tin xe và trạng thái xuất bến Hub - Hub
          </p>
        </div>
        <button
          onClick={() => setShowWizard(true)}
          className="px-4 py-2.5 bg-orange-600 hover:bg-orange-700 text-white rounded-xl text-xs font-bold transition flex items-center gap-1.5 shadow-sm shadow-orange-600/20 active:scale-95"
        >
          <Plus className="w-4 h-4" />
          Tạo chuyến xe
        </button>
      </div>

      {/* Filter Tabs & Search */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
        <div className="flex items-center gap-1.5 overflow-x-auto pb-1 sm:pb-0">
          {[
            { key: "ALL", label: "Tất cả" },
            { key: "ChoBocHang", label: "Chờ bốc hàng" },
            { key: "DangTaiHang", label: "Đang tải hàng" },
            { key: "DangTrungChuyen", label: "Đang trung chuyển" },
            { key: "HoanThanh", label: "Hoàn thành" },
          ].map((tab) => (
            <button
              key={tab.key}
              onClick={() => setActiveTab(tab.key)}
              className={`px-3 py-1.5 rounded-xl text-xs transition font-semibold ${
                activeTab === tab.key
                  ? "bg-orange-600 text-white shadow-xs"
                  : "bg-white text-slate-600 hover:bg-orange-50 hover:text-orange-600 border border-orange-100"
              }`}
            >
              {tab.label}
            </button>
          ))}
        </div>

        <div className="relative w-full sm:w-64">
          <Search className="w-4 h-4 text-orange-400 absolute left-3 top-2.5" />
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder="Tìm chuyến xe, biển số..."
            className="w-full pl-9 pr-3 py-1.5 bg-white rounded-xl border border-orange-100 text-xs text-slate-800 placeholder-slate-400 focus:border-orange-500 focus:ring-1 focus:ring-orange-200 outline-none"
          />
        </div>
      </div>

      {/* Grid danh sách chuyến */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {filteredTrips.length === 0 ? (
          <div className="col-span-full bg-white rounded-2xl p-10 text-center border border-orange-100 text-slate-500 text-xs">
            Không tìm thấy chuyến xe nào phù hợp
          </div>
        ) : (
          filteredTrips.map((trip) => (
            <div
              key={trip.id}
              className="bg-white rounded-2xl p-5 border border-orange-100 hover:border-orange-200 shadow-xs hover:shadow-md transition flex flex-col justify-between"
            >
              <div className="space-y-3">
                <div className="flex items-center justify-between">
                  <span className="font-mono text-xs font-bold text-orange-700 bg-orange-50 border border-orange-100 px-2 py-0.5 rounded-lg">
                    {trip.maChuyen}
                  </span>
                  <span className="text-[11px] text-orange-700 bg-orange-50 px-2 py-0.5 rounded-lg font-medium border border-orange-100">
                    {getStatusText(trip.trangThai)}
                  </span>
                </div>

                <h3 className="font-bold text-sm text-slate-900">{trip.tuyenDuong}</h3>

                <div className="space-y-1.5 text-xs text-slate-600 border-t border-orange-50 pt-3">
                  <div className="flex justify-between">
                    <span className="text-slate-400">Biển số xe:</span>
                    <span className="font-mono font-bold text-slate-800">{trip.bienSo}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-slate-400">Tài xế phụ trách:</span>
                    <span className="font-medium text-slate-800">{trip.taiXe} ({trip.sdtTaiXe})</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-slate-400">Tải trọng / Hàng:</span>
                    <span className="font-bold text-orange-600">
                      {trip.soLuongTO} bao • {trip.soLuongKien} kiện
                    </span>
                  </div>
                  <div className="flex justify-between text-[11px] text-slate-400">
                    <span className="flex items-center gap-1">
                      <Calendar className="w-3 h-3 text-orange-400" /> Xuất bến:
                    </span>
                    <span>{trip.thoiGianXuatBen ? new Date(trip.thoiGianXuatBen).toLocaleTimeString("vi-VN", { hour: "2-digit", minute: "2-digit" }) : "---"}</span>
                  </div>
                </div>
              </div>

              {/* Action Buttons Tone Cam / Trắng */}
              <div className="mt-4 pt-3 border-t border-orange-50 flex items-center gap-2">
                {trip.trangThai === "ChoBocHang" && (
                  <button
                    onClick={() => handleUpdateStatus(trip.id, "DangTaiHang")}
                    className="flex-1 py-2 px-3 bg-orange-600 hover:bg-orange-700 text-white text-xs font-bold rounded-xl transition flex items-center justify-center gap-1.5 shadow-xs"
                  >
                    <Box className="w-3.5 h-3.5" /> Bốc hàng lên xe
                  </button>
                )}

                {trip.trangThai === "DangTaiHang" && (
                  <button
                    onClick={() => handleUpdateStatus(trip.id, "DangTrungChuyen")}
                    className="flex-1 py-2 px-3 bg-orange-600 hover:bg-orange-700 text-white text-xs font-bold rounded-xl transition flex items-center justify-center gap-1.5 shadow-xs"
                  >
                    <Play className="w-3.5 h-3.5" /> Xuất bến rời kho
                  </button>
                )}

                {trip.trangThai === "DangTrungChuyen" && (
                  <button
                    onClick={() => handleUpdateStatus(trip.id, "HoanThanh")}
                    className="flex-1 py-2 px-3 bg-orange-600 hover:bg-orange-700 text-white text-xs font-bold rounded-xl transition flex items-center justify-center gap-1.5 shadow-xs"
                  >
                    <CheckCircle className="w-3.5 h-3.5" /> Đã đến Hub đích
                  </button>
                )}

                {trip.trangThai === "HoanThanh" && (
                  <span className="flex-1 text-center py-1.5 text-xs text-orange-600 font-bold bg-orange-50 rounded-xl border border-orange-100">
                    ✓ Chuyến đã hoàn thành
                  </span>
                )}

                <button
                  onClick={() => handleDeleteTrip(trip.id)}
                  title="Hủy chuyến"
                  className="p-2 text-slate-400 hover:text-orange-600 hover:bg-orange-50 rounded-xl transition"
                >
                  <Ban className="w-4 h-4" />
                </button>
              </div>
            </div>
          ))
        )}
      </div>

      {showWizard && (
        <TripWizardModal
          onClose={() => setShowWizard(false)}
          onSuccess={handleCreateTrip}
          availableTOs={availableTOs}
        />
      )}
    </div>
  );
}
