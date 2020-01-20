using Patronage_NET.Application.Common.Models;
using System.Threading.Tasks;

namespace Patronage_NET.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
