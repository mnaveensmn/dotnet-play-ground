namespace webapi_play_ground_test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        List<string> list1 = ["B","B"];
        List<string> list2 = ["B","B"];
        
        Assert.Equal(list1,list2);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("     ")]
    [InlineData("     ")]
    [InlineData(null)]
    public void TestIsNullOfWhiteSpace(string? value)
    {
        Assert.True(string.IsNullOrWhiteSpace(value));
    }
}