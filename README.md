# nccsv_netcdf

## Introduction

This is a class library for the verification and conversion of [NCCSV files](https://coastwatch.pfeg.noaa.gov/erddap/download/NCCSV.html).   

It's purpose is to be integrated into other applications to parse NCCSV-files, as well as convert them to other useful formats.  

Currently, we are working on making a serializer for JSON with [Schema.org Dataset](https://schema.org/Dataset) formatting

## How To Use
You can import a NCCSV file, either as a text file with the .nccsv or .nc suffix, or take it as a stream.

1. Import the NccsvParser-project as a using.
2. Use either of the static methods FromFile or FromStream to create a DataSet object.

This will also generate an error list in the MessageRepository, which can be acessed and printed as error message.

Included in the project is also a basic console application, with an example of how to use the classes, along with some example .nccsv-files.
