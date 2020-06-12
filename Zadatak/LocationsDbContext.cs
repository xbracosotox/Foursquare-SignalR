using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zadatak.Entity;

namespace Zadatak
{
    public class LocationsDbContext : DbContext
    {
        public LocationsDbContext(DbContextOptions<LocationsDbContext> options) : base(options)
        {

        }

        public virtual DbSet<LocationEntity> Locations { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
