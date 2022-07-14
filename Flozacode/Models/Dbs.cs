using Flozacode.Extensions.ExpressionExtension;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Flozacode.Models
{
    public class Dbs : DbContext
    {
        public Dbs([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected Dbs()
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                    .Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                if (entity.HasProperty("CreatedDate") &&
                    entity.HasProperty("UpdatedDate") &&
                    entry.State == EntityState.Added)
                {
                    entity.GetType().GetProperty("CreatedDate")?.SetValue(entity, DateTime.UtcNow);
                    entity.GetType().GetProperty("UpdatedDate")?.SetValue(entity, DateTime.UtcNow);
                }

                if (entry.Entity.HasProperty("UpdatedDate") && entry.State == EntityState.Modified)
                {
                    entity.GetType().GetProperty("UpdatedDate")?.SetValue(entity, DateTime.UtcNow);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
