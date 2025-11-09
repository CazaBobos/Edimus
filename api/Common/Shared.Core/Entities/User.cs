using Dawn;
using Shared.Core.Abstractions;
using Shared.Core.Domain;
using Shared.Core.Extensions;
using Shared.Core.Services;
namespace Shared.Core.Entities;

public class User : Entity<int>, IUserRecord
{
    public override int Id { get; protected set; }
    public string Username { get; protected set; } = string.Empty;
    public string Email { get; protected set; } = string.Empty;
    public string Password { get; protected set; } = string.Empty;
    public UserRole Role { get; protected set; }
    public List<int> CompanyIds { get; protected set; } = new();

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
        UserRole ? role = null,
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
            var hashedCurrentPassword = HashService.CreateHash(currentPassword!);
            var hashedNewPassword = HashService.CreateHash(newPassword!);

            Guard.Operation(hashedCurrentPassword == Password, "User password is incorrect.");
            Guard.Operation(hashedCurrentPassword != hashedNewPassword, "The new password must be different than the previous one.");

            Guard.Argument(() => newPassword)
                .MinLength(8)
                .MaxLength(64)
                .ValidPasswordFormat();
            Password = hashedNewPassword;

            affectedMembers.Add(nameof(Password));
        }
        
        if (companyIds is not null && !companyIds.OrderBy(x => x).SequenceEqual(CompanyIds.OrderBy(x => x)))
        {
            CompanyIds = ValidateCompanies(companyIds);
        }

        if(role is not null && role != Role)
        {
            Guard.Operation(Role != UserRole .Root, "A user with root role cannot be changed.");

            Role = (UserRole )role;
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

    public void SetRandomPassword()
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

        Password = new string(passwordChars);
    }

}
