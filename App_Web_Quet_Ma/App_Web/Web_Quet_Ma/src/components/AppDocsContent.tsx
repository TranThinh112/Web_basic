"use client";

import Image from "next/image";
import Link from "next/link";
import { Download, ExternalLink, Smartphone, CheckCircle, Key, Layers, ArrowRight, ShieldCheck, Sparkles, FolderArchive, HelpCircle } from "lucide-react";

export default function AppDocsContent() {
  const steps = [
    {
      step: "01",
      title: "Tải file nén Web Build của App",
      desc: "Tải gói spx_mobile_web_build.zip (đã build sẵn từ source Flutter Mobile & cấu hình trỏ thẳng Backend Railway).",
      action: (
        <a
          href="/downloads/spx_mobile_web_build.zip"
          download="spx_mobile_web_build.zip"
          className="inline-flex items-center gap-2 px-4 py-2.5 rounded-xl bg-orange-600 text-white font-bold text-sm shadow hover:bg-orange-700 transition"
        >
          <Download className="w-4 h-4" />
          Tải ZIP Mobile Web (12.8 MB)
        </a>
      ),
    },
    {
      step: "02",
      title: "Giải nén thư mục trên máy",
      desc: "Chuột phải vào spx_mobile_web_build.zip -> Chọn 'Extract All...' (Giải nén). Bạn sẽ nhận được folder chứa file index.html, flutter.js, main.dart.js, v.v.",
    },
    {
      step: "03",
      title: "Kéo thả vào Netlify Drop để tạo Link Online",
      desc: "Truy cập đường dẫn Netlify Drop, kéo toàn bộ thư mục vừa giải nén (hoặc các file bên trong) thả vào ô tròn upload để Netlify tự động cấp link chạy ngay lập tức.",
      action: (
        <a
          href="https://app.netlify.com/drop"
          target="_blank"
          rel="noopener noreferrer"
          className="inline-flex items-center gap-2 px-4 py-2.5 rounded-xl bg-slate-900 text-white font-bold text-sm shadow hover:bg-slate-800 transition"
        >
          <ExternalLink className="w-4 h-4 text-orange-400" />
          Mở Netlify Drop (app.netlify.com/drop)
        </a>
      ),
    },
    {
      step: "04",
      title: "Đăng nhập tài khoản Admin trải nghiệm",
      desc: "Mở đường link Netlify vừa được tạo và đăng nhập hệ thống với tài khoản quản trị mặc định.",
      badge: (
        <div className="inline-flex items-center gap-2 bg-amber-50 border border-amber-200 px-3 py-1.5 rounded-lg text-xs font-mono text-amber-900">
          <Key className="w-3.5 h-3.5 text-amber-600" />
          Tài khoản: <strong>admin</strong> | Mật khẩu: <strong>admin</strong>
        </div>
      ),
    },
  ];

  const tiers = [
    {
      tier: "TẦNG 1",
      title: "ĐĂNG NHẬP HỆ THỐNG & MENU CHÍNH (HOME)",
      desc: "Màn hình xác thực tài khoản quản trị viên và bảng điều hướng trung tâm các nghiệp vụ kho bãi.",
      badge: "Xác thực & Menu",
      items: [
        {
          src: "/screenshots/01_app_login.png",
          title: "1. Màn hình Đăng nhập App",
          desc: "Tài khoản: admin / Mật khẩu: admin. Tích hợp ghi nhớ phiên làm việc.",
        },
        {
          src: "/screenshots/02_app_home_menu.png",
          title: "2. Menu chính (Home Screen)",
          desc: "Dashboard điều hướng: Phân loại bao (TO), Quản lý đơn hàng, Tra cứu nhanh.",
        },
      ],
    },
    {
      tier: "TẦNG 2",
      title: "PHÂN LOẠI & SCAN VÀO BAO (3 TABS: TẠO TO - SCAN TO - TABLE TO)",
      desc: "Quy trình cốt lõi đóng bao vận chuyển: Tạo mã TO -> Quét mã đơn hàng vào bao -> Theo dõi bảng danh sách chi tiết kiện trong bao.",
      badge: "Core Workflow",
      items: [
        {
          src: "/screenshots/03_app_to_menu.png",
          title: "1. Menu Chức Năng TO",
          desc: "Bao gồm 3 chức năng: Tạo mã TO mới, Scan hàng vào TO và Danh sách TO đã tạo.",
        },
        {
          src: "/screenshots/04_app_create_to.png",
          title: "2. Tab 1: Tạo Bao TO (Create TO)",
          desc: "Nhập mã TO hoặc bấm Tự Động Tạo (vd: TO_HN_261107_...), chọn Kho đích đến (Hà Nội, Pleiku, Đà Nẵng,...).",
        },
        {
          src: "/screenshots/05_app_scan_to.png",
          title: "3. Tab 2: Scan Đơn Vào TO (Scan TO)",
          desc: "Camera Scanner tốc độ cao hoặc nhập tay mã đơn SPXVN... Kiểm tra nơi nhận tự động, trùng đích mới cho phép Inbound.",
        },
        {
          src: "/screenshots/06_app_table_to.png",
          title: "4. Tab 3: Bảng Danh Sách TO (Table TO)",
          desc: "Quản lý toàn bộ danh sách các mã bao TO đang mở / đã đóng, tổng số kiện và trọng lượng thực tế.",
        },
      ],
    },
    {
      tier: "TẦNG 3",
      title: "TRA CỨU VẬN ĐƠN & CHI TIẾT IN BILL",
      desc: "Tìm kiếm mọi mã đơn SPXVN, hiển thị trạng thái Inbound / Outbound, kho đích và xem phiếu gửi hàng.",
      badge: "Tracking & Bill",
      items: [
        {
          src: "/screenshots/07_app_tra_cuu.png",
          title: "1. Màn hình Tra Cứu Đơn Hàng",
          desc: "Quét Barcode/QR hoặc nhập mã đơn để tra cứu tức thì thông tin người gửi, người nhận, cước phí.",
        },
        {
          src: "/screenshots/08_app_bill_modal.png",
          title: "2. Modal Chi Tiết Vận Đơn (In Bill)",
          desc: "Mô phỏng phiếu gửi hàng SPX Express tiêu chuẩn, Barcode định danh và chi tiết gói cước.",
        },
      ],
    },
    {
      tier: "TẦNG 4",
      title: "QUẢN LÝ TỔNG ĐƠN HÀNG & TẠO ĐƠN MỚI",
      desc: "Quản trị cơ sở dữ liệu đơn hàng trong kho và tạo mới đơn xuất / nhập nhanh chóng.",
      badge: "Orders Hub",
      items: [
        {
          src: "/screenshots/09_app_quan_ly_don.png",
          title: "1. Menu Quản Lý Đơn Hàng",
          desc: "Phân bổ các nghiệp vụ: Xem tất cả đơn, Lọc theo trạng thái Inbound/Outbound, Tạo mới đơn hàng.",
        },
        {
          src: "/screenshots/10_app_tao_don.png",
          title: "2. Form Tạo Đơn Hàng Mới",
          desc: "Tạo đơn hàng kèm sinh mã SPXVN tự động có kiểm tra chống trùng lặp, lưu trực tiếp vào Backend Railway.",
        },
      ],
    },
  ];

  return (
    <div className="space-y-8 pb-16 max-w-7xl mx-auto">
      {/* HEADER BANNER */}
      <div className="bg-gradient-to-r from-orange-600 via-orange-500 to-amber-500 rounded-3xl p-6 sm:p-8 text-white shadow-xl shadow-orange-500/20 relative overflow-hidden">
        <div className="absolute right-0 top-0 translate-x-12 -translate-y-8 opacity-10 pointer-events-none">
          <Smartphone className="w-80 h-80" />
        </div>
        <div className="relative z-10 space-y-3 max-w-3xl">
          <div className="inline-flex items-center gap-2 px-3 py-1 bg-white/20 backdrop-blur-md rounded-full text-xs font-semibold uppercase tracking-wider text-orange-50">
            <Sparkles className="w-3.5 h-3.5" />
            Trải nghiệm SPX Mobile App trên Trình Duyệt Web
          </div>
          <h1 className="text-2xl sm:text-3xl font-extrabold tracking-tight">
            Giao diện App Mobile & Hướng dẫn Triển khai Web
          </h1>
          <p className="text-orange-100 text-sm leading-relaxed">
            Bộ giao diện chuẩn 10 màn hình thực tế của ứng dụng Flutter Mobile được sắp xếp theo từng tầng nghiệp vụ (Tạo TO, Scan TO, Table TO, Tra cứu, Tạo đơn). Bạn có thể tải ngay file Web Build bên dưới và đưa lên Netlify Drop để dùng ngay trên điện thoại hoặc trình duyệt!
          </p>
          <div className="pt-2 flex flex-wrap gap-3">
            <a
              href="/downloads/spx_mobile_web_build.zip"
              download="spx_mobile_web_build.zip"
              className="inline-flex items-center gap-2 px-4 py-2.5 rounded-xl bg-white text-orange-600 font-bold text-sm shadow hover:bg-orange-50 transition"
            >
              <Download className="w-4 h-4 text-orange-600" />
              Tải Gói Web Build (ZIP)
            </a>
            <a
              href="https://app.netlify.com/drop"
              target="_blank"
              rel="noopener noreferrer"
              className="inline-flex items-center gap-2 px-4 py-2.5 rounded-xl bg-slate-900/80 hover:bg-slate-900 text-white font-bold text-sm backdrop-blur transition"
            >
              <ExternalLink className="w-4 h-4 text-orange-400" />
              Mở Netlify Drop
            </a>
            <a
              href="https://spx-mobile.netlify.app"
              target="_blank"
              rel="noopener noreferrer"
              className="inline-flex items-center gap-2 px-4 py-2.5 rounded-xl bg-orange-700/60 hover:bg-orange-700 text-white font-medium text-sm backdrop-blur transition"
            >
              <Smartphone className="w-4 h-4 text-orange-300" />
              Xem bản Demo Đang Chạy
            </a>
          </div>
        </div>
      </div>

      {/* QUICK GUIDE CÀI ĐẶT / UPLOAD NETLIFY DROP */}
      <div className="bg-white rounded-3xl p-6 sm:p-8 border border-orange-200/80 shadow-sm space-y-6">
        <div className="flex items-center justify-between border-b border-orange-100 pb-4">
        {/* LÝ DO TẠI SAO DÙNG NETLIFY DROP */}
        <div className="p-4 sm:p-5 rounded-2xl bg-amber-50/80 border border-amber-200/90 text-xs text-amber-950 space-y-2">
          <div className="flex items-center gap-2 font-black text-amber-900 text-sm">
            <HelpCircle className="w-4 h-4 text-amber-600" />
            <span>Tại sao lại dùng Netlify Drop thay vì chạy bằng localhost thông thường?</span>
          </div>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-3 pt-1 text-slate-700">
            <div className="p-3 bg-white/80 rounded-xl border border-amber-100 space-y-1">
              <p className="font-bold text-amber-800">1. Không cần cài môi trường Flutter</p>
              <p className="text-[11px] leading-relaxed">
                Source mobile Flutter gốc yêu cầu cài đặt Flutter SDK, Dart, Android Studio, Gradle rất phức tạp (&gt;10 GB). Với gói Web Build thả vào Netlify Drop, người dùng mở trình duyệt là chạy ngay lập tức.
              </p>
            </div>
            <div className="p-3 bg-white/80 rounded-xl border border-amber-100 space-y-1">
              <p className="font-bold text-amber-800">2. Test trực tiếp trên Điện thoại thật</p>
              <p className="text-[11px] leading-relaxed">
                Netlify Drop tự động cấp URL công khai HTTPS toàn cầu (như <span className="font-mono text-orange-600 font-bold">spx-mobile.netlify.app</span>). Bạn có thể cầm điện thoại thật quét Barcode/QR mượt mà mà không lo bị chặn quyền Camera hay giới hạn mạng LAN.
              </p>
            </div>
            <div className="p-3 bg-white/80 rounded-xl border border-amber-100 space-y-1">
              <p className="font-bold text-amber-800">3. Máy chủ tĩnh 100% miễn phí &amp; vĩnh viễn</p>
              <p className="text-[11px] leading-relaxed">
                Netlify lưu trữ các file tĩnh HTML/JS/Wasm của Flutter Web trên hệ thống CDN toàn cầu, kết nối thẳng về Backend Railway không tốn thêm chi phí server hay tài nguyên máy cá nhân.
              </p>
            </div>
          </div>
        </div>

          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-2xl bg-orange-100 text-orange-600 flex items-center justify-center font-bold">
              <FolderArchive className="w-5 h-5" />
            </div>
            <div>
              <h2 className="text-lg font-bold text-slate-900">
                Hướng dẫn chạy Web App qua Netlify Drop (Không cần cài đặt)
              </h2>
              <p className="text-xs text-slate-500">
                Thao tác kéo thả trong 30 giây để có ngay đường link web mobile hoạt động vĩnh viễn
              </p>
            </div>
          </div>
          <span className="hidden sm:inline-flex items-center gap-1.5 text-xs font-semibold text-emerald-700 bg-emerald-50 px-3 py-1 rounded-full border border-emerald-200">
            <CheckCircle className="w-3.5 h-3.5" /> Đã kết nối Railway Backend
          </span>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
          {steps.map((s) => (
            <div
              key={s.step}
              className="bg-orange-50/50 hover:bg-orange-50 border border-orange-100 rounded-2xl p-5 flex flex-col justify-between transition group"
            >
              <div className="space-y-3">
                <span className="inline-block text-xs font-extrabold px-2.5 py-1 rounded-lg bg-orange-600 text-white shadow-sm">
                  BƯỚC {s.step}
                </span>
                <h3 className="font-bold text-sm text-slate-900 group-hover:text-orange-600 transition">
                  {s.title}
                </h3>
                <p className="text-xs text-slate-600 leading-relaxed">{s.desc}</p>
                {s.badge && <div className="pt-1">{s.badge}</div>}
              </div>
              {s.action && <div className="pt-4">{s.action}</div>}
            </div>
          ))}
        </div>

        <div className="p-4 rounded-2xl bg-slate-900 text-slate-200 text-xs flex flex-col sm:flex-row items-start sm:items-center justify-between gap-3">
          <div className="flex items-center gap-2">
            <ShieldCheck className="w-4 h-4 text-emerald-400 shrink-0" />
            <span>
              <strong>Lưu ý:</strong> Sau khi kéo thả vào Netlify Drop, Netlify sẽ cấp đường link dạng:{" "}
              <code className="text-orange-300 font-mono">https://[random-name].netlify.app</code>. Đăng nhập với:{" "}
              <code className="text-emerald-300 font-mono font-bold">admin / admin</code>.
            </span>
          </div>
          <a
            href="https://app.netlify.com/drop"
            target="_blank"
            rel="noopener noreferrer"
            className="text-orange-400 hover:text-orange-300 font-bold inline-flex items-center gap-1 shrink-0"
          >
            Đến trang Drop <ArrowRight className="w-3.5 h-3.5" />
          </a>
        </div>
      </div>

      {/* GALLERY 4 TẦNG UI APP MOBILE */}
      <div className="space-y-10">
        <div className="flex items-center gap-3">
          <div className="w-10 h-10 rounded-2xl bg-orange-600 text-white flex items-center justify-center font-bold shadow-md shadow-orange-600/30">
            <Layers className="w-5 h-5" />
          </div>
          <div>
            <h2 className="text-xl font-extrabold text-slate-900">
              Bộ Ảnh Giao Diện Thực Tế Của App Mobile (Sắp Xếp 4 Tầng)
            </h2>
            <p className="text-xs text-slate-500">
              Toàn bộ ảnh chụp thực tế từ ứng dụng Flutter, được chuẩn hóa theo đúng luồng tác nghiệp kho
            </p>
          </div>
        </div>

        {tiers.map((t, idx) => (
          <div
            key={t.tier}
            className="bg-white rounded-3xl p-6 sm:p-8 border border-orange-200/80 shadow-sm space-y-6"
          >
            {/* TIER HEADER */}
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-2 border-b border-orange-100 pb-4">
              <div>
                <div className="flex items-center gap-2">
                  <span className="text-xs font-black text-orange-600 tracking-wider bg-orange-100 px-2.5 py-0.5 rounded-full">
                    {t.tier}
                  </span>
                  <h3 className="text-base sm:text-lg font-bold text-slate-900">
                    {t.title}
                  </h3>
                </div>
                <p className="text-xs text-slate-500 mt-1">{t.desc}</p>
              </div>
              <span className="self-start sm:self-auto text-xs font-semibold px-3 py-1 rounded-full bg-slate-100 text-slate-700">
                {t.badge}
              </span>
            </div>

            {/* SCREENSHOTS GRID */}
            <div
              className={`grid grid-cols-1 sm:grid-cols-2 ${t.items.length > 2 ? "lg:grid-cols-4" : "lg:grid-cols-2 max-w-3xl"} gap-6`}
            >
              {t.items.map((item, i) => (
                <div
                  key={item.title}
                  className="bg-slate-50 rounded-2xl p-4 border border-slate-200/80 hover:border-orange-300 hover:shadow-md transition flex flex-col justify-between group"
                >
                  <div className="space-y-3">
                    <div className="w-full h-[440px] bg-slate-900 rounded-xl overflow-hidden shadow-inner flex items-center justify-center p-1.5">
                      <img
                        src={item.src}
                        alt={item.title}
                        className="w-full h-full object-contain rounded-lg transition-transform duration-300 group-hover:scale-105"
                        loading="lazy"
                      />
                    </div>
                    <h4 className="font-bold text-sm text-slate-900 group-hover:text-orange-600 transition">
                      {item.title}
                    </h4>
                  </div>
                  <p className="text-xs text-slate-600 mt-2 leading-relaxed">
                    {item.desc}
                  </p>
                </div>
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
