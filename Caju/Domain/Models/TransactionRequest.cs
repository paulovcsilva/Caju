namespace Caju.Domain.Models
{
    public class TransactionRequest
    {
        public required string Id { get; set; }
        public required string AccountId { get; set; }
        public decimal Amount { get; set; }
        public required string Mcc { get; set; }
        public required string Merchant { get; set; }
    }
}
