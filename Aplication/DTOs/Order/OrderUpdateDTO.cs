using Domain.Models.Models;

namespace Aplication.DTOs.Order
{
    public class OrderUpdateDTO : OrderBaseDTO
    {
        public int OrderId { get; set; }
        public List<int> Products { get; set; }
    }
}
