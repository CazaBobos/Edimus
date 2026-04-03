using Dawn;
using Shared.Core.Abstractions;
using Shared.Core.Domain;
using Shared.Core.Extensions;
using Shared.Core.Services;
using System.Security.Cryptography;
namespace Shared.Core.Entities;

public class User : Entity<int>, IUserRecord
{
    public override int Id { get; protected set; }
    public string Username { get; protected set; } = string.Empty;
    public string Email { get; protected set; } = string.Empty;
    public string Password { get; protected set; } = string.Empty;
    public UserRole Role { get; protected set; }
    private List<int>? _companyIds;
    public List<int> CompanyIds
    {
        get => _companyIds ?? [];
        protected set => _companyIds = value;
    }
    public string? PasswordResetToken { get; protected set; }
    public DateTime? PasswordResetExpiresAt { get; protected set; }

    protected User() { }

    public User(string username, string email, string password, UserRole role)
    {
        Email = Guard.Argument(() => email)
               .NotNull()
               .MinLength(8)
               .MaxLength(64)
               .ValidEmailFormat();
        Username = Guard.Argument(() => username)
            .NotNull()
            .MinLength(4)
            .MaxLength(32)
            .ValidUsernameFormat();
        Guard.Argument(() => password)
            .NotNull()
            .MinLength(8)
            .MaxLength(64)
            .ValidPasswordFormat();
        Password = HashService.CreateHash(password);
        Role = role;
        Enabled = true;
    }

    public void Update(
        string? username = null,
        string? email = null,
        string? currentPassword = null,
        string? newPassword = null,
        UserRole? role = null,
        List<int>? companyIds = null)
    {
        Guard.Operation(Enabled == true, "A user cannot be modified when it's not active. Restore it and try again.");

        var affectedMembers = new List<string>();

        if (username is not null && username != Username)
        {
            Username = Guard.Argument(() => username)
                .NotNull()
                .MinLength(4)
                .MaxLength(32)
                .ValidUsernameFormat();
            affectedMembers.Add(nameof(Username));
        }
        if (email is not null && email != Email)
        {
            Email = Guard.Argument(() => email)
                .NotNull()
                .MaxLength(64)
                .ValidEmailFormat();
            affectedMembers.Add(nameof(Email));
        }
        if (currentPassword is not null || newPassword is not null)
        {
            Guard.Operation(HashService.Verify(currentPassword!, Password), "User password is incorrect.");
            Guard.Operation(currentPassword != newPassword, "The new password must be different than the previous one.");

            Guard.Argument(() => newPassword)
                .MinLength(8)
                .MaxLength(64)
                .ValidPasswordFormat();
            Password = HashService.CreateHash(newPassword!);

            affectedMembers.Add(nameof(Password));
        }

        if (companyIds is not null && !companyIds.OrderBy(x => x).SequenceEqual(CompanyIds.OrderBy(x => x)))
        {
            CompanyIds = ValidateCompanies(companyIds);
        }

        if (role is not null && role != Role)
        {
            Guard.Operation(Role != UserRole.Root, "A user with root role cannot be changed.");

            Role = (UserRole)role;
        }

        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }

    private List<int> ValidateCompanies(List<int> companies) =>
        Guard.Argument(() => companies)
            .DoesNotContainDuplicate()
            .DoesNotContainNull()
            .Require(companyIds => companies.All(id => id > 0));

    public static IUserRecord MakeRecord(int id, string username, UserRole role, List<int> companies)
    {
        return new User
        {
            Id = id,
            Username = username,
            Role = role,
            CompanyIds = companies,
        };
    }

    public string GenerateResetToken()
    {
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        PasswordResetToken = HashService.CreateDeterministicHash(token);
        PasswordResetExpiresAt = DateTime.UtcNow.AddMinutes(15);
        return token;
    }

    public void ResetPassword(string token, string newPassword)
    {
        Guard.Operation(PasswordResetToken is not null, "No reset token has been requested.");
        Guard.Operation(PasswordResetExpiresAt > DateTime.UtcNow, "Reset link has expired.");
        Guard.Operation(HashService.CreateDeterministicHash(token) == PasswordResetToken, "Invalid reset token.");

        Guard.Argument(() => newPassword)
            .NotNull()
            .MinLength(8)
            .MaxLength(64)
            .ValidPasswordFormat();
        Password = HashService.CreateHash(newPassword);
        PasswordResetToken = null;
        PasswordResetExpiresAt = null;
    }

    public string SetRandomPassword()
    {
        string allowedSymbols = "@#$~%&=_!¡*^";
        string allowedNumbers = "0123456789";
        string allowedUppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string allowedLowercase = "abcdefghijklmnopqrstuvwxyz";
        var random = new Random();

        char[] passwordChars = new char[8];
        passwordChars[0] = allowedUppercase[random.Next(allowedUppercase.Length)];
        passwordChars[1] = allowedLowercase[random.Next(allowedLowercase.Length)];
        passwordChars[2] = allowedNumbers[random.Next(allowedNumbers.Length)];
        passwordChars[3] = allowedSymbols[random.Next(allowedSymbols.Length)];

        for (int i = 4; i < 8; i++)
        {
            string allCharacters = allowedUppercase + allowedLowercase + allowedNumbers + allowedSymbols;
            passwordChars[i] = allCharacters[random.Next(allCharacters.Length)];
        }

        for (int i = passwordChars.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            char temp = passwordChars[i];
            passwordChars[i] = passwordChars[j];
            passwordChars[j] = temp;
        }

        var plainPassword = new string(passwordChars);
        Password = HashService.CreateHash(plainPassword);
        return plainPassword;
    }

}
