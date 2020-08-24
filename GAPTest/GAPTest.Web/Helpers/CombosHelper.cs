using GAPTest.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboRiskTypes()
        {
            var list = _dataContext.RiskTypes.Select(rt => new SelectListItem
            {
                Text = rt.Name,
                Value = $"{rt.Id}"
            }).OrderBy(ct => ct.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Policy risk type]",
                Value = "0"
            });

            return list;
        }
      

        public IEnumerable<SelectListItem> GetComboCoveringTypes()
        {
            var list = _dataContext.CoveringTypes.Select(ct => new SelectListItem
            {
                Text = ct.Name,
                Value = $"{ct.Id}"
            }).OrderBy(ct => ct.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Policy covering type]",
                Value = "0"
            });
            return list;
        }
    }
}
