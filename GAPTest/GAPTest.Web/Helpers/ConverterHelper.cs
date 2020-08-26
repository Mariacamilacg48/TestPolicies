using GAPTest.Common.Models;
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
        public async Task<Policy> ToPolicyAsync(PolicyViewModel model, bool IsNew)
        {
            var policy=  new Policy
            {
                PolicyName = model.PolicyName,
                Description = model.Description,
                Id = IsNew ? 0 : model.Id,
                PolicyStartDate = model.PolicyStartDate,
                CoveringPeriod = model.CoveringPeriod,
                Price = model.Price,
                State=model.State,
                CoveringType = await _dataContext.CoveringTypes.FindAsync(model.CoveringTypeId),
                RiskType = await _dataContext.RiskTypes.FindAsync(model.RiskTypeId),
                Customer = await _dataContext.Customers.FindAsync(model.CustomerId),
            };

            return policy;
        }

        public PolicyViewModel ToPolicyViewModel(Policy policy)
        {
            return new PolicyViewModel
            {
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
                State=policy.State,
                CoveringTypeId = policy.CoveringType.Id,
                RiskTypeId = policy.RiskType.Id,                
            };
        }

        public PolicyResponse ToPolicyResponse(Policy policy)
        {
            if (policy == null)
            {
                return null;
            }

            return new PolicyResponse
            {
                Id=policy.Id,
                PolicyName = policy.PolicyName,
                Description = policy.Description,
                PolicyStartDate = policy.PolicyStartDate,
                CoveringPeriod = policy.CoveringPeriod,
                Price = policy.Price,
                CoveringType = policy.CoveringType.Name,
                RiskType = policy.RiskType.Name,
            };
        }
    }
}
