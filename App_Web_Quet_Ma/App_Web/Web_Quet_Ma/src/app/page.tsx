"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";

export default function RootPage() {
  const router = useRouter();

  useEffect(() => {
    const saved = localStorage.getItem("spx_user");
    if (saved) {
      router.replace("/dispatch");
    } else {
      router.replace("/login");
    }
  }, [router]);

  return (
    <div className="min-h-screen bg-white flex items-center justify-center text-xs font-semibold text-orange-600">
      Đang chuyển hướng...
    </div>
  );
}
