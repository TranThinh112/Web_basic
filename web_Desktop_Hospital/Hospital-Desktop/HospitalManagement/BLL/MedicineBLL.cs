using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.BLL
{
    public class MedicineBLL
    {
        private readonly MedicineDAL _medicineDAL;

        public MedicineBLL()
        {
            _medicineDAL = new MedicineDAL();
        }

        /// <summary>
        /// Lấy tất cả thuốc
        /// </summary>
        public List<MedicineDTO> GetAllMedicines()
        {
            return _medicineDAL.GetAll();
        }

        /// <summary>
        /// Lấy thuốc theo Id
        /// </summary>
        public MedicineDTO GetMedicineById(int medicineId)
        {
            return _medicineDAL.GetById(medicineId);
        }

        /// <summary>
        /// Tìm kiếm thuốc theo tên
        /// </summary>
        public List<MedicineDTO> SearchMedicines(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _medicineDAL.GetAll();
            return _medicineDAL.Search(keyword);
        }

        /// <summary>
        /// Thêm thuốc mới
        /// </summary>
        public int AddMedicine(MedicineDTO medicine)
        {
            ValidateMedicine(medicine);
            return _medicineDAL.Insert(medicine);
        }

        /// <summary>
        /// Cập nhật thuốc
        /// </summary>
        public bool UpdateMedicine(MedicineDTO medicine)
        {
            ValidateMedicine(medicine);
            return _medicineDAL.Update(medicine);
        }

        /// <summary>
        /// Xóa thuốc
        /// </summary>
        public bool DeleteMedicine(int medicineId)
        {
            return _medicineDAL.Delete(medicineId);
        }

        /// <summary>
        /// Validate thông tin thuốc
        /// </summary>
        private void ValidateMedicine(MedicineDTO medicine)
        {
            if (medicine == null)
                throw new ArgumentNullException(nameof(medicine));

            if (string.IsNullOrWhiteSpace(medicine.MedicineName))
                throw new ArgumentException("Tên thuốc không được để trống");

            if (!ValidationHelper.ValidateAmount(medicine.Price))
                throw new ArgumentException("Giá thuốc không hợp lệ");
        }
    }
}
