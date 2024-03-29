using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using quorum_data.model;

namespace quorum_data.dataaccess 
{
    public class BillsDataAccess
    {
        private readonly string csvFilePath = "csv//bills.csv";

        public BillsDataAccess(string csvPath) {
            csvFilePath = csvPath;
        }
        public BillsDataAccess() {
        }

        public List<Bill> GetAll()
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Bill>().ToList();
            }
        }
        public Bill Get(int id)
        {
            var bills = GetAll();
            return bills.FirstOrDefault(l => l.Id == id);
        }        

        public void WriteData(IEnumerable<Bill> bills)
        {
            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                csv.WriteRecords(bills);
            }
        }

        public void Insert(Bill newBill)
        {
            var bills = GetAll();
            bills.Add(newBill); // Assuming no need to check for duplicates
            WriteData(bills);
        }

        public void Update(Bill updatedBill)
        {
            var bills = GetAll();
            var bill = bills.FirstOrDefault(b => b.Id == updatedBill.Id);
            if (bill != null)
            {
                bill.Title = updatedBill.Title;
                bill.SponsorId = updatedBill.SponsorId;
                WriteData(bills);
            }
        }

        public void Delete(int id)
        {
            var bills = GetAll();
            var bill = bills.FirstOrDefault(b => b.Id == id);
            if (bill != null)
            {
                bills.Remove(bill);
                WriteData(bills);
            }
        }
    }
}