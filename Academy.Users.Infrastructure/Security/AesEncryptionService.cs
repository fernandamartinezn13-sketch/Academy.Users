using System.Security.Cryptography;
using System.Text;
using Academy.Users.Domain.Services;
using Academy.Users.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Academy.Users.Infrastructure.Security;

public sealed class AesEncryptionService : IEncryptionService
{
    private readonly byte[] _key;

    public AesEncryptionService(IOptions<EncryptionSettings> options)
    {
        if (options.Value is null || string.IsNullOrWhiteSpace(options.Value.Key))
        {
            throw new InvalidOperationException("Encryption key configuration is missing.");
        }

        _key = Convert.FromBase64String(options.Value.Key);
    }

    public string Decrypt(string encryptedContent)
    {
        var cipherBytes = Convert.FromBase64String(encryptedContent);

        using var aes = Aes.Create();
        aes.Key = _key;

        var ivLength = aes.BlockSize / 8;
        var iv = new byte[ivLength];
        var cipher = new byte[cipherBytes.Length - ivLength];

        Buffer.BlockCopy(cipherBytes, 0, iv, 0, ivLength);
        Buffer.BlockCopy(cipherBytes, ivLength, cipher, 0, cipher.Length);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipher);
        using var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cryptoStream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    public string Encrypt(string plaintext)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8))
        {
            writer.Write(plaintext);
        }

        var cipher = ms.ToArray();
        var result = new byte[aes.IV.Length + cipher.Length];
        Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
        Buffer.BlockCopy(cipher, 0, result, aes.IV.Length, cipher.Length);
        return Convert.ToBase64String(result);
    }
}
