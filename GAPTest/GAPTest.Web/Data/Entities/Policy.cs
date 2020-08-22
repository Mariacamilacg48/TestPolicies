using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data.Entities
{
    public class Policy
    {
        public int Id { get; set; }

        [Display(Name = "Policy Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string PolicyName { get; set; }
       
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        [Display(Name = "Policy Start Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime PolicyStartDate { get; set; }


        [Display(Name = "Covering Period defined in motnhs")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int CoveringPeriod { get; set; }

        [Display(Name = "Policy Price")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Price { get; set; }

        public CoveringType CoveringType { get; set; }

        public RiskType RiskType{ get; set; }

        public Customer Customer { get; set; }

        public ICollection<PolicyCustomer> PolicyCustomers { get; set; }

    }
}
