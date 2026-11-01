import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "SPX Express Web Portal - Điều Phối & Quét Mã Vận Đơn",
  description: "Hệ thống quản lý, phân loại bưu kiện, gom bao TO và điều phối xe trung chuyển SPX Express",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="vi" suppressHydrationWarning>
      <body className="antialiased bg-slate-100 text-slate-900" suppressHydrationWarning>{children}</body>
    </html>
  );
}
