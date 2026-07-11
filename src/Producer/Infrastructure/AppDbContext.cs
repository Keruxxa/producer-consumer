using Microsoft.EntityFrameworkCore;
using Producer.Models;

namespace Producer.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }
}
