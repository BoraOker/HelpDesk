using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Models
{
    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<FAQ> FAQs => Set<FAQ>();
    }
    
}