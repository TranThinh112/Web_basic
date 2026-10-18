using System;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer cho bảng Employees - Entity Framework Version
    /// </summary>
    public class EmployeeDAL
    {
        public List<EmployeeDTO> GetAll()
        {
            using (var db = new ClinicDbContext())
            {
                var employees = db.Employees.OrderBy(e => e.FullName).ToList();
                
                // Load username for linked users
                foreach (var emp in employees)
                {
                    if (emp.UserId.HasValue)
                    {
                        var user = db.Users.Find(emp.UserId.Value);
                        emp.Username = user?.Username;
                    }
                }
                return employees;
            }
        }

        public EmployeeDTO GetById(int id)
        {
            using (var db = new ClinicDbContext())
            {
                var emp = db.Employees.Find(id);
                if (emp != null && emp.UserId.HasValue)
                {
                    emp.Username = db.Users.Find(emp.UserId.Value)?.Username;
                }
                return emp;
            }
        }

        public EmployeeDTO GetByUserId(int userId)
        {
            using (var db = new ClinicDbContext())
            {
                var emp = db.Employees.FirstOrDefault(e => e.UserId == userId);
                if (emp != null)
                {
                    emp.Username = db.Users.Find(userId)?.Username;
                }
                return emp;
            }
        }

        public int Insert(EmployeeDTO emp)
        {
            using (var db = new ClinicDbContext())
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return emp.EmployeeId;
            }
        }

        public bool Update(EmployeeDTO emp)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.Employees.Find(emp.EmployeeId);
                if (existing == null) return false;

                existing.FullName = emp.FullName;
                existing.DateOfBirth = emp.DateOfBirth;
                existing.Gender = emp.Gender;
                existing.Phone = emp.Phone;
                existing.Email = emp.Email;
                existing.Address = emp.Address;
                existing.Position = emp.Position;
                existing.Department = emp.Department;
                existing.HireDate = emp.HireDate;
                existing.UserId = emp.UserId;

                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new ClinicDbContext())
            {
                var emp = db.Employees.Find(id);
                if (emp == null) return false;

                db.Employees.Remove(emp);
                return db.SaveChanges() > 0;
            }
        }

        public List<EmployeeDTO> Search(string keyword)
        {
            using (var db = new ClinicDbContext())
            {
                var employees = db.Employees
                    .Where(e => e.FullName.Contains(keyword) || 
                                e.Phone.Contains(keyword) || 
                                e.Position.Contains(keyword))
                    .OrderBy(e => e.FullName)
                    .ToList();

                foreach (var emp in employees)
                {
                    if (emp.UserId.HasValue)
                    {
                        emp.Username = db.Users.Find(emp.UserId.Value)?.Username;
                    }
                }
                return employees;
            }
        }
    }
}
