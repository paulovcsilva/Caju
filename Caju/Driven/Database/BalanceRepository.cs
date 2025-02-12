using Caju.Domain.Enums;
using Caju.Domain.Models;
using Caju.Domain.Ports;
using Caju.Driven.Database.Configurations;

namespace Caju.Driven.Database
{
    public class BalanceRepository(ApplicationDbContext context) : IBalancePorts
    {
        private readonly ApplicationDbContext _context = context;

        public Balance GetBalance(string userId, TransactionCategory category)
        {
            var balance = _context.Balances.FirstOrDefault(x => x.Category == category && x.UserId == userId);

            if (balance == null)
            {
                throw new InvalidOperationException($"Balance not found");
            }

            return balance;
        }
    }
}
