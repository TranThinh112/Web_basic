// Thuật toán chuẩn hóa & phân loại bưu cục / Hub đích dựa trên logic của SPX Express (tương thích TaoDon_logic.dart)

export const HUB_MAP: Record<string, string> = {
  HN: "Hub Hà Nội",
  HCM: "Hub TP.HCM",
  DN: "Hub Đà Nẵng",
  CT: "Hub Cần Thơ",
  MT: "Hub Miền Trung",
};

// 1. Ánh xạ 63 tỉnh/thành về 5 Hub vùng
export const PROVINCE_TO_REGION: Record<string, string> = {
  // Đông Nam Bộ -> Hub HCM
  "ho chi minh": "HCM",
  "binh duong": "HCM",
  "dong nai": "HCM",
  "ba ria vung tau": "HCM",
  "tay ninh": "HCM",
  "binh phuoc": "HCM",

  // Miền Tây -> Hub Cần Thơ
  "can tho": "CT",
  "an giang": "CT",
  "kien giang": "CT",
  "dong thap": "CT",
  "ca mau": "CT",
  "bac lieu": "CT",
  "soc trang": "CT",
  "vinh long": "CT",
  "tra vinh": "CT",
  "hau giang": "CT",
  "tien giang": "CT",
  "ben tre": "CT",
  "long an": "CT",

  // Khu vực Đà Nẵng -> Hub DN
  "da nang": "DN",
  "thua thien hue": "DN",
  "quang nam": "DN",
  "quang tri": "DN",

  // Miền Trung & Tây Nguyên -> Hub MT
  "quang ngai": "MT",
  "binh dinh": "MT",
  "phu yen": "MT",
  "khanh hoa": "MT",
  "ninh thuan": "MT",
  "binh thuan": "MT",
  "quang binh": "MT",
  "ha tinh": "MT",
  "nghe an": "MT",
  "thanh hoa": "MT",
  "kon tum": "MT",
  "gia lai": "MT",
  "dak lak": "MT",
  "dak nong": "MT",
  "lam dong": "MT",

  // Miền Bắc -> Hub Hà Nội
  "ha noi": "HN",
  "hai phong": "HN",
  "bac ninh": "HN",
  "hai duong": "HN",
  "hung yen": "HN",
  "nam dinh": "HN",
  "thai binh": "HN",
  "ninh binh": "HN",
  "vinh phuc": "HN",
  "phu tho": "HN",
  "thai nguyen": "HN",
  "bac giang": "HN",
  "lao cai": "HN",
  "yen bai": "HN",
  "tuyen quang": "HN",
  "cao bang": "HN",
  "lang son": "HN",
  "son la": "HN",
  "dien bien": "HN",
  "lai chau": "HN",
  "ha giang": "HN",
  "bac kan": "HN",
  "hoa binh": "HN",
  "quang ninh": "HN",
  "ha nam": "HN",
};

// 2. Tên viết tắt / Tên thông dụng (Alias)
export const PROVINCE_ALIAS: Record<string, string> = {
  hcm: "ho chi minh",
  sg: "ho chi minh",
  "sai gon": "ho chi minh",
  bd: "binh duong",
  pleiku: "gia lai",
  "nha trang": "khanh hoa",
  "buon ma thuot": "dak lak",
  bmt: "dak lak",
  "nghi son": "thanh hoa",
  hn: "ha noi",
  dn: "da nang",
  hue: "thua thien hue",
  vt: "ba ria vung tau",
  "vung tau": "ba ria vung tau",
  "ba ria": "ba ria vung tau",
  ct: "can tho",
};

// 3. Chuẩn hóa chuỗi (Bỏ dấu tiếng Việt, chữ thường, bỏ ký tự đặc biệt)
export function removeVietnameseTones(str: string): string {
  return str
    .normalize("NFD")
    .replace(/[\u0300-\u036f]/g, "")
    .replace(/đ/g, "d")
    .replace(/Đ/g, "D")
    .toLowerCase()
    .replace(/[^\w\s]/g, " ")
    .trim();
}

// 4. Áp dụng alias
export function applyAlias(input: string): string {
  return PROVINCE_ALIAS[input] || input;
}

// 5. Tính khoảng cách Levenshtein (Fuzzy Match)
export function levenshteinDistance(s1: string, s2: string): number {
  const m = s1.length;
  const n = s2.length;
  const dp: number[][] = Array.from({ length: m + 1 }, () => Array(n + 1).fill(0));

  for (let i = 0; i <= m; i++) dp[i][0] = i;
  for (let j = 0; j <= n; j++) dp[0][j] = j;

  for (let i = 1; i <= m; i++) {
    for (let j = 1; j <= n; j++) {
      const cost = s1[i - 1] === s2[j - 1] ? 0 : 1;
      dp[i][j] = Math.min(
        dp[i - 1][j] + 1,
        dp[i][j - 1] + 1,
        dp[i - 1][j - 1] + cost
      );
    }
  }
  return dp[m][n];
}

// 6. Trích xuất tỉnh thành từ chuỗi địa chỉ
export function extractProvince(address: string): string {
  let temp = removeVietnameseTones(address);
  if (!temp) return "unknown";

  temp = applyAlias(temp);

  // Khớp chính xác hoặc chứa trong chuỗi
  for (const province of Object.keys(PROVINCE_TO_REGION)) {
    if (temp.includes(province) || province.includes(temp)) {
      return province;
    }
  }

  // Khớp mờ Levenshtein
  let minDist = 999;
  let bestMatch = "unknown";

  for (const province of Object.keys(PROVINCE_TO_REGION)) {
    const dist = levenshteinDistance(temp, province);
    if (dist < minDist) {
      minDist = dist;
      bestMatch = province;
    }
  }

  return minDist <= 5 ? bestMatch : "unknown";
}

// 7. Nhận diện Hub từ địa chỉ
export function getRegionFromAddress(address: string): string {
  const province = extractProvince(address);
  return PROVINCE_TO_REGION[province] || "HN";
}

// Sinh mã vận đơn SPXVN đúng chuẩn logic (SPXVN + yearCode + 8 số ngẫu nhiên + monthCode)
export function generateSPXCode(): string {
  const now = new Date();
  const year = now.getFullYear();
  const yearCodeNum = year % 10 === 0 ? year % 100 : year % 10;
  const yearCode = String(yearCodeNum).padStart(2, "0");

  const month = now.getMonth() + 1;
  let monthCode = String(month);
  if (month === 10) monthCode = "A";
  if (month === 11) monthCode = "B";
  if (month === 12) monthCode = "C";

  let rand = "";
  for (let i = 0; i < 8; i++) {
    rand += Math.floor(Math.random() * 10).toString();
  }

  return `SPXVN${yearCode}${rand}${monthCode}`;
}
