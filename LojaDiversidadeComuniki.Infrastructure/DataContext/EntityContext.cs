using LojaDiversidadeComuniki.Domain.Model.Order;
using LojaDiversidadeComuniki.Domain.Model.Products;
using LojaDiversidadeComuniki.Domain.Model.ShoppingCart;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LojaDiversidadeComuniki.Infrastructure.DataContext
{
    public class EntityContext : IdentityDbContext<IdentityUser>
    {
        public EntityContext(DbContextOptions<EntityContext> option) : base(option)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}
