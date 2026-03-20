using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BDMS.Infrastructure.Data
{
    public class BDMSDbContextFactory : IDesignTimeDbContextFactory<BDMSDbContext>
    {
        public BDMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BDMSDbContext>();

            optionsBuilder.UseSqlite("Data Source=bdms.db");

            return new BDMSDbContext(optionsBuilder.Options);
        }
    }
}