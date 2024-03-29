using Microsoft.AspNetCore.Mvc;
using quorum_api.models;
using quorum_data.dataaccess;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class BillSummaryController : ControllerBase
{
    private readonly BillsDataAccess _billsDataAccess;
    private readonly LegislatorsDataAccess _legislatorsDataAccess;
    private readonly VoteResultsDataAccess _voteResultsDataAccess;
    private readonly VotesDataAccess _votesDataAccess;

    public BillSummaryController(BillsDataAccess billsDataAccess, LegislatorsDataAccess legislatorsDataAccess, VoteResultsDataAccess voteResultsDataAccess, VotesDataAccess votesDataAccess)
    {
        _billsDataAccess = billsDataAccess;
        _legislatorsDataAccess = legislatorsDataAccess;
        _voteResultsDataAccess = voteResultsDataAccess;
        _votesDataAccess = votesDataAccess;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BillSummary>> Get()
    {
        var bills = _billsDataAccess.GetAll();
        var legislators = _legislatorsDataAccess.GetAll();
        var voteResults = _voteResultsDataAccess.GetAll();
        var votes = _votesDataAccess.GetAll();

        var billSummaries = from bill in bills
                            join vote in votes on bill.Id equals vote.BillId into billVotes
                            from subVote in billVotes.DefaultIfEmpty()
                            let supporters = voteResults.Count(vr => vr.VoteId == subVote.Id && vr.VoteType == 1) // 1 para suporte
                            let opposers = voteResults.Count(vr => vr.VoteId == subVote.Id && vr.VoteType == 2) // 2 para oposição
                            select new BillSummary
                            {
                                ID = bill.Id,
                                Bill = bill.Title,
                                Supporters = supporters,
                                Opposers = opposers,
                                PrimarySponsor = legislators.FirstOrDefault(l => l.Id == bill.SponsorId)?.Name ?? "Unknown"
                            };
        return Ok(billSummaries);
    }
}
