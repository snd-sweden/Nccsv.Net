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
            var result = Handler.NccsvFileReader(testFile);

            //Assert
            Assert.IsType<List<string[]>>(result);
        }


        [Fact]
        public void FromList_ReturnsCorrectDataSetObject()
        {
            //Arrange
            var testFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                           + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            var testCollection = Handler.NccsvFileReader(testFile);

            var globalAttributes = NccsvParserMethods.FindGlobalAttributes(testCollection) ;

            var expectedGlobalAttributes = new Dictionary<string, string>();

            foreach (var keyValuePair in globalAttributes)
            {
                expectedGlobalAttributes.Add(keyValuePair[0], keyValuePair[1]);
            }

            var expectedTitle = NccsvParserMethods.FindTitle(globalAttributes) ;

            var expectedSummary = NccsvParserMethods.FindSummary(globalAttributes) ;

            //var expectedVariables = NccsvParserMethods.FindVariableMetaData(testCollection) ;

            //var expectedData = NccsvParserMethods.FindData(testCollection) ;

            //Act
            var dataSet = Handler.NccsvHandler(testCollection);

            //Assert
            Assert.Equal(expectedTitle, dataSet.Title);
            Assert.Equal(expectedSummary, dataSet.Summary);
            Assert.Equal(expectedGlobalAttributes, dataSet.GlobalAttributes);
        }

    }


}