using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GAPTest.Common.Models
{
    public class PolicyRequest
    {
        public int Id { get; set; }

        [Required]
        public string PolicyName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime PolicyStartDate { get; set; }

        [Required]
        public int CoveringPeriod { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public int CoveringTypeId { get; set; }

        [Required]
        public int RiskTypeId { get; set; }

        [Required]
        public int CustomerId { get; set; }


    }
}
