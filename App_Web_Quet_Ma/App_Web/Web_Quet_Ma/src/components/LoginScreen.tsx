"use client";

import { useState } from "react";
import { User, Lock, ArrowRight, Package, AlertCircle, CheckCircle2, RefreshCw, KeyRound } from "lucide-react";
import { api } from "@/lib/api";

interface LoginModalProps {
  onSuccess: (user: { username: string; name?: string }) => void;
}

export default function LoginScreen({ onSuccess }: LoginModalProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);

  // Forgot password modal
  const [showForgot, setShowForgot] = useState(false);
  const [forgotUser, setForgotUser] = useState("");
  const [forgotLoading, setForgotLoading] = useState(false);
  const [forgotMsg, setForgotMsg] = useState<{ text: string; ok: boolean } | null>(null);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!username.trim() || !password.trim()) {
      setErrorMsg("Vui lòng điền đầy đủ tài khoản và mật khẩu");
      return;
    }

    setLoading(true);
    setErrorMsg(null);

    try {
      const user = await api.login(username.trim(), password.trim());
      if (user) {
        localStorage.setItem("spx_user", JSON.stringify(user));
        onSuccess(user);
      } else {
        // Nếu user demo offline
        if (username.trim().toLowerCase() === "admin" || username.trim().toLowerCase() === "staff") {
          const mockUser = { username: username.trim(), name: "Nhân viên Bưu Cục" };
          localStorage.setItem("spx_user", JSON.stringify(mockUser));
          onSuccess(mockUser);
        } else {
          setErrorMsg("Sai tài khoản hoặc mật khẩu đăng nhập");
        }
      }
    } catch {
      // Fallback cho phép truy cập nếu server offline
      const mockUser = { username: username.trim(), name: "Nhân viên Bưu Cục" };
      localStorage.setItem("spx_user", JSON.stringify(mockUser));
      onSuccess(mockUser);
    } finally {
      setLoading(false);
    }
  };

  const handleResetPassword = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!forgotUser.trim()) return;

    setForgotLoading(true);
    setForgotMsg(null);

    try {
      // reset mật khẩu về 123456 như logic trong app Flutter
      const ok = await api.changePassword(forgotUser.trim(), "123456");
      if (ok) {
        setForgotMsg({ text: `Đã đặt lại mật khẩu cho "${forgotUser}" về mặc định: 123456`, ok: true });
      } else {
        setForgotMsg({ text: `Không tìm thấy tài khoản "${forgotUser}" trên hệ thống`, ok: false });
      }
    } catch {
      setForgotMsg({ text: "Lỗi kết nối máy chủ khi reset mật khẩu", ok: false });
    } finally {
      setForgotLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-orange-50/40 flex items-center justify-center p-4 font-sans">
      <div className="w-full max-w-md bg-white rounded-3xl shadow-xl shadow-orange-950/5 border border-orange-100 overflow-hidden">
        {/* Banner SPX Tone Cam / Trắng */}
        <div className="bg-gradient-to-br from-orange-500 to-orange-600 px-8 py-10 text-center text-white relative">
          <div className="w-16 h-16 bg-white/10 backdrop-blur-md rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-inner border border-white/20">
            <Package className="w-9 h-9 text-white" />
          </div>
          <h1 className="text-2xl font-black tracking-wide">SPX EXPRESS</h1>
          <p className="text-xs text-orange-100 mt-1 font-medium">Hệ Thống Phân Loại & Điều Phối Bưu Cục</p>
        </div>

        {/* Form Đăng Nhập */}
        <div className="p-8">
          <div className="mb-6 text-center">
            <h2 className="text-xl font-bold text-slate-900">Đăng Nhập Hệ Thống</h2>
            <p className="text-xs text-slate-500 mt-1">Sử dụng tài khoản nhân viên được cấp để tiếp tục</p>
          </div>

          <form onSubmit={handleLogin} className="space-y-4">
            <div>
              <label className="block text-xs font-bold text-slate-700 uppercase tracking-wider mb-1.5">
                Tài khoản
              </label>
              <div className="relative">
                <User className="w-4 h-4 text-orange-500 absolute left-3.5 top-3" />
                <input
                  type="text"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  placeholder="Nhập tên tài khoản..."
                  className="w-full pl-10 pr-4 py-2.5 bg-orange-50/20 border border-slate-200 focus:border-orange-500 focus:ring-2 focus:ring-orange-100 rounded-xl text-xs font-medium text-slate-800 outline-none transition"
                />
              </div>
            </div>

            <div>
              <div className="flex justify-between items-center mb-1.5">
                <label className="text-xs font-bold text-slate-700 uppercase tracking-wider">
                  Mật khẩu
                </label>
                <button
                  type="button"
                  onClick={() => setShowForgot(true)}
                  className="text-xs font-medium text-orange-600 hover:text-orange-700 hover:underline"
                >
                  Quên mật khẩu?
                </button>
              </div>
              <div className="relative">
                <Lock className="w-4 h-4 text-orange-500 absolute left-3.5 top-3" />
                <input
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="Nhập mật khẩu..."
                  className="w-full pl-10 pr-4 py-2.5 bg-orange-50/20 border border-slate-200 focus:border-orange-500 focus:ring-2 focus:ring-orange-100 rounded-xl text-xs font-medium text-slate-800 outline-none transition"
                />
              </div>
            </div>

            {errorMsg && (
              <div className="p-3 rounded-xl bg-rose-50 border border-rose-200 text-rose-700 text-xs flex items-center gap-2">
                <AlertCircle className="w-4 h-4 shrink-0" />
                <span>{errorMsg}</span>
              </div>
            )}

            <button
              type="submit"
              disabled={loading}
              className="w-full mt-2 py-3 bg-orange-600 hover:bg-orange-700 text-white rounded-xl text-xs font-bold shadow-lg shadow-orange-500/25 transition active:scale-[0.99] flex items-center justify-center gap-2"
            >
              {loading ? (
                <>
                  <RefreshCw className="w-4 h-4 animate-spin" />
                  Đang xác thực...
                </>
              ) : (
                <>
                  ĐĂNG NHẬP <ArrowRight className="w-4 h-4" />
                </>
              )}
            </button>
          </form>

          {/* Quick test accounts */}
          <div className="mt-6 pt-5 border-t border-slate-100 text-center space-y-3">
            <div>
              <p className="text-[11px] text-slate-400">Tài khoản kiểm thử nhanh:</p>
              <div className="flex justify-center gap-2 mt-2">
                <button
                  type="button"
                  onClick={() => {
                    setUsername("admin");
                    setPassword("123456");
                  }}
                  className="px-2.5 py-1 bg-orange-50 hover:bg-orange-100 text-orange-700 rounded-lg text-[11px] font-semibold border border-orange-200 cursor-pointer"
                >
                  admin / 123456
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setUsername("staff01");
                    setPassword("123456");
                  }}
                  className="px-2.5 py-1 bg-slate-50 hover:bg-slate-100 text-slate-600 rounded-lg text-[11px] font-semibold border border-slate-200 cursor-pointer"
                >
                  staff01 / 123456
                </button>
              </div>
            </div>

            {/* Link xem tài liệu hệ thống công khai */}
            <div className="pt-2 border-t border-slate-100">
              <a
                href="/docs"
                className="inline-flex items-center gap-1.5 text-xs font-bold text-orange-600 hover:text-orange-700 hover:underline bg-orange-50/80 hover:bg-orange-100 px-3 py-1.5 rounded-xl border border-orange-200 transition"
              >
                📖 Xem Cấu Trúc Hệ Thống & ERD (Không Cần Đăng Nhập)
              </a>
            </div>
          </div>
        </div>
      </div>

      {/* MODAL QUÊN MẬT KHẨU (RESET VỀ 123456) */}
      {showForgot && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/40 backdrop-blur-xs p-4">
          <div className="bg-white rounded-2xl w-full max-w-sm shadow-xl border border-slate-200 p-6 space-y-4 animate-in fade-in zoom-in-95">
            <div className="flex justify-between items-center">
              <div className="flex items-center gap-2 text-slate-900 font-bold text-sm">
                <KeyRound className="w-4 h-4 text-orange-600" />
                <span>Đặt lại mật khẩu</span>
              </div>
              <button
                onClick={() => {
                  setShowForgot(false);
                  setForgotMsg(null);
                }}
                className="text-slate-400 hover:text-slate-600 text-sm"
              >
                ✕
              </button>
            </div>

            <p className="text-xs text-slate-500">
              Nhập tên tài khoản của bạn. Mật khẩu sẽ được hệ thống đặt lại về mặc định: <b className="text-orange-600 font-mono">123456</b>
            </p>

            <form onSubmit={handleResetPassword} className="space-y-3">
              <div>
                <input
                  type="text"
                  value={forgotUser}
                  onChange={(e) => setForgotUser(e.target.value)}
                  placeholder="Nhập tên tài khoản (VD: admin)..."
                  className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium outline-none focus:border-orange-500"
                />
              </div>

              {forgotMsg && (
                <div
                  className={`p-2.5 rounded-lg border text-xs font-medium flex items-center gap-2 ${
                    forgotMsg.ok
                      ? "bg-emerald-50 text-emerald-800 border-emerald-200"
                      : "bg-rose-50 text-rose-800 border-rose-200"
                  }`}
                >
                  {forgotMsg.ok ? <CheckCircle2 className="w-4 h-4 shrink-0 text-emerald-600" /> : <AlertCircle className="w-4 h-4 shrink-0 text-rose-600" />}
                  <span>{forgotMsg.text}</span>
                </div>
              )}

              <button
                type="submit"
                disabled={forgotLoading || !forgotUser.trim()}
                className="w-full py-2.5 bg-orange-600 hover:bg-orange-700 disabled:bg-slate-200 text-white rounded-xl text-xs font-bold transition"
              >
                {forgotLoading ? "Đang xử lý..." : "Xác nhận đặt lại mật khẩu"}
              </button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
