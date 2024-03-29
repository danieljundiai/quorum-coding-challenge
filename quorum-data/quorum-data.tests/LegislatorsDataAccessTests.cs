namespace quorum_data.tests;

using System.IO;
using Xunit;
using FluentAssertions;
using quorum_data.dataaccess;
using quorum_data.model;

public class LegislatorsDataAccessTests
{
    private readonly string testCsvPath = "csv\\TestLegislators.csv";
    private LegislatorsDataAccess dataAccess;

    public LegislatorsDataAccessTests()
    {
        SetupTestData();
        this.dataAccess = new LegislatorsDataAccess(testCsvPath);
    }

    [Fact]
    public void GetAll_ShouldReturnAllLegislators()
    {
        var result = dataAccess.GetAll();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void Insert_ShouldAddLegislator()
    {
        var newLegislator = new Legislator { Id = 999, Name = "Test Legislator" };
        dataAccess.Insert(newLegislator);
        var result = dataAccess.GetAll();
        result.Should().ContainSingle(l => l.Id == newLegislator.Id);
    }

    [Fact]
    public void Update_ShouldModifyExistingLegislator()
    {
        var existingLegislator = new Legislator { Id = 999, Name = "Test Legislator" };
        dataAccess.Insert(existingLegislator);
        var updatedLegislator = new Legislator { Id = 999, Name = "Updated Legislator Name" };
        dataAccess.Update(updatedLegislator);
        var result = dataAccess.GetAll();
        result.Should().ContainSingle(l => l.Id == updatedLegislator.Id && l.Name == updatedLegislator.Name);
    }

    [Fact]
    public void Delete_ShouldRemoveLegislator()
    {
        var legislatorToDelete = new Legislator { Id = 999, Name = "Legislator to Delete" };
        dataAccess.Insert(legislatorToDelete);
        dataAccess.Delete(legislatorToDelete.Id);
        var result = dataAccess.GetAll();
        result.Should().NotContain(l => l.Id == legislatorToDelete.Id);
    }

    private void SetupTestData()
    {
        File.Delete(testCsvPath);
        File.WriteAllText(testCsvPath, "Id,Name\n999,Test Legislator\n");
    }
}
