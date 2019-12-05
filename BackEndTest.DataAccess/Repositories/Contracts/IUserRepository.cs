using BackEndTest.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.DataAccess.Repositories.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User user);
        User Update(User user);
        int RemoveById(int id);
    }
}
