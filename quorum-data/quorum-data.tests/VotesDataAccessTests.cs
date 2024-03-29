using FluentAssertions;
using quorum_data.dataaccess;
using quorum_data.model;

namespace quorum_data.tests;

    public class VotesDataAccessTests
    {
        private readonly string testCsvPath = "csv//TestVotes.csv";
        private VotesDataAccess dataAccess;

        public VotesDataAccessTests()
        {
            SetupTestData();
            this.dataAccess = new VotesDataAccess(testCsvPath);
        }

        [Fact]
        public void GetAll_ShouldReturnAllVotes()
        {
            var result = dataAccess.GetAll();

            result.Should().NotBeEmpty();
        }

        [Fact]
        public void Insert_ShouldAddVote()
        {
            var newVote = new Vote { Id = 999, BillId = 888 };

            dataAccess.Insert(newVote);
            var result = dataAccess.GetAll();

            result.Should().ContainSingle(v => v.Id == newVote.Id && v.BillId == newVote.BillId);
        }

        [Fact]
        public void Update_ShouldModifyExistingVote()
        {
            var existingVote = new Vote { Id = 999, BillId = 888 };
            dataAccess.Insert(existingVote);
            var updatedVote = new Vote { Id = 999, BillId = 777 }; // Modificando BillId

            dataAccess.Update(updatedVote);
            var result = dataAccess.GetAll();

            result.Should().ContainSingle(v => v.Id == updatedVote.Id && v.BillId == updatedVote.BillId);
        }

        [Fact]
        public void Delete_ShouldRemoveVote()
        {
            var voteToDelete = new Vote { Id = 999, BillId = 888 };
            dataAccess.Insert(voteToDelete);

            dataAccess.Delete(voteToDelete.Id);
            var result = dataAccess.GetAll();

            result.Should().NotContain(v => v.Id == voteToDelete.Id);
        }

        private void SetupTestData()
        {
            File.Delete(testCsvPath);
            File.WriteAllText(testCsvPath, "Id,BillId\n999,888\n");
        }
    }