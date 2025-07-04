﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;

namespace TechStore.Domain.Interfaces.Repositories;

public interface ICartItemRepository:IBaseRepository<CartItem>
{
    Task<List<CartItem>> GetItemsByCartIdAsync(int id);
    Task<bool> DeleteCartItemsByCartId(int id);
}
