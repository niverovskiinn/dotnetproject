using Microsoft.EntityFrameworkCore;
using Models;

namespace Messenger.DataAccess
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialogue> Dialogues { get; set; }
        public DbSet<User> Users { get; set; }
    }
}