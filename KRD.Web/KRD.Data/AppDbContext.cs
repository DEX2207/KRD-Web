using KRD.Data.Enum;
using KRD.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KRD.Data;

public class AppDbContext:DbContext
{
    public DbSet<Car> CarsDb { get; set; }
    public DbSet<Client> ClientsDb { get; set; }
    public DbSet<Contact> ContactsDb { get; set; }
    public DbSet<Option> OptionsDb { get; set; }
    public DbSet<Orders> OrdersDb { get; set; }
    public DbSet<OrderStatus> OrderStatusDb { get; set; }
    public DbSet<CarOption> CarOptionsDb { get; set; }
    
    public IConfiguration Configuration { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CarOption>()
                    .HasKey(co => new { co.CarId, co.OptionId });
        
                modelBuilder.Entity<CarOption>()
                    .HasOne(co => co.Car)
                    .WithMany(c => c.CarOptions)
                    .HasForeignKey(co => co.CarId);
        
                modelBuilder.Entity<CarOption>()
                    .HasOne(co => co.Option)
                    .WithMany(o => o.CarOptions)
                    .HasForeignKey(co => co.OptionId);
        
                modelBuilder.Entity<Orders>()
                    .HasOne<Car>()
                    .WithMany()
                    .HasForeignKey(o => o.CarId);
        
                modelBuilder.Entity<Orders>()
                    .HasOne<Contact>()
                    .WithMany()
                    .HasForeignKey(o => o.ContactId);
        
                modelBuilder.Entity<Orders>()
                    .HasOne<OrderStatus>()
                    .WithMany()
                    .HasForeignKey(o => o.OrderStatusId);
        
                modelBuilder.Entity<Client>()
                    .HasOne<Orders>()
                    .WithMany()
                    .HasForeignKey(c => c.OrdersId);
        
                modelBuilder.Entity<Client>()
                    .HasOne<Contact>()
                    .WithMany()
                    .HasForeignKey(c => c.ContactId);
        
                modelBuilder.Entity<Client>()
                    .HasOne<Car>()
                    .WithMany()
                    .HasForeignKey(c => c.CarId);
        
                modelBuilder.HasPostgresEnum<Color>("color");
                modelBuilder.HasPostgresEnum<OptionType>("option_type");
                modelBuilder.HasPostgresEnum<Status>("status");
                
                modelBuilder.Entity<OrderStatus>()
                    .Property(o => o.Status)
                    .HasColumnType("status");
                modelBuilder.Entity<Option>()
                    .Property(o => o.OptionType)
                    .HasColumnType("option_type");
                modelBuilder.Entity<Car>()
                    .Property(c=>c.Color)
                    .HasColumnType("color");
    }
}