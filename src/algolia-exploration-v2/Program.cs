// See https://aka.ms/new-console-template for more information

using algolia_exploration_v2;

var algoliaClient = new AlgoliaClient("show_qrmanager", "", "");

await algoliaClient.FetchAndUpdateObjects("ATM--00");

//await algoliaClient.UpdateObjects();

// foreach (var x in Enumerable.Range(1,200))
// {
//     await algoliaClient.CleanUpRecords($"PERF-LOAD-TEST-{x}");
// }

// foreach (var x in Enumerable.Range(1,201))
// {
//     await algoliaClient.UploadQrSticker($"PERF-LOAD-TEST-{x}");
// }


// foreach (var x in Enumerable.Range(1,201))
// {
//     var list = await algoliaClient.FetchAlgoliaRecords<SearchModel>($"PERF-LOAD-TEST-{x}");
//     list.ForEach(x=>Console.WriteLine(x.ToString()));
// }