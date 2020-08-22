using GAPTest.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCoveringTypesAsync();
            await CheckRiskTypesAsync();
            await CheckCustomersAsync();
            //await CheckPoliciesAsync();
        }

        private async Task CheckCoveringTypesAsync()
        {
            if (!_context.CoveringType.Any())
            {
                _context.CoveringType.Add(new CoveringType { Name = "Earthquake" });
                _context.CoveringType.Add(new CoveringType { Name = "Fire" });
                _context.CoveringType.Add(new CoveringType { Name = "Stole" });
                _context.CoveringType.Add(new CoveringType { Name = "Lost" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRiskTypesAsync()
        {
            if (!_context.RiskType.Any())
            {
                _context.RiskType.Add(new RiskType { Name = "Low Risk" });
                _context.RiskType.Add(new RiskType { Name = "Medium Risk" });
                _context.RiskType.Add(new RiskType { Name = "Medium-High" });
                _context.RiskType.Add(new RiskType { Name = "High" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {
                AddCustomer("1017241448","María Camila Chica Gómez", "3217901412", "Carrera 50D. #65-62");
                AddCustomer("43735477", "María Anais Chica Gómez", "3216110278", "Carrera 50D. #65-62");
                AddCustomer("41303580", "Anais Gómez de Chica", "3122304483", "Carrera 50D. #65-62");
                await _context.SaveChangesAsync();

            }
        }

        private void AddCustomer(string document, 
            string name, 
            string cellphone, 
            string address)
        {
            _context.Customers.Add(new Customer
            {
                Document = document,
                Name = name,
                CellPhone = cellphone,
                Address = address,
            });
        }

        private async Task CheckPoliciesAsync()
        {
            if (!_context.Policies.Any())
            {
                AddPolicy("Aseguradora Solidaria de Colombia", "Canon de arrendamiento, cuotas de administración, servicios públicos, asistencia domiciliaria, renta, honorarios de abogado", DateTime.Parse("23/08/2020"), 12, 168000);
                AddPolicy("Equidad seguros", "Incumplimiento de los cánones de arrendamiento y en el pago de la administración de los servicios públicos por el arrendatario.", DateTime.Parse("25/08/2020"), 12, 200000);
                AddPolicy("Mapfre", "Canon de arrendamiento, cuotas de administración, servicios públicos, incluye asistencia domiciliaria y pérdida de arrendamiento por daños materiales.", DateTime.Parse("30/08/2020"), 24, 370000);
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
