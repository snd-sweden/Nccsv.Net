# nccsv_netcdf

## Introduction

This is a class library for the verification and conversion of [NCCSV files](https://coastwatch.pfeg.noaa.gov/erddap/download/NCCSV.html).   

It's purpose is to be integrated into other applications to parse NCCSV-files, as well as convert them to other useful formats.  

Currently, we are working on making a serializer for JSON with [Schema.org Dataset](https://schema.org/Dataset) formatting

## How To Use

### NCCSV-parser
You can import a NCCSV file, either as a text file with the .nccsv or .nc suffix, or take it as a stream.

1. Import the NccsvParser-project as a using.
2. Use either of the static methods FromFile or FromStream to create a DataSet object.

If there are any problems with the file (such as illegal whitespaces, data without an assigned variable, etc) this will also generate an error list in the MessageRepository, which can be printed as error message.


### Schema.org JSON-serializer
When the DataSet is generated, and contains no errors, you can convert it into a JSON formatted string according to Schema.org standard for metadata.

In the NccsvConverter.NccsvSerializer-project, you will find the "SchemaDatasetJsonSerializer"-folder, which contains a Serializer class. 
You ca just call the static method ToJson from that class, and give your DataSet as a parameter, and it will return the DataSet as a string with the formatting described above.

### Console Application
Included in the project is also a basic console application, with an example of how to use the classes, along with some example .nccsv-files.
