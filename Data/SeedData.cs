using EmployeeManager.Authorization;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

// dotnet aspnet-codegenerator razorpage -m Employee -dc Employee -outDir Pages\Employees --referenceScriptLibraries
namespace EmployeeManager.Data
{
    public static class SeedData
    {
        #region snippet_Initialize
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new EmployeeContext(
                serviceProvider.GetRequiredService<DbContextOptions<EmployeeContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.EmployeeAdministratorsRole);

                // allowed user can create and edit Employees that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.EmployeeManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if(user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }
            
            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        #endregion
        #region snippet1
        public static void SeedDB(EmployeeContext context, string adminID)
        {
            if (context.Employee.Any())
            {
                return;   // DB has been seeded
            }

            context.Employee.AddRange(
            #region snippet_Employee
                new Employee
                {
                    Name = "Melissa",
                    Surname = "Rogers",
                    Address = "165 Hall Valley Drive",
                    Position = "Chairwoman",
                    City = "Harrisville",
                    StartDate = DateTime.Parse("2020-3-13"),
                    Status = JobStatus.Submitted,
                    OwnerID = adminID
                },
            #endregion
            #endregion
                new Employee
                {
                    Name = "Thorsten",
                    Surname = "Weinrich",
                    Address = "5678 1st Ave W",
                    Position = "CEO",
                    City = "Redmond",
                    StartDate = DateTime.Parse("2020-3-13"),
                    Status = JobStatus.Submitted,
                    OwnerID = adminID
                },
             new Employee
             {
                 Name = "Thomas",
                 Surname = "Gilbert",
                 Address = "2795 Waldeck Street",
                 Position = "CFO",
                 City = "Frisco",
                 StartDate = DateTime.Parse("2020-3-13"),
                 Status = JobStatus.Submitted,
                 OwnerID = adminID
             },
             new Employee
             {
                 Name = "Amy",
                 Surname = "Holston",
                 Address = "1355 Coulter Lane",
                 Position = "IT Manager",
                 City = "Richmond",
                 StartDate = DateTime.Parse("2020-3-14"),
                 Status = JobStatus.Submitted,
                 OwnerID = adminID
             },
             new Employee
             {
                 Name = "Derek",
                 Surname = "Marinez",
                 Address = "1058 Pointe Lane",
                 Position = "Software Engineer",
                 City = "Hollywood",
                 StartDate = DateTime.Parse("2020-3-15"),
                 Status = JobStatus.Submitted,
                 OwnerID = adminID
             }
             );
            context.SaveChanges();
        }
    }
}
