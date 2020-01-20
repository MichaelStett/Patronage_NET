using Microsoft.AspNetCore.Identity;
using Patronage_NET.Application.Common.Models;
using System.Linq;

namespace Patronage_NET.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
