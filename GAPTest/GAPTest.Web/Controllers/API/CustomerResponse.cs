using GAPTest.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Controllers.API
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public ICollection<PolicyResponse> Policies { get; set; }

    }
}
