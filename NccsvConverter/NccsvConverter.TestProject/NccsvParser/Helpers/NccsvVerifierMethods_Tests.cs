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
    }
}