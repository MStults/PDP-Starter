using Microsoft.EntityFrameworkCore;
using PDP.Web.API.Models;

namespace PDP.Web.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
    }
}