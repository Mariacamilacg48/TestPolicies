using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GAPTest.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboRiskTypes();

        IEnumerable<SelectListItem> GetComboCoveringTypes();
    }
    
}