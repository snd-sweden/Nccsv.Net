using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Xunit.Sdk;
using nccsv_verifier;

namespace nccsv_verifier.Tests
{
    public class NccsvVerifierMethods_Tests
    {
        [Fact]
        public void Utf8Checker_ReturnsTrueIfFileIsUtf8()
        {
            //Arrange
            var sut = new NccsvVerifierMethods();
            string filePath = "C:\\SND_repos\\nccsv_netcdf\\nccsv_converter\\nccsv_verifier_tests\\testcase-utf-8-true.csv";

            //Act 
            var result = sut.Utf8Checker(filePath);

            //Assert
            Assert.True(result);
        }
    }
}