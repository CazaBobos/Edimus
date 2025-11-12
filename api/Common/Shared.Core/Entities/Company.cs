using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Company : AggregateRoot<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Slogan { get; protected set; } = string.Empty;
    public string Acronym { get; protected set; } = string.Empty;
    public virtual List<Establishment>? Establishments { get; protected set; }
    public virtual List<Category>? Categories { get; protected set; }

    protected Company() { }
    public Company(string name, string slogan, string? acronmym = null)
    {
        Name = ValidateName(name);
        Slogan = Guard.Argument(() => slogan).NotWhiteSpace().DoesNotContain("  ");
        Acronym = acronmym ?? GenerateAcronym(name);
        Enabled = true;
    }
    public void Update(
        string? name = null,
        string? slogan = null,
        string? acronym = null)
    {
        var affectedMembers = new List<string>();

        if (name is not null && name != Name)
        {
            Name = ValidateName(name);
            Acronym = GenerateAcronym(name);
            affectedMembers.Add(nameof(Name));
        }
        if (slogan is not null && slogan != Slogan)
        {
            Slogan = ValidateName(slogan);
            affectedMembers.Add(nameof(Slogan));
        }
        if (acronym is not null && acronym != Acronym)
        {
            Acronym = Guard.Argument(() => acronym).NotNull().NotEmpty().MaxLength(8);
            affectedMembers.Add(nameof(Acronym));
        }
        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }

    private string GenerateAcronym(string name) => name.Replace(' ', '-').ToLower().Substring(0, 7);
    private string ValidateName(string name) => Guard.Argument(() => name).NotNull().NotEmpty().NotWhiteSpace().DoesNotContain("  ");
}