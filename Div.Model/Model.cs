using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Div.Model
{
   public class DivxModel : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=divx.db");
        }
    }
    public class User
    {
        public int Id { get; set; }
        public long PK { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Session { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Photo { get; set; }

    }
}
