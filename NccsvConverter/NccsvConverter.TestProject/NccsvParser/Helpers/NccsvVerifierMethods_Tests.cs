namespace NccsvConverter.TestProject.NccsvParser.Helpers
{
    public class NccsvVerifierMethods_Tests
    {
        [Theory]
        [InlineData("test.nccsv")]
        [InlineData(".nccsv")]
        public void CheckNccsvExtension_ReturnsTrueIfExtensionIsNccsv(string filePath)
        {
            //Act 
            var result = NccsvVerifierMethods.CheckNccsvExtension(filePath);

            //Assert
            Assert.True(result);
        }


        [Theory]
        [InlineData("test.csv")]
        [InlineData("")]
        public void CheckNccsvExtension_ReturnsFalseIfExtensionIsNotNccsv(string filePath)
        {
            //Act 
            var result = NccsvVerifierMethods.CheckNccsvExtension(filePath);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void VerifyNccsv_ReturnsTrueIfFileIsNccsv()
        {
            //Arrange
            string filePath =
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";
            var potentialNccsv = Parser.FromText(filePath);

            //Act
            var result = NccsvVerifierMethods.VerifyNccsv(potentialNccsv);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void VerifyNccsv_ReturnsFalseIfFileIsNotNccsv()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[6] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.VerifyNccsv(potentialNccsv);

            //Assert
            Assert.False(result);
        }

    }
}