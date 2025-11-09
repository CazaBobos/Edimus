
using Shared.Core.Extensions;
using System;

namespace Shared.Test
{
    public class DateShould
    {
        [Theory]
        [InlineData(@"1753/01/01")]
        [InlineData(@"1970/12/01")]
        [InlineData(@"9999/12/31")]
        public void ShouldValidate(DateTime date)
        {
            Guard.Argument(() => date).ValidSqlDate();
        }

        [Theory]
        [InlineData(@"1752/12/31")]
        public void ThrowWhenDateIsNotValid(DateTime date)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => date).ValidSqlDate()
            );
        }

        [Fact]
        public void ShouldValidateFuture()
        {
            Guard.Argument(() => DateTime.MaxValue).Future();
        }

        [Fact]
        public void ThrowWhenDateIsNotFuture()
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => DateTime.MinValue).Future()
            );
        }

        [Fact]
        public void ShouldValidateNotFuture()
        {
            Guard.Argument(() => DateTime.MinValue).NotFuture();
        }

        [Fact]
        public void ThrowWhenDateIsFuture()
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => DateTime.MaxValue).NotFuture()
            );
        }
    }
}
