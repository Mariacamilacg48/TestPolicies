using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GAPTest.Common.Models
{
    public class UserRequest
    {
        [Required]
        public string Document { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CellPhone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }
       

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
