
using Shared.Core.Extensions;
using System;

namespace Shared.Test
{
    public class LegalIdShould
    {
        [Theory]
        [InlineData(@"00-00.000.000-0")]
        [InlineData("00-00000000-0")]
        [InlineData("00.000.000")]
        [InlineData("0.000.000")]
        [InlineData("00000000")]
        [InlineData("0000000")]
        public void ShouldValidate(string legalId)
        {
            Guard.Argument(() => legalId).ValidLegalIdFormat();
        }

        [Theory]
        [InlineData(@"00 000 000")]
        [InlineData(@"0-00000000-0")]
        [InlineData(@"0-00000000-00")]
        public void ThrowWhenLegalIdIsNotValid(string legalId)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                Guard.Argument(() => legalId).ValidLegalIdFormat()
            );
        }
    }
}
