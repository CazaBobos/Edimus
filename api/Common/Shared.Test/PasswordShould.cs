using Shared.Core.Extensions;
using System;

namespace Shared.Test
{
    public class PasswordShould
    {
        [Theory]
        [InlineData(@"#Pass_word1")]
        [InlineData(@"@Password1")]
        [InlineData(@"Pass#word1")]
        [InlineData(@"Password$1")]
        [InlineData(@"Password1~")]
        [InlineData(@"1%Password")]
        [InlineData(@"1Pass&word")]
        [InlineData(@"1Password=")]
        [InlineData(@"_Pass1word")]
        [InlineData(@"Pass!1word")]
        [InlineData(@"Pass1word¡")]
        [InlineData(@"*1Password")]
        [InlineData(@"Password^1")]
        public void ShouldValidate(string password)
        {
            Guard.Argument(() => password).ValidPasswordFormat();
        }

        [Theory]
        [InlineData(@"")]
        [InlineData(@" ")]
        [InlineData(@"password")]
        [InlineData(@"Password")]
        [InlineData(@"PASSWORD")]
        [InlineData(@"password1")]
        [InlineData(@"Password1")]
        [InlineData(@"PASSWORD1")]
        [InlineData(@"#password")]
        [InlineData(@"#Password")]
        [InlineData(@"#PASSWORD")]
        [InlineData(@"#password1")]
        [InlineData(@"#PASSWORD1")]
        [InlineData(@"(Password1")]
        [InlineData(@")Password1")]
        [InlineData(@"?Password1")]
        [InlineData(@"¿Password1")]
        [InlineData(@"1Password1")]
        [InlineData(@"*Password*")]
        public void ThrowWhenPasswordIsInvalid(string password)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => password).ValidPasswordFormat()
            );
        }
    }
}