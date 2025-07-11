﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Enums;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);
    Task<Order?> GetAsync(int id);
    Task<PaginatedList<Order>> GetAllAsync(int pageIndex, int pageSize);
    Task<Order> SetDeliveryStatus(Order order, DeliveryStatus deliveryStatus);
}
