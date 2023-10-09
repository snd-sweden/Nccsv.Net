using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.FileHandling;

public class Verifier
{
    // Verifiers returns true if all critical Verifier methods return true, false if not.

    public static bool VerifyPath(string filePath)
    {
        bool result = true;

        //Critical

        //Non-critical
        NccsvVerifierMethods.CheckNccsvExtension(filePath);
        
        return result;
    }


    public static bool VerifyLines(string[] lines)
    {
        bool result = true;

        //Critical
        if (!NccsvVerifierMethods.CheckForContent(lines))
            result = false;

        //Non-critical
        
        return result;
    }


    public static bool VerifyValue(string value, int row)
    {
        bool result = true;

        //Critical

        //Non-critical
        NccsvVerifierMethods.CheckValueForSpace(value, row);
        
        return result;
    }


    public static bool VerifySeparatedLines(List<string[]> separatedLines)
    {
        bool result = true;

        //Critical
        if (!NccsvVerifierMethods.CheckForMetaDataEndTag(separatedLines))
            result = false;

        //Non-critical
        NccsvVerifierMethods.CheckGlobalConventions(separatedLines);
        NccsvVerifierMethods.CheckNccsvVerification(separatedLines);
        NccsvVerifierMethods.CheckForDataEndTag(separatedLines);
        NccsvVerifierMethods.CheckOrderOfEndTags(separatedLines);

        return result;
    }


    public static bool VerifyVariableMetaData(List<string[]> variableMetaData)
    {
        bool result = true;

        //Critical

        //Non-critical
        NccsvVerifierMethods.CheckAttributesForValue(variableMetaData);
        NccsvVerifierMethods.CheckVariableNames(variableMetaData);
        NccsvVerifierMethods.CheckAttributeNames(variableMetaData);
        NccsvVerifierMethods.CheckVariableMetaDataForDataType(variableMetaData);

        return result;
    }


    public static bool VerifyVariable(Variable variable, List<string[]> data)
    {
        bool result = true;

        //Critical

        //Non-critical
        if (variable.Scalar)
        {
            NccsvVerifierMethods.CheckDataForScalarVariable(variable, data);
        }

        return result;
    }


    public static bool VerifyData(List<string[]> data)
    {
        bool result = true;

        //Critical

        //Non-critical

        return result;
    }


    public static bool VerifyDataSet(DataSet dataSet)
    {
        bool result = true;

        //Critical

        //Non-critical
        NccsvVerifierMethods.CheckNumberOfDataValuesToVariables(dataSet.Data, dataSet.Variables);

        return result;
    }
}

