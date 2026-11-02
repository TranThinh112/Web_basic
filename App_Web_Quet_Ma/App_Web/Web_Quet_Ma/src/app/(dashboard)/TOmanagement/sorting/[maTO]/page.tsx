"use client";

import { use } from "react";
import PhanLoaiScanView from "@/components/PhanLoaiScanView";

export default function DynamicSortingTOPage({
  params,
}: {
  params: Promise<{ maTO: string }>;
}) {
  const resolvedParams = use(params);
  return <PhanLoaiScanView initialMaTO={resolvedParams.maTO} />;
}