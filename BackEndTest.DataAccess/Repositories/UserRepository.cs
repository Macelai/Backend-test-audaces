using System.Collections.Generic;
using System.Linq;
using BackEndTest.DataAccess.Repositories.Contracts;
using BackEndTest.Database;
using BackEndTest.Database.Models;

namespace BackEndTest.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BackEndTestContext _db;

        public  UserRepository(BackEndTestContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User GetById(int id)
        {
            return _db.Users.SingleOrDefault(x => x.Id == id);
        }

        public User Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            User userToUpdate = _db.Users.Find(user.Id);
            userToUpdate.Name = user.Name;
            userToUpdate.Salary = user.Salary;
            _db.SaveChanges();
            return userToUpdate;
        }

        public int RemoveById(int id)
        {
            int minId = _db.Users.Min(x => x.Id);
            if(minId == id) {
                return 0;
            }
            var movementsToUpdate = _db.Movements.Where(x => x.User == id);

            foreach(var movement in movementsToUpdate.ToList())
            {
                movement.User = minId;
            }

            User userToRemove = _db.Users.Find(id);
            _db.Users.Attach(userToRemove);
            _db.Users.Remove(userToRemove);

            _db.SaveChanges();
            return id;
        }
    }
}
