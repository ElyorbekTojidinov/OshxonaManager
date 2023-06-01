namespace Aplication.DTOs.Order
{
    public class OrderBaseDTO 
    {
        public int OrderTable { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsActive { get; set; }
    }
}
