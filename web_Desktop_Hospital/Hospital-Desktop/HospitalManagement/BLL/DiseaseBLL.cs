using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.BLL
{
    /// <summary>
    /// Business Logic Layer cho Diseases (Bệnh)
    /// </summary>
    public class DiseaseBLL
    {
        private readonly DiseaseDAL _diseaseDAL;

        public DiseaseBLL()
        {
            _diseaseDAL = new DiseaseDAL();
        }

        /// <summary>
        /// Lấy tất cả bệnh
        /// </summary>
        public List<DiseaseDTO> GetAllDiseases()
        {
            return _diseaseDAL.GetAll();
        }

        /// <summary>
        /// Lấy bệnh theo Id
        /// </summary>
        public DiseaseDTO GetDiseaseById(int diseaseId)
        {
            return _diseaseDAL.GetById(diseaseId);
        }

        /// <summary>
        /// Lấy bệnh theo category
        /// </summary>
        public List<DiseaseDTO> GetDiseasesByCategory(string category)
        {
            return _diseaseDAL.GetByCategory(category);
        }

        /// <summary>
        /// Tìm kiếm bệnh
        /// </summary>
        public List<DiseaseDTO> SearchDiseases(string keyword)
        {
            return _diseaseDAL.Search(keyword);
        }

        /// <summary>
        /// Thêm bệnh mới
        /// </summary>
        public int AddDisease(DiseaseDTO disease)
        {
            ValidateDisease(disease);
            return _diseaseDAL.Insert(disease);
        }

        /// <summary>
        /// Cập nhật bệnh
        /// </summary>
        public bool UpdateDisease(DiseaseDTO disease)
        {
            ValidateDisease(disease);
            return _diseaseDAL.Update(disease);
        }

        /// <summary>
        /// Xóa bệnh
        /// </summary>
        public bool DeleteDisease(int diseaseId)
        {
            return _diseaseDAL.Delete(diseaseId);
        }

        /// <summary>
        /// Validate thông tin bệnh
        /// </summary>
        private void ValidateDisease(DiseaseDTO disease)
        {
            if (disease == null)
                throw new ArgumentNullException(nameof(disease));

            if (string.IsNullOrWhiteSpace(disease.DiseaseName))
                throw new ArgumentException("Tên bệnh không được để trống");

            if (disease.DiseaseName.Length > 200)
                throw new ArgumentException("Tên bệnh không được quá 200 ký tự");
        }
    }
}
