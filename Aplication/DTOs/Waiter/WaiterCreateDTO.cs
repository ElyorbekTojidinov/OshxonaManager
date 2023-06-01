using Domain.Models.Models;

namespace Aplication.DTOs.Waiter
{
    public class WaiterCreateDTO :WaiterBaseDTO
    {
        public List<int> Orders { get; set; }
    }
}
