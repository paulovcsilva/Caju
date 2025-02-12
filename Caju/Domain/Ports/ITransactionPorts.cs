using Caju.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caju.Domain.Ports
{
    public interface ITransactionPorts
    {
        public void ProcessTransactionDebit(Balance balance, decimal newAmount);
    }
}
