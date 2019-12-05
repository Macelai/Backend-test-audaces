using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BackEndTest.Database.Models;

namespace BackEndTest.Database
{
    public class BackEndTestContext : DbContext
    {
        public BackEndTestContext(DbContextOptions<BackEndTestContext> options) 
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
