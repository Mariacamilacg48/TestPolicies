using GAPTest.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Models
{
    public class PolicyViewModel : Policy
    {

        public int CustomerId { get; set; }

        [Display(Name = "Policy Name")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Policy covering type")]
        public int CoveringTypeId { get; set; }

        [Display(Name = "Policy Name")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Policy risk type")]
        public int RiskTypeId { get; set; }

        public IEnumerable<SelectListItem> CoveringTypes { get; set; }
        public IEnumerable<SelectListItem> RiskTypes { get; set; }

    }
}
