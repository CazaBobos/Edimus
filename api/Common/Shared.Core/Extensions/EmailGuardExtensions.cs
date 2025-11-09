using Dawn;
using System.Text.RegularExpressions;

namespace Shared.Core.Extensions;
public static class EmailGuardExtensions
{
    public static ref readonly Guard.ArgumentInfo<string> ValidEmailFormat(in this Guard.ArgumentInfo<string> argument)
    {
        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        if (argument.HasValue() && !regex.IsMatch(argument))
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} is not a valid e-mail format.",
                argument.Name));
        }
        return ref argument;
    }
}
