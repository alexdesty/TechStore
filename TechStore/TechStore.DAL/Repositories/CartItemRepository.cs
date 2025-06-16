using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DAL.Data;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Repositories;

namespace TechStore.DAL.Repositories;

public class CartItemRepository(TechStoreDbContext context) : BaseRepository<CartItem>(context), ICartItemRepository
{
}
