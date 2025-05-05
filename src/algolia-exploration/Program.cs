// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using algolia_exploration;using Algolia.Search.Models.Common;

Console.WriteLine("Hello, World!");

var algoliaClient = new AlgoliaClient("show_qrmanager","8BBEN1F0M4","212135af1919360c0e8ae6aed9b5063e");

var browseIndexQuery = new BrowseIndexQuery()
{
    FacetFilters = [["groupId:AEA--23"],["location: \"-\""]],
    AttributesToRetrieve = new []{"shortCode"}
};

var taskWatch = new Stopwatch();
taskWatch.Restart();
var allRecords= await algoliaClient.GetAllRecordsAsync<SearchModel>(browseIndexQuery);

Console.WriteLine(allRecords.Count);
