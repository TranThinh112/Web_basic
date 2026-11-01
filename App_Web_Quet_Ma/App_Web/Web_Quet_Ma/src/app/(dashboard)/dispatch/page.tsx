"use client";

import { useState, useEffect } from "react";
import ChuyenXeView from "@/components/ChuyenXeView";
import { api } from "@/lib/api";

export default function DispatchPage() {
  const [availableTOs, setAvailableTOs] = useState<string[]>([]);

  useEffect(() => {
    async function loadTOs() {
      try {
        const list = await api.getAllTOs();
        setAvailableTOs(list.map((item) => item.maTO));
      } catch {
        setAvailableTOs(["TO_881230", "TO_881231", "TO_881232"]);
      }
    }
    loadTOs();
  }, []);

  return <ChuyenXeView availableTOs={availableTOs} />;
}
