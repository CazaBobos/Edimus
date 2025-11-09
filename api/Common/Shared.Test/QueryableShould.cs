
using Shared.Core.Extensions;
using System;
using System.Linq;

namespace Shared.Test
{
    public class QueryableShould
    {
        private int[] TestArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        [Theory]
        [InlineData(null, 0)]
        [InlineData(null, -1)]
        [InlineData(0, null)]
        [InlineData(-1, null)]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, -1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, 1)]
        [InlineData(-1, -1)]
        public void ShouldThrowArgumentException(int? limit, int? page)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                TestArray.AsQueryable().Paginate(limit, page)
            );
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(10, 10)]
        public void ShouldReturnEmpty(int? limit, int? page)
        {
            Assert.Empty(
                TestArray.AsQueryable().Paginate(limit, page)
            );
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 1)]
        [InlineData(1, null)]
        public void ShouldReturnQueryable(int? limit, int? page)
        {
            var pagedArray = TestArray.AsQueryable().Paginate(limit, page);
            Assert.Equal(pagedArray.Count(), TestArray.Count());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        [InlineData(9, 1)]
        [InlineData(1, 8)]
        [InlineData(99, 1)]
        public void ShouldPaginate(int limit, int page)
        {
            var pagedArray = TestArray
                .AsQueryable()
                .Paginate(limit, page)
                .ToArray();

            var slicedArray = TestArray
                .Skip(limit * (page - 1))
                .Take(limit)
                .ToArray();

            for (int i = 0; i < slicedArray.Length; i++)
                Assert.Equal(pagedArray[i], slicedArray[i]);
        }
    }
}
