namespace data_extractor;

public class FailedScenario
{
    public string Scenario { get; set; }

    public string Step { get; set; }

    public string FileName { get; set; }

    public List<string> BuildNumbers { get; set; }

    public override bool Equals(object obj)
    {
        return Equals(obj as FailedScenario);
    }

    public bool Equals(FailedScenario other)
    {
        return other != null &&
               Scenario == other.Scenario &&
               Step == other.Step &&
               FileName == other.FileName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Scenario, Step);
    }

}
