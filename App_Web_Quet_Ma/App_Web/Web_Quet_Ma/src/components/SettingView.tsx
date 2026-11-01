"use client";

import { useState, useEffect } from "react";
import { Settings, Server, Volume2, Shield, Info, CheckCircle2, RotateCcw } from "lucide-react";
import { sounds } from "@/lib/sound";

export default function SettingView() {
  const [apiUrl, setApiUrl] = useState("http://localhost:3000");
  const [soundEnabled, setSoundEnabled] = useState(true);
  const [savedMsg, setSavedMsg] = useState(false);

  useEffect(() => {
    const saved = localStorage.getItem("spx_backend_url");
    if (saved) {
      setApiUrl(saved);
    }
  }, []);

  const handleSave = () => {
    localStorage.setItem("spx_backend_url", apiUrl.trim());
    sounds.enabled = soundEnabled;
    setSavedMsg(true);
    sounds.playSuccess();
    setTimeout(() => setSavedMsg(false), 3000);
  };

  const testBeep = () => {
    sounds.playSuccess();
  };

  const testBuzz = () => {
    sounds.playError();
  };

  return (
    <div className="max-w-3xl mx-auto space-y-6">
      {/* Header */}
      <div className="bg-white p-6 rounded-2xl shadow-sm border border-slate-100">
        <h2 className="text-xl font-black text-slate-800 flex items-center gap-2">
          <Settings className="w-6 h-6 text-orange-600" />
          Cấu Hình Thiết Bị & Kết Nối Hệ Thống
        </h2>
        <p className="text-xs text-slate-500 mt-1">
          Thiết lập địa chỉ API Backend Node.js, âm thanh cảnh báo phản hồi khi quét mã
        </p>
      </div>

      {/* Backend API Settings */}
      <div className="bg-white p-6 rounded-2xl border border-slate-200/80 shadow-sm space-y-4">
        <h3 className="font-bold text-sm text-slate-900 flex items-center gap-2">
          <Server className="w-4 h-4 text-orange-600" />
          Địa Chỉ Máy Chủ Backend (Node.js API)
        </h3>

        <div>
          <label className="block text-xs font-semibold text-slate-600 mb-1">Base URL Backend</label>
          <input
            type="text"
            value={apiUrl}
            onChange={(e) => setApiUrl(e.target.value)}
            placeholder="http://localhost:3000 hoặc URL Railway"
            className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl text-xs font-mono font-bold text-slate-800 outline-none focus:border-orange-500"
          />
          <p className="text-[11px] text-slate-400 mt-1.5">
            Mặc định máy chủ chạy tại <code className="text-orange-600 font-bold">http://localhost:3000</code>. Nếu bạn chạy trên Railway / Server Online hãy dán link tương ứng vào đây.
          </p>
        </div>

        <div className="flex gap-2">
          <button
            type="button"
            onClick={() => setApiUrl("http://localhost:3000")}
            className="px-3 py-1.5 rounded-lg bg-slate-100 hover:bg-slate-200 text-xs font-semibold text-slate-700"
          >
            Chạy Localhost (Port 3000)
          </button>
          <button
            type="button"
            onClick={() => setApiUrl("https://server-production-7598.up.railway.app")}
            className="px-3 py-1.5 rounded-lg bg-slate-100 hover:bg-slate-200 text-xs font-semibold text-slate-700"
          >
            Chạy Railway Server
          </button>
        </div>
      </div>

      {/* Audio Sound Settings */}
      <div className="bg-white p-6 rounded-2xl border border-slate-200/80 shadow-sm space-y-4">
        <h3 className="font-bold text-sm text-slate-900 flex items-center gap-2">
          <Volume2 className="w-4 h-4 text-orange-600" />
          Âm Thanh Phản Hồi Quét Mã (Web Audio)
        </h3>

        <div className="flex items-center justify-between p-3.5 bg-slate-50 rounded-xl border border-slate-100">
          <div>
            <p className="text-xs font-bold text-slate-800">Bật âm thanh phản hồi</p>
            <p className="text-[11px] text-slate-500">Phát âm Beep khi quét thành công hoặc Buzz khi phát hiện lỗi</p>
          </div>
          <input
            type="checkbox"
            checked={soundEnabled}
            onChange={(e) => {
              setSoundEnabled(e.target.checked);
              sounds.enabled = e.target.checked;
            }}
            className="w-5 h-5 text-orange-600 rounded cursor-pointer"
          />
        </div>

        <div className="flex items-center gap-3">
          <button
            type="button"
            onClick={testBeep}
            className="px-4 py-2 bg-emerald-50 hover:bg-emerald-100 text-emerald-700 rounded-xl text-xs font-bold border border-emerald-200 transition"
          >
            🔊 Thử tiếng Beep (Thành công)
          </button>
          <button
            type="button"
            onClick={testBuzz}
            className="px-4 py-2 bg-rose-50 hover:bg-rose-100 text-rose-700 rounded-xl text-xs font-bold border border-rose-200 transition"
          >
            ⚠️ Thử tiếng Cảnh báo (Lỗi)
          </button>
        </div>
      </div>

      {/* Save Button */}
      <button
        onClick={handleSave}
        className="w-full py-3 bg-orange-600 hover:bg-orange-700 text-white rounded-xl text-sm font-bold shadow-md shadow-orange-600/20 transition active:scale-95"
      >
        Lưu Cài Đặt Hệ Thống
      </button>

      {savedMsg && (
        <div className="bg-emerald-50 border border-emerald-200 rounded-xl p-3 text-xs font-bold text-emerald-800 flex items-center gap-2">
          <CheckCircle2 className="w-4 h-4 text-emerald-600" />
          Đã lưu cài đặt cấu hình thành công!
        </div>
      )}
    </div>
  );
}
