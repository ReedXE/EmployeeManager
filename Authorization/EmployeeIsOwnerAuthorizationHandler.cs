﻿using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EmployeeManager.Authorization
{
    public class EmployeeIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Employee>
    {
        UserManager<IdentityUser> _userManager;

        public EmployeeIsOwnerAuthorizationHandler(UserManager<IdentityUser> 
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Employee resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }


            if (requirement.Name == Constants.ReadOperationName)
            {
                return Task.CompletedTask;
            }

            

            return Task.CompletedTask;
        }
    }
}
