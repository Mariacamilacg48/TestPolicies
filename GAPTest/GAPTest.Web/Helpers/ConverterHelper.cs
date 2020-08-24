using GAPTest.Web.Data;
using GAPTest.Web.Data.Entities;
using GAPTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public async Task<Policy> ToPolicyAsync(PolicyViewModel model)
        {
            return new Policy
            {
                PolicyCustomers=model.PolicyCustomers,
                PolicyName = model.PolicyName,
                Description = model.Description,
                PolicyStartDate = model.PolicyStartDate,
                CoveringPeriod = model.CoveringPeriod,
                Price = model.Price,
                CoveringType = await _dataContext.CoveringTypes.FindAsync(model.CoveringTypeId),
                RiskType = await _dataContext.RiskTypes.FindAsync(model.RiskTypeId),
                Customer = await _dataContext.Customers.FindAsync(model.CustomerId),
            };
        }

        public PolicyViewModel ToPolicyViewModel(Policy policy)
        {
            return new PolicyViewModel
            {
                PolicyCustomers = policy.PolicyCustomers,
                PolicyName = policy.PolicyName,
                Description = policy.Description,
                PolicyStartDate = policy.PolicyStartDate,
                CoveringPeriod = policy.CoveringPeriod,
                Price = policy.Price,
                Customer = policy.Customer,
                Id=policy.Id,
                CoveringTypes = _combosHelper.GetComboCoveringTypes(),
                RiskTypes = _combosHelper.GetComboRiskTypes(),
                CustomerId = policy.Customer.Id,
                CoveringTypeId = policy.CoveringType.Id,
                RiskTypeId = policy.RiskType.Id,
                
            };
        }
    }
}
