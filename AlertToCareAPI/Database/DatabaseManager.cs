using System.Collections.Generic;
using System.IO;
using AlertToCareAPI.Models;
using Newtonsoft.Json;

namespace AlertToCareAPI.Database
{
    public class DatabaseManager
    {
        private readonly List<Patient> _patients=new List<Patient>();
        private readonly List<Icu> _icuList= new List<Icu>();
        private readonly List<Bed> _beds = new List<Bed>();
        public DatabaseManager()
        {

            var patient1 = new Patient()
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
            _patients.Add(patient3);
            var icu = new Icu()
            {
                IcuId = "ICU01",
                LayoutId = "LID02",
                BedsCount = 7,
                Patients = _patients
            };

            _icuList.Add(icu);
            
            _beds.Add(new Bed()
            {
                BedId = "BID1",
                Status = true,
                IcuId = "ICU01"
            });
            _beds.Add(new Bed()
            {
                BedId = "BID2",
                Status = true,
                IcuId = "ICU01"
            });
            _beds.Add(new Bed()
            {
                BedId = "BID3",
                Status = true,
                IcuId = "ICU01"
            });
            _beds.Add(new Bed()
            {
                BedId = "BID4",
                Status = false,
                IcuId = "ICU01"
            });
            _beds.Add(new Bed()
            {
                BedId = "BID5",
                Status = false,
                IcuId = "ICU01"
            });
            _beds.Add(new Bed()
            {
                BedId = "BID6",
                Status = false,
                IcuId = "ICU01"
            });
            _beds.Add(new Bed()
            {
                BedId = "BID7",
                Status = false,
                IcuId = "ICU01"
            });
            
            WriteToPatientsDatabase(_patients);
            WriteToIcuDatabase(_icuList);
            WriteToBedsDatabase(_beds);
        }

        public void WriteToPatientsDatabase(List<Patient> patients)
        {
            var writer = new StreamWriter("Patients.json");
            foreach (var patient in patients)
            {
                writer.WriteLine(JsonConvert.SerializeObject(patient));
            }
            writer.Close();
        }

        public void WriteToIcuDatabase(List<Icu> icuList)
        {
            var writer = new StreamWriter("Icu.json");
            foreach (var icu in icuList)
            {
                writer.WriteLine(JsonConvert.SerializeObject(icu));
            }
            writer.Close();
        }
        public void WriteToBedsDatabase(List<Bed> beds)
        {
            var writer = new StreamWriter("Beds.json");
            foreach (var bed in beds)
            {
                writer.WriteLine(JsonConvert.SerializeObject(bed));
            }
            writer.Close();
        }
        

        public List<Icu> ReadIcuDatabase()
        {
            var icuList = new List<Icu>();
            var reader = new StreamReader("Icu.json");
            while (reader.EndOfStream != true)
            {
                var line = reader.ReadLine();
                var icu = JsonConvert.DeserializeObject<Icu>(line);
                icuList.Add(icu);
            }

            reader.Close();
            return icuList;
        }
        public List<Patient> ReadPatientDatabase()
        {
            var patients = new List<Patient>();
            var reader = new StreamReader("Patients.json");
            while (reader.EndOfStream != true)
            {
                var line = reader.ReadLine();
                var patient = JsonConvert.DeserializeObject<Patient>(line);
                patients.Add(patient);
            }
            
            reader.Close();
            return patients;
        }

        public List<Vitals> ReadVitalsDatabase()
        {
            var reader = new StreamReader("Patients.json");
            var patients = new List<Patient>();
            while (reader.EndOfStream != true)
            {
                var line = reader.ReadLine();
                var patient = JsonConvert.DeserializeObject<Patient>(line);
                patients.Add(patient);
            }
            var vitals = new List<Vitals>();
            foreach(var patient in patients)
            {
                vitals.Add(patient.Vitals);
            }
            reader.Close(); 
            return vitals;
        }

        public List<Bed> ReadBedsDatabase()
        {
            var beds = new List<Bed>();
            var reader = new StreamReader("Beds.json");
            while (reader.EndOfStream != true)
            {
                var line = reader.ReadLine();
                var bed = JsonConvert.DeserializeObject<Bed>(line);
                beds.Add(bed);
            }

            reader.Close();
            return beds;
        }
    }
}
