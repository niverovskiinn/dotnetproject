using Microsoft.EntityFrameworkCore;
using Models;

namespace Messenger.DataAccess
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseMySql("server=localhost;UserId=root;Password=password;database=messengerdb;");
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Dialogue> Dialogues { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Add Entities to DB(OLD)


//             List<User> users = new List<User>            {
//                new User
//                {
//                    Passport = "AA123456",
//                    Accounts = accounts
//
//                },
//                new User
//                {
//                    Passport = "AA654321",
//                    Accounts = accounts
//                }
//            };
//            
//            modelBuilder.Entity<User>().HasData(users);

            #endregion
            
            base.OnModelCreating(modelBuilder);
        }
    }
}