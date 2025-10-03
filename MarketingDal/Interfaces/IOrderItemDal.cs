using MarketingDAL.Entities;
using System.Collections.Generic;

namespace MarketingDAL.Interfaces
{
    public interface IOrderItemDal
    {
        OrderItem Create(OrderItem orderItem);
        List<OrderItem> GetAll();
        OrderItem GetById(int orderItemId);
        OrderItem Update(OrderItem orderItem);
        bool Delete(int orderItemId);
    }
}
