using Caju.Domain.Enums;

namespace Caju.Domain.Ports
{
    public interface ICategoryPorts
    {
        TransactionCategory? GetCategory(string mcc, string merchantName);
    }
}
