using Dawn;

namespace Shared.Core.Extensions;
public static class DateTimeGuardExtensions
{
    public static ref readonly Guard.ArgumentInfo<DateTime> ValidSqlDate(in this Guard.ArgumentInfo<DateTime> argument)
    {
        if (argument.HasValue() && argument.Value.Year < 1753)
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} is not a valid SQL date. " +
                "The minimum valid date is 1753/01/01.",
                argument.Name));
        }
        return ref argument;
    }
    public static ref readonly Guard.ArgumentInfo<DateTime?> ValidSqlDate(in this Guard.ArgumentInfo<DateTime?> argument)
    {
        if (argument.HasValue() && argument.Value?.Year < 1753)
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} is not a valid SQL date. " +
                "The minimum valid date is 1753/01/01.",
                argument.Name));
        }
        return ref argument;
    }

    public static ref readonly Guard.ArgumentInfo<DateTime> Future(in this Guard.ArgumentInfo<DateTime> argument)
    {
        if (argument.HasValue() && argument.Value <= DateTime.UtcNow)
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} cannot be a past date.",
                argument.Name));
        }
        return ref argument;
    }
    public static ref readonly Guard.ArgumentInfo<DateTime?> Future(in this Guard.ArgumentInfo<DateTime?> argument)
    {
        if (argument.HasValue() && argument.Value <= DateTime.UtcNow)
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} cannot be a past date.",
                argument.Name));
        }
        return ref argument;
    }

    public static ref readonly Guard.ArgumentInfo<DateTime> NotFuture(in this Guard.ArgumentInfo<DateTime> argument)
    {
        if (argument.HasValue() && argument.Value > DateTime.UtcNow)
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} cannot be a future date.",
                argument.Name));
        }
        return ref argument;
    }
    public static ref readonly Guard.ArgumentInfo<DateTime?> NotFuture(in this Guard.ArgumentInfo<DateTime?> argument)
    {
        if (argument.HasValue() && argument.Value > DateTime.UtcNow)
        {
            throw Guard.Fail(new ArgumentException(
                $"{argument.Name} cannot be a future date.",
                argument.Name));
        }
        return ref argument;
    }
}
