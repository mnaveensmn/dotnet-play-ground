using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace csharp_foundation;

public class ProductData
{
    public QrContentData QrContent { get; set; } = new QrContentData();
}

public class QrContentData
{
    public string ProductName { get; set; }
}

public class ShowConfig
{
    public bool IsEnabled { get; set; } = false;
}

public class JsonPatchExploration
{
    public static void Explore()
    {
        // Sample object that needs to be patched
        var productData = new ProductData
        {
            QrContent = new QrContentData { ProductName = "Old Product" }
        };

        // Create JsonPatchDocument and define the replacement
        var patchDoc = new JsonPatchDocument<ProductData>();
        patchDoc.Replace(p => p.QrContent.ProductName, "New Product");

        Console.WriteLine(JsonSerializer.Serialize(patchDoc));
        // Apply patch
        patchDoc.ApplyTo(productData);

        // Output result
        Console.WriteLine($"Updated Product Name: {productData.QrContent.ProductName}");

        ShowConfig showConfig = null;
        if (showConfig is { IsEnabled: true })
        {
            Console.WriteLine("ShowConfig is enabled");
        }
        else
        {
            Console.WriteLine("ShowConfig is not enabled");
        }
    }
}