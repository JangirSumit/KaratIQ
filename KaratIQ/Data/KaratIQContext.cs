using Microsoft.EntityFrameworkCore;
using KaratIQ.Models;

namespace KaratIQ.Data;

public class KaratIQContext : DbContext
{
    public DbSet<JewelleryItem> JewelleryItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MetalRate> MetalRates { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<CustomerTransaction> CustomerTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "karatiq.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<CustomerTransaction>()
            .HasOne(ct => ct.Customer)
            .WithMany(c => c.Transactions)
            .HasForeignKey(ct => ct.CustomerId);

        modelBuilder.Entity<InvoiceItem>()
            .HasOne<Invoice>()
            .WithMany(i => i.Items)
            .HasForeignKey(ii => ii.InvoiceId);

        // Configure decimal precision
        modelBuilder.Entity<JewelleryItem>()
            .Property(j => j.GrossWeight)
            .HasPrecision(10, 3);

        modelBuilder.Entity<JewelleryItem>()
            .Property(j => j.NetWeight)
            .HasPrecision(10, 3);

        modelBuilder.Entity<MetalRate>()
            .Property(m => m.RatePerGram)
            .HasPrecision(10, 2);

        // Seed initial data
        modelBuilder.Entity<MetalRate>().HasData(
            new MetalRate { Id = 1, MetalType = MetalType.Gold, Purity = "22K", RatePerGram = 6500, EffectiveDate = DateTime.Today },
            new MetalRate { Id = 2, MetalType = MetalType.Gold, Purity = "24K", RatePerGram = 7000, EffectiveDate = DateTime.Today },
            new MetalRate { Id = 3, MetalType = MetalType.Silver, Purity = "925", RatePerGram = 85, EffectiveDate = DateTime.Today }
        );
    }
}