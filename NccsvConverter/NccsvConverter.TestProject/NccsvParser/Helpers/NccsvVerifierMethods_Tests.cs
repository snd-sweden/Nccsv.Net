using NccsvConverter.MainProject.NccsvParser.Helpers;

namespace NccsvConverter.TestProject.NccsvParser.Helpers
{
    public class NccsvVerifierMethods_Tests
    {
        [Fact]
        public void Utf8Checker_ReturnsTrueIfFileIsUtf8()
        {
            //Arrange
            var sut = new NccsvVerifierMethods();
            string filePath = "C:\\SND_repos\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            //Act 
            var result = sut.Utf8Checker(filePath);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Utf8Checker_ReturnsFalseIfFileIsNotUtf8()
        {
            //Arrange
            var sut = new NccsvVerifierMethods();
            string filePath = "C:\\SND_repos\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            //Act 
            var result = sut.Utf8Checker(filePath);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyNccsv_ReturnsTrueIfFileIsNccsv()
        {
            //Arrange
            var sut = new NccsvVerifierMethods();
            var parser = new MainProject.NccsvParser.FileHandling.NccsvParser();
            string filePath = "C:\\SND_repos\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";

            //Act
            var csv = parser.FromText(filePath);
            var result = sut.VerifyNccsv(csv);

            //Assert
            Assert.True(result);
        }
    }
}