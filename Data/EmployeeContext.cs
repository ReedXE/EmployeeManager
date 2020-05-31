using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeManager.Data
{
    public class EmployeeContext : IdentityDbContext
    {
        public EmployeeContext (DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeManager.Models.Employee> Employee { get; set; }
    }
}
