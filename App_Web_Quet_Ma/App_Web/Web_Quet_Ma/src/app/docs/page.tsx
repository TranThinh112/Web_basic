"use client";

import DocsSystemView from "@/components/DocsSystemView";

export default function StandaloneDocsPage() {
  return (
    <div className="min-h-screen bg-[#FFF9F6] text-slate-800 font-sans flex flex-col">
      {/* HEADER CÔNG KHAI RIÊNG BIỆT - KHÔNG CÓ SIDEBAR VÀ KHÔNG CÓ NÚT Ở GÓC PHẢI */}
      <header className="h-16 bg-white border-b border-orange-100 px-4 sm:px-8 flex items-center justify-between sticky top-0 z-30 shadow-xs">
        <div className="flex items-center gap-3">
          <div className="w-8 h-8 rounded-xl bg-orange-600 flex items-center justify-center text-white font-black text-sm shadow-sm shadow-orange-600/30">
            S
          </div>
          <div>
            <div className="flex items-center gap-1.5 leading-none">
              <span className="font-black text-base text-orange-600 tracking-wide">SPX</span>
              <span className="font-black text-base text-slate-900 tracking-tight">EXPRESS</span>
            </div>
            <p className="text-[10px] text-slate-400 font-medium tracking-wider">Hệ Thống Phân Loại & Điều Phối Bưu Cục</p>
          </div>
        </div>
      </header>

      {/* NỘI DUNG TÀI LIỆU TOÀN MÀN HÌNH */}
      <main className="p-4 sm:p-8 flex-1 max-w-6xl mx-auto w-full">
        <DocsSystemView />
      </main>
    </div>
  );
}
