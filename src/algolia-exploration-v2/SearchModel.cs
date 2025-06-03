namespace algolia_exploration_v2;

public class SearchModel
{
    public string ObjectID { get; set; }
    public string GroupId { get; set; }
    public string ShortCode { get; set; }

    public override string ToString()
    {
        return $"{GroupId},{ShortCode}";
    }
}