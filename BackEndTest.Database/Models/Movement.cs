using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Database.Models
{
    public class Movement
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int User {get; set; }
        //public User User { get; set; }
    }
}
