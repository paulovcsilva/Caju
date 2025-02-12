using Caju.Domain.Enums;
using Caju.Domain.Models;

namespace Caju.Domain.Ports
{
    public interface IBalancePorts
    {
        public Balance GetBalance(string userId, TransactionCategory category);
    }
}
