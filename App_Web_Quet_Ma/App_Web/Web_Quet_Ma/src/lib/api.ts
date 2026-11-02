import { Order, TOOrder } from "@/types";

const getBaseUrl = () => {
  if (typeof window !== "undefined") {
    const saved = localStorage.getItem("spx_backend_url");
    if (saved) return saved.replace(/\/$/, "");
  }
  // Mặc định kết nối thẳng tới Railway Backend đã deploy
  return process.env.NEXT_PUBLIC_API_URL || "https://webbasic-production-47cd.up.railway.app";
};

export const api = {
  getBaseUrl,

  // Lấy danh sách đơn hàng có phân trang / search / lọc
  async getOrders(params?: { id?: string; trangThai?: string; keyword?: string; page?: number; limit?: number }) {
    const query = new URLSearchParams();
    if (params?.id) query.append("id", params.id);
    if (params?.trangThai) query.append("trangThai", params.trangThai);
    if (params?.keyword) query.append("keyword", params.keyword);
    if (params?.page) query.append("page", String(params.page));
    if (params?.limit) query.append("limit", String(params.limit));

    const res = await fetch(`${getBaseUrl()}/orders/getIn4?${query.toString()}`);
    if (!res.ok) throw new Error("Không thể tải danh sách đơn hàng");
    return (await res.json()) as { data: Order[]; total: number; inbound: number };
  },

  // Tìm 1 đơn hàng theo ID
  async getOrderById(id: string): Promise<Order | null> {
    try {
      const res = await fetch(`${getBaseUrl()}/orders/getIn4?id=${encodeURIComponent(id)}`);
      if (!res.ok) return null;
      const json = await res.json();
      if (json.data && json.data.length > 0) {
        return json.data[0];
      }
      return null;
    } catch {
      return null;
    }
  },

  // Quét đơn vào bao TO
  async scanOrder(id: string, maTO: string): Promise<{ success: boolean; message?: string; order?: Order }> {
    try {
      const res = await fetch(`${getBaseUrl()}/orders/scan/${encodeURIComponent(id)}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ maTO }),
      });
      const data = await res.json();
      if (res.ok) {
        return { success: true, ...data };
      }
      return { success: false, message: data.message || "Lỗi xử lý quét đơn" };
    } catch (err: unknown) {
      return { success: false, message: err instanceof Error ? err.message : "Mất kết nối máy chủ Railway" };
    }
  },

  // Bỏ đơn ra khỏi bao TO
  async removeOrderFromTO(id: string): Promise<boolean> {
    try {
      const res = await fetch(`${getBaseUrl()}/orders/remove/${encodeURIComponent(id)}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
      });
      return res.ok;
    } catch {
      return false;
    }
  },

  // Tạo đơn hàng mới
  async createOrder(order: Partial<Order>): Promise<Order | null> {
    try {
      const res = await fetch(`${getBaseUrl()}/orders`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(order),
      });
      if (res.ok) {
        return await res.json();
      }
      return null;
    } catch {
      return null;
    }
  },

  // === MODULE TRANSFER ORDER (TO) ===
  async getAllTOs(): Promise<TOOrder[]> {
    try {
      const res = await fetch(`${getBaseUrl()}/TO`);
      if (!res.ok) return [];
      return await res.json();
    } catch {
      return [];
    }
  },

  async getOneTO(maTO: string): Promise<TOOrder | null> {
    try {
      const res = await fetch(`${getBaseUrl()}/TO?maTO=${encodeURIComponent(maTO)}`);
      if (!res.ok) return null;
      const data = await res.json();
      if (Array.isArray(data) && data.length > 0) return data[0];
      if (data && typeof data === "object") return data;
      return null;
    } catch {
      return null;
    }
  },

  async createTO(to: TOOrder): Promise<boolean> {
    try {
      const res = await fetch(`${getBaseUrl()}/TO`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          ...to,
          danhSachGoiHang: Array.isArray(to.danhSachGoiHang) ? to.danhSachGoiHang : [],
        }),
      });
      return res.ok;
    } catch {
      return false;
    }
  },

  async updateTO(to: TOOrder): Promise<boolean> {
    try {
      const res = await fetch(`${getBaseUrl()}/TO/${encodeURIComponent(to.maTO)}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          ...to,
          danhSachGoiHang: Array.isArray(to.danhSachGoiHang) ? to.danhSachGoiHang : [],
        }),
      });
      return res.ok;
    } catch {
      return false;
    }
  },

  async reopenTO(maTO: string): Promise<boolean> {
    try {
      const res = await fetch(`${getBaseUrl()}/TO/${encodeURIComponent(maTO)}/reopen`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
      });
      return res.ok;
    } catch {
      return false;
    }
  },

  async deleteTO(maTO: string): Promise<boolean> {
    try {
      const res = await fetch(`${getBaseUrl()}/TO/${encodeURIComponent(maTO)}`, {
        method: "DELETE",
      });
      return res.ok;
    } catch {
      return false;
    }
  },

  // === USER AUTH ===
  async login(username: string, password?: string) {
    let url = `${getBaseUrl()}/login/users/${encodeURIComponent(username)}`;
    if (password) url += `/${encodeURIComponent(password)}`;
    const res = await fetch(url);
    if (!res.ok) return null;
    const data = await res.json();
    return Array.isArray(data) ? data[0] : data;
  },

  async changePassword(username: string, password: string): Promise<boolean> {
    const res = await fetch(`${getBaseUrl()}/repass/users/${encodeURIComponent(username)}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ password }),
    });
    return res.ok;
  },
};
