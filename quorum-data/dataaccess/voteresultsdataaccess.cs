using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using quorum_data.model;

namespace quorum_data.dataaccess 
{
    public class VoteResultsDataAccess
    {
        private string csvFilePath = "csv//vote_results.csv";

        public VoteResultsDataAccess(string csvPath) {
            csvFilePath = csvPath;
        }
        public VoteResultsDataAccess() {
        }

        public List<VoteResult> GetAll()
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<VoteResult>().ToList();
            }
        }

        public void WriteData(IEnumerable<VoteResult> voteResults)
        {
            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                csv.WriteRecords(voteResults);
            }
        }

        public void Insert(VoteResult voteResult)
        {
            var voteResults = GetAll();
            if (!voteResults.Any(vr => vr.Id == voteResult.Id))
            {
                voteResults.Add(voteResult);
                WriteData(voteResults);
            }
        }

        public void Update(VoteResult updatedVoteResult)
        {
            var voteResults = GetAll();
            var voteResult = voteResults.FirstOrDefault(vr => vr.Id == updatedVoteResult.Id);
            if (voteResult != null)
            {
                voteResult.LegislatorId = updatedVoteResult.LegislatorId;
                voteResult.VoteId = updatedVoteResult.VoteId;
                voteResult.VoteType = updatedVoteResult.VoteType;
                WriteData(voteResults);
            }
        }

        public void Delete(int id)
        {
            var voteResults = GetAll();
            var voteResult = voteResults.FirstOrDefault(vr => vr.Id == id);
            if (voteResult != null)
            {
                voteResults.Remove(voteResult);
                WriteData(voteResults);
            }
        }
    }
}