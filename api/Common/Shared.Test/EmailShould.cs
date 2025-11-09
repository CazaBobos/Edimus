using Shared.Core.Extensions;
using System;

namespace Shared.Test
{
    public class EmailShould
    {
        [Theory]
        [InlineData(@"""test\\blah""@example.com")]
        [InlineData("\"test\\\rblah\"@example.com")]
        [InlineData(@"""test\""blah""@example.com")]
        [InlineData(@"customer/department@example.com")]
        [InlineData(@"$A12345@example.com")]
        [InlineData(@"!def!xyz%abc@example.com")]
        [InlineData(@"_Yosemite.Sam@example.com")]
        [InlineData(@"~@example.com")]
        [InlineData(@"""Austin@Powers""@example.com")]
        [InlineData(@"Ima.Fool@example.com")]
        [InlineData(@"""Ima.Fool""@example.com")]
        [InlineData(@"""Ima Fool""@example.com")]
        public void ShouldValidate(string email)
        {
            Guard.Argument(() => email).ValidEmailFormat();
        }

        [Theory]
        [InlineData(@"NotAnEmail")]
        [InlineData(@"@NotAnEmail")]
        [InlineData(@"""test\blah""@example.com")]
        [InlineData("\"test\rblah\"@example.com")]
        [InlineData(@"""test""blah""@example.com")]
        [InlineData(@".@example.com")]
        [InlineData(@".wooly@example.com")]
        [InlineData(@"wo..oly@example.com")]
        [InlineData(@"pootietang.@example.com")]
        [InlineData(@"Ima Fool@example.com")]
        public void ThrowWhenEmailIsInvalid(string email)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => email).ValidEmailFormat()
            );
        }
    }
}