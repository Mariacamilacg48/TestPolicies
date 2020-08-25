using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GAPTest.Common.Models
{
    public class EmailRequestcs
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
