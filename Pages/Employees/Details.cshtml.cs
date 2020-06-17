using EmployeeManager.Authorization;
using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeManager.Pages.Employees
{
    #region snippet
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            EmployeeContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await Context.Employee.FirstOrDefaultAsync(m => m.Id == id);

            if (Employee == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.EmployeeManagersRole) ||
                               User.IsInRole(Constants.EmployeeAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && Employee.Status != JobStatus.Employed)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, JobStatus status)
        {
            var employees = await Context.Employee.FirstOrDefaultAsync(
                                                      m => m.Id == id);

            if (employees == null)
            {
                return NotFound();
            }

            var employeesOperation = (status == JobStatus.Employed)
                                                       ? EmployeeOperations.Approve
                                                       : EmployeeOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, employees,
                                        employeesOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            employees.Status = status;
            Context.Employee.Update(employees);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    #endregion
}
