# nccsv-parser

* Validates nccsv accroding to [the specification below](#validation-parameters)
* Parse header and data to objects and arrays
* Supports read by filepath or stream
* Outputs metadata as a [schema.org](https://schema.org)  JSON-LD string

## Introduction

This is a class library for the verification and conversion of [NCCSV files](https://coastwatch.pfeg.noaa.gov/erddap/download/NCCSV.html).   

It's purpose is to be integrated into other applications to parse NCCSV-files, as well as convert them to other useful formats.  

## How To Use

### NCCSV-parser
You can import a NCCSV file, either as a text file with the .nccsv suffix, or take it as a stream.

1. Import the NccsvParser-project as a using.
2. Use either of the static methods FromFile or FromStream to create a DataSet object.

If there are any problems with the file (such as illegal whitespaces, data without an assigned variable, etc) this will also generate an error list in the MessageRepository, which can be printed as error message.


### Schema.org JSON-serializer
When the DataSet is generated, and contains no errors, you can convert it into a JSON-LD formatted string according to Schema.org standard for metadata.

In the NccsvConverter.NccsvSerializer-project, you will find the "SchemaDatasetJsonSerializer"-folder, which contains a Serializer class. 
You ca just call the static method ToJson from that class, and give your DataSet as a parameter, and it will return the DataSet as a string with the formatting described above.

### Console Application
Included in the project is also a basic console application, with an example of how to use the classes, along with some example .nccsv-files.

Below, you can see the code for the application, where you also can see the simplest implementation of the library:

```cs
Console.WriteLine("If you want to validate, write name of file:");
string filePathFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName
                        + "\\NccsvConverter.ConsoleApp\\TestData\\";
string fileName = Console.ReadLine();
string filePath = Path.Combine(filePathFolder, fileName);

if (File.Exists(filePath))
{
    //create a DataSet
    DataSet dataSet = DataSet.FromFile(filePath, true);

    if (MessageRepository.Messages.Count > 0)
    {
        foreach (var message in MessageRepository.Messages)
        {
            Console.WriteLine(message.Text);
        }
    }
    else
    {
        Console.WriteLine("No problems found.");
        
        Console.WriteLine("If you want to serialize, press y:");
        var input = Console.ReadKey();
        Console.WriteLine();
        if (input.KeyChar == 'y')
            //Serializing the DataSet to JSON-LD:
            Console.WriteLine(Serializer.ToJson(dataSet));
    }
}
else
    Console.WriteLine($"File \"{filePath}\" could not be found.");

```
## Validation parameters
Currently, the code is validating the following parameters, as described in the [NCCSV documentation](https://coastwatch.pfeg.noaa.gov/erddap/download/NCCSV.html):

### File parameters:
* File has .nccsv-suffix

### Metadata parameters:
* File has Global Attributes
* Global Attributes includes conventions
* The conventions include NCCSV 
* All attributes include value
* There is an \*END_METADATA\*-tag
* All variables have names
* All variables have an assigned data type
  * *Note: Scalar variables does not need explicit data type, as per the documentation*
* That the data type specified is of one of the 
* All attributes of variables have names

### Data parameters:
* That there is a header row for the data
* that the section ends with \*END_DATA\*
* Each row of data corresponds to the specified headers
* That data values does not start or end with whitespace

## Authors
* Ida Borén, [idaboren](https://github.com/idaboren)
* Arvid Leimar, [pinkros](https://github.com/pinkros)
