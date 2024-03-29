namespace quorum_api.controllers;

using Microsoft.AspNetCore.Mvc;
using quorum_api.models;
using quorum_data.dataaccess;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class LegislatorsSummaryController : ControllerBase
{
    private readonly VotesDataAccess _votesDataAccess;
    private readonly VoteResultsDataAccess _voteResultsDataAccess;
    private readonly LegislatorsDataAccess _legislatorsDataAccess;

    public LegislatorsSummaryController(VotesDataAccess votesDataAccess, VoteResultsDataAccess voteResultsDataAccess, LegislatorsDataAccess legislatorsDataAccess)
    {
        _votesDataAccess = votesDataAccess;
        _voteResultsDataAccess = voteResultsDataAccess;
        _legislatorsDataAccess = legislatorsDataAccess;
    }

    [HttpGet]
    public ActionResult<IEnumerable<LegislatorSummary>> Get()
    {
        var legislators = _legislatorsDataAccess.GetAll();
        var voteResults = _voteResultsDataAccess.GetAll();

        var summary = legislators.Select(l => new LegislatorSummary
        {
            Id = l.Id,
            Legislator = l.Name,
            SupportedBills = voteResults.Count(v => v.LegislatorId == l.Id && v.VoteType == 1), 
            OpposedBills = voteResults.Count(v => v.LegislatorId == l.Id && v.VoteType == 2) 
        }).ToList();

        return Ok(summary);
    }
}
