using Caju.Domain.Enums;

namespace Caju.Domain.Models
{
    public class Balance
    {
        public required string UserId { get; set; }
        public required TransactionCategory Category { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
