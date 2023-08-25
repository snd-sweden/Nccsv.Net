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
            string filePath = "";

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
            string filePath = "";

            //Act 
            var result = sut.Utf8Checker(filePath);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("test.nccsv")]
        [InlineData(".nccsv")]
        public void NccsvExtensionChecker_ReturnsTrueIfExtensionIsNccsv(string filePath)
        {
            //Arrange
            var sut = new NccsvVerifierMethods();

            //Act 
            var result = sut.NccsvExtensionChecker(filePath);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test.csv")]
        [InlineData("")]
        public void NccsvExtensionChecker_ReturnsFalseIfExtensionIsNotNccsv(string filePath)
        {
            //Arrange
            var sut = new NccsvVerifierMethods();

            //Act 
            var result = sut.NccsvExtensionChecker(filePath);

            //Assert
            Assert.False(result);
        }


    }
}