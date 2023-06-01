using Domain.Models.Models;

namespace Aplication.DTOs.Order
{
    public class OrderCreateDTO : OrderBaseDTO
    {
        public List<int> Products { get; set; }

    }
}
