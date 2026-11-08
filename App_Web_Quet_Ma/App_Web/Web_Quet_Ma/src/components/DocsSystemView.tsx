"use client";

import AppDocsContent from "@/components/AppDocsContent";
import {
  useState, useEffect } from "react";
import Link from "next/link";
import {
  Monitor,
  Server,
  Database,
  Layers,
  ArrowRight,
  ShieldCheck,
  Zap,
  Package,
  QrCode,
  Truck,
  Boxes,
  FileText,
  Search,
  Check,
  CheckCircle2,
  AlertTriangle,
  RotateCcw,
  Sparkles,
  Workflow,
  Copy,
  LogIn,
  Smartphone,
  Download,
  ExternalLink
} from "lucide-react";

export default function DocsSystemView() {
  const [docsMode, setDocsMode] = useState<"system" | "app">("system");
  const [mainScope, setMainScope] = useState<"system" | "app">("system");
  const [activeTab, setActiveTab] = useState<"overview" | "code_format" | "scan_flow" | "erd" | "apis">("overview");
  const [copiedText, setCopiedText] = useState<string | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    if (typeof window !== "undefined") {
      const user = localStorage.getItem("spx_user");
      setIsLoggedIn(!!user);
    }
  }, []);

  const handleCopy = (text: string) => {
    navigator.clipboard.writeText(text);
    setCopiedText(text);
    setTimeout(() => setCopiedText(null), 1500);
  };

  return (
    <div className="space-y-6 max-w-6xl mx-auto pb-16">
      {/* SWITCHER TABS TRÊN CÙNG: WEB (HỆ THỐNG) vs APP (MOBILE) */}
      <div className="bg-white rounded-2xl p-2.5 border border-orange-200/90 shadow-xs flex flex-col sm:flex-row items-center justify-between gap-3">
        <div className="flex items-center gap-2 p-1 bg-orange-50/80 rounded-xl w-full sm:w-auto">
          <button
            type="button"
            onClick={() => setDocsMode("system")}
            className={`flex-1 sm:flex-none flex items-center justify-center gap-2 px-5 py-2.5 rounded-lg text-xs font-bold transition-all cursor-pointer ${
              docsMode === "system"
                ? "bg-orange-600 text-white shadow-md shadow-orange-600/30"
                : "text-slate-600 hover:text-orange-600 hover:bg-orange-100/60"
            }`}
          >
            <Monitor className="w-4 h-4" />
            <span>Tài Liệu Cấu Trúc Hệ Thống (Web & BE)</span>
          </button>

          <button
            type="button"
            onClick={() => setDocsMode("app")}
            className={`flex-1 sm:flex-none flex items-center justify-center gap-2 px-5 py-2.5 rounded-lg text-xs font-bold transition-all cursor-pointer ${
              docsMode === "app"
                ? "bg-orange-600 text-white shadow-md shadow-orange-600/30"
                : "text-slate-600 hover:text-orange-600 hover:bg-orange-100/60"
            }`}
          >
            <Smartphone className="w-4 h-4" />
            <span>Giao Diện App Mobile & Hướng Dẫn Netlify</span>
            <span className={`text-[10px] px-1.5 py-0.5 rounded font-black uppercase ${
              docsMode === "app" ? "bg-white text-orange-600" : "bg-orange-200 text-orange-950"
            }`}>
              APP
            </span>
          </button>
        </div>

        <div className="hidden md:flex items-center gap-2 pr-2 text-xs text-slate-500">
          <Sparkles className="w-3.5 h-3.5 text-orange-500" />
          <span>Chuyển đổi linh hoạt giữa tài liệu Web và App Mobile</span>
        </div>
      </div>

      {docsMode === "app" ? (
        <AppDocsContent />
      ) : (
        <>
      {/* HEADER BANNER */}
      <div className="relative overflow-hidden bg-gradient-to-r from-orange-600 via-orange-500 to-amber-500 rounded-2xl p-6 sm:p-8 text-white shadow-lg shadow-orange-600/20">
        <div className="relative z-10 flex flex-col md:flex-row md:items-center justify-between gap-4">
          <div className="space-y-2">
            <div className="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-white/20 backdrop-blur-md text-xs font-bold uppercase tracking-wider">
              <Sparkles className="w-3.5 h-3.5" /> SPX Express System Architecture
            </div>
            <h1 className="text-2xl sm:text-3xl font-black tracking-tight">
              Tài Liệu Cấu Trúc Hệ Thống & Cơ Chế Hoạt Động
            </h1>
            <p className="text-orange-100 text-xs sm:text-sm max-w-2xl leading-relaxed">
              Mô tả toàn diện kiến trúc 3 tầng, cấu trúc định dạng mã vận đơn, quy trình tạo đơn chống trùng mã, luồng quét kiểm tra và gom bao TO an toàn cùng sơ đồ cơ sở dữ liệu ERD trực quan.
            </p>
          </div>

          <div className="shrink-0 flex items-center gap-2.5">
            {isLoggedIn ? (
              <Link
                href="/orders"
                className="px-5 py-3 bg-white text-orange-600 hover:bg-orange-50 rounded-xl font-bold text-xs flex items-center gap-2 shadow-md transition active:scale-95 cursor-pointer"
              >
                <span>Vào Trang Quản Lý Kho</span>
                <ArrowRight className="w-4 h-4" />
              </Link>
            ) : (
              <Link
                href="/login"
                className="px-5 py-3 bg-white text-orange-600 hover:bg-orange-50 rounded-xl font-bold text-xs flex items-center gap-2 shadow-md transition active:scale-95 cursor-pointer"
              >
                <LogIn className="w-4 h-4" />
                <span>Đăng Nhập Để Vào Quản Lý</span>
              </Link>
            )}
          </div>
        </div>
        <div className="absolute -right-8 -bottom-8 w-64 h-64 bg-white/10 rounded-full blur-2xl pointer-events-none" />
      </div>

      {/* NAVIGATION TABS */}
      <div className="flex items-center gap-2 overflow-x-auto pb-2 border-b border-orange-100 text-xs font-bold scrollbar-none">
        {[
          { id: "overview", label: "1. Tổng Quan Kiến Trúc", icon: Layers },
          { id: "code_format", label: "2. Cấu Trúc Mã Vận Đơn", icon: QrCode },
          { id: "scan_flow", label: "3. Luồng Quét & Gom Bao TO", icon: Workflow },
                    { id: "erd", label: "4. Sơ Đồ Cơ Sở Dữ Liệu (ERD)", icon: Database },
          { id: "apis", label: "5. Danh Mục Endpoints API", icon: Server },
        ].map((tab) => {
          const Icon = tab.icon;
          const isActive = activeTab === tab.id;
          return (
            <button
              key={tab.id}
              onClick={() => setActiveTab(tab.id as any)}
              className={`flex items-center gap-2 px-4 py-2.5 rounded-xl transition whitespace-nowrap cursor-pointer ${
                isActive
                  ? "bg-orange-600 text-white shadow-sm shadow-orange-600/30"
                  : "bg-white text-slate-600 hover:bg-orange-50 hover:text-orange-600 border border-slate-200"
              }`}
            >
              <Icon className="w-4 h-4" />
              <span>{tab.label}</span>
            </button>
          );
        })}
      </div>

      {/* TAB 1: TỔNG QUAN KIẾN TRÚC */}
      {mainScope === "system" && activeTab === "overview" && (
        <div className="space-y-6">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            {/* Frontend Client */}
            <div className="bg-white rounded-2xl p-5 border border-orange-100 shadow-xs space-y-3">
              <div className="w-10 h-10 rounded-xl bg-orange-50 text-orange-600 flex items-center justify-center font-bold">
                <Layers className="w-5 h-5" />
              </div>
              <h3 className="font-bold text-slate-900 text-sm">1. Frontend Layer</h3>
              <p className="text-slate-600 text-xs leading-relaxed">
                Được xây dựng trên <b>Next.js 14 App Router</b> & <b>React 18</b> kết hợp Tailwind CSS. Giao diện tối ưu hóa cho màn hình máy tính bàn, súng bắn mã vạch Barcode và camera webcam quét trực tiếp qua <code className="text-orange-600 font-mono text-[11px]">html5-qrcode</code>.
              </p>
              <div className="text-[11px] font-mono text-slate-500 bg-slate-50 p-2.5 rounded-lg border border-slate-100">
                Port: 3001 (Local) / Railway Web
              </div>
            </div>

            {/* Backend Server */}
            <div className="bg-white rounded-2xl p-5 border border-orange-100 shadow-xs space-y-3">
              <div className="w-10 h-10 rounded-xl bg-blue-50 text-blue-600 flex items-center justify-center font-bold">
                <Server className="w-5 h-5" />
              </div>
              <h3 className="font-bold text-slate-900 text-sm">2. Backend Service</h3>
              <p className="text-slate-600 text-xs leading-relaxed">
                Máy chủ <b>Node.js & Express RESTful API</b>. Chịu trách nhiệm quản lý phiên làm việc, chuẩn hóa định tuyến Hub theo 63 tỉnh thành, auto-retry chống trùng mã khóa chính và đồng bộ danh sách kiện hàng.
              </p>
              <div className="text-[11px] font-mono text-slate-500 bg-slate-50 p-2.5 rounded-lg border border-slate-100">
                Railway: webbasic-production-47cd
              </div>
            </div>

            {/* Database */}
            <div className="bg-white rounded-2xl p-5 border border-orange-100 shadow-xs space-y-3">
              <div className="w-10 h-10 rounded-xl bg-emerald-50 text-emerald-600 flex items-center justify-center font-bold">
                <Database className="w-5 h-5" />
              </div>
              <h3 className="font-bold text-slate-900 text-sm">3. Database Layer</h3>
              <p className="text-slate-600 text-xs leading-relaxed">
                Cơ sở dữ liệu quan hệ <b>MySQL 8.0</b> lưu trữ tập trung. Tự động khởi tạo cấu trúc bảng (`orders`, `to_orders`, `users`) và tự động dọn dẹp các bao hàng rỗng quá 24 giờ.
              </p>
              <div className="text-[11px] font-mono text-slate-500 bg-slate-50 p-2.5 rounded-lg border border-slate-100">
                Driver: mysql2 Pool (10 connections)
              </div>
            </div>
          </div>

          {/* SƠ ĐỒ DÒNG DỮ LIỆU */}
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-4">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <Workflow className="w-4 h-4 text-orange-600" /> Luồng Dữ Liệu Giữa Các Thành Phần
            </h3>
            <div className="p-4 bg-slate-50 rounded-xl border border-slate-200 text-xs font-mono text-slate-700 leading-relaxed overflow-x-auto">
              <pre>{`[ Súng Barcode / Camera / User Form ]
               │
               ▼  (HTTP GET / POST / PUT)
┌─────────────────────────────────────────────────────────────┐
│ Next.js Web Client (http://localhost:3001)                 │
│ - Tự động nhận diện Hub từ địa chỉ bằng Levenshtein         │
│ - Quản lý âm thanh Web Audio (Beep / Buzz)                 │
│ - Hiển thị trạng thái phân loại Inbound / Outbound          │
└──────────────────────────────┬──────────────────────────────┘
                               │ JSON Payload (REST API)
                               ▼
┌─────────────────────────────────────────────────────────────┐
│ Node.js Backend API (https://webbasic-production-47cd)       │
│ - POST /orders: Auto-retry 5 lần chống trùng khóa chính     │
│ - POST /orders/scan/:id: Kiểm tra hợp lệ trước khi UPDATE   │
│ - POST /orders/remove/:id: Khôi phục Outbound & tính lại kg │
│ - PUT /TO/:maTO: Đồng bộ danh sách kiện hàng JSON           │
└──────────────────────────────┬──────────────────────────────┘
                               │ MySQL Pool Connection
                               ▼
┌─────────────────────────────────────────────────────────────┐
│ MySQL Database Engine (orders, to_orders, users)           │
│ - Đảm bảo toàn vẹn dữ liệu khóa chính PRIMARY KEY           │
│ - Lưu mốc thời gian GMT+7 (Asia/Ho_Chi_Minh)               │
└─────────────────────────────────────────────────────────────┘`}</pre>
            </div>
          </div>
        </div>
      )}

      {/* TAB 2: CẤU TRÚC MÃ VẬN ĐƠN */}
      {mainScope === "system" && activeTab === "code_format" && (
        <div className="space-y-6">
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-4">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <QrCode className="w-4 h-4 text-orange-600" /> Cấu Trúc Mã Vận Đơn SPX Express (Tracking ID)
            </h3>
            <p className="text-xs text-slate-600 leading-relaxed">
              Mã vận đơn tiêu chuẩn của SPX có chiều dài 16-17 ký tự, bao gồm tiền tố thương hiệu, mã hóa năm, chuỗi ngẫu nhiên và mã hóa tháng theo quy chuẩn logistics:
            </p>

            {/* VISUAL CODE BREAKDOWN */}
            <div className="p-4 bg-orange-50/50 rounded-xl border border-orange-200">
              <div className="text-center mb-3">
                <span className="text-xs text-slate-500 font-medium">Ví dụ mã đơn tạo vào tháng 11 năm 2026:</span>
                <div className="font-mono text-xl sm:text-2xl font-black text-slate-900 tracking-wider mt-1">
                  <span className="text-orange-600 bg-orange-100 px-1 py-0.5 rounded">SPXVN</span>
                  <span className="text-blue-600 bg-blue-100 px-1 py-0.5 rounded mx-1">06</span>
                  <span className="text-slate-800 bg-slate-200 px-1 py-0.5 rounded">84574507</span>
                  <span className="text-emerald-700 bg-emerald-100 px-1 py-0.5 rounded ml-1">B</span>
                </div>
              </div>

              <div className="grid grid-cols-1 sm:grid-cols-4 gap-2 text-[11px] pt-3 border-t border-orange-200/60">
                <div className="bg-white p-2.5 rounded-lg border border-orange-100">
                  <span className="font-bold text-orange-600 block">SPXVN (5 ký tự)</span>
                  <span className="text-slate-500">Tiền tố nhận diện mạng lưới bưu kiện SPX Express Việt Nam.</span>
                </div>
                <div className="bg-white p-2.5 rounded-lg border border-blue-100">
                  <span className="font-bold text-blue-600 block">06 (2 chữ số)</span>
                  <span className="text-slate-500">Mã năm: Năm 2026 lấy số hàng đơn vị thêm số 0 đằng trước.</span>
                </div>
                <div className="bg-white p-2.5 rounded-lg border border-slate-200">
                  <span className="font-bold text-slate-800 block">84574507 (8 chữ số)</span>
                  <span className="text-slate-500">Chuỗi ngẫu nhiên (100 triệu tổ hợp mã trong 1 tháng).</span>
                </div>
                <div className="bg-white p-2.5 rounded-lg border border-emerald-100">
                  <span className="font-bold text-emerald-600 block">B (1 ký tự tháng)</span>
                  <span className="text-slate-500">T1-T9: số 1-9; T10: A; T11: B; T12: C.</span>
                </div>
              </div>
            </div>

            {/* CƠ CHẾ AUTO-RETRY */}
            <div className="bg-slate-50 p-4 rounded-xl border border-slate-200 space-y-2">
              <h4 className="text-xs font-bold text-slate-800 flex items-center gap-1.5">
                <ShieldCheck className="w-4 h-4 text-emerald-600" /> Cơ Chế Chống Trùng Mã (Backend Auto-Retry 5 Lần)
              </h4>
              <p className="text-xs text-slate-600 leading-relaxed">
                Để tránh việc người dùng bấm Tạo đơn mà bị báo lỗi <code className="text-rose-600 font-mono text-[11px]">ER_DUP_ENTRY</code> khi mã vô tình bị trùng, Backend tự động bắt lỗi mã khóa chính, kích hoạt hàm <code className="text-orange-600 font-mono text-[11px]">generateUniqueSPXCode()</code> để tạo mã mới và lưu lại ngầm (thử lại tối đa 5 lần). Tỉ lệ lỗi trùng mã đối với trải nghiệm người dùng là <b>xấp xỉ 0%</b>.
              </p>
            </div>
          </div>
        </div>
      )}

      {/* TAB 3: LUỒNG QUÉT & GOM BAO TO */}
      {mainScope === "system" && activeTab === "scan_flow" && (
        <div className="space-y-6">
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-4">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <Workflow className="w-4 h-4 text-orange-600" /> Cơ Chế Quét Đơn Vào Bao Hàng TO (An Toàn Dữ Liệu)
            </h3>
            <p className="text-xs text-slate-600 leading-relaxed">
              Quy trình gom hàng vào bao tuân thủ nghiêm ngặt nguyên tắc: <b>Chỉ thay đổi trạng thái đơn hàng khi khớp nơi đến với bao hàng</b>.
            </p>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              {/* Bước 1 & 2 */}
              <div className="p-4 rounded-xl border border-slate-200 bg-slate-50/70 space-y-2">
                <div className="flex items-center gap-2 text-xs font-bold text-slate-800">
                  <span className="w-5 h-5 rounded-full bg-orange-600 text-white flex items-center justify-center text-[11px]">1</span>
                  Kiểm tra định dạng & Tồn tại
                </div>
                <p className="text-[11px] text-slate-600 leading-relaxed">
                  Backend kiểm tra regex mã đơn theo pattern <code className="text-orange-600 font-mono">/^SPXVN\d&#123;10&#125;[\dA-C]$/i</code>. Đơn hàng phải đang ở trạng thái <span className="text-rose-600 font-bold">Outbound</span> (chưa vào bao nào khác).
                </p>
              </div>

              {/* Bước 3 */}
              <div className="p-4 rounded-xl border border-amber-200 bg-amber-50/50 space-y-2">
                <div className="flex items-center gap-2 text-xs font-bold text-amber-900">
                  <span className="w-5 h-5 rounded-full bg-amber-600 text-white flex items-center justify-center text-[11px]">2</span>
                  Kiểm tra Điểm Đến (Sorting Hub)
                </div>
                <p className="text-[11px] text-amber-800 leading-relaxed">
                  Nếu bao TO đã có đơn, điểm đến của đơn mới phải <b>trùng khớp 100%</b> với điểm đến của các đơn trước. Nếu khác nơi nhận, Backend <b>ngắt ngay lập tức</b>, tuyệt đối không chỉnh sửa gì trong Database.
                </p>
              </div>

              {/* Bước 4 */}
              <div className="p-4 rounded-xl border border-emerald-200 bg-emerald-50/50 space-y-2">
                <div className="flex items-center gap-2 text-xs font-bold text-emerald-900">
                  <span className="w-5 h-5 rounded-full bg-emerald-600 text-white flex items-center justify-center text-[11px]">3</span>
                  Cập nhật bao TO_orders
                </div>
                <p className="text-[11px] text-emerald-800 leading-relaxed">
                  Thêm thông tin kiện hàng vào mảng <code className="font-mono">danhSachGoiHang</code>, cộng dồn khối lượng <code className="font-mono">totalWeight</code> và tăng số lượng <code className="font-mono">SL</code>.
                </p>
              </div>

              {/* Bước 5 */}
              <div className="p-4 rounded-xl border border-blue-200 bg-blue-50/50 space-y-2">
                <div className="flex items-center gap-2 text-xs font-bold text-blue-900">
                  <span className="w-5 h-5 rounded-full bg-blue-600 text-white flex items-center justify-center text-[11px]">4</span>
                  Chuyển trạng thái Inbound
                </div>
                <p className="text-[11px] text-blue-800 leading-relaxed">
                  Sau khi bao TO đã ghi nhận thành công, đơn hàng mới được chuyển thành <span className="text-emerald-700 font-bold">Inbound</span>, gán <code className="font-mono">maTO</code> và lưu mốc giờ đóng bao theo GMT+7.
                </p>
              </div>
            </div>
          </div>
        </div>
      )}


      {/* PHÂN HỆ RIÊNG BIỆT: ỨNG DỤNG DI ĐỘNG & HƯỚNG DẪN CHẠY WEB QUA NETLIFY DROP */}
      {mainScope === "app" && (
        <div className="space-y-6">
          {/* BANNER TẢI BẢN BUILD NÉN */}
          <div className="bg-gradient-to-br from-amber-500 to-orange-600 rounded-2xl p-6 text-white shadow-md space-y-4">
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
              <div className="space-y-1">
                <div className="inline-flex items-center gap-1.5 px-2.5 py-0.5 rounded-full bg-white/20 text-[11px] font-bold">
                  <Package className="w-3.5 h-3.5" /> Gói Biên Dịch Flutter Web Sẵn Sàng
                </div>
                <h3 className="text-xl font-black">Chạy Ứng Dụng Flutter Web Trực Tuyến</h3>
                <p className="text-xs text-orange-100 max-w-xl">
                  Bạn có thể tải gói mã nguồn web đã biên dịch (spx_mobile_web_build.zip), giải nén và kéo thả trực tiếp lên Netlify Drop để chạy ứng dụng độc lập trên trình duyệt.
                </p>
              </div>

              <div className="flex flex-wrap items-center gap-2">
                <a
                  href="/downloads/spx_mobile_web_build.zip"
                  download
                  className="px-4 py-2.5 bg-white text-orange-600 hover:bg-orange-50 rounded-xl font-bold text-xs flex items-center gap-2 shadow-sm transition active:scale-95 cursor-pointer"
                >
                  <Download className="w-4 h-4" />
                  <span>Tải spx_mobile_web_build.zip</span>
                </a>

                <a
                  href="https://app.netlify.com/drop"
                  target="_blank"
                  rel="noreferrer"
                  className="px-4 py-2.5 bg-slate-900/40 hover:bg-slate-900/60 text-white rounded-xl font-bold text-xs flex items-center gap-2 border border-white/20 transition cursor-pointer"
                >
                  <ExternalLink className="w-4 h-4" />
                  <span>Trang Netlify Drop</span>
                </a>
              </div>
            </div>
          </div>

          {/* HƯỚNG DẪN CÁC BƯỚC UPLOAD NETLIFY DROP */}
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-4">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <Workflow className="w-4 h-4 text-orange-600" /> Hướng Dẫn Chi Tiết 3 Bước Chạy Bằng Netlify Drop
            </h3>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4 text-xs">
              <div className="p-4 rounded-xl border border-slate-200 bg-slate-50/70 space-y-2">
                <span className="w-6 h-6 rounded-full bg-orange-600 text-white font-bold flex items-center justify-center text-xs">1</span>
                <h4 className="font-bold text-slate-900">Bước 1: Tải & Giải Nén</h4>
                <p className="text-slate-600 text-[11px] leading-relaxed">
                  Bấm nút <b>Tải spx_mobile_web_build.zip</b> ở trên về máy tính, sau đó nhấp chuột phải chọn <b>Extract All (Giải nén)</b> để thu được thư mục web chứa các tệp index.html, main.dart.js, canvaskit...
                </p>
              </div>

              <div className="p-4 rounded-xl border border-slate-200 bg-slate-50/70 space-y-2">
                <span className="w-6 h-6 rounded-full bg-orange-600 text-white font-bold flex items-center justify-center text-xs">2</span>
                <h4 className="font-bold text-slate-900">Bước 2: Mở Netlify Drop</h4>
                <p className="text-slate-600 text-[11px] leading-relaxed">
                  Truy cập đường dẫn công cụ miễn phí: <a href="https://app.netlify.com/drop" target="_blank" rel="noreferrer" className="text-orange-600 font-bold underline">https://app.netlify.com/drop</a> trên trình duyệt web.
                </p>
              </div>

              <div className="p-4 rounded-xl border border-slate-200 bg-slate-50/70 space-y-2">
                <span className="w-6 h-6 rounded-full bg-orange-600 text-white font-bold flex items-center justify-center text-xs">3</span>
                <h4 className="font-bold text-slate-900">Bước 3: Kéo Thả & Khởi Chạy</h4>
                <p className="text-slate-600 text-[11px] leading-relaxed">
                  Kéo chính thư mục web đã giải nén thả vào khung tròn của Netlify. Chỉ sau vài giây, Netlify sẽ cấp cho bạn một đường link trực tuyến (VD: <a href="https://tranquil-figolla-16df43.netlify.app" target="_blank" rel="noreferrer" className="text-emerald-700 font-bold underline">tranquil-figolla-16df43.netlify.app</a>) để trải nghiệm app ngay lập tức!
                </p>
              </div>
            </div>
          </div>

          {/* GIAO DIỆN PHÂN TẦNG CỦA APP */}
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-6">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <Layers className="w-4 h-4 text-orange-600" /> Cấu Trúc Giao Diện Phân Tầng Của Ứng Dụng (Mobile UI Hierarchy)
            </h3>

            {/* TẦNG 1: ĐĂNG NHẬP & MENU CHÍNH */}
            <div className="space-y-3">
              <h4 className="text-xs font-bold text-slate-800 uppercase tracking-wider bg-orange-50 px-3 py-1.5 rounded-lg border border-orange-100 inline-block">
                Tầng 1: Đăng Nhập & Menu Điều Hướng Chính
              </h4>
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <p className="text-xs font-bold text-slate-800">1.1. Màn Hình Đăng Nhập</p>
                  <img src="/screenshots/01_app_login.png" alt="Đăng Nhập App" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Tài khoản mặc định: admin / 123456</p>
                </div>
                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <p className="text-xs font-bold text-slate-800">1.2. Menu Điều Hướng Chính (Home Menu)</p>
                  <img src="/screenshots/02_app_home_menu.png" alt="Menu Điều Hướng" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">4 chức năng: Phân loại, Tra cứu, Quản lý đơn, Tạo đơn</p>
                </div>
              </div>
            </div>

            {/* TẦNG 2: PHÂN LOẠI TO (3 TAB CHÍNH) */}
            <div className="space-y-3">
              <h4 className="text-xs font-bold text-slate-800 uppercase tracking-wider bg-orange-50 px-3 py-1.5 rounded-lg border border-orange-100 inline-block">
                Tầng 2: Phân Hệ Phân Loại & Gom Bao Hàng TO (3 Tab Theo Thứ Tự)
              </h4>
              <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <span className="text-[10px] bg-orange-100 text-orange-700 font-bold px-2 py-0.5 rounded">Tab 1: Create TO</span>
                  <p className="text-xs font-bold text-slate-800">Khởi Tạo Bao Hàng Mới</p>
                  <img src="/screenshots/04_app_create_to.png" alt="Create TO" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Sinh mã TO, chọn packer, quét gói đầu tiên gán Hub</p>
                </div>

                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <span className="text-[10px] bg-orange-100 text-orange-700 font-bold px-2 py-0.5 rounded">Tab 2: Scan TO</span>
                  <p className="text-xs font-bold text-slate-800">Quét Mã Kiểm Tra Bao</p>
                  <img src="/screenshots/05_app_scan_to.png" alt="Scan TO" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Quét camera nhanh thông tin bao hàng hiện có</p>
                </div>

                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <span className="text-[10px] bg-orange-100 text-orange-700 font-bold px-2 py-0.5 rounded">Tab 3: Table TO</span>
                  <p className="text-xs font-bold text-slate-800">Danh Sách Quản Lý Bao</p>
                  <img src="/screenshots/06_app_table_to.png" alt="Table TO" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Theo dõi trạng thái Packing/Packed, tải trọng, ReOpen</p>
                </div>
              </div>
            </div>

            {/* TẦNG 3: TRA CỨU & QUẢN LÝ ĐƠN HÀNG */}
            <div className="space-y-3">
              <h4 className="text-xs font-bold text-slate-800 uppercase tracking-wider bg-orange-50 px-3 py-1.5 rounded-lg border border-orange-100 inline-block">
                Tầng 3: Tra Cứu, In Phiếu Vận Đơn & Tạo Đơn Mới
              </h4>
              <div className="grid grid-cols-1 sm:grid-cols-3 gap-4">
                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <p className="text-xs font-bold text-slate-800">Tra Cứu Bưu Kiện & Xem Bill</p>
                  <img src="/screenshots/08_app_bill_modal.png" alt="Phiếu Bưu Kiện" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Mẫu tem Barcode Code128 & QR Code tiêu chuẩn SPX</p>
                </div>

                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <p className="text-xs font-bold text-slate-800">Quản Lý Toàn Bộ Đơn Kho</p>
                  <img src="/screenshots/09_app_quan_ly_don.png" alt="Quản Lý Đơn" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Phân biệt Inbound xanh lá & Outbound cam</p>
                </div>

                <div className="border border-slate-200 rounded-xl p-3 bg-slate-50/50 text-center space-y-2">
                  <p className="text-xs font-bold text-slate-800">Tạo Đơn Hàng Mới</p>
                  <img src="/screenshots/10_app_tao_don.png" alt="Tạo Bưu Kiện" className="mx-auto rounded-lg shadow-xs max-h-72 object-contain" />
                  <p className="text-[11px] text-slate-500">Tự động định tuyến tỉnh thành về 5 Hub bưu cục</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* TAB 4: SƠ ĐỒ CƠ SỞ DỮ LIỆU ERD */}
      {mainScope === "system" && activeTab === "erd" && (
        <div className="space-y-6">
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-4">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <Database className="w-4 h-4 text-orange-600" /> Sơ Đồ Thực Thể Quan Hệ (ERD - Entity Relationship Diagram)
            </h3>
            <p className="text-xs text-slate-600 leading-relaxed">
              Mô hình dữ liệu quan hệ giữa bảng <b>orders (Kiện hàng)</b>, <b>to_orders (Bao hàng trung chuyển)</b> và <b>users (Tài khoản điều phối)</b>:
            </p>

            {/* ERD GRAPHICAL BOXES */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6 pt-2">
              {/* TABLE: ORDERS */}
              <div className="border-2 border-orange-200 rounded-2xl overflow-hidden shadow-xs bg-white">
                <div className="bg-orange-600 text-white px-4 py-2.5 font-mono font-bold text-xs flex items-center justify-between">
                  <span>TABLE: orders</span>
                  <span className="text-[10px] bg-white/20 px-2 py-0.5 rounded">Quản lý bưu kiện</span>
                </div>
                <div className="p-3 text-xs font-mono space-y-1.5 divide-y divide-slate-100">
                  <div className="flex justify-between py-1 text-orange-700 font-bold">
                    <span>🔑 id (VARCHAR 50)</span>
                    <span className="text-[10px] bg-orange-100 px-1.5 rounded">PRIMARY KEY</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>nguoiGui / nguoiNhan</span>
                    <span className="text-slate-400">VARCHAR(255)</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>diaChiGui / diaChiNhan</span>
                    <span className="text-slate-400">TEXT</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>noiGui / noiNhan (Hub)</span>
                    <span className="text-slate-400">VARCHAR(255)</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>sanPham</span>
                    <span className="text-slate-400">VARCHAR(500)</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>soKi (kg) / giaTien (VND)</span>
                    <span className="text-slate-400">DECIMAL / INT</span>
                  </div>
                  <div className="flex justify-between py-1 font-semibold text-emerald-700">
                    <span>trangThai (Outbound/Inbound)</span>
                    <span className="text-slate-400">VARCHAR(50)</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>thoiGianTao / thoiGianDongBao</span>
                    <span className="text-slate-400">TIMESTAMP</span>
                  </div>
                  <div className="flex justify-between py-1 text-blue-700 font-bold bg-blue-50/50 px-1 rounded">
                    <span>🔗 maTO (VARCHAR 255)</span>
                    <span className="text-[10px] bg-blue-100 px-1.5 rounded">FOREIGN REF</span>
                  </div>
                </div>
              </div>

              {/* TABLE: TO_ORDERS */}
              <div className="border-2 border-blue-200 rounded-2xl overflow-hidden shadow-xs bg-white">
                <div className="bg-blue-600 text-white px-4 py-2.5 font-mono font-bold text-xs flex items-center justify-between">
                  <span>TABLE: to_orders</span>
                  <span className="text-[10px] bg-white/20 px-2 py-0.5 rounded">Bao hàng TO</span>
                </div>
                <div className="p-3 text-xs font-mono space-y-1.5 divide-y divide-slate-100">
                  <div className="flex justify-between py-1 text-blue-700 font-bold">
                    <span>🔑 maTO (VARCHAR 50)</span>
                    <span className="text-[10px] bg-blue-100 px-1.5 rounded">PRIMARY KEY</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>diaDiemGiaoHang (Hub đến)</span>
                    <span className="text-slate-400">TEXT (VD: CT, HN)</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>trangThai (Packing / Packed)</span>
                    <span className="text-slate-400">TEXT</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>packer (Nhân viên đóng)</span>
                    <span className="text-slate-400">TEXT</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>totalWeight (Tổng kg)</span>
                    <span className="text-slate-400">DOUBLE</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>SL (Số kiện hàng)</span>
                    <span className="text-slate-400">INT(11)</span>
                  </div>
                  <div className="flex justify-between py-1 text-slate-700">
                    <span>ngayTao / completeTime</span>
                    <span className="text-slate-400">TIMESTAMP / TEXT</span>
                  </div>
                  <div className="flex justify-between py-1 text-purple-700 font-bold bg-purple-50/50 px-1 rounded">
                    <span>📦 danhSachGoiHang</span>
                    <span className="text-[10px] bg-purple-100 px-1.5 rounded">JSON TEXT</span>
                  </div>
                </div>
              </div>
            </div>

            {/* QUAN HỆ 1 - N */}
            <div className="p-4 rounded-xl border border-slate-200 bg-slate-50 text-xs text-slate-600 leading-relaxed">
              <span className="font-bold text-slate-800">Mối quan hệ (1 - N):</span> Một bao hàng TO (<code className="text-blue-600 font-mono">to_orders</code>) có thể chứa nhiều kiện hàng (<code className="text-orange-600 font-mono">orders</code>). Mỗi kiện hàng tại một thời điểm chỉ thuộc về tối đa một bao TO thông qua trường liên kết <code className="text-blue-600 font-mono">orders.maTO</code> và mảng JSON <code className="text-purple-600 font-mono">to_orders.danhSachGoiHang</code>.
            </div>
          </div>
        </div>
      )}

      {/* TAB 5: APIS */}
      {mainScope === "system" && activeTab === "apis" && (
        <div className="space-y-4">
          <div className="bg-white rounded-2xl p-6 border border-orange-100 shadow-xs space-y-4">
            <h3 className="text-sm font-bold text-slate-900 flex items-center gap-2">
              <Server className="w-4 h-4 text-orange-600" /> Danh Mục RESTful API Đã Triển Khai
            </h3>

            <div className="space-y-2 text-xs">
              {[
                { method: "GET", path: "/orders/getIn4", desc: "Lấy danh sách đơn hàng có phân trang, tìm kiếm từ khóa và lọc theo Outbound / Inbound (đã tối ưu thứ tự thời gian)." },
                { method: "POST", path: "/orders", desc: "Tạo mới đơn hàng từ Web/App. Tích hợp auto-retry 5 lần tự động sinh mã mới nếu trùng." },
                { method: "POST", path: "/orders/scan/:id", desc: "Quét kiện hàng vào bao TO. Kiểm tra hợp lệ và nơi nhận trước khi cập nhật DB." },
                { method: "POST", path: "/orders/remove/:id", desc: "Gỡ kiện hàng khỏi bao TO, khôi phục trạng thái Outbound và tính lại khối lượng bao." },
                { method: "GET", path: "/TO", desc: "Lấy danh sách toàn bộ bao hàng trong kho (hỗ trợ lọc theo mã bao maTO hoặc trạng thái)." },
                { method: "POST", path: "/TO", desc: "Khởi tạo bao hàng mới với trạng thái Packing." },
                { method: "PUT", path: "/TO/:maTO", desc: "Cập nhật thông tin bao hàng, danh sách kiện hàng JSON, tổng khối lượng và trạng thái đóng bao." },
                { method: "PUT", path: "/TO/:maTO/reopen", desc: "Mở lại bao đã đóng (ReOpen) để tiếp tục quét thêm hoặc xóa đơn." },
                { method: "DELETE", path: "/TO/:maTO", desc: "Xóa thủ công bao hàng và tự động giải phóng các đơn bên trong về trạng thái Outbound." },
              ].map((api, idx) => (
                <div key={idx} className="p-3 bg-slate-50 rounded-xl border border-slate-200 flex flex-col sm:flex-row sm:items-center justify-between gap-2">
                  <div className="flex items-center gap-2">
                    <span className={`px-2 py-0.5 rounded text-[10px] font-mono font-bold ${
                      api.method === "GET" ? "bg-blue-100 text-blue-700" :
                      api.method === "POST" ? "bg-emerald-100 text-emerald-700" :
                      api.method === "PUT" ? "bg-amber-100 text-amber-700" : "bg-rose-100 text-rose-700"
                    }`}>
                      {api.method}
                    </span>
                    <span className="font-mono font-bold text-slate-800">{api.path}</span>
                  </div>
                  <span className="text-slate-500 text-[11px] sm:max-w-md text-left sm:text-right">{api.desc}</span>
                </div>
              ))}
            </div>
          </div>
        </div>
      )}
            </>
      )}
    </div>
  );
}