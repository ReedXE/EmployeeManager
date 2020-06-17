using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models
{

    public class Employee
    {
        public int Id { get; set; }

        
        public string OwnerID { get; set; }
        [StringLength(40, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }
        [StringLength(40, MinimumLength = 2)]
        [Required]
        public string Surname { get; set; }
        [StringLength(40)]
        [Required]
        public string Address { get; set; }
        [StringLength(40)]
        [Required]
        public string Position { get; set; }
        [StringLength(40)]
        [Required]
        public string City { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }
        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        public JobStatus Status { get; set; }
    }

    public enum JobStatus
    {
        Submitted,
        Employed,
        Disemployed
    }

}