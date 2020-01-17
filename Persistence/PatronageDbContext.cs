using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Patronage_NET.Application.Common.Interfaces;
using Patronage_NET.Common;
using Patronage_NET.Domain.Entities;
using Patronage_NET.Domain.Common;

namespace Patronage_NET.Persistence
{
    public class PatronageDbContext : DbContext, IPatronageDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public PatronageDbContext(DbContextOptions<PatronageDbContext> options)
            : base(options)
        {
        }

        public PatronageDbContext(
            DbContextOptions<PatronageDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Myfile> Files { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatronageDbContext).Assembly);
        }
    }
}
