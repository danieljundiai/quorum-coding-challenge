using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using quorum_data.model;


namespace quorum_data.dataaccess 
{
    public class VotesDataAccess
    {
        private string csvFilePath = "csv//votes.csv";

        public VotesDataAccess(string csvPath) {
            csvFilePath = csvPath;
        }
        public VotesDataAccess() {
        }


        public List<Vote> GetAll()
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Vote>().ToList();
            }
        }

        public Vote Get(int id)
        {
            return GetAll().FirstOrDefault(v => v.Id == id);
        }

        public void WriteData(IEnumerable<Vote> votes)
        {
            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                csv.WriteRecords(votes);
            }
        }

        public void Insert(Vote vote)
        {
            var votes = GetAll();
            if (!votes.Any(v => v.Id == vote.Id))
            {
                votes.Add(vote);
                WriteData(votes);
            }
        }

        public void Update(Vote updatedVote)
        {
            var votes = GetAll();
            var vote = votes.FirstOrDefault(v => v.Id == updatedVote.Id);
            if (vote != null)
            {
                vote.BillId = updatedVote.BillId;
                WriteData(votes);
            }
        }

        public void Delete(int id)
        {
            var votes = GetAll();
            var vote = votes.FirstOrDefault(v => v.Id == id);
            if (vote != null)
            {
                votes.Remove(vote);
                WriteData(votes);
            }
        }
    }
}