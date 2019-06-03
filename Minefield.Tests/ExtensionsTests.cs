using Minefield.Extensions;
using Xunit;

namespace Minefield.Tests
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData(0, 'A')]
        [InlineData(1, 'B')]
        [InlineData(2, 'C')]
        [InlineData(3, 'D')]
        [InlineData(4, 'E')]
        [InlineData(5, 'F')]
        [InlineData(6, 'G')]
        [InlineData(7, 'H')]
        public void ConvertToCharSuccess(int number, char expected)
        {
            Assert.Equal(number.ToChar(), expected);
        }
    }
}
