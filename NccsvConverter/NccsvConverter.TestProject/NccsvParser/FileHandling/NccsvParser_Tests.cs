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


    }
}