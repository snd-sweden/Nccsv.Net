using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Verifier
{
    public static bool VerifyPath(string filePath)
    {
        if (!NccsvVerifierMethods.CheckNccsvExtension(filePath))
            return false;
        else
            return true;
    }


    public static bool VerifyLines(string[] lines)
    {
        if (!NccsvVerifierMethods.CheckForContent(lines)) // kritisk
            return false;
        else
            return true;
    }

    public static bool VerifyValue(string value, int row)
    {
        if (!NccsvVerifierMethods.CheckValueForSpace(value, row))
            return false;
        else
            return true;
    }


    public static bool VerifySeparatedLines(List<string[]> separatedLines)
    {
        if (!NccsvVerifierMethods.CheckGlobalConventions(separatedLines)
            || !NccsvVerifierMethods.CheckNccsvVerification(separatedLines)
            || !NccsvVerifierMethods.CheckForMetaDataEndTag(separatedLines) // kritisk
            || !NccsvVerifierMethods.CheckForDataEndTag(separatedLines)
            || !NccsvVerifierMethods.CheckOrderOfEndTags(separatedLines))
            return false;
        else
            return true;
    }


    public static bool VerifyVariableMetaData(List<string[]> variableMetaData)
    {
        if (!NccsvVerifierMethods.CheckAttributesForValue(variableMetaData)
            || !NccsvVerifierMethods.CheckVariableNames(variableMetaData)
            || !NccsvVerifierMethods.CheckAttributeNames(variableMetaData)
            || !NccsvVerifierMethods.CheckVariableMetaDataForDataType(variableMetaData)
            // if *DATA_TYPE* attribute, should only have data type as value
            )
            return false;
        else
            return true;
    }


    public static bool VerifyVariable(Variable variable, List<string[]> data)
    {
        if (variable.Scalar)
        {
            if (!NccsvVerifierMethods.CheckDataForScalarVariable(variable, data))
                return false;
        }

        if (!NccsvVerifierMethods.CheckVariableForDataType(variable))
            return false;
        else
            return true;
    }


    public static bool VerifyData(List<string[]> data)
    {
            return true;
    }

    public static bool VerifyDataSet(DataSet dataSet)
    {
        if (!NccsvVerifierMethods.CheckNumberOfDataValuesToVariables(dataSet.Data, dataSet.Variables))
            return false;
        else
            return true;
    }
}

