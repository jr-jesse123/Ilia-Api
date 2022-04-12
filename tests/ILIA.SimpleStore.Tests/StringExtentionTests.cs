using FluentAssertions;
using ILIA.SimpleStore.API.Extentions;
using Xunit;

namespace ILIA.SimpleStore.Tests;

public class StringExtentionTests
{
    [Fact(DisplayName = "Text should be removed from original string")]
    public void Test1()
    { 
        //Arrange  
        var originaSentence = "this is the original sentence";
        var textToRemove = "original";

        //Act
        var underTest = originaSentence.RemoveSentence(textToRemove);

        //Assert
        underTest.Should().NotContain(textToRemove);
    }


    [Theory(DisplayName = "Text is preserved when Sentence to remove is empty, null, or not present in the original string ")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("notExistent")]
    public void Test2(string textToRemove)
    {
        //Arrange
        var originaSentence = "this is the original sentence";

        //Act
        var underTest = originaSentence.RemoveSentence(textToRemove);

        //Assert
        underTest.Should().Be(originaSentence);
    }
}
