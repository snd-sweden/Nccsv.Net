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
        if (!NccsvVerifierMethods.CheckForMetaDataEndTag(separatedLines)
            || !NccsvVerifierMethods.CheckAttributesForValue(separatedLines))
            result = false;

        //Dependent non-critical
        if (result)
        {
            NccsvVerifierMethods.CheckGlobalConventions(separatedLines);
            NccsvVerifierMethods.CheckNccsvVerification(separatedLines);
        }

        //Non-critical
        NccsvVerifierMethods.CheckForGlobalAttributes(separatedLines);
        NccsvVerifierMethods.CheckForDataEndTag(separatedLines);
        NccsvVerifierMethods.CheckOrderOfEndTags(separatedLines);

        return result;
    }

    public static bool VerifyMetaData(List<string[]> metaData, bool endMetaDataFound)
    {
        bool result = true;

        //Non-critical
        NccsvVerifierMethods.CheckForGlobalAttributes(metaData);

        //Critical
        if(!NccsvVerifierMethods.CheckForMetaDataEndTag(endMetaDataFound))
            result = false;

        if(!NccsvVerifierMethods.CheckAttributesForValue(metaData))
            result = false;
        else
        {
            NccsvVerifierMethods.CheckGlobalConventions(metaData);
            NccsvVerifierMethods.CheckNccsvVerification(metaData);
        }

        return result;
    }


    public static bool VerifyVariableMetaData(List<string[]> variableMetaData)
    {
        bool result = true;

        //Critical

        //Non-critical
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


    public static bool VerifyData(bool endDataFound)
    {
        bool result = true;

        //Critical

        //Non-critical
        NccsvVerifierMethods.CheckForDataEndTag(endDataFound);

        return result;
    }


    public static bool VerifyData(string[] line)
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
        //NccsvVerifierMethods.CheckNumberOfDataValuesToVariables(dataSet.Data, dataSet.Variables);

        return result;
    }
}

