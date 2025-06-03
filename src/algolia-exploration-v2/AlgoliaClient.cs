using System.Diagnostics;
using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace algolia_exploration_v2;

public class AlgoliaClient(string indexName, string appId, string apiKey)
{
    private readonly SearchClient _client = new(appId, apiKey);
    private const string _bucketName = "perf-qr-manager-qr-files";
    
    public async Task FetchAndUpdateObjects(string gbsCode)
    {
            var browseParams = new BrowseParamsObject()
            {
                FacetFilters = new FacetFilters($"groupId: {gbsCode}"),
                TypoTolerance = new TypoTolerance(false)
            };
        
        var browseResult =
            (await _client.BrowseObjectsAsync<SearchModel>(indexName, browseParams)).ToList();
        Console.WriteLine($"Fetched {browseResult.Count} records");

        var updateSearchModels = browseResult.Select(x => new
        {
            x.ObjectID,
            Location = "NO_LOCATION"
        }).ToList();

        await _client.PartialUpdateObjectsAsync(indexName, updateSearchModels, false);

        Console.WriteLine("Update Successful");
    }

    public async Task UpdateObjects()
    {
        List<string> browseResult = ["test11-TWFKBR"];

        var updateSearchModels = browseResult.Select(x => new
        {
            ObjectID = x,
            Location = "Other Location Test"
        }).ToList();

        await _client.PartialUpdateObjectsAsync(indexName, updateSearchModels, false);

        Console.WriteLine("Update Successful");
    }

    public async Task<List<T>> FetchAlgoliaRecords<T>(string gbsCode)
    {
        var browseParams = new BrowseParamsObject()
        {
            FacetFilters = new FacetFilters($"groupId: {gbsCode}"),
            TypoTolerance = new TypoTolerance(false)
        };
        
        return (await _client.BrowseObjectsAsync<T>(indexName, browseParams)).ToList();
    }

    public async Task CleanUpRecords(string gbsCode)
    {
        var browseResult = await FetchAlgoliaRecords<SearchModel>(gbsCode);
        var objectIds = browseResult.Select(x => x.ObjectID).ToList();
        await _client.DeleteObjectsAsync(indexName, objectIds);

        Console.WriteLine($"Data cleaned up for {gbsCode}");
    }
    
    public async Task UploadQrSticker(string gbsCode)
    {
        var qrStickerData = await File.ReadAllBytesAsync("testdata/QrSticker.pdf");

        var result = await FetchAlgoliaRecords<SearchModel>(gbsCode);
        var shortCodes = result.Select(x => x.ShortCode).ToList();
        foreach (var shortCode in shortCodes)
        {
            Console.WriteLine($"Processing {shortCode} from {gbsCode}");
            var key = $"qr-codes/colleqt/{gbsCode}/stickers/{shortCode}.pdf";
            await UploadToS3(key, qrStickerData);
        }
    }

    private async Task UploadToS3(string keyName, byte[] csvData)
    {
        var credentials = FallbackCredentialsFactory.GetCredentials();
        var s3Client = new AmazonS3Client(credentials, RegionEndpoint.EUWest1);

        try
        {
            using var stream = new MemoryStream(csvData);
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName,
                InputStream = stream,
                ContentType = "application/pdf"
            };

            var response = await s3Client.PutObjectAsync(putRequest);
            Console.WriteLine($"Successfully uploaded to {_bucketName}/{keyName}");
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Error encountered on server. Message:'{e.Message}'");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unknown error encountered. Message:'{e.Message}'");
        }
    }
}