using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeManager.Pages.Administration
{
    [Authorize(Roles = "EmployeeAdministrators")]
    public class ListUserModel : PageModel
    {
        private readonly EmployeeContext _context;
        public ListUserModel(EmployeeContext application)
        {
            _context = application;
        }

        public List<IdentityUser> Users { get; private set; }

        public List<IdentityUserRole<string>> UsersRoles { get; set; }  // get my roles or context of user

        public List<IdentityRole> AllRoles { get; private set; }

        public void OnGet()
        {
            Users = _context.Users.ToList();
            UsersRoles = _context.UserRoles.ToList(); // get my roles or context 
            AllRoles = _context.Roles.ToList();
        }


    }
}