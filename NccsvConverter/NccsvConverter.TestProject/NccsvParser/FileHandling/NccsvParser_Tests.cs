namespace NccsvConverter.TestProject.NccsvParser.FileHandling
{
    public class NccsvParser_Tests
    {
        [Fact]
        public void FromText_ReturnsStringArrayList()
        {
            //Arrange
            var testFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                           + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            //Act
            var result = Parser.FromText(testFile);

            //Assert
            Assert.IsType<List<string[]>>(result);
        }

        [Fact]
        public void FromList_ReturnsCorrectDataSetObject()
        {
            //Arrange
            var testFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                           + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            var testCollection = Parser.FromText(testFile);

            var globalProperties = NccsvParserMethods.FindGlobalAttributes(testCollection) ;

            var expectedGlobalProperties = new Dictionary<string, string>();

            foreach (var keyValuePair in globalProperties)
            {
                expectedGlobalProperties.Add(keyValuePair[0], keyValuePair[1]);
            }

            var expectedTitle = NccsvParserMethods.FindTitle(globalProperties) ;

            var expectedSummary = NccsvParserMethods.FindSummary(globalProperties) ;

            //var expectedVariables = NccsvParserMethods.FindVariableMetaData(testCollection) ;

            //var expectedData = NccsvParserMethods.FindData(testCollection) ;

            //Act

            var dataSet = Parser.FromList(testCollection);

            //Assert

            Assert.Equal(expectedTitle, dataSet.Title);
            Assert.Equal(expectedSummary, dataSet.Summary);
            Assert.Equal(expectedGlobalProperties, dataSet.GlobalAttributes);

        }

    }


}