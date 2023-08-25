using NccsvConverter.MainProject.NccsvParser.Helpers;

namespace NccsvConverter.TestProject.NccsvParser.Helpers
{
    public class NccsvVerifierMethods_Tests
    {
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