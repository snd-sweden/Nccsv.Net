using System;

namespace NccsvConverter.TestProject.NccsvParser.Helpers;

public class NccsvParserMethods_Tests
{
    [Fact]
    public void FindGlobalProperties_ReturnsCorrectList()
    {
        //Arrange
        var parser = new MainProject.NccsvParser.FileHandling.NccsvParser();
        var parserMethods = new NccsvParserMethods();
        var csv = parser.FromText(
            "C:\\SND_repos\\Nccsv Converter\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv");
        bool result = true;
        //Act
        var globalProperties = parserMethods.FindGlobalProperties(csv);
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
}