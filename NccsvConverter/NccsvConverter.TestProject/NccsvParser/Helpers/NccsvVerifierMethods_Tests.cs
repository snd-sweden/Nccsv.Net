using NccsvConverter.MainProject.NccsvParser.Helpers;

namespace NccsvConverter.TestProject.NccsvParser.Helpers
{
    public class NccsvVerifierMethods_Tests
    {
        [Theory]
        [InlineData("test.nccsv")]
        [InlineData(".nccsv")]
        public void CheckNccsvExtension_ReturnsTrueIfExtensionIsNccsv(string filePath)
        {
            //Arrange
            var sut = new NccsvVerifierMethods();

            //Act 
            var result = sut.CheckNccsvExtension(filePath);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("test.csv")]
        [InlineData("")]
        public void CheckNccsvExtension_ReturnsFalseIfExtensionIsNotNccsv(string filePath)
        {
            //Arrange
            var sut = new NccsvVerifierMethods();

            //Act 
            var result = sut.CheckNccsvExtension(filePath);

            //Assert
            Assert.False(result);
        }
        [Fact]
        public void VerifyNccsv_ReturnsTrueIfFileIsNccsv()
        {
            //Arrange
            var sut = new NccsvVerifierMethods();
            var parser = new MainProject.NccsvParser.FileHandling.NccsvParser();
            string filePath = "C:\\SND_repos\\NccsvConverter\\NccsvConverter\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";
            var csv = parser.FromText(filePath);


            //Act
            var result = sut.VerifyNccsv(csv);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyNccsv_ReturnsFalseIfFileIsNotNccsv()
        {
            //Arrange
            var sut = new NccsvVerifierMethods();
            var csv = new List<string[]>
            {
                new string[6] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = sut.VerifyNccsv(csv);

            //Assert
            Assert.False(result);
        }

    }
}