/*using System.Collections.Generic;
using System.IO;
using AlertToCareAPI.Models;
using Newtonsoft.Json;

namespace AlertToCareAPI.Database
{
    public class DatabaseManager
    {

        public void WriteToPatientsDatabase(List<Patient> patients)
        {
            var fs = new FileStream("Patients.json", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var writer = new StreamWriter(fs);
            foreach (var patient in patients)
            {
                writer.WriteLine(JsonConvert.SerializeObject(patient));
            }
            writer.Dispose();
        }

        public void WriteToIcuDatabase(List<Icu> icuList)
        {
            var fs = new FileStream("Icu.json", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var writer = new StreamWriter(fs);
            foreach (var icu in icuList)
            {
                writer.WriteLine(JsonConvert.SerializeObject(icu));
            }
            writer.Dispose();
        }


        public List<Icu> ReadIcuDatabase()
        {
            var icuList = new List<Icu>();
            var fs = new FileStream("Icu.json", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var reader = new StreamReader(fs);
            while (reader.EndOfStream != true)
            {
                var line = reader.ReadLine();
                var icu = JsonConvert.DeserializeObject<Icu>(line);
                icuList.Add(icu);
            }

            reader.Dispose();
            return icuList;
        }
        public List<Patient> ReadPatientDatabase()
        {
            var patients = new List<Patient>();
            var fs = new FileStream("Patients.json", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var reader = new StreamReader(fs);
            while (reader.EndOfStream != true)
            {
                var line = reader.ReadLine();
                var patient = JsonConvert.DeserializeObject<Patient>(line);
                patients.Add(patient);
            }
            
            reader.Dispose();
            return patients;
        }

        public List<Vitals> ReadVitalsDatabase()
        {
            var patients = ReadPatientDatabase();
            var vitals = new List<Vitals>();
            foreach(var patient in patients)
            {
                vitals.Add(patient.Vitals);
            }
            return vitals;
        }

        public List<Bed> ReadBedsDatabase()
        {
            var icuList = ReadIcuDatabase();
            var beds = new List<Bed>();
            foreach (var icu in icuList)
            {
                foreach (var bed in icu.Beds)
                {
                    beds.Add(bed);
                }
            }
            return beds;
        }
    }
}
*/