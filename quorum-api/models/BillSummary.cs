namespace quorum_api.models;

public class BillSummary
{
    public int ID { get; set; }
    public string Bill { get; set; }
    public int Supporters { get; set; }
    public int Opposers { get; set; }
    public string PrimarySponsor { get; set; }
}
