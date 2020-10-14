using System.Collections.Generic;
using System.IO;
using AlertToCareAPI.Models;
using Newtonsoft.Json;

namespace AlertToCareAPI.Database
{
    public class DatabaseManager
    {

        public DatabaseManager()
        {

            /*var patient1 = new Patient()
            {

                PatientId = "PID001",
                PatientName = "Anjali",
                Age = 25,
                ContactNo = "7348379805",
                BedId = "BID1",
                IcuId = "ICU01",
                Email = "anjali123@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "7",
                    Street = "Gyansu",
                    City = "Uttarkashi",
                    State = "Uttarakand",
                    Pincode = "249193"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID001",
                    Spo2 = 100,
                    Bpm = 70,
                    RespRate = 120
                }
            };
            _patients.Add(patient1);
            var patient2 = new Patient()
            {
                PatientId = "PID002",
                PatientName = "Varsha",
                Age = 23,
                ContactNo = "7493200389",
                BedId = "BID2",
                IcuId = "ICU01",
                Email = "varsha1234@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "8",
                    Street = "Gyansu",
                    City = "Uttarkashi",
                    State = "Uttarakand",
                    Pincode = "249193"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID002",
                    Spo2 = 56,
                    Bpm = 78,
                    RespRate = 10
                }
            };
            _patients.Add(patient2);

            var patient3 = new Patient()
            {
                PatientId = "PID003",
                PatientName = "Geetha",
                Age = 50,
                ContactNo = "9348938475",
                BedId = "BID3",
                IcuId = "ICU01",
                Email = "geetha@gmail.com",
                Address = new PatientAddress()
                {
                    HouseNo = "7",
                    Street = "Gyansu",
                    City = "Uttarkashi",
                    State = "Uttarakand",
                    Pincode = "249193"
                },
                Vitals = new Vitals()
                {
                    PatientId = "PID003",
                    Spo2 = 120,
                    Bpm = 100,
                    RespRate = 90
                }
            };
            var icu = new ICU()
            {
                IcuId = "ICU01",
                LayoutId = "LID02",
                BedsCount = 7,
                Patients = _patients
            };

            _icuList.Add(icu);
            _beds = new List<Bed>()
            {
                new Bed()
                {
                    BedId = "BID1",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed()
                {
                    BedId = "BID2",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed()
                {
                    BedId = "BID3",
                    Status = true,
                    IcuId = "ICU01"
                },
                new Bed()
                {
                    BedId = "BID4",
                    Status = false,
                    IcuId = "ICU01"
                },
                new Bed()
                {
                    BedId = "BID5",
                    Status = false,
                    IcuId = "ICU01"
                },
                new Bed()
                {
                    BedId = "BID6",
                    Status = false,
                    IcuId = "ICU01"
                },
                new Bed()
                {
                    BedId = "BID7",
                    Status = false,
                    IcuId = "ICU01"
                }
            };*/
        }

        public void WriteToPatientsDatabase(List<Patient> patients)
        {
            var writer = new StreamWriter("Patients.json");
            foreach (var patient in patients)
            {
                writer.WriteLine(patient);
            }
            writer.Close();
        }

        public void WriteToIcuDatabase(List<ICU> icuList)
        {
            var writer = new StreamWriter("Icu.json");
            foreach (var icu in icuList)
            {
                writer.WriteLine(icu);
            }
            writer.Close();
        }
        public void WriteToBedsDatabase(List<Bed> beds)
        {
            var writer = new StreamWriter("Beds.json");
            foreach (var bed in beds)
            {
                writer.WriteLine(bed);
            }
            writer.Close();
        }
        public void WriteToVitalsDatabase(List<Vitals> vitals)
        {
            var writer = new StreamWriter("Vitals.json");
            foreach (var record in vitals)
            {
                writer.WriteLine(record);
            }
            writer.Close();
        }

        public List<ICU> ReadIcuDatabase()
        {
            var reader = new StreamReader("Icu.json");
            var json = reader.ReadToEnd();
            var icuList= JsonConvert.DeserializeObject<List<ICU>>(json);
            reader.Close();
            return icuList;
        }
        public List<Patient> ReadPatientDatabase()
        {
            var reader = new StreamReader("Patients.json");
            var json = reader.ReadToEnd();
            var patients = JsonConvert.DeserializeObject<List<Patient>>(json);
            reader.Close();
            return patients;
        }

        public List<Vitals> ReadVitalsDatabase()
        {
            var reader = new StreamReader("Vitals.json");
            
            var json = reader.ReadToEnd();
            var vitals = JsonConvert.DeserializeObject<List<Vitals>>(json);
            reader.Close(); ;
            return vitals;
        }

        public List<Bed> ReadBedsDatabase()
        {
            var reader = new StreamReader("Beds.json");
            var json = reader.ReadToEnd();
            var beds = JsonConvert.DeserializeObject<List<Bed>>(json);
            reader.Close();
            return beds;
        }
    }
}
