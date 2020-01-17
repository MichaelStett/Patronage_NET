using Microsoft.EntityFrameworkCore;
using Patronage_NET.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Patronage_NET.Application.Common.Interfaces
{
    public interface IPatronageDbContext
    {
        DbSet<Myfile> Files { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
