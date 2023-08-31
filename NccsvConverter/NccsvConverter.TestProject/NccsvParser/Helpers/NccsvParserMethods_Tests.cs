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

}