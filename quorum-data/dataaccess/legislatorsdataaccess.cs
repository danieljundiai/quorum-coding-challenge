using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using quorum_data.model;

namespace quorum_data.dataaccess 
{
    public class LegislatorsDataAccess
    {
        private string csvFilePath = "csv//legislators.csv";

        public LegislatorsDataAccess(string csvPath) {
            csvFilePath = csvPath;
        }
        public LegislatorsDataAccess() {
        }

        public List<Legislator> GetAll()
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Legislator>().ToList();
                return records;
            }
        }

        public Legislator Get(int id)
        {
            var legislators = GetAll();
            return legislators.FirstOrDefault(l => l.Id == id);
        }

        // Assumindo que você quer sobrescrever o arquivo inteiro após uma operação de update ou insert.
        // Se o desempenho for uma preocupação, considere outras abordagens.
        public void WriteData(IEnumerable<Legislator> legislators)
        {
            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                csv.WriteRecords(legislators);
            }
        }

        public void Insert(Legislator newLegislator)
        {
            var legislators = GetAll();
            if (!legislators.Any(l => l.Id == newLegislator.Id))
            {
                legislators.Add(newLegislator);
                WriteData(legislators);
            }
        }

        public void Update(Legislator updatedLegislator)
        {
            var legislators = GetAll();
            var legislator = legislators.FirstOrDefault(l => l.Id == updatedLegislator.Id);
            if (legislator != null)
            {
                legislator.Name = updatedLegislator.Name;
                WriteData(legislators);
            }
        }

        public void Delete(int id)
        {
            var legislators = GetAll();
            var legislator = legislators.FirstOrDefault(l => l.Id == id);
            if (legislator != null)
            {
                legislators.Remove(legislator);
                WriteData(legislators);
            }
        }
    }
}