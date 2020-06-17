using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManager.Pages.Administration
{
    [Authorize(Roles = "EmployeeAdministrators")]
    public class CreateRole : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public CreateRole(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public CreateRoleModel RoleName { get; set; }

        public async Task<IActionResult> OnPostAsync(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {

                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}
