namespace CarBuddy.Application.Contracts
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string hashedPassword, string password);
    }
}