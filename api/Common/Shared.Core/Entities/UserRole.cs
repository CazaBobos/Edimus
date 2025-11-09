namespace Shared.Core.Entities;

public enum UserRole
{
    None,
    Waiter,
    Manager,
    Admin,
    Root,
}
//public class UserRole : ValueObject
//{
//    public static UserRole None => new UserRole(0);
//    public static UserRole Waiter => new UserRole(1);
//    public static UserRole Manager => new UserRole(2);
//    public static UserRole Admin => new UserRole(3);
//    public static UserRole Root => new UserRole(4);
//
//    private int Value { get; }
//
//    public UserRole(int value)
//    {
//       Value = value;
//    }
//
//    protected override IEnumerable<object> GetEqualityComponents()
//    {
//        yield return Value;
//    }
//
//    public static bool operator >(UserRole a, UserRole b) => a.Value > b.Value;
//    public static bool operator <(UserRole a, UserRole b) => a.Value < b.Value;
//    public static bool operator >=(UserRole a, UserRole b) => a.Value >= b.Value;
//    public static bool operator <=(UserRole a, UserRole b) => a.Value <= b.Value;
//}
