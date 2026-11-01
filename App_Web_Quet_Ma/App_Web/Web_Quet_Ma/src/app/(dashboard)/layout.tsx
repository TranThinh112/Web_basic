"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { usePathname, useRouter } from "next/navigation";
import {
  Truck,
  Scan,
  Package,
  Search,
  PlusCircle,
  Settings,
  LogOut,
  ChevronRight,
  Menu,
  X,
  Volume2,
  VolumeX,
} from "lucide-react";
import { sounds } from "@/lib/sound";

const NAV_ITEMS = [
  { href: "/dispatch", label: "Quản lý chuyến xe", icon: Truck },
  { href: "/sorting", label: "Phân loại & quét TO", icon: Scan },
  { href: "/transfer-orders", label: "Danh sách bao hàng", icon: Package },
  { href: "/tracking", label: "Tra cứu vận đơn", icon: Search },
  { href: "/create-order", label: "Tạo mới đơn hàng", icon: PlusCircle },
  { href: "/settings", label: "Cài đặt & thiết bị", icon: Settings },
];

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
  const pathname = usePathname();
  const router = useRouter();

  const [currentUser, setCurrentUser] = useState<{ username: string; name?: string } | null>(null);
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [soundActive, setSoundActive] = useState(true);

  useEffect(() => {
    const saved = localStorage.getItem("spx_user");
    if (!saved) {
      router.replace("/login");
    } else {
      try {
        setCurrentUser(JSON.parse(saved));
      } catch {
        router.replace("/login");
      }
    }
  }, [router]);

  const handleLogout = () => {
    if (confirm("Bạn có chắc muốn đăng xuất?")) {
      localStorage.removeItem("spx_user");
      router.replace("/login");
    }
  };

  const currentNavItem = NAV_ITEMS.find((item) => pathname.startsWith(item.href)) || NAV_ITEMS[0];

  return (
    <div className="min-h-screen bg-[#FFF9F6] text-slate-800 flex font-sans">
      {/* SIDEBAR TONE CAM & TRẮNG - CỐ ĐỊNH KHÔNG RENDER LẠI */}
      <aside
        className={`fixed inset-y-0 left-0 z-40 w-64 bg-white border-r border-orange-200/70 flex flex-col justify-between transition-transform duration-200 lg:static lg:translate-x-0 ${
          mobileMenuOpen ? "translate-x-0" : "-translate-x-full"
        }`}
      >
        <div>
          {/* LOGO */}
          <div className="h-16 flex items-center justify-between px-6 border-b border-orange-100 bg-white">
            <Link href="/dispatch" className="flex items-center gap-2.5">
              <div className="w-8 h-8 rounded-xl bg-orange-600 flex items-center justify-center text-white font-black text-sm shadow-sm shadow-orange-600/30">
                S
              </div>
              <div>
                <div className="flex items-center gap-1.5 leading-none">
                  <span className="font-black text-base text-orange-600 tracking-wide">SPX</span>
                  <span className="font-bold text-xs text-slate-800">EXPRESS</span>
                </div>
                <p className="text-[10px] text-slate-400 mt-0.5">Bưu cục phân loại</p>
              </div>
            </Link>

            <button
              onClick={() => setMobileMenuOpen(false)}
              className="lg:hidden p-1 text-slate-400 hover:text-slate-700"
            >
              <X className="w-5 h-5" />
            </button>
          </div>

          {/* HUB INFO */}
          <div className="mx-4 my-3 p-3 bg-orange-50/70 rounded-xl border border-orange-100">
            <p className="text-[10px] font-bold text-orange-600 uppercase tracking-wider">Trạm trung chuyển</p>
            <p className="text-xs font-bold text-slate-800 mt-0.5">SOC TP.Hồ Chí Minh</p>
          </div>

          {/* MENU CỐ ĐỊNH, CHUYỂN TAB MƯỢT MÀ */}
          <nav className="p-3 space-y-1">
            {NAV_ITEMS.map((item) => {
              const Icon = item.icon;
              const isActive = pathname === item.href || pathname.startsWith(item.href + "/");
              return (
                <Link
                  key={item.href}
                  href={item.href}
                  onClick={() => setMobileMenuOpen(false)}
                  className={`w-full flex items-center gap-3 px-3.5 py-2.5 rounded-xl text-xs font-semibold transition-colors duration-150 ${
                    isActive
                      ? "bg-orange-600 text-white shadow-md shadow-orange-600/25"
                      : "text-slate-600 hover:bg-orange-50 hover:text-orange-600"
                  }`}
                >
                  <Icon className="w-4 h-4 shrink-0" />
                  <span>{item.label}</span>
                </Link>
              );
            })}
          </nav>
        </div>

        {/* USER INFO & LOGOUT */}
        <div className="p-4 border-t border-orange-100 bg-orange-50/20 flex items-center justify-between text-xs">
          <div>
            <p className="font-bold text-slate-800">{currentUser?.username || "admin"}</p>
            <p className="text-[11px] text-slate-500">{currentUser?.name || "Nhân viên Bưu cục"}</p>
          </div>

          <button
            onClick={handleLogout}
            title="Đăng xuất"
            className="p-2 text-slate-400 hover:text-orange-600 hover:bg-orange-100/50 rounded-xl transition-colors"
          >
            <LogOut className="w-4 h-4" />
          </button>
        </div>
      </aside>

      {/* MOBILE BACKDROP */}
      {mobileMenuOpen && (
        <div
          onClick={() => setMobileMenuOpen(false)}
          className="fixed inset-0 z-30 bg-black/30 backdrop-blur-xs lg:hidden"
        />
      )}

      {/* MAIN CONTAINER */}
      <div className="flex-1 flex flex-col min-w-0">
        <header className="h-16 bg-white border-b border-orange-100 px-4 sm:px-6 flex items-center justify-between sticky top-0 z-20">
          <div className="flex items-center gap-2">
            <button
              onClick={() => setMobileMenuOpen(true)}
              className="lg:hidden p-1.5 text-slate-600 hover:bg-orange-50 rounded-lg"
            >
              <Menu className="w-5 h-5" />
            </button>
            <div className="flex items-center gap-1.5 text-xs text-slate-500">
              <span className="text-orange-600 font-bold">SPX Express</span>
              <ChevronRight className="w-3.5 h-3.5 text-slate-300" />
              <span className="font-bold text-slate-900">{currentNavItem.label}</span>
            </div>
          </div>

          <div className="flex items-center gap-3">
            <button
              onClick={() => {
                const s = !soundActive;
                setSoundActive(s);
                sounds.enabled = s;
              }}
              className="px-3 py-1.5 rounded-xl border border-orange-200 bg-orange-50/60 text-xs text-orange-700 hover:bg-orange-100 flex items-center gap-1.5 transition font-medium"
            >
              {soundActive ? <Volume2 className="w-3.5 h-3.5 text-orange-600" /> : <VolumeX className="w-3.5 h-3.5 text-slate-400" />}
              <span>{soundActive ? "Âm thanh: Bật" : "Âm thanh: Tắt"}</span>
            </button>

            <div className="flex items-center gap-1.5 px-3 py-1 bg-white border border-orange-200 rounded-xl text-xs text-slate-700 font-medium">
              <span className="w-2 h-2 rounded-full bg-emerald-500" />
              <span>Máy chủ Online</span>
            </div>
          </div>
        </header>

        <main className="p-4 sm:p-6 flex-1 max-w-6xl mx-auto w-full">
          {children}
        </main>
      </div>
    </div>
  );
}
