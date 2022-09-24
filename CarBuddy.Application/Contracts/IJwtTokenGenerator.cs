using CarBuddy.Domain.Models;

namespace CarBuddy.Application.Contracts
{
    public interface IJwtTokenGenerator
    {
        string Generate(Entity user);
    }
}
