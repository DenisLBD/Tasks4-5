using Microsoft.EntityFrameworkCore;

namespace Users.ViewModels
{
    public class MessageContext : DbContext
    {
        public DbSet<MessageViewModel> Messages { get; set; }
        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
