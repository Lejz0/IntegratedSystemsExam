using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;

        private readonly IRepository<Vaccine> _vaccineRepository;

        public PatientService(IRepository<Patient> patientRepository, IRepository<Vaccine> vaccineRepository)
        {
            _patientRepository = patientRepository;
            _vaccineRepository = vaccineRepository;
        }

        public Patient CreateNewPatient(Patient patient)
        {
            return _patientRepository.Insert(patient);
        }

        public Patient DeletePatient(Guid id)
        {
            Patient patient = _patientRepository.Get(id);
            return _patientRepository.Delete(patient);
        }

        public Patient GetPatientById(Guid? id)
        {
            return _patientRepository.Get(id);
        }

        public List<Patient> GetPatients()
        {
            return _patientRepository.GetAll().ToList();
        }

        public List<Vaccine> GetVaccinesForPatient(Guid? id)
        {
            return _vaccineRepository.GetAll().Where(x => x.PatientId == id).ToList();
        }

        public Patient UpdatePatient(Patient patient)
        {
            return _patientRepository.Update(patient);
        }
    }
}
