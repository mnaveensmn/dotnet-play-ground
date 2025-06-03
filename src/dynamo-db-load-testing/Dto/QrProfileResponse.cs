namespace dynamo_db_load_testing.Dto;

public class QrProfileResponse
{
    public required string GroupId { get; set; }

    public required string ShortCode { get; set; }

    public required string GroupType { get; set; }
    public required string QrStatus { get; set; }
    public required string OwnerStatus { get; set; } 
    public required MetaDataResponse MetaData { get; set; }

    public DataResponse? Data { get; set; }
}
public class QrFileResponse
{
    public required string Type { get; set; }

    public string? Location { get; set; }

    public required string Format { get; set; }

}
public class MetaDataResponse
{
    public required string DownloadStatus { get; set; }

    public QrDisplayLocationResponse? QrDisplayLocation { get; set; }

    public CustomContentResponse? CustomContent { get; set; }

    public required string SourceName { get; set; }

}
public class CustomContentResponse
{
    public string? CustomUrl { get; set; }

    public string? CustomUrlName { get; set; }
}
public class QrDisplayLocationResponse
{
    public required string Type { get; init; }
    public string? Value { get; init; }
}
public class DataResponse
{
    public required string QrType { get; set; }

    public required QrTextResponse QrText { get; set; }

    public required QrContentResponse QrContent { get; set; }

}
public class QrTextResponse
{
    public string? Url { get; set; }
}

public class QrContentResponse
{
    public required string Type { get; set; }

    public string? ExhibitorId { get; set; }

    public string? ProductId { get; set; }
}