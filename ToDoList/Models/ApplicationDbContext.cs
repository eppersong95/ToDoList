using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ToDoList.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=ToDoListDb;Trusted_Connection=True;ConnectRetryCount=0";

        public ApplicationDbContext() 
            : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
