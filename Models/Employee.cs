using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models
{
    #region snippet1
    public class Employee
    {
        public int Id { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }

        public JobStatus Status { get; set; }
    }

    public enum JobStatus
    {
        Submitted,
        Employed,
        Disemployed
    }
    #endregion
}