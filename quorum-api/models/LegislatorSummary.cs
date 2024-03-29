namespace quorum_api.models;

public class LegislatorSummary
{
    public int Id { get; set; }
    public string Legislator { get; set; }
    public int SupportedBills { get; set; }
    public int OpposedBills { get; set; }
}
