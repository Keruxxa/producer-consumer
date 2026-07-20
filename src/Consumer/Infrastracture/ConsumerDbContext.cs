using MassTransit;
using Microsoft.EntityFrameworkCore;
using Producer.Models;

namespace Consumer.Infrastracture;

public class ConsumerDbContext(DbContextOptions<ConsumerDbContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}
