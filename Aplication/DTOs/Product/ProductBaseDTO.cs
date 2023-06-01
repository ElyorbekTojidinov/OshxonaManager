namespace Aplication.DTOs.Product
{
    public class ProductBaseDTO
    {
        public required string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string? ProductImg { get; set; }
    }
}
