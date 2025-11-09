using Dawn;
using System.Text.RegularExpressions;

namespace Shared.Core.Extensions;
public static class UsernameGuardExtensions
{
    public static ref readonly Guard.ArgumentInfo<string> ValidUsernameFormat(in this Guard.ArgumentInfo<string> argument)
    {
        /* Allowed Usernames must:
            - Only contain alphanumeric characters, underscore and dot.
            - Underscore and dot can't be at the end or start of a username (e.g _username / username_ / .username / username.).
            - Underscore and dot can't be next to each other (e.g user_.name).
            - Underscore or dot can't be used multiple times in a row (e.g user__name / user..name).
        */
        //alternative pattern: ^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$
        string pattern = @"^[a-zA-Z0-9]+([._]?[a-zA-Z0-9]+)*$";

        var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        if (argument.HasValue() && !regex.IsMatch(argument))
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} is not a valid username format.",
                argument.Name));
        }
        return ref argument;
    }
}
