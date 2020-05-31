using EmployeeManager.Authorization;
using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Pages.Employees
{
    public class IndexModel : DI_BasePageModel
    {
        private readonly EmployeeManager.Data.EmployeeContext _context;
        public IndexModel(
            EmployeeContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public IList<Employee> Employee { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Position { get; set; }
        [BindProperty(SupportsGet = true)]
        public string EmployeePos { get; set; }

        public async Task OnGetAsync()
        {
            var employees = from m in _context.Employee
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                employees = employees.Where(s => s.Surname.Contains(SearchString));
            }


            IQueryable<string> posQuery = from m in _context.Employee
                                            orderby m.Position
                                            select m.Position;


            if (!string.IsNullOrEmpty(SearchString))
            {
                employees = employees.Where(s => s.Surname.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(EmployeePos))
            {
                employees = employees.Where(x => x.Position == EmployeePos);
            }
            Position = new SelectList(await posQuery.Distinct().ToListAsync());

            Employee = await employees.ToListAsync();
        }


    }

}