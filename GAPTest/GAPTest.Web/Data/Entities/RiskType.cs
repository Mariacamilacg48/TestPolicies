﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data.Entities
{
    public class RiskType
    {
        public int Id { get; set; }

        [Display(Name = "Risk Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<Policy> Policies { get; set; }
    }
}
