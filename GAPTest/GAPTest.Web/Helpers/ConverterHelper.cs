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

        public ConverterHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Policy> ToPolicyAsync(PolicyViewModel model)
        {
            var policy= new Policy
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

            if(model.Id != 0)
            {
                policy.Id = model.Id;
            }

            return policy;
        }
    }
}
