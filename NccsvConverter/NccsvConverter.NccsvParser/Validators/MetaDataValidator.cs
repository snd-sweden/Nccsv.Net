
using NccsvConverter.NccsvParser.Helpers;
using NccsvConverter.NccsvParser.Models;
using NccsvConverter.NccsvParser.Repositories;

namespace NccsvConverter.NccsvParser.Validators;

public class MetaDataValidator : Validator
{
    public static bool Validate(List<string[]> metaData, bool endMetaDataFound)
    {
        var result = true;
        //Non-critical
        CheckForGlobalAttributes(metaData);

        //Critical
        if (!CheckForMetaDataEndTag(endMetaDataFound))
            result = false;

        if (!CheckAttributesForValue(metaData))
            result = false;
        else
        {
            CheckGlobalConventions(metaData);
            CheckNccsvVerification(metaData);
        }

        return result;
    }


    // Returns true if global attributes is found, false if not.
    public static bool CheckForGlobalAttributes(List<string[]> metaData)
    {
        if (metaData[0][0].Equals("*GLOBAL*"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Row 1: Couldn't find global attributes.", Severity.NonCritical));
            return false;
        }
    }


    //TODO: write tests
    public static bool CheckForMetaDataEndTag(bool metaDataEndFound)
    {
        if (!metaDataEndFound)
        {
            MessageRepository.Messages.Add(
                new Message("Couldn't find \"*END_METADATA*\".", Severity.Critical));
            return false;
        }
        else
            return true;
    }


    // not currently in use, might delete
    // Checks for *END_METADATA* that must exist at end of metadata section.
    // Returns true if found.
    // Note: This only checks that *END_METADATA* exists *somewhere* in the file.
    public static bool CheckForMetaDataEndTag(List<string[]> separatedLines)
    {
        foreach (var row in separatedLines)
        {
            if (row[0].Equals("*END_METADATA*"))
                return true;
        }

        MessageRepository.Messages.Add(
            new Message("Couldn't find \"*END_METADATA*\".", Severity.Critical));

        return false;
    }


    // Attributes must have value. Returns true if all metadata
    // rows has more than 2 columns.
    public static bool CheckAttributesForValue(List<string[]> metaData)
    {
        bool flag = true;

        // Checks if rows are more than 2 columns because values resides on column [2] to [n]
        foreach (var row in metaData)
        {
            if (row[0] == "*END_METADATA*")
                break;
            if (row.Length > 2)
                continue;
            else
            {
                MessageRepository.Messages.Add(
                    new Message($"Row {metaData.IndexOf(row) + 1}: Couldn't find values for attribute.", Severity.Critical));

                flag = false;
            }
        }

        return flag;
    }


    // Returns true if the *GLOBAL* "Conventions" attribute is first row.
    public static bool CheckGlobalConventions(List<string[]> metaData)
    {
        if (metaData[0][1].Equals("Conventions"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Row 1: Couldn't find global \"Conventions\" attribute.", Severity.NonCritical));
            return false;
        }
    }


    // Returns true if Global Conventions row include a reference to NCCSV.
    public static bool CheckNccsvVerification(List<string[]> metaData)
    {
        if (metaData[0][2].Contains("NCCSV"))
            return true;
        else
        {
            MessageRepository.Messages.Add(
                new Message("Row 1: Couldn't find reference to \"NCCSV\" in global conventions.", Severity.NonCritical));
            return false;
        }
    }
}