﻿using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesProduct;
using Domain.Models.Models;
using Infrastructure.Services;

namespace Aplication.Services.ServiceProduct;

public class OrderRepository : Repository<Orders>, IOrdersRepository
{
    
    public OrderRepository(IAplicationDbContext context) : base(context)
    {
        
    }

   
}
