using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SignIn> SignIns { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
    }
}
