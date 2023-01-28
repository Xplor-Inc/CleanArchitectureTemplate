namespace CleanArchitectureTemplate.Core.Interfaces.Utility.Security;
public interface IEncryption
{
    string GenerateSalt();
    string GenerateHash(string value, string salt);
}