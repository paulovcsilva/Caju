using Caju.Domain.Enums;
using Caju.Domain.Models;
using Caju.Domain.Ports;
using Caju.Domain.Servicies;
using Caju.Driven.Database.Configurations;

namespace Caju.Driven.Database
{
    public class CategoryRepository : ICategoryPorts
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TransactionCategory? GetCategory(string mcc, string merchantName)
        {
            var merchantMcc = _context.Merchants
                .Where(x => x.Name == merchantName)
                .Select(x => x.Mcc)
                .FirstOrDefault();

            if (MccCategoryMapping.MccCategories.TryGetValue(merchantMcc ?? mcc, out var category))
            {
                return category;
            }

            return null;
        }
    }
}
