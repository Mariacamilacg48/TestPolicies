using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data.Entities
{
    public class PolicyCustomer
    {
        public int Id { get; set; }

        [Display(Name = "Covering Percentaje")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public float CoveringPercentage { get; set; }

        public bool State { get; set; }

        public Customer Customer { get; set; }

        public Policy Policy { get; set; }

    }
}
