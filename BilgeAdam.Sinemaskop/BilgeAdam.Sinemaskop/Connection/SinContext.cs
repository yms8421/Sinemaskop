using BilgeAdam.Sinemaskop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BilgeAdam.Sinemaskop.Connection
{
    public class SinContext : DbContext
    {
        private readonly AppSettings options;

        public SinContext(IOptions<AppSettings> options)
        {
            this.options = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Yöntem 2 : Cool yöntem
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(options.ConnectionStrings.SinContext);
            }
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
