using Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Waiter
{
    public class WaiterUpdateDTO : WaiterBaseDTO
    {
        public int waiterId { get; set; }
        public List<int> Orders { get; set; }
    }
}
