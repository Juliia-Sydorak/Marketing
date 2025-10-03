using MarketingDAL.Entities;
using System.Collections.Generic;

namespace MarketingDAL.Interfaces
{
    public interface IUserDal
    {
        User Create(User user);
        List<User> GetAll();
        User GetById(int userId);
        User Update(User user);
        bool Delete(int userId);
    }
}
