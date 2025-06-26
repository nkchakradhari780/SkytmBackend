using Microsoft.EntityFrameworkCore;
using SkytmBackend.Models;


namespace SkytmBackend.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        internal void SaveChanger()
        {
            throw new NotImplementedException();
        }
    }
}
