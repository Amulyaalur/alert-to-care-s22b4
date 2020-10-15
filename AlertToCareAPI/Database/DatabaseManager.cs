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
        private readonly List<Bed> _beds1 = new List<Bed>();
        private readonly List<Bed> _beds2 = new List<Bed>();
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
                    Bpm = 60,
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
                    Bpm = 190,
                    RespRate = 90
                }
            };
            _patients.Add(patient3);

            _beds1.Add(new Bed()
            {
                BedId = "BID1",
                Status = true,
                IcuId = "ICU01"
            });
            _beds1.Add(new Bed()
            {
                BedId = "BID2",
                Status = true,
                IcuId = "ICU01"
            });
            _beds1.Add(new Bed()
            {
                BedId = "BID3",
                Status = true,
                IcuId = "ICU01"
            });
            _beds1.Add(new Bed()
            {
                BedId = "BID4",
                Status = false,
                IcuId = "ICU01"
            });
            _beds1.Add(new Bed()
            {
                BedId = "BID5",
                Status = false,
                IcuId = "ICU01"
            });
            _beds1.Add(new Bed()
            {
                BedId = "BID6",
                Status = false,
                IcuId = "ICU01"
            });
            _beds1.Add(new Bed()
            {
                BedId = "BID7",
                Status = false,
                IcuId = "ICU01"
            });
            var icu = new Icu()
            {
                IcuId = "ICU01",
                LayoutId = "LID02",
                BedsCount = 7,
                Beds = _beds1
            };

            _icuList.Add(icu);

            _beds2.Add(new Bed()
            {
                BedId = "BID50",
                Status = false,
                IcuId = "ICU03"
            });
            _beds2.Add(new Bed()
            {
                BedId = "BID51",
                Status = false,
                IcuId = "ICU03"
            });
            _beds2.Add(new Bed()
            {
                BedId = "BID52",
                Status = false,
                IcuId = "ICU03"
            });

            _icuList.Add(new Icu()
            {
                IcuId =  "ICU03",
                LayoutId = "LID03",
                BedsCount = 3,
                Beds = _beds2
            });
            
            WriteToPatientsDatabase(_patients);
            WriteToIcuDatabase(_icuList);
        }

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
