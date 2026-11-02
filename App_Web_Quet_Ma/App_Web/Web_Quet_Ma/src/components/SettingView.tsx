"use client";

import { useState, useEffect } from "react";
import { Settings, Server, Volume2, CheckCircle2, RefreshCw, AlertTriangle, Database } from "lucide-react";
import { sounds } from "@/lib/sound";

export default function SettingView() {
  const [apiUrl, setApiUrl] = useState("https://webbasic-production-47cd.up.railway.app");
  const [soundEnabled, setSoundEnabled] = useState(true);
  const [savedMsg, setSavedMsg] = useState(false);
  const [checking, setChecking] = useState(false);
  const [statusResult, setStatusResult] = useState<{
    tested: boolean;
    serverOk?: boolean;
    dbOk?: boolean;
    message?: string;
  }>({ tested: false });

  useEffect(() => {
    const saved = localStorage.getItem("spx_backend_url");
    if (saved) {
      setApiUrl(saved);
    }
  }, []);

  const handleTestConnection = async (targetUrl = apiUrl) => {
    setChecking(true);
    setStatusResult({ tested: false });
    const cleanUrl = targetUrl.trim().replace(/\/$/, "");

    try {
      // 1. Test Server Ping
      const serverRes = await fetch(`${cleanUrl}/`, { method: "GET" });
      if (!serverRes.ok) {
        setStatusResult({
          tested: true,
          serverOk: false,
          dbOk: false,
          message: `Máy chủ phản hồi mã lỗi HTTP ${serverRes.status}`,
        });
        setChecking(false);
        return;
      }

      // 2. Test DB Query
      const dbRes = await fetch(`${cleanUrl}/orders/getIn4?limit=1`);
      if (dbRes.ok) {
        setStatusResult({
          tested: true,
          serverOk: true,
          dbOk: true,
          message: "Đã kết nối hoàn hảo: Server Online & Database MySQL hoạt động tốt!",
        });
      } else {
        const errJson = await dbRes.json().catch(() => ({}));
        setStatusResult({
          tested: true,
          serverOk: true,
          dbOk: false,
          message: `Server Online nhưng Database báo lỗi: ${errJson.error || "Mất kết nối MySQL"}`,
        });
      }
    } catch (err: unknown) {
      setStatusResult({
        tested: true,
        serverOk: false,
        dbOk: false,
        message: err instanceof Error ? err.message : "Không thể kết nối tới địa chỉ máy chủ này",
      });
    } finally {
      setChecking(false);
    }
  };

  const handleSave = () => {
    localStorage.setItem("spx_backend_url", apiUrl.trim());
    sounds.enabled = soundEnabled;
    setSavedMsg(true);
    sounds.playSuccess();
    setTimeout(() => setSavedMsg(false), 3000);
  };

  return (
    <div className="max-w-3xl mx-auto space-y-6">
      {/* Header */}
      <div className="bg-white p-6 rounded-2xl shadow-xs border border-orange-100">
        <h2 className="text-base font-bold text-slate-800 flex items-center gap-2">
          <Settings className="w-5 h-5 text-orange-600" />
          Cấu hình đồng bộ hệ thống & thiết bị
        </h2>
        <p className="text-xs text-slate-500 mt-1">
          Thiết lập địa chỉ API Backend đồng bộ dữ liệu giữa Web, App di động và Cơ sở dữ liệu
        </p>
      </div>

      {/* Backend API Settings */}
      <div className="bg-white p-6 rounded-2xl border border-orange-100 shadow-xs space-y-4">
        <h3 className="font-bold text-xs text-slate-900 uppercase tracking-wide flex items-center gap-2">
          <Server className="w-4 h-4 text-orange-600" />
          Địa chỉ máy chủ Backend (Node.js API)
        </h3>

        <div>
          <label className="block text-xs font-semibold text-slate-600 mb-1">Base URL Backend</label>
          <div className="flex gap-2">
            <input
              type="text"
              value={apiUrl}
              onChange={(e) => setApiUrl(e.target.value)}
              placeholder="https://webbasic-production-47cd.up.railway.app"
              className="flex-1 px-4 py-2.5 bg-orange-50/20 border border-slate-200 rounded-xl text-xs font-mono font-bold text-slate-800 outline-none focus:border-orange-500"
            />
            <button
              type="button"
              onClick={() => handleTestConnection(apiUrl)}
              disabled={checking}
              className="px-4 py-2.5 bg-orange-600 hover:bg-orange-700 disabled:bg-slate-300 text-white rounded-xl text-xs font-bold transition flex items-center gap-1.5 shrink-0 shadow-xs"
            >
              <RefreshCw className={`w-3.5 h-3.5 ${checking ? "animate-spin" : ""}`} />
              {checking ? "Đang thử..." : "Kiểm tra kết nối"}
            </button>
          </div>
        </div>

        {/* Trạng thái kiểm tra kết nối */}
        {statusResult.tested && (
          <div
            className={`p-3.5 rounded-xl border text-xs flex items-start gap-2.5 ${
              statusResult.dbOk
                ? "bg-emerald-50 text-emerald-800 border-emerald-200"
                : statusResult.serverOk
                ? "bg-amber-50 text-amber-800 border-amber-200"
                : "bg-rose-50 text-rose-800 border-rose-200"
            }`}
          >
            {statusResult.dbOk ? (
              <CheckCircle2 className="w-4 h-4 text-emerald-600 shrink-0 mt-0.5" />
            ) : (
              <AlertTriangle className="w-4 h-4 text-amber-600 shrink-0 mt-0.5" />
            )}
            <div>
              <p className="font-bold">{statusResult.message}</p>
              {statusResult.serverOk && !statusResult.dbOk && (
                <p className="mt-1 text-[11px] text-amber-700">
                  👉 Cần gắn biến môi trường <code>MYSQL_URL</code> cho Service Node.js trên Railway Dashboard để hoàn tất đồng bộ online.
                </p>
              )}
            </div>
          </div>
        )}

        <div className="flex flex-wrap gap-2 pt-1">
          <button
            type="button"
            onClick={() => {
              setApiUrl("https://webbasic-production-47cd.up.railway.app");
              handleTestConnection("https://webbasic-production-47cd.up.railway.app");
            }}
            className="px-3 py-1.5 rounded-lg bg-orange-50 hover:bg-orange-100 text-xs font-semibold text-orange-700 border border-orange-200 flex items-center gap-1.5"
          >
            <Server className="w-3.5 h-3.5" />
            Sử dụng Railway Production Server
          </button>
          <button
            type="button"
            onClick={() => {
              setApiUrl("http://localhost:5000");
              handleTestConnection("http://localhost:5000");
            }}
            className="px-3 py-1.5 rounded-lg bg-slate-100 hover:bg-slate-200 text-xs font-semibold text-slate-700 flex items-center gap-1.5"
          >
            <Database className="w-3.5 h-3.5" />
            Chạy Localhost Backend (Port 5000)
          </button>
        </div>
      </div>

      {/* Audio Sound Settings */}
      <div className="bg-white p-6 rounded-2xl border border-orange-100 shadow-xs space-y-4">
        <h3 className="font-bold text-xs text-slate-900 uppercase tracking-wide flex items-center gap-2">
          <Volume2 className="w-4 h-4 text-orange-600" />
          Âm thanh phản hồi quét mã (Web Audio)
        </h3>

        <div className="flex items-center justify-between p-3.5 bg-orange-50/30 rounded-xl border border-orange-100">
          <div>
            <p className="text-xs font-bold text-slate-800">Bật âm thanh phản hồi</p>
            <p className="text-[11px] text-slate-500">Phát âm Beep khi quét thành công hoặc Buzz khi phát hiện lỗi/trùng mã</p>
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
            onClick={() => sounds.playSuccess()}
            className="px-4 py-2 bg-emerald-50 hover:bg-emerald-100 text-emerald-700 rounded-xl text-xs font-bold border border-emerald-200 transition"
          >
            🔊 Thử tiếng Beep (Thành công)
          </button>
          <button
            type="button"
            onClick={() => sounds.playError()}
            className="px-4 py-2 bg-rose-50 hover:bg-rose-100 text-rose-700 rounded-xl text-xs font-bold border border-rose-200 transition"
          >
            ⚠️ Thử tiếng Cảnh báo (Lỗi)
          </button>
        </div>
      </div>

      {/* Save Button */}
      <button
        onClick={handleSave}
        className="w-full py-3 bg-orange-600 hover:bg-orange-700 text-white rounded-xl text-sm font-bold shadow-md shadow-orange-600/20 transition active:scale-95 cursor-pointer"
      >
        Lưu cài đặt kết nối
      </button>

      {savedMsg && (
        <div className="bg-emerald-50 border border-emerald-200 rounded-xl p-3 text-xs font-bold text-emerald-800 flex items-center gap-2">
          <CheckCircle2 className="w-4 h-4 text-emerald-600" />
          Đã lưu cài đặt kết nối thành công! Mọi thay đổi đã được áp dụng.
        </div>
      )}
    </div>
  );
}
