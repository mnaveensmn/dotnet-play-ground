namespace algolia_exploration;

public class SearchModel
{
    public string ObjectID { get; set; }
    public string ShortCode { get; set; }
    public string ContentType { get; set; }

    public override string ToString()
    {
        return $"{ObjectID},{ShortCode},{ContentType}";
    }
}