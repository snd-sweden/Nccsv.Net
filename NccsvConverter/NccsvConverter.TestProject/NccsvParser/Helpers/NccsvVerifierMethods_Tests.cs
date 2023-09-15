using NccsvConverter.NccsvParser.Models;

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
        public void CheckFileForContent_ReturnsFalseWhenNoContent()
        {
            //Arrange
            var lines = new string[] {}; 

            //Act
            var result = NccsvVerifierMethods.CheckFileForContent(lines);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckFileForContent_ReturnsTrueWhenContent()
        {
            //Arrange
            var lines = new string[]
            {
                "abc"
            }; 

            //Act
            var result = NccsvVerifierMethods.CheckFileForContent(lines);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckGlobalConventions_ReturnsFalseIfNoConventions()
        {
            //Arrange
            var globalAttributes = new List<string[]>
            {
                new string[]
                {
                    "*GLOBAL*", "NotAConvention", "StillNoConventions"
                },
                new string[]
                {
                    "*GLOBAL*", "NoConventionsHere", "Sorry"
                }
            }; 

            //Act
            var result = NccsvVerifierMethods.CheckGlobalConventions(globalAttributes);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckGlobalConventions_ReturnsTrueIfConventionsExists()
        {
            //Arrange
            var globalAttributes = new List<string[]>
            {
                new string[]
                {
                    "*GLOBAL*", "Conventions", "HereTheyAre"
                },
                new string[]
                {
                    "*GLOBAL*", "NoConventionsHere", "Sorry"
                }
            }; 

            //Act
            var result = NccsvVerifierMethods.CheckGlobalConventions(globalAttributes);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckNccsvVerification_ReturnsTrueIfFileIsNccsv()
        {
            //Arrange
            string filePath =
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                + "\\NccsvConverter.ConsoleApp\\TestData\\ryder.nccsv";
            var potentialNccsv = Parser.FromText(filePath);

            //Act
            var result = NccsvVerifierMethods.CheckNccsvVerification(potentialNccsv);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckNccsvVerification_ReturnsFalseIfFileIsNotNccsv()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[6] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckNccsvVerification(potentialNccsv);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckForMetaDataEndTag_ReturnsFalseIfNoTagExists()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckForMetaDataEndTag(potentialNccsv);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckForMetaDataEndTag_ReturnsTrueIfTagExists()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "*END_METADATA*" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckForMetaDataEndTag(potentialNccsv);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckForDataEndTag_ReturnsFalseIfNoTagExists()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckForDataEndTag(potentialNccsv);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckForDataEndTag_ReturnsTrueIfTagExists()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "*END_DATA*" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckForDataEndTag(potentialNccsv);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckAttributeForValue_ReturnsFalseIfNoValue()
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "1", "2" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckAttributeForValue(variableMetaData);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckAttributeValue_ReturnsTrueIfValueExists()
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "1", "2", "3" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckAttributeForValue(variableMetaData);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckVariableForDataType_ReturnsTrueIfVariableHasDataType()
        {
            //Arrange
            var variable = new Variable()
            {
                DataType = "int"
            };

            //Act
            var result = NccsvVerifierMethods.CheckVariableForDataType(variable);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckVariableForDataType_ReturnsFalseIfVariableHasNoDataType()
        {
            //Arrange
            var variable = new Variable();

            //Act
            var result = NccsvVerifierMethods.CheckVariableForDataType(variable);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckDataValuesForSpace_ReturnsFalseWhenNoSpace()
        {
            //Arrange
            var data = new List<string[]>
            {
                new string[] { "1", "2" },
                new string[] { "3 4", "5" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckDataValuesForSpace(data);

            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("1 ")]
        [InlineData(" 2")]
        public void CheckDataValuesForSpace_ReturnsTrueWhenSpace(string spacedDataValue)
        {
            //Arrange
            var data = new List<string[]>
            {
                new string[] { "1", "2" },
                new string[] { spacedDataValue, "4" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckDataValuesForSpace(data);

            //Assert
            Assert.True(result);
        }
    }
}