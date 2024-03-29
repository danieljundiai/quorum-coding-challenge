namespace quorum_data.tests;

using System.IO;
using Xunit;
using FluentAssertions;
using quorum_data.dataaccess;
using quorum_data.model;

public class VoteResultsDataAccessTests
{
    private readonly string testCsvPath = "csv\\TestVoteResults.csv";
    private VoteResultsDataAccess dataAccess;

    public VoteResultsDataAccessTests()
    {
        SetupTestData();
        this.dataAccess = new VoteResultsDataAccess(testCsvPath);
    }

    [Fact]
    public void GetAll_ShouldReturnAllVoteResults()
    {
        var result = dataAccess.GetAll();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void Insert_ShouldAddVoteResult()
    {
        var newVoteResult = new VoteResult { Id = 999, LegislatorId = 100, VoteId = 200, VoteType = 1 };
        dataAccess.Insert(newVoteResult);
        var result = dataAccess.GetAll();
        result.Should().ContainSingle(vr => vr.Id == newVoteResult.Id);
    }

    [Fact]
    public void Update_ShouldModifyExistingVoteResult()
    {
        var existingVoteResult = new VoteResult { Id = 999, LegislatorId = 100, VoteId = 200, VoteType = 1 };
        dataAccess.Insert(existingVoteResult);
        var updatedVoteResult = new VoteResult { Id = 999, LegislatorId = 101, VoteId = 201, VoteType = 2 };
        dataAccess.Update(updatedVoteResult);
        var result = dataAccess.GetAll();
        result.Should().ContainSingle(vr => vr.Id == updatedVoteResult.Id && vr.VoteType == updatedVoteResult.VoteType);
    }

    [Fact]
    public void Delete_ShouldRemoveVoteResult()
    {
        var voteResultToDelete = new VoteResult { Id = 999, LegislatorId = 100, VoteId = 200, VoteType = 1 };
        dataAccess.Insert(voteResultToDelete);
        dataAccess.Delete(voteResultToDelete.Id);
        var result = dataAccess.GetAll();
        result.Should().NotContain(vr => vr.Id == voteResultToDelete.Id);
    }

    private void SetupTestData()
    {
        File.Delete(testCsvPath);
        File.WriteAllText(testCsvPath, "Id,LegislatorId,VoteId,VoteType\n999,100,200,1\n");
    }
}
