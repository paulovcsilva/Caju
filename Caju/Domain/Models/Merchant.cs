namespace Caju.Domain.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Mcc { get; set; }
    }
}
