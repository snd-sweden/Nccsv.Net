namespace NccsvConverter.TestProject.NccsvParser.FileHandling
{
    public class NccsvParser_Tests
    {
        [Fact]

        public void FromText_ReturnsStringArrayList()
        {
            //Arrange
            var sut = new MainProject.NccsvParser.FileHandling.NccsvParser();

            var testFile = "C:\\SND_repos\\Nccsv Converter\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            //Act
            var result = sut.FromText(testFile);

            //Assert
            Assert.IsType<List<string[]>>(result);
        }


    }
}