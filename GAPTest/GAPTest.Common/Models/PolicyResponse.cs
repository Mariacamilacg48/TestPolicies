using System;
using System.Collections.Generic;
using System.Text;

namespace GAPTest.Common.Models
{
    public class PolicyResponse
    {
        public int Id { get; set; }
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public int CoveringPeriod { get; set; }
        public double Price { get; set; }
        public bool State { get; set; }
        public float CoveringPercentage { get; set; }
        public string CoveringType { get; set; }
        public string RiskType { get; set; }

    }
}
