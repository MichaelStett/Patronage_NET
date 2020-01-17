namespace Patronage_NET.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        bool IsAuthenticated { get; }
    }
}
