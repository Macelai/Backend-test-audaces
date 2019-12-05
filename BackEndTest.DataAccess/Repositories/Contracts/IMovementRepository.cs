using System;
using System.Collections.Generic;
using System.Text;
using BackEndTest.Database.Models;

namespace BackEndTest.DataAccess.Repositories.Contracts
{
    public interface IMovementRepository
    {
        IEnumerable<Movement> GetAll();
        IEnumerable<Movement> GetAllMovementForUserId(int UserId);
        int balanceBetweenDates(string start, string end);
        int balanceBetweenDatesByUser(string start, string end, int userId);
        Movement Add(Movement movement);
        Movement Update(Movement movement);
        int RemoveById(int movementId);
    }
}
