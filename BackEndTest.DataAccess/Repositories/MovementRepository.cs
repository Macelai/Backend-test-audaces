using System.Collections.Generic;
using System.Linq;
using BackEndTest.DataAccess.Repositories.Contracts;
using BackEndTest.Database;
using BackEndTest.Database.Models;
using System;

namespace BackEndTest.DataAccess.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly BackEndTestContext _db;

        public MovementRepository(BackEndTestContext db)
        {
            _db = db;
        }

        public Movement Add(Movement movement)
        {
            _db.Movements.Add(movement);
            _db.SaveChanges();
            return movement;
        }

        public Movement Update(Movement movement)
        {
            Movement movevementToUpdate = _db.Movements.Find(movement.Id);
            movevementToUpdate.Amount = movement.Amount;
            movevementToUpdate.Type = movement.Type;
            movevementToUpdate.Description = movement.Description;
            movevementToUpdate.Date = movement.Date;
            movevementToUpdate.User = movement.User;
            _db.SaveChanges();
            return movevementToUpdate;
        }

        public int RemoveById(int movementId)
        {
            Movement movevementToRemove = _db.Movements.Find(movementId);
            _db.Movements.Attach(movevementToRemove);
            _db.Movements.Remove(movevementToRemove);
            _db.SaveChanges();
            return movementId;        }

        public IEnumerable<Movement> GetAll()
        {
            return _db.Movements;
        }

        public IEnumerable<Movement> GetAllMovementForUserId(int userId)
        {
            return _db.Movements.Where(x => x.User == userId);
        }
        

        private DateTime stringToDate(string stringDate)
        {
            return DateTime.ParseExact(stringDate, "yyyy-MM-dd HH:mm:ss.fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        public int balanceBetweenDates(string start, string end)
        {
            DateTime startDate = stringToDate(start);
            DateTime endDate = stringToDate(end);


            var movements = _db.Movements.Where(x => stringToDate(x.Date) > startDate && stringToDate(x.Date) < endDate);
            int total = 0;
            foreach(var movement in movements.ToList())
            {
                if (movement.Type == "IN")
                {
                    total += movement.Amount;
                }
                if (movement.Type == "OUT")
                {
                    total -= movement.Amount;
                }
            }

            return total;

        }

        public int balanceBetweenDatesByUser(string start, string end, int userId)
        {
            DateTime startDate = stringToDate(start);
            DateTime endDate = stringToDate(end);


            var movements = _db.Movements.Where(x => stringToDate(x.Date) > startDate && stringToDate(x.Date) < endDate && x.User == userId);
            int total = 0;
            foreach(var movement in movements.ToList())
            {
                if (movement.Type == "IN")
                {
                    total += movement.Amount;
                }
                if (movement.Type == "OUT")
                {
                    total -= movement.Amount;
                }
            }

            return total;

        }
    }
}
