using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    public class CreateRoleModel
    {
        
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
