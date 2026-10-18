using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Diseases - Entity Framework Version
    /// </summary>
    public class DiseaseDAL
    {
        public DiseaseDTO GetById(int diseaseId)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Diseases.Find(diseaseId);
            }
        }

        public List<DiseaseDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                return db.Diseases.OrderBy(d => d.DiseaseName).ToList();
            }
        }

        public List<DiseaseDTO> GetByCategory(string category)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Diseases
                    .Where(d => d.Category == category)
                    .OrderBy(d => d.DiseaseName)
                    .ToList();
            }
        }

        public List<DiseaseDTO> Search(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                return db.Diseases
                    .Where(d => d.DiseaseName.Contains(keyword))
                    .OrderBy(d => d.DiseaseName)
                    .ToList();
            }
        }

        public int Insert(DiseaseDTO disease)
        {
            using (var db = new ClinicDbContext())
            {
                db.Diseases.Add(disease);
                db.SaveChanges();
                return disease.DiseaseId;
            }
        }

        public bool Update(DiseaseDTO disease)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Diseases.Find(disease.DiseaseId);
                if (existing == null) return false;

                existing.DiseaseName = disease.DiseaseName;
                existing.Category = disease.Category;
                existing.Description = disease.Description;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int diseaseId)
        {
            using (var db = new ClinicDbContext())
            {
                var disease = db.Diseases.Find(diseaseId);
                if (disease == null) return false;

                db.Diseases.Remove(disease);
                return db.SaveChanges() > 0;
            }
        }
    }
}
