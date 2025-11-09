using Dawn;
using System.Text.RegularExpressions;

namespace Shared.Core.Extensions;
public static class LegalIdGuardExtensions
{
    public static ref readonly Guard.ArgumentInfo<string> ValidLegalIdFormat(in this Guard.ArgumentInfo<string> argument)
    {
        /* Allowed LegalIds must be:
            - 00.000.000
            - 0.000.000
            - 00000000
            - 0000000
            - 00-00000000-0
        */
        string pattern = @"^\d{2}-\d{1,2}\.?\d{3}\.?\d{3}-\d{1}$|^\d{1,2}\.?\d{3}\.?\d{3}$";

        var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        if (argument.HasValue() && !regex.IsMatch(argument))
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} is not a valid legal ID format.",
                argument.Name));
        }
        return ref argument;
    }
}
