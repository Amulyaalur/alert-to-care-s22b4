using System;
using System.Collections.Generic;
using AlertToCareAPI.Models;
using AlertToCareAPI.Database;
using AlertToCareAPI.Repositories.Field_Validators;

namespace AlertToCareAPI.Repositories
{
    public class PatientDbRepository : IPatientDbRepository
    {
        readonly DatabaseManager _creator=new DatabaseManager();
        readonly List<Patient> _patients; 
        readonly List<Bed> _beds;
        readonly PatientFieldsValidator _validator;


        public PatientDbRepository(DatabaseManager creator)
        {
           
           this._patients = _creator.GetPatientList();
           this._beds = _creator.GetBedsList();
           this._validator = new PatientFieldsValidator();
        }
        public void AddPatient(Patient newState)
        {
            _validator.ValidateNewPatientId(newState.PatientId, newState, _patients);
            _patients.Add(newState);
            _creator.UpdatePatient(_patients);
            ChangeBedStatus(newState.BedId, true);
        }
        public void RemovePatient(string patientId)
        {
            _validator.ValidateOldPatientId(patientId, _patients);
            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].PatientId == patientId)
                {
                    _patients.Remove(_patients[i]);
                    _creator.UpdatePatient(_patients);
                    ChangeBedStatus(_patients[i].BedId, false);
                    return;
                }
            }
        }
        public void UpdatePatient(string patientId, Patient state)
        {
            _validator.ValidateOldPatientId(patientId, _patients);
            _validator.ValidatePatientRecord(state);

            for (var i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].PatientId == patientId)
                {
                    _patients.Insert(i, state);
                    _creator.UpdatePatient(_patients);
                    return;
                }
            }
        }
        public IEnumerable<Patient> GetAllPatients()
        {
            return _patients;
        }
        public void ChangeBedStatus(string bedId, bool status)
        {
            for (var i = 0; i < _beds.Count; i++)
            {
                if (_beds[i].BedId == bedId)
                {
                    _beds[i].Status = status;
                    return;
                }
            }
            throw new Exception("Invalid data field");
        }
    }
}
