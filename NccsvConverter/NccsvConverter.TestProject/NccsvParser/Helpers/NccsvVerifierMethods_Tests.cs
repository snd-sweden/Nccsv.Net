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
            var result = NccsvVerifierMethods.CheckForContent(lines);

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
            var result = NccsvVerifierMethods.CheckForContent(lines);

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
            var potentialNccsv = Handler.NccsvFileReader(filePath);

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
                new string[] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckNccsvVerification(potentialNccsv);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckOrderOfEndTags_ReturnsFalseIfNoEndTags()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckOrderOfEndTags(potentialNccsv);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckOrderOfEndTags_ReturnsFalseIfIncorrectOrder()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "*END_DATA*"},
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "*END_METADATA*"}
            };

            //Act
            var result = NccsvVerifierMethods.CheckOrderOfEndTags(potentialNccsv);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckOrderOfEndTags_ReturnsTrueIfCorrectOrder()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "*END_METADATA*"},
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "*END_DATA*"},
            };

            //Act
            var result = NccsvVerifierMethods.CheckOrderOfEndTags(potentialNccsv);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckForMetaDataEndTag_ReturnsFalseIfNoTagExists()
        {
            //Arrange
            var potentialNccsv = new List<string[]>
            {
                new string[] { "asd", "argh", "", "", "", "" },
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
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "*END_METADATA*" },
                new string[] { "asd", "argh", "", "", "", "" }
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
                new string[] { "asd", "argh", "", "", "", "" },
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
                new string[] { "asd", "argh", "", "", "", "" },
                new string[] { "*END_DATA*" },
                new string[] { "asd", "argh", "", "", "", "" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckForDataEndTag(potentialNccsv);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckAttributesForValue_ReturnsFalseIfNoValue()
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "1", "2", "3" },
                new string[] { "1", "2" },
                new string[] { "1", "2", "3" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckAttributesForValue(variableMetaData);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckAttributesForValue_ReturnsTrueIfValueExists()
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "1", "2", "3" },
                new string[] { "1", "2", "3" },
                new string[] { "1", "2", "3" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckAttributesForValue(variableMetaData);

            //Assert
            Assert.True(result);
        }


        [Theory]
        [InlineData("2")]
        [InlineData("1Name")]        
        public void CheckVariableNames_ReturnsFalseIfNameIsNotLegal(string variableName)
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "variableName", "attributeName", "value" },
                new string[] { variableName, "attributeName", "value" },
                new string[] { "variableName", "attributeName", "value" },
            };

            //Act
            var result = NccsvVerifierMethods.CheckVariableNames(variableMetaData);

            //Assert
            Assert.False(result);
        }


        [Theory]
        [InlineData("GoodName")]
        [InlineData("Thanks2")]      
        public void CheckVariableNames_ReturnsTrueIfNameIsLegal(string variableName)
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "variableName", "attributeName", "value" },
                new string[] { variableName, "attributeName", "value" },
                new string[] { "variableName", "attributeName", "value" },
            };

            //Act
            var result = NccsvVerifierMethods.CheckVariableNames(variableMetaData);

            //Assert
            Assert.True(result);
        }


        [Theory]
        [InlineData("2")]
        [InlineData("1Name")]      
        public void CheckAttributeNames_ReturnsFalseIfNameIsNotLegal(string attributeName)
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "variableName", "attributeName", "value" },
                new string[] { "variableName", attributeName, "value" },
                new string[] { "variableName", "attributeName", "value" },
            };

            //Act
            var result = NccsvVerifierMethods.CheckAttributeNames(variableMetaData);

            //Assert
            Assert.False(result);
        }


        [Theory]
        [InlineData("GoodName")]
        [InlineData("Thanks2")]      
        public void CheckAttributeNames_ReturnsTrueIfNameIsLegal(string attributeName)
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "variableName", "attributeName", "value" },
                new string[] { "variableName", attributeName, "value" },
                new string[] { "variableName", "attributeName", "value" },
            };

            //Act
            var result = NccsvVerifierMethods.CheckAttributeNames(variableMetaData);

            //Assert
            Assert.True(result);
        }


        [Fact]     
        public void CheckVariableMetaDataForDataType_ReturnsTrueIfDistinctVariables_Match_VariablesWithDataType()
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "variableName", "*DATA_TYPE*", "String" },
                new string[] { "variableName", "attributeName", "value" },
                new string[] { "variableNameTwo", "*DATA_TYPE*", "int" },
                new string[] { "variableNameTwo", "attributeName", "value" },
                new string[] { "variableNameThree", "*SCALAR*", "scalarValue" },
                new string[] { "variableNameThree", "attributeName", "value" },
            };

            //Act
            var result = NccsvVerifierMethods.CheckVariableMetaDataForDataType(variableMetaData);

            //Assert
            Assert.True(result);
        }


        [Fact]     
        public void CheckVariableMetaDataForDataType_ReturnsFalseIfDistinctVariables_DoesNotMatch_VariablesWithDataType()
        {
            //Arrange
            var variableMetaData = new List<string[]>
            {
                new string[] { "variableName", "*DATA_TYPE*", "String" },
                new string[] { "variableName", "attributeName", "value" },
                new string[] { "variableNameTwo", "attributeName", "value" },
                new string[] { "variableNameThree", "*DATA_TYPE*", "double" },
                new string[] { "variableNameThree", "attributeName", "value" },
            };

            //Act
            var result = NccsvVerifierMethods.CheckVariableMetaDataForDataType(variableMetaData);

            //Assert
            Assert.False(result);
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
        public void CheckDataForScalarVariable_ReturnsFalse_IfHeaderIsFound()
        {
            //Arrange
            var scalarVariable = new Variable
            {
                VariableName = "jkl"
            };

            var data = new List<string[]>
            {
                new string[] { "ghi", "jkl" },
                new string[] { "abc", "def" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckDataForScalarVariable(scalarVariable, data);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckDataForScalarVariable_ReturnsTrue_IfHeaderIsNotFound()
        {
            //Arrange
            var scalarVariable = new Variable
            {
                VariableName = "mno"
            };

            var data = new List<string[]>
            {
                new string[] { "ghi", "jkl" },
                new string[] { "abc", "def" }
            };

            //Act
            var result = NccsvVerifierMethods.CheckDataForScalarVariable(scalarVariable, data);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckDataForDataType_ReturnsFalseTypeDoesNotMatch()
        {
            //Arrange
            var data = new List<string[]>
            {
                new string[] { "header", "anotherheader", "moreheader"},
                new string[] { "value", "anothervalue", "morevalue"}
            };

            var variables = new List<Variable>
            {
                new Variable
                {
                    VariableName = "header",
                    DataType = "System.String"
                },
                new Variable 
                {
                    VariableName = "anotherheader",
                    DataType = "System.Int32"
                },
                new Variable 
                {
                    VariableName = "moreheader",
                    DataType = "System.String"
                },
            };

            //Act
            var result = NccsvVerifierMethods.CheckDataForDataType(data, variables);

            //Assert
            Assert.False(result);
        }


        [Fact]
        public void CheckDataForDataType_ReturnsTrueTypeMatches()
        {
            //Arrange
            var data = new List<string[]>
            {
                new string[] { "header", "anotherheader", "moreheader"},
                new string[] { "value", "anothervalue", "morevalue"}
            };

            var variables = new List<Variable>
            {
                new Variable
                {
                    VariableName = "header",
                    DataType = "System.String"
                },
                new Variable 
                {
                    VariableName = "anotherheader",
                    DataType = "System.String"
                },
                new Variable 
                {
                    VariableName = "moreheader",
                    DataType = "System.String"
                },
            };

            //Act
            var result = NccsvVerifierMethods.CheckDataForDataType(data, variables);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckDataValuesForSpace_ReturnsTrueWhenNoSpace()
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
            Assert.True(result);
        }


        [Theory]
        [InlineData("1 ")]
        [InlineData(" 2")]
        public void CheckDataValuesForSpace_ReturnsFalseWhenSpace(string spacedDataValue)
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
            Assert.False(result);
        }


        [Fact]
        public void CheckNumberOfDataValuesToVariables_ReturnsTrueIfNumbersMatch()
        {
            //Arrange
            var data = new List<DataValue[]>
            {
                new DataValue[] 
                { 
                    new DataValueAs<string> { Value = "1"},
                    new DataValueAs<string> { Value = "2"}
                },
                new DataValue[] 
                { 
                    new DataValueAs<string> { Value = "3"},
                    new DataValueAs<string> { Value = "4"}
                },
                new DataValue[] 
                { 
                    new DataValueAs<string> { Value = "5"},
                    new DataValueAs<string> { Value = "6"}
                }
            };

            var variables = new List<Variable>
            {
                new Variable(),
                new Variable()
            };

            //Act
            var result = NccsvVerifierMethods.CheckNumberOfDataValuesToVariables(data, variables);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public void CheckNumberOfDataValuesToVariables_ReturnsFalseIfNumbersDoNotMatch()
        {
            //Arrange
            var data = new List<DataValue[]>
            {
                new DataValue[] 
                { 
                    new DataValueAs<string> { Value = "1"},
                    new DataValueAs<string> { Value = "2"}
                },
                new DataValue[] 
                { 
                    new DataValueAs<string> { Value = "3"},
                    new DataValueAs<string> { Value = "4"}
                },
                new DataValue[] 
                { 
                    new DataValueAs<string> { Value = "5"},
                    new DataValueAs<string> { Value = "6"}
                }
            };

            var variables = new List<Variable>
            {
                new Variable()
            };

            //Act
            var result = NccsvVerifierMethods.CheckNumberOfDataValuesToVariables(data, variables);

            //Assert
            Assert.False(result);
        }


        //[Theory]
        //[InlineData("12.3d","double")]
        //[InlineData("123s","short")]
        //[InlineData("12.3f","float")]
        //public void CheckDataForIllegalSuffix_ReturnsTrueIfIllegalSuffixIsFound(string value, string dataType)
        //{
        //    //Act
        //    var result = NccsvVerifierMethods.CheckDataForIllegalSuffix(dataType, value);

        //    //Assert
        //    Assert.True(result);
        //}


        //[Theory]
        //[InlineData("good value","String")]
        //[InlineData("123L","long")]
        //[InlineData("123","int")]
        //[InlineData("12.3","double")]
        //public void CheckDataForIllegalSuffix_ReturnsFalseIfIllegalSuffixIsNotFound(string value, string dataType)
        //{
        //    //Act
        //    var result = NccsvVerifierMethods.CheckDataForIllegalSuffix(dataType, value);

        //    //Assert
        //    Assert.False(result);
        //}
    }
}