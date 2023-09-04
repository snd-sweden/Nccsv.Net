using System;
using System.IO;
using NccsvConverter.NccsvParser.Models;
using Xunit.Sdk;

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
    public void AddGlobalProperties_AddsPropertiesProperly()
    {
        //Arrange
        var globalProperties = new List<string[]>
        {
            new string[2] { "hej", "då" },
            new string[2] { "ses", "sen" }
        };

        var dataSet = new DataSet();

        //Act

        NccsvParserMethods.AddGlobalProperties(dataSet, globalProperties);

        //Assert
        Assert.Equal("då", dataSet.GlobalProperties["hej"]);
        Assert.Equal("sen", dataSet.GlobalProperties["ses"]);

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

    [Theory]
    [InlineData("rainfall_avg")]
    [InlineData("ship_name")]
    public void CheckIfVariableExists_FindsVariable(string variableName)
    {
        //Arrange
        var variableList = new List<Variable>
        {
            new Variable() { DataType = "int", VariableName = "rainfall_avg" },
            new Variable() { DataType = "string",VariableName = "ship_name"}
        };

        //Act
        var result = NccsvParserMethods.CheckIfVariableExists(variableList, variableName);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void CreateVariable_CreastesVariableWithAllProperties()
    {
        //Arrange
        var variableProperties = new List<string[]>
        {

        };



        //Act

        var newVar = NccsvParserMethods.CreateVariable(variableProperties);

        //Assert
        Assert.NotNull(newVar.VariableName);
        Assert.NotEmpty(newVar.Properties);
    }

    [Fact]
    public void IsolateProperty_ReturnsCorrectLines()
    {
        //Arrange
        var csv = Parser.FromText(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");
        var props = NccsvParserMethods.FindProperties(csv);
        var propName = "depth";
        //Act

        var depthProperty = NccsvParserMethods.IsolateProperty(props, propName);

        //Assert
        Assert.True(depthProperty.Count >= 2);
        foreach (var line in depthProperty)
        {
            Assert.Equal(propName, line[0]);
        }

    }

    [Fact]
    public void SetVariableDataType_SetsCorrectDataType()
    {
        //Arrange
        var csv = Parser.FromText(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
            + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");
        var props = NccsvParserMethods.FindProperties(csv);
        var propName = "depth";
        var depthProperty = NccsvParserMethods.IsolateProperty(props, propName);

        var testVariable = new Variable() { VariableName = propName };

        var expected = "double";

        //Act

        var completeVariable = NccsvParserMethods.SetVariableDataType(testVariable, depthProperty);

        //Assert
        Assert.Equal(expected, completeVariable.DataType);

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

}