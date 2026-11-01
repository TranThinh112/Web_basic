export interface Order {
  id: string; // SPXVN...
  noiNhan?: string;
  nguoiGui?: string;
  nguoiNhan?: string;
  sdtNhan?: string;
  diaChi?: string;
  soKi?: number;
  cod?: number;
  trangThai: 'Inbound' | 'Outbound' | 'Delivered' | string;
  maTO?: string | null;
  thoiGianDongBao?: string | null;
  createdAt?: string;
}

export interface TOOrder {
  id?: string;
  maTO: string;
  danhSachGoiHang: string[] | string;
  diaDiemGiaoHang: string;
  trangThai: 'Packing' | 'Complete' | 'Shipping' | string;
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
