using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Enums;
using TechStore.Domain.Exceptions;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Interfaces.Services;

namespace TechStore.Domain.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Order> CreateAsync(Order order)
    {
        var addedOrder = await unitOfWork.OrderRepository.CreateAsync(order);
        return await unitOfWork.SaveAsync() > 0 ? addedOrder : throw new DomainException("Order has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await unitOfWork.OrderRepository.GetAsync(id) ?? throw new DomainException("Order not found");
        var deleted = await unitOfWork.OrderRepository.DeleteAsync(id);
        await unitOfWork.SaveAsync();
        return deleted;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await unitOfWork.OrderRepository.GetAllAsync();
    }

    public async Task<Order?> GetAsync(int id)
    {
        return await unitOfWork.OrderRepository.GetAsync(id);
    }

    public async Task<Order> SetDeliveryStatus(Order order, DeliveryStatus deliveryStatus)
    {
        order.DeliveryStatus = deliveryStatus;
        return await unitOfWork.SaveAsync() > 0 ? order
     : throw new DomainException("Delivery status has not been set");
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var updatedOrder = unitOfWork.OrderRepository.Update(order);
        return await unitOfWork.SaveAsync() > 0 ? order
            : throw new DomainException("Order has not been updated");
    }
}
