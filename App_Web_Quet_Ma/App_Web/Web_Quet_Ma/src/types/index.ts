export interface Order {
  id: string; // SPXVN...
  noiGui?: string;
  noiNhan?: string;
  nguoiGui?: string;
  nguoiNhan?: string;
  diaChiGui?: string;
  diaChiNhan?: string;
  sanPham?: string;
  soKi?: number;
  giaTien?: number;
  cod?: number;
  trangThai: 'Inbound' | 'Outbound' | 'Delivered' | string;
  maTO?: string | null;
  thoiGianTao?: string | null;
  thoiGianDongBao?: string | null;
  createdAt?: string;
}

export interface TOOrder {
  id?: string;
  maTO: string;
  danhSachGoiHang: any[] | string;
  diaDiemGiaoHang: string;
  trangThai: 'Packing' | 'Packed' | 'Complete' | 'Shipping' | string;
  packer: string;
  ngayTao: string;
  completeTime?: string | null;
  totalWeight: number;
}

export interface User {
  id: number;
  username: string;
  name?: string;
  role?: string;
}

export interface ChuyenXe {
  id: string;
  maChuyen: string;
  tuyenDuong: string;
  bienSo: string;
  taiXe: string;
  sdtTaiXe: string;
  trangThai: 'ChoBocHang' | 'DangTaiHang' | 'DangTrungChuyen' | 'HoanThanh';
  thoiGianXuatBen?: string;
  soLuongTO: number;
  soLuongKien: number;
  dsMaTO: string[];
}

