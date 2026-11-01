"use client";

import { useEffect, useRef, useState } from "react";
import { Camera, CameraOff, RefreshCw } from "lucide-react";

interface BarcodeScannerProps {
  onScan: (code: string) => void;
  active: boolean;
}

export default function BarcodeScanner({ onScan, active }: BarcodeScannerProps) {
  const [cameraStarted, setCameraStarted] = useState(false);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);
  const scannerRef = useRef<unknown>(null);
  const readerId = "html5qr-code-full-region";

  useEffect(() => {
    let isMounted = true;

    async function startScanner() {
      if (!active) {
        stopScanner();
        return;
      }

      try {
        const { Html5Qrcode } = await import("html5-qrcode");
        if (!isMounted) return;

        const scanner = new Html5Qrcode(readerId);
        scannerRef.current = scanner;

        await scanner.start(
          { facingMode: "environment" },
          {
            fps: 15,
            qrbox: { width: 280, height: 180 },
          },
          (decodedText) => {
            if (decodedText) {
              onScan(decodedText.trim());
            }
          },
          () => {
            // ignore scan frame errors
          }
        );
        if (isMounted) {
          setCameraStarted(true);
          setErrorMsg(null);
        }
      } catch (err: unknown) {
        if (isMounted) {
          setErrorMsg("Không tìm thấy camera hoặc quyền truy cập bị từ chối");
          setCameraStarted(false);
        }
        console.warn("Camera start error", err);
      }
    }

    startScanner();

    return () => {
      isMounted = false;
      stopScanner();
    };
  }, [active]);

  const stopScanner = async () => {
    try {
      if (scannerRef.current) {
        const scanner = scannerRef.current as { isScanning: boolean; stop: () => Promise<void>; clear: () => void };
        if (scanner.isScanning) {
          await scanner.stop();
        }
        scanner.clear();
        scannerRef.current = null;
      }
    } catch {
      // ignore
    } finally {
      setCameraStarted(false);
    }
  };

  return (
    <div className="relative w-full rounded-2xl overflow-hidden bg-slate-900 border border-slate-800 shadow-inner">
      <div id={readerId} className="w-full min-h-[220px]" />

      {!cameraStarted && !errorMsg && active && (
        <div className="absolute inset-0 flex flex-col items-center justify-center text-slate-400 bg-slate-900/80 p-4">
          <RefreshCw className="w-8 h-8 animate-spin text-orange-500 mb-2" />
          <p className="text-sm font-medium">Đang khởi động Camera quét mã...</p>
        </div>
      )}

      {errorMsg && (
        <div className="absolute inset-0 flex flex-col items-center justify-center text-rose-400 bg-slate-900/90 p-4 text-center">
          <CameraOff className="w-8 h-8 mb-2" />
          <p className="text-sm font-medium">{errorMsg}</p>
          <p className="text-xs text-slate-400 mt-1">
            Bạn vẫn có thể quét bằng máy quét mã vạch (Barcode Gun) hoặc nhập tay vào ô bên dưới!
          </p>
        </div>
      )}

      <div className="absolute top-2 right-2 bg-black/60 backdrop-blur-md px-2.5 py-1 rounded-full flex items-center gap-1.5 text-xs text-white">
        <Camera className="w-3.5 h-3.5 text-orange-400" />
        <span>Camera Scanner</span>
      </div>
    </div>
  );
}
