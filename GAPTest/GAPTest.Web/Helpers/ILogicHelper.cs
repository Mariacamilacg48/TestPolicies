

using GAPTest.Web.Models;
using System.Threading.Tasks;

namespace GAPTest.Web.Helpers
{
    public interface ILogicHelper
    {
        bool IsHighRiskType(int riskTypeId);
    }
}