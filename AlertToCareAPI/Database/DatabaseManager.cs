using System.Collections.Generic;
using System.IO;
using AlertToCareAPI.Models;

namespace AlertToCareAPI.Database
{
    public class DatabaseManager
    {
        List<ICU> _icuList = new List<ICU>();
        List<Patient> _patients = new List<Patient>();
        List<Bed> _beds = new List<Bed>();
        List<Vitals> _vitals = new List<Vitals>();

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

        public void WriteToPatientsDatabase(List<Patient> patients, StreamWriter writer)
        {
            foreach (var patient in patients)
            {
                writer.WriteLine(patient);
            }
        }

        public void WriteToIcuDatabase(List<ICU> icuList, StreamWriter writer)
        {
            foreach (var icu in icuList)
            {
                writer.WriteLine(icu);
            }
        }
        public void WriteToBedsDatabase(List<Bed> beds, StreamWriter writer)
        {
            foreach (var bed in beds)
            {
                writer.WriteLine(bed);
            }
        }
        public void WriteToVitalsDatabase(List<Vitals> vitals, StreamWriter writer)
        {
            foreach (var record in vitals)
            {
                writer.WriteLine(record);
            }
        }

        public List<ICU> GetIcuList()
        {
            return _icuList;
        }
        public List<Patient> GetPatientList()
        {
            return _patients;
        }

        public List<Vitals> GetVitalsList()
        {
            foreach (var patient in _patients)
            {
                _vitals.Add(patient.Vitals);
            }

            return _vitals;
        }

        public List<Bed> GetBedsList()
        {
            return _beds;
        }

        public void UpdatePatient(List<Patient> patients)
        {
            _patients = patients;
        }

        public void UpdateIcuList(List<ICU> icuList)
        {
            _icuList = icuList;
        }

        public void UpdateBedsList(List<Bed> beds)
        {
            _beds = beds;
        }

        public void UpdateVitalsList(List<Vitals> vitals)
        {
            _vitals = vitals;
        }
    }
}
