using Caju.Domain.Enums;

namespace Caju.Domain.Servicies
{
    public static class MccCategoryMapping
    {
        public static readonly Dictionary<string, TransactionCategory> MccCategories = new()
        {
            { "5411", TransactionCategory.FOOD },
            { "5412", TransactionCategory.FOOD },
            { "5811", TransactionCategory.MEAL },
            { "5812", TransactionCategory.MEAL }
        };
    }
}
