using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Data;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Repositories;

namespace TechStore.DAL.Repositories;

public class CartItemRepository(TechStoreDbContext context) : BaseRepository<CartItem>(context), ICartItemRepository
{
    public async Task<List<CartItem?>> GetItemsByCartIdAsync(int id)
    {
        return context.Set<CartItem?>()
            .Where(x => x.CartId == id).ToList();
    }
}
