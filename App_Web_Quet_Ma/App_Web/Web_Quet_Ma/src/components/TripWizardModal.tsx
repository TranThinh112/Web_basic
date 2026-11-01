"use client";

import { useState } from "react";
import { Plus, Trash2, CheckCircle2, ChevronRight, Truck, MapPin, User, Package } from "lucide-react";
import { ChuyenXe } from "@/types";

interface TripWizardModalProps {
  onClose: () => void;
  onSuccess: (trip: ChuyenXe) => void;
  availableTOs: string[];
}

export default function TripWizardModal({ onClose, onSuccess, availableTOs }: TripWizardModalProps) {
  const [step, setStep] = useState(1);

  const [tuyenDuong, setTuyenDuong] = useState("SOC HCM -> Hub Hà Nội");
  const [bienSo, setBienSo] = useState("29C-888.88");
  const [taiXe, setTaiXe] = useState("Nguyễn Văn Hùng");
  const [sdtTaiXe, setSdtTaiXe] = useState("0987654321");
  const [selectedTOs, setSelectedTOs] = useState<string[]>([]);
  const [customTO, setCustomTO] = useState("");

  const handleToggleTO = (toCode: string) => {
    if (selectedTOs.includes(toCode)) {
      setSelectedTOs(selectedTOs.filter((t) => t !== toCode));
    } else {
      setSelectedTOs([...selectedTOs, toCode]);
    }
  };

  const handleAddCustomTO = () => {
    if (customTO.trim() && !selectedTOs.includes(customTO.trim())) {
      setSelectedTOs([...selectedTOs, customTO.trim().toUpperCase()]);
      setCustomTO("");
    }
  };

  const handleFinish = () => {
    const maChuyen = "TRIP_" + Math.floor(100000 + Math.random() * 900000);
    const newTrip: ChuyenXe = {
      id: Date.now().toString(),
      maChuyen,
      tuyenDuong,
      bienSo,
      taiXe,
      sdtTaiXe,
      trangThai: "ChoBocHang",
      thoiGianXuatBen: new Date().toISOString(),
      soLuongTO: selectedTOs.length,
      soLuongKien: selectedTOs.length * 12,
      dsMaTO: selectedTOs,
    };
    onSuccess(newTrip);
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-xs p-4">
      <div className="bg-white rounded-xl w-full max-w-xl shadow-lg border border-slate-200 overflow-hidden text-slate-800">
        {/* Header tối giản */}
        <div className="px-5 py-4 border-b border-slate-200 flex justify-between items-center bg-slate-50">
          <div className="flex items-center gap-2">
            <Truck className="w-5 h-5 text-slate-700" />
            <h3 className="font-semibold text-sm text-slate-900">Tạo chuyến xe trung chuyển</h3>
          </div>
          <button onClick={onClose} className="text-slate-400 hover:text-slate-600 text-sm">✕</button>
        </div>

        {/* Steps indicator tối giản */}
        <div className="flex items-center justify-between px-6 py-3 border-b border-slate-100 bg-white text-xs">
          {[
            { num: 1, label: "Tuyến đường" },
            { num: 2, label: "Phương tiện" },
            { num: 3, label: "Bao hàng" },
            { num: 4, label: "Xác nhận" },
          ].map((s, idx) => (
            <div key={s.num} className="flex items-center gap-1.5">
              <span
                className={`w-5 h-5 rounded-full flex items-center justify-center text-[11px] font-medium ${
                  step >= s.num ? "bg-slate-900 text-white" : "bg-slate-100 text-slate-400"
                }`}
              >
                {s.num}
              </span>
              <span className={`text-[11px] ${step >= s.num ? "text-slate-900 font-medium" : "text-slate-400"}`}>
                {s.label}
              </span>
              {idx < 3 && <ChevronRight className="w-3 h-3 text-slate-300 ml-1" />}
            </div>
          ))}
        </div>

        {/* Body per step */}
        <div className="p-5 text-xs">
          {step === 1 && (
            <div className="space-y-3">
              <div>
                <label className="block text-slate-600 mb-1">Chọn tuyến vận chuyển xuất bến</label>
                <select
                  value={tuyenDuong}
                  onChange={(e) => setTuyenDuong(e.target.value)}
                  className="w-full px-3 py-2 bg-white border border-slate-200 rounded-lg text-xs font-medium"
                >
                  <option value="SOC HCM -> Hub Hà Nội">SOC TP.HCM ➔ Hub Hà Nội</option>
                  <option value="SOC HCM -> Hub Đà Nẵng">SOC TP.HCM ➔ Hub Đà Nẵng</option>
                  <option value="SOC HCM -> Hub Cần Thơ">SOC TP.HCM ➔ Hub Cần Thơ</option>
                  <option value="Hub Tân Bình -> SOC HCM">Hub Tân Bình ➔ SOC TP.HCM</option>
                </select>
              </div>
            </div>
          )}

          {step === 2 && (
            <div className="space-y-3">
              <div className="grid grid-cols-2 gap-3">
                <div>
                  <label className="block text-slate-600 mb-1">Biển số xe</label>
                  <input
                    type="text"
                    value={bienSo}
                    onChange={(e) => setBienSo(e.target.value)}
                    className="w-full px-3 py-2 border border-slate-200 rounded-lg font-mono"
                  />
                </div>
                <div>
                  <label className="block text-slate-600 mb-1">Tài xế phụ trách</label>
                  <input
                    type="text"
                    value={taiXe}
                    onChange={(e) => setTaiXe(e.target.value)}
                    className="w-full px-3 py-2 border border-slate-200 rounded-lg"
                  />
                </div>
              </div>
              <div>
                <label className="block text-slate-600 mb-1">Số điện thoại liên hệ</label>
                <input
                  type="text"
                  value={sdtTaiXe}
                  onChange={(e) => setSdtTaiXe(e.target.value)}
                  className="w-full px-3 py-2 border border-slate-200 rounded-lg"
                />
              </div>
            </div>
          )}

          {step === 3 && (
            <div className="space-y-3">
              <div className="flex gap-2">
                <input
                  type="text"
                  value={customTO}
                  onChange={(e) => setCustomTO(e.target.value)}
                  placeholder="Nhập mã TO (vd: TO_123456)..."
                  className="flex-1 px-3 py-1.5 border border-slate-200 rounded-lg font-mono"
                />
                <button
                  type="button"
                  onClick={handleAddCustomTO}
                  className="px-3 py-1.5 bg-slate-900 text-white rounded-lg font-medium"
                >
                  + Thêm
                </button>
              </div>

              <div className="max-h-40 overflow-y-auto border border-slate-200 rounded-lg p-2 bg-slate-50 space-y-1">
                {availableTOs.length === 0 && selectedTOs.length === 0 ? (
                  <p className="text-center text-slate-400 py-3">Chưa có bao TO nào được gán</p>
                ) : null}

                {availableTOs.map((toCode) => (
                  <label
                    key={toCode}
                    className="flex items-center justify-between p-2 rounded hover:bg-slate-100 cursor-pointer font-mono"
                  >
                    <span>{toCode}</span>
                    <input
                      type="checkbox"
                      checked={selectedTOs.includes(toCode)}
                      onChange={() => handleToggleTO(toCode)}
                      className="rounded"
                    />
                  </label>
                ))}

                {selectedTOs
                  .filter((code) => !availableTOs.includes(code))
                  .map((toCode) => (
                    <div
                      key={toCode}
                      className="flex items-center justify-between p-2 rounded bg-slate-100 font-mono"
                    >
                      <span>{toCode}</span>
                      <button
                        onClick={() => setSelectedTOs(selectedTOs.filter((t) => t !== toCode))}
                        className="text-slate-400 hover:text-slate-700"
                      >
                        ✕
                      </button>
                    </div>
                  ))}
              </div>
            </div>
          )}

          {step === 4 && (
            <div className="space-y-2 bg-slate-50 p-3.5 rounded-lg border border-slate-200 text-xs">
              <div className="flex justify-between py-1 border-b border-slate-200">
                <span className="text-slate-500">Tuyến đường:</span>
                <span className="font-medium text-slate-900">{tuyenDuong}</span>
              </div>
              <div className="flex justify-between py-1 border-b border-slate-200">
                <span className="text-slate-500">Biển số & Tài xế:</span>
                <span className="font-medium text-slate-900">{bienSo} - {taiXe}</span>
              </div>
              <div className="flex justify-between py-1">
                <span className="text-slate-500">Bao hàng gán vào chuyến:</span>
                <span className="font-medium text-slate-900">{selectedTOs.length} bao TO</span>
              </div>
            </div>
          )}
        </div>

        {/* Footer buttons */}
        <div className="bg-slate-50 px-5 py-3 border-t border-slate-200 flex justify-between">
          {step > 1 ? (
            <button
              onClick={() => setStep(step - 1)}
              className="px-3 py-1.5 text-xs text-slate-600 hover:bg-slate-200 rounded-lg"
            >
              Quay lại
            </button>
          ) : <div />}

          {step < 4 ? (
            <button
              onClick={() => setStep(step + 1)}
              className="px-4 py-1.5 bg-slate-900 hover:bg-slate-800 text-white text-xs font-medium rounded-lg"
            >
              Tiếp tục
            </button>
          ) : (
            <button
              onClick={handleFinish}
              className="px-4 py-1.5 bg-slate-900 hover:bg-slate-800 text-white text-xs font-medium rounded-lg"
            >
              Xác nhận tạo chuyến
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
