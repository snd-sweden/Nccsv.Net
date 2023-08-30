namespace NccsvConverter.TestProject.NccsvParser.FileHandling
{
    public class NccsvParser_Tests
    {
        [Fact]

        public void FromText_ReturnsStringArrayList()
        {
            //Arrange
            var sut = new Parser();

            var testFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                           + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            //Act
            var result = sut.FromText(testFile);

            //Assert
            Assert.IsType<List<string[]>>(result);
        }

    }


}