using Microsoft.EntityFrameworkCore;
using LoginandRegistration.Models;

namespace LoginandRegistration.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}