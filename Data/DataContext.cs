using GatewayMusalaTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GatewayMusalaTest.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<GateWay> Gateway {get; set;}
        public DbSet<Peripheral> Peripheral {get; set;}

    }
}