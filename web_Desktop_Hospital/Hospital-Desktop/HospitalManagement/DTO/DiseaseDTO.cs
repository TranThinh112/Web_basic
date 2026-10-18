using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// Data Transfer Object cho bảng Diseases
    /// </summary>
    public class DiseaseDTO
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string Category { get; set; }  // Seasonal (theo mùa), Common (thông thường)
        public string Description { get; set; }

        public DiseaseDTO()
        {
        }

        public DiseaseDTO(int diseaseId, string diseaseName, string category, string description)
        {
            DiseaseId = diseaseId;
            DiseaseName = diseaseName;
            Category = category;
            Description = description;
        }
    }
}
