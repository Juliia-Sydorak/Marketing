using MarketingDAL.Entities;
using System.Collections.Generic;

namespace MarketingDAL.Interfaces
{
    public interface IDiscountDal
    {
        Discount Create(Discount discount);
        List<Discount> GetAll();
        Discount GetById(int discountId);
        Discount Update(Discount discount);
        bool Delete(int discountId);
    }
}
