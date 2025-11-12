using Dawn;
using System.Text.RegularExpressions;

namespace Shared.Core.Extensions;
public static class PasswordGuardExtensions
{
    public static ref readonly Guard.ArgumentInfo<string> ValidPasswordFormat(in this Guard.ArgumentInfo<string> argument)
    {
        /*Pattern explanation:
            ^(?=.*[a-z]{x,})(?=.*[A-Z]{y,})(?=.*[0-9]{z,})(?=.*[@#$~%&=_!¡*^]{w,}).{n,}$

            Allowed passwords must contain:
            - At least x small letters
            - At least y capital letters
            - At least z numbers
            - At least w special characters (@ # $ ~ % & = _ ! ¡ * ^)
            - A minimum lenght of n
        */

        var regex = new Regex(@"^(?=.*[a-z]{1,})(?=.*[A-Z]{1,})(?=.*[0-9]{1,})(?=.*[@#$~%&=_!¡*^]{1,}).{0,}$");

        if (argument.HasValue() && !regex.IsMatch(argument))
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} is not a valid password format.",
                argument.Name));
        }
        return ref argument;
    }
}
