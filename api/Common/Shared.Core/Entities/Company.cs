using Dawn;
using Shared.Core.Domain;
using System.Globalization;
using System.Text;

namespace Shared.Core.Entities;

public class Company : AggregateRoot<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Slogan { get; protected set; } = string.Empty;
    public string Slug { get; protected set; } = string.Empty;
    public virtual List<Premise> Premises { get; protected set; } = [];
    public virtual List<Category> Categories { get; protected set; } = [];

    // Configurable company-level settings
    public bool ReactiveStock { get; protected set; } = true;
    public bool PublicPrices { get; protected set; } = true;
    public bool PublicOrders { get; protected set; } = true;

    protected Company() { }
    public Company(string name, string slogan, string? slug = null)
    {
        Name = ValidateName(name);
        Slogan = Guard.Argument(() => slogan).NotWhiteSpace().DoesNotContain("  ");
        Slug = slug ?? GenerateSlug(name);
        Enabled = true;
    }
    public void Update(
        string? name = null,
        string? slogan = null,
        string? slug = null,
        bool? reactiveStock = null,
        bool? publicPrices = null,
        bool? publicOrders = null)
    {
        if (name is not null && name != Name)
        {
            Name = ValidateName(name);
            Slug = GenerateSlug(name);
        }
        if (slogan is not null && slogan != Slogan)
            Slogan = ValidateName(slogan);
        if (slug is not null && slug != Slug)
            Slug = Guard.Argument(() => slug).NotNull().NotEmpty().MaxLength(50);
        if (reactiveStock is not null && reactiveStock != ReactiveStock) ReactiveStock = reactiveStock.Value;
        if (publicPrices is not null && publicPrices != PublicPrices) PublicPrices = publicPrices.Value;
        if (publicOrders is not null && publicOrders != PublicOrders) PublicOrders = publicOrders.Value;
    }

    private static string GenerateSlug(string name)
    {
        var normalized = name.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
            if (char.IsLetterOrDigit(c)) sb.Append(char.ToLower(c));
            else if (c == ' ' || c == '-') sb.Append('-');
        }
        return sb.ToString().Trim('-');
    }

    private string ValidateName(string name) => Guard.Argument(() => name).NotNull().NotEmpty().NotWhiteSpace().DoesNotContain("  ");
}