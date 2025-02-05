using System;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RpnCalculatorDomain;

namespace RpnCalculatorTests;

public class RpnCalculatorSpecifications
{

    [Fact]
    public void Enter_Empty_String()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When   
        var exception = Assert.Throws<RpnCalculatorException>(() => rpnCalculator.Process(""));
        //Then
        Assert.Equal(RpnExceptionType.InvalidExpression, exception.ExceptionType);
        Assert.Equal("Expression cannot be empty.", exception.Message);
    }

    [Fact]
    public void Enter_One_Value()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        var result = rpnCalculator.Process("7");
        //Then
        result.Should().Be(7);

    }

    //Example of parametrized tests
    //When we want to test with different inputs
    [Theory]
    [InlineData("2 1 +", 3)]
    [InlineData("3 4 +", 7)]
    public void Try_Simple_Addition(string expression, double result)
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        rpnCalculator.Process(expression);
        //Then
        result.Should().Be(result);    
    }

    [Fact]
    public void Try_Simple_Substraction()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        var result =rpnCalculator.Process("7 2 -");
        //Then
        result.Should().Be(5);
    }

    [Fact]
    public void Try_Simple_Division()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        var result = rpnCalculator.Process("6 2 /");
        //Then
        result.Should().Be(3);
    }

    [Fact]
    public void Division_By_Zero_Is_Not_Allowed()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        //Then
        var exception = Assert.Throws<RpnCalculatorException>(() => rpnCalculator.Process("2 0 /"));
        Assert.Equal("Division by zero is not allowed.", exception.Message);
        Assert.Equal(RpnExceptionType.DivisionByZero, exception.ExceptionType);

    }

    [Fact]
    public void Try_Simple_Multiplication()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        var result = rpnCalculator.Process("6 2 *");
        //Then
        Assert.Equal(12, result);
        //result.Should().Be(12);
    }

    [Fact]
    public void Try_Simple_Modulus()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        var result = rpnCalculator.Process("6 4 %");
        //Then
        Assert.Equal(2, result);
        //result.Should().Be(12);
    }

    [Fact]
    public void Try_Modulus_Error_Division_By_Zero()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        var exception = Assert.Throws<RpnCalculatorException>(() => rpnCalculator.Process("6 0 %"));
        //Then
        Assert.Equal("Division by zero is not allowed.", exception.Message);
        Assert.Equal(RpnExceptionType.DivisionByZero, exception.ExceptionType);
    }

    [Fact]
    public void Try_Complex_Expression()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        //Then
        Assert.Equal(14, rpnCalculator.Process("5 1 2 + 4 * + 3 -"));
    }

    [Fact]
    public void Try_Too_Many_Spaces()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        //Then
        Assert.Equal(6, rpnCalculator.Process("5      1       +"));
    }

    [Theory]
    [InlineData("9 +")]
    [InlineData("+")]
    [InlineData("+ / *")]
    public void Try_Invalid_Expression(string input)
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        //Then
        var exception = Assert.Throws<RpnCalculatorException>(()=> rpnCalculator.Process(input));
        Assert.Equal(RpnExceptionType.InvalidExpression, exception.ExceptionType);
        Assert.Equal("Invalid expression: insufficient operands.", exception.Message);
    }

    [Theory]
    [InlineData("4 2 X","X")]
    [InlineData("4 2 $","$")]
    [InlineData("4 2 &","&")]
    public void Try_Invalid_Operands(string expression, string unknownoperator)
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        var expectedMessage = $"Invalid character in input: {unknownoperator}.";
        //When
        //Then
        var exception = Assert.Throws<RpnCalculatorException>(() => rpnCalculator.Process(expression));
        Assert.Equal(expectedMessage, exception.Message);
        Assert.Equal(RpnExceptionType.InvalidExpression, exception.ExceptionType);

    }

    [Fact]
    public void Try_large_Number()
    {
        //Given
        var rpnCalculator = new RpnCalculator();
        //When
        //Then
        var exception = Assert.Throws<RpnCalculatorException>(() => rpnCalculator.Process("9999999999999999999999999999999 2 *"));
        Assert.Equal(RpnExceptionType.OutOfRange, exception.ExceptionType);
        Assert.Equal("Number too large: 9999999999999999999999999999999.", exception.Message);
    }



}
