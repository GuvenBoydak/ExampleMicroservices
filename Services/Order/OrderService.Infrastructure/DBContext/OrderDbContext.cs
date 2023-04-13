using Microsoft.EntityFrameworkCore;
using OrderService.Domain.OrderAggregate;

namespace OrderService.Infrastructure.DBContext;

public class OrderDbContext : DbContext
{
    public const string DefaultSchema = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("Orders", DefaultSchema);
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems", DefaultSchema);

        modelBuilder.Entity<OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>().OwnsOne(o => o.Address).WithOwner();
        base.OnModelCreating(modelBuilder);
    }
}