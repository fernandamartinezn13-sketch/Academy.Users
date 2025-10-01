namespace Academy.Users.Domain.Abstractions;
using Academy.Users.Domain.Users.Entities;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}