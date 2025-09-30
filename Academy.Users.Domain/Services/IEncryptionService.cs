namespace Academy.Users.Domain.Services;

public interface IEncryptionService
{
    string Decrypt(string encryptedContent);
    string Encrypt(string plaintext);
}
