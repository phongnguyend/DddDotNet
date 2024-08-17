using DddDotNet.CrossCuttingConcerns.Uris;

namespace DddDotNet.CrossCuttingConcerns.UnitTests;

public class UriPathTests
{
    [Fact]
    public void Combine_OnePath_ReturnTheOriginal()
    {
        var url = UriPath.Combine("https://stackoverflow.com/questions/372865");
        Assert.Equal("https://stackoverflow.com/questions/372865", url);
    }

    [Fact]
    public void Combine_OnePath_ShouldNotRemoveTheSlashAtTheEnd()
    {
        var url = UriPath.Combine("https://stackoverflow.com/questions/372865/");
        Assert.Equal("https://stackoverflow.com/questions/372865/", url);
    }

    [Theory]
    [InlineData("https://stackoverflow.com/questions/372865", "path-combine-for-urls")]
    [InlineData("https://stackoverflow.com/questions/372865", "/path-combine-for-urls")]
    [InlineData("https://stackoverflow.com/questions/372865/", "path-combine-for-urls")]
    [InlineData("https://stackoverflow.com/questions/372865/", "/path-combine-for-urls")]
    public void Combine_TwoPaths_ReturnSameResult(string uri1, string uri2)
    {
        var url = UriPath.Combine(uri1, uri2);
        Assert.Equal("https://stackoverflow.com/questions/372865/path-combine-for-urls", url);
    }

    [Theory]
    [InlineData("https://stackoverflow.com/questions/372865", "path-combine-for-urls/")]
    [InlineData("https://stackoverflow.com/questions/372865", "/path-combine-for-urls/")]
    [InlineData("https://stackoverflow.com/questions/372865/", "path-combine-for-urls/")]
    [InlineData("https://stackoverflow.com/questions/372865/", "/path-combine-for-urls/")]
    public void Combine_TwoPaths_ShouldNotRemoveTheSlashAtTheEnd(string uri1, string uri2)
    {
        var url = UriPath.Combine(uri1, uri2);
        Assert.Equal("https://stackoverflow.com/questions/372865/path-combine-for-urls/", url);
    }

    [Theory]
    [InlineData("https://stackoverflow.com/questions/372865", "path-combine-for-urls", "xxx")]
    [InlineData("https://stackoverflow.com/questions/372865", "path-combine-for-urls", "/xxx")]
    [InlineData("https://stackoverflow.com/questions/372865", "/path-combine-for-urls", "xxx")]
    [InlineData("https://stackoverflow.com/questions/372865", "/path-combine-for-urls", "/xxx")]
    [InlineData("https://stackoverflow.com/questions/372865/", "path-combine-for-urls", "xxx")]
    [InlineData("https://stackoverflow.com/questions/372865/", "path-combine-for-urls", "/xxx")]
    [InlineData("https://stackoverflow.com/questions/372865/", "/path-combine-for-urls", "xxx")]
    [InlineData("https://stackoverflow.com/questions/372865/", "/path-combine-for-urls", "/xxx")]
    public void Combine_ThreePaths_ReturnSameResult(string uri1, string uri2, string uri3)
    {
        var url = UriPath.Combine(uri1, uri2, uri3);
        Assert.Equal("https://stackoverflow.com/questions/372865/path-combine-for-urls/xxx", url);
    }

    [Theory]
    [InlineData("https://stackoverflow.com/questions/372865", "path-combine-for-urls", "xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865", "path-combine-for-urls", "/xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865", "/path-combine-for-urls", "xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865", "/path-combine-for-urls", "/xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865/", "path-combine-for-urls", "xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865/", "path-combine-for-urls", "/xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865/", "/path-combine-for-urls", "xxx/")]
    [InlineData("https://stackoverflow.com/questions/372865/", "/path-combine-for-urls", "/xxx/")]
    public void Combine_ThreePaths_ShouldNotRemoveTheSlashAtTheEnd(string uri1, string uri2, string uri3)
    {
        var url = UriPath.Combine(uri1, uri2, uri3);
        Assert.Equal("https://stackoverflow.com/questions/372865/path-combine-for-urls/xxx/", url);
    }
}