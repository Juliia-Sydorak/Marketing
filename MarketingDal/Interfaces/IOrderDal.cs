using MarketingDAL.Entities;
using System.Collections.Generic;

namespace MarketingDAL.Interfaces
{
    public interface IOrderDal
    {
        Order Create(Order order);
        List<Order> GetAll();
        Order GetById(int orderId);
        Order Update(Order order);
        bool Delete(int orderId);
    }
}