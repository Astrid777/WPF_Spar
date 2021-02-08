using System.Data.Entity;

namespace WpfApp20.Models
{
    class forecastContext : DbContext
    {
        public forecastContext() : base("DefaultConnection")
        {

        }
        //бд
        public DbSet<forecast> forecasts { get; set; }
    }
}
