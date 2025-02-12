using Caju.Domain.Models;
using Caju.Domain.Ports;
using Caju.Driven.Database.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Caju.Driven.Database
{
    public class TransactionRepository(ApplicationDbContext context) : ITransactionPorts
    {
        private readonly ApplicationDbContext _context = context;

        public void ProcessTransactionDebit(Balance balance, decimal newAmount)
        {
            balance.TotalAmount = newAmount;
            _context.SaveChanges();
        }
    }
}
