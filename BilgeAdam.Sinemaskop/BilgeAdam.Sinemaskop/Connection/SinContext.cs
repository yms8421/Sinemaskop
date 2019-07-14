using BilgeAdam.Sinemaskop.Models;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdam.Sinemaskop.Connection
{
    public class SinContext : DbContext
    {
        public SinContext(DbContextOptions<SinContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
