using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="{0} required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Enter a valid email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        [Display(Name = "Base Salary")]
        [DataType(DataType.Currency)]
        [Range(100, 50000, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord record)
        {
            Sales.Add(record);
        }

        public void RemoveSales(SalesRecord record)
        {
            Sales.Remove(record);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(s => s.Date >= initial && s.Date <= final && s.Status == Enums.SaleStatus.Billed).Sum(s => s.Amount);
        }
    }
}
