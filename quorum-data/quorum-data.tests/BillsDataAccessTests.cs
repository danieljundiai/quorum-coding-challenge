namespace quorum_data.tests;

using System.IO;
using Xunit;
using FluentAssertions;
using quorum_data.dataaccess;
using quorum_data.model;

public class BillsDataAccessTests
{
    private readonly string testCsvPath = "csv\\TestBills.csv";
    private BillsDataAccess dataAccess;

    public BillsDataAccessTests()
    {
        SetupTestData();
        this.dataAccess = new BillsDataAccess(testCsvPath);
    }

    [Fact]
    public void GetAll_ShouldReturnAllBills()
    {
        var result = dataAccess.GetAll();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void Insert_ShouldAddBill()
    {
        var newBill = new Bill { Id = 999, Title = "New Bill for Testing", SponsorId = 100 };
        dataAccess.Insert(newBill);
        var result = dataAccess.GetAll();
        result.Should().Contain(b => b.Id == newBill.Id);
    }

    [Fact]
    public void Update_ShouldModifyExistingBill()
    {
        var existingBill = new Bill { Id = 999, Title = "New Bill for Testing", SponsorId = 100 };
        dataAccess.Insert(existingBill);
        var updatedBill = new Bill { Id = 999, Title = "Updated Bill Title", SponsorId = 101 };
        dataAccess.Update(updatedBill);
        var result = dataAccess.GetAll();
        result.Should().ContainSingle(b => b.Id == updatedBill.Id && b.Title == updatedBill.Title);
    }

    [Fact]
    public void Delete_ShouldRemoveBill()
    {
        var billToDelete = new Bill { Id = 998, Title = "Bill to Delete", SponsorId = 100 };
        dataAccess.Insert(billToDelete);
        dataAccess.Delete(billToDelete.Id);
        var result = dataAccess.GetAll();
        result.Should().NotContain(b => b.Id == billToDelete.Id);
    }

    private void SetupTestData()
    {
        File.Delete(testCsvPath);
        File.WriteAllText(testCsvPath, "Id,Title,SponsorId\n999,New Bill for Testing,100\n");
    }
}