using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiUsers.Domain.Models;

namespace WebApiUsers.EFCore.DataContext
{
    public class WebApiContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WebApiContext(DbContextOptions<WebApiContext> options, IConfiguration configuration) :
            base(options)
        {
            _configuration = configuration;
        }

       
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var result = base.SaveChanges();


            return result;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStrings = _configuration != null ? _configuration["ConnectionStrings:DbConection"] : "none";
            optionsBuilder.UseSqlServer(connectionStrings);
        }
    }
}
