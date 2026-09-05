using Shouldly;
using Tk.Toolkit.Cli.TextFormatting;
using Xunit;

namespace Tk.Toolkit.Cli.Tests.Unit.TextFormatting;

public class AltCaseTextFormatterTests
{
    private readonly AltCaseTextFormatter _formatter = new();

    [Fact]
    public void Format_WithSimpleText_AlternatesCase()
    {
        // Arrange
        var input = "hello world";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO wOrLd");
    }

    [Fact]
    public void Format_WithSingleCharacter_ReturnsUppercase()
    {
        // Arrange
        var input = "a";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("A");
    }

    [Fact]
    public void Format_WithMultipleWords_AlternatesCaseAcrossWords()
    {
        // Arrange
        var input = "the quick brown fox";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("ThE qUiCk BrOwN fOx");
    }

    [Fact]
    public void Format_WithNumbers_IgnoresNumbersAndContinuesAlternation()
    {
        // Arrange
        var input = "abc123def456ghi";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("AbC123dEf456GhI");
    }

    [Fact]
    public void Format_WithSpecialCharacters_IgnoresSpecialCharacters()
    {
        // Arrange
        var input = "hello@world!test";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO@wOrLd!TeSt");
    }

    [Fact]
    public void Format_WithOnlyNumbers_ReturnsUnchanged()
    {
        // Arrange
        var input = "123456789";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("123456789");
    }

    [Fact]
    public void Format_WithOnlySpecialCharacters_ReturnsUnchanged()
    {
        // Arrange
        var input = "!@#$%^&*()";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("!@#$%^&*()");
    }

    [Fact]
    public void Format_WithEmptyString_ReturnsEmptyString()
    {
        // Arrange
        var input = "";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("");
    }

    [Fact]
    public void Format_WithNull_ReturnsNull()
    {
        // Act
        var result = _formatter.Format(null);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void Format_WithMixedCaseInput_ResetsFromStartOfString()
    {
        // Arrange
        var input = "HeLLo";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO");
    }

    [Fact]
    public void Format_WithSpaces_IgnoresSpacesInAlternation()
    {
        // Arrange
        var input = "a b c d e";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("A b C d E");
    }

    [Fact]
    public void Format_WithMultipleConsecutiveSpecialChars_IgnoresAll()
    {
        // Arrange
        var input = "a!!!b---c";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("A!!!b---C");
    }

    [Fact]
    public void Format_WithPunctuation_IgnoresPunctuation()
    {
        // Arrange
        var input = "Hello, World! How are you?";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO, wOrLd! HoW aRe YoU?");
    }

    [Fact]
    public void Format_WithUppercaseInput_AlternatesCorrectly()
    {
        // Arrange
        var input = "HELLO";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO");
    }

    [Fact]
    public void Format_WithLowercaseInput_AlternatesCorrectly()
    {
        // Arrange
        var input = "hello";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO");
    }

    [Fact]
    public void Format_WithLongText_MaintainsAlternationPattern()
    {
        // Arrange
        var input = "thequickbrownfoxjumpsoverthelazydog";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("ThEqUiCkBrOwNfOxJuMpSoVeRtHeLaZyDoG");
    }

    [Fact]
    public void Format_WithUnicodeLetters_AlternatesCase()
    {
        // Arrange
        var input = "café";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("CaFé");
    }

    [Fact]
    public void Format_WithTabAndNewline_IgnoresWhitespace()
    {
        // Arrange
        var input = "hello\tworld\ntest";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.ShouldBe("HeLlO\twOrLd\nTeSt");
    }

    [Fact]
    public void Format_PreservesStringLength()
    {
        // Arrange
        var input = "abc123!@#def";

        // Act
        var result = _formatter.Format(input);

        // Assert
        result.Length.ShouldBe(input.Length);
    }
}
