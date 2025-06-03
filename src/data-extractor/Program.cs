// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using data_extractor;
using data_extractor.Extractors;


var extractor = new DataExtractor();
extractor.Extract();

//Utils.ListFilesNames();