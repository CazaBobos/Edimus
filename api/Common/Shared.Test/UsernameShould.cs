using Shared.Core.Extensions;
using System;

namespace Shared.Test
{
    public class UsernameShould
    {
        [Theory]
        [InlineData(@"UserName")]
        [InlineData(@"USERNAME")]
        [InlineData(@"username")]
        [InlineData(@"123")]
        [InlineData(@"username123")]
        [InlineData(@"user_name")]
        [InlineData(@"user.name")]
        public void ShouldValidate(string username)
        {
            Guard.Argument(() => username).ValidUsernameFormat();
        }

        [Theory]
        [InlineData(@"")]
        [InlineData(@" ")]
        [InlineData(@"user@name")]
        [InlineData(@"user#1name")]
        [InlineData(@"_username")]
        [InlineData(@"username_")]
        [InlineData(@".username")]
        [InlineData(@"username.")]
        [InlineData(@"username ")]
        [InlineData(@"user name")]
        [InlineData(@" username")]
        [InlineData(@" user name")]
        [InlineData(@"user_.name")]
        [InlineData(@"user._name")]
        [InlineData(@"user..name")]
        [InlineData(@"user__name")]
        public void ThrowWhenUsernameIsInvalid(string username)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => username).ValidUsernameFormat()
            );
        }
    }
}