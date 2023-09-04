using NccsvConverter.NccsvParser.Models;
using System;

namespace NccsvConverter.TestProject.NccsvParser.Helpers;

public class NccsvParserMethods_Tests
{
    [Fact]
    public void FindGlobalProperties_ReturnsCorrectList()
    {
        //Arrange
        var csv = Parser.FromText(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");
        bool result = true;

        //Act
        var globalProperties = NccsvParserMethods.FindGlobalProperties(csv);

        //Assert

        /*if the nccvs was handled properly, it should consist of a list of arrays of length 2:
        each a key/value pair excellent for putting in a Dictionary.*/

        foreach (var sArr in globalProperties)
        {
            if (sArr.Length != 2)
            {
                result = false;
            }
        }

        Assert.True(result);
    }


    [Fact]
    public void FindProperties_ReturnsListOfStringArrays()
    {
        //Arrange
         var csv = Parser.FromText(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");

        //Act 
        var result = NccsvParserMethods.FindProperties(csv);

        //Assert
        Assert.IsType<List<string[]>>(result);
    }

    [Fact]
    public void FindProperties_DoesNotReturnGlobalProperties()
    {
        //Arrange
        var csv = Parser.FromText(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");
        var expected = "*GLOBAL*";

        //Act 
        var result = NccsvParserMethods.FindProperties(csv);

        //Assert
        Assert.NotEqual(expected, result[0][0]);
    }

    [Fact]
    public void FindProperties_ReturnsExpectedList()
    {
        //Arrange
        var csv = new List<string[]>
        {
            new string[] { "*GLOBAL*", "abc", "def" },
            new string[] { "abc", "def", "ghi", "j\",k\"l" },
            new string[] { "mno", "pqr", "stu", "vxy" },
            new string[] { "*END_METADATA*"}
        };
            
        var expected = new List<string[]>
        {
            new string[] { "abc", "def", "ghi", "j\",k\"l" },
            new string[] { "mno", "pqr", "stu", "vxy" }
        };

        //Act 
        var result = NccsvParserMethods.FindProperties(csv);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Separate_ReturnsSeparatedValuesAsList()
    {
        //Arrange
        var line = "abc,def,ghi,jkl";
        List<string> expected = new List<string>
        {
            "abc",
            "def",
            "ghi",
            "jkl"
        };

        //Act 
        var result = NccsvParserMethods.Separate(line);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Separate_ReturnsSeparatedValues_WhenQuotes()
    {
        //Arrange
        var line = "abc,def,ghi,\"jkl,mno\"";
        List<string> expected = new List<string>
        {
            "abc",
            "def",
            "ghi",
            "\"jkl,mno\""
        };

        //Act 
        var result = NccsvParserMethods.Separate(line);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Separate_ReturnsSeparatedValues_WhenQuotesInQuotes()
    {
        //Arrange
        var line = "abc,def,ghi,\"jkl,\"\"m,\"\"no\"";
        List<string> expected = new List<string>
        {
            "abc",
            "def",
            "ghi",
            "\"jkl,\"\"m,\"\"no\""
        };

        //Act 
        var result = NccsvParserMethods.Separate(line);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddProperties_AddsPropertiesAsExpected()
    {
        //Arrange
        var variable = new Variable();
        var varProperties = new List<string[]> 
        { 
            new [] {"abc", "def", "ghi", "j\",k\"l" }
        };
        var expected = new Dictionary<string, List<string>>()
        {
            { "def", new List<string> { "ghi", "j\",k\"l" }}
        };

        //Act 
        NccsvParserMethods.AddProperties(varProperties, variable);

        //Assert
        Assert.Equal(expected, variable.Properties);
    }

};