using EmployeeManager.Authorization;
using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Pages.Employees
{
    #region snippet
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
            EmployeeContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await Context.Employee.FirstOrDefaultAsync(
                                                 m => m.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                      User, Employee,
                                                      EmployeeOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Employee from DB to get OwnerID.
            var employees = await Context
                .Employee.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employees == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, employees,
                                                     EmployeeOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Employee.OwnerID = employees.OwnerID;

            Context.Attach(Employee).State = EntityState.Modified;

            if (Employee.Status == JobStatus.Employed)
            {
                // If the employees is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        employees,
                                        EmployeeOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    Employee.Status = JobStatus.Submitted;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    #endregion
}
