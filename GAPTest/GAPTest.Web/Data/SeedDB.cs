﻿using GAPTest.Web.Data.Entities;
using GAPTest.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1017241448", "María Camila Chica Gómez", "mariacamilacg48@gmail.com", "3217901412", "Carrera 50 D. #65-62. Prado Centro", "Admin");
            var customer = await CheckUserAsync("43735477", "María Anais Chica Gómez", "mariaannais@hotmail.com", "3216110278", "Carrera 50 D. #65-62. Prado Centro", "Customer");
            await CheckCoveringTypesAsync();
            await CheckRiskTypesAsync();
            await CheckCustomersAsync(customer);
            await CheckManagerAsync(manager);
           // await CheckPoliciesAsync();
            //await CheckPoliciesAsync();
        }
        //private void AddPolicy(string policyName, 
        //    string description, 
        //    DateTime policyStartDate, 
        //    int coveringPeriod,
        //    double price, 
        //    Customer customer, 
        //    CoveringType CoveringType, 
        //    RiskType riskType)
        //{
        //    _context.Policies.Add(new Policy
        //    {
        //        PolicyName = policyName,
        //        Description = description,
        //        PolicyStartDate = policyStartDate,
        //        CoveringPeriod = coveringPeriod,
        //        Price = price,
        //        Customer = customer,
        //        CoveringType = CoveringType,
        //        RiskType= riskType,

        //    });
        //}

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "abc@xyz.com",
                    Email = "abc@xyz.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "PasswordHere").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
        private async Task<User> CheckUserAsync(string document, 
            string name, 
            string email, 
            string cellPhone, 
            string address, 
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {

                user = new User
                {
                    Document = document,
                    Name = name,
                    Email = email,
                    UserName = email,
                    PhoneNumber = cellPhone,
                    Address = address,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                IdentityResult result = _userHelper.AddUserAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    _userHelper.AddUserToRoleAsync(user, role).Wait();
                }
                //await _userHelper.AddUserAsync(user, "1234");
                await _context.SaveChangesAsync();
                //await _userHelper.AddUserToRoleAsync(user, role);
               
            }

            return user;
        }
        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task CheckCoveringTypesAsync()
        {
            if (!_context.CoveringTypes.Any())
            {
                _context.CoveringTypes.Add(new CoveringType { Name = "Earthquake" });
                _context.CoveringTypes.Add(new CoveringType { Name = "Fire" });
                _context.CoveringTypes.Add(new CoveringType { Name = "Stole" });
                _context.CoveringTypes.Add(new CoveringType { Name = "Lost" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRiskTypesAsync()
        {
            if (!_context.RiskTypes.Any())
            {
                _context.RiskTypes.Add(new RiskType { Name = "Low Risk" });
                _context.RiskTypes.Add(new RiskType { Name = "Medium Risk" });
                _context.RiskTypes.Add(new RiskType { Name = "Medium-High" });
                _context.RiskTypes.Add(new RiskType { Name = "High" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomersAsync(User user)
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { User = user });
                await _context.SaveChangesAsync();
            }

        }
        private async Task CheckPoliciesAsync()
        {
            if (!_context.Policies.Any())
            {
                AddPolicy("Aseguradora Solidaria de Colombia", "Canon de arrendamiento, cuotas de administración, servicios públicos, asistencia domiciliaria, renta, honorarios de abogado", DateTime.Parse("23/08/2020"), 12, 168000);
                AddPolicy("Equidad seguros", "Incumplimiento de los cánones de arrendamiento y en el pago de la administración de los servicios públicos por el arrendatario.", DateTime.Parse("25/08/2020"), 12, 200000);
                AddPolicy("Mapfre", "Canon de arrendamiento, servicios públicos, incluye asistencia domiciliaria y pérdida de arrendamiento por daños materiales.", DateTime.Parse("30/08/2020"), 12, 350000);
                await _context.SaveChangesAsync();

            }
        }

        private void AddPolicy(string policyName,
            string description,
            DateTime policyStartDate,
            int coveringPeriod,
            double price)
        {
            _context.Policies.Add(new Policy
            {
                PolicyName = policyName,
                Description = description,
                PolicyStartDate = policyStartDate,
                CoveringPeriod = coveringPeriod,
                Price = price,
            });
        }

    }
}
