using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public User User { get; set; }
        public ICollection<Policy> Policies { get; set; }

    }
}
