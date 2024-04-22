using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _centerRepository;

        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Vaccine> _vaccineRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> centerRepository, IRepository<Patient> patientRepository, IRepository<Vaccine> vaccineRepository)
        {
            _centerRepository = centerRepository;
            _patientRepository = patientRepository;
            _vaccineRepository = vaccineRepository;
        }

        public bool addPacientToCenter(VaccinationDTO p)
        {
            Vaccine vaccine = new Vaccine();
            vaccine.Manufacturer = p.manufacturer;
            vaccine.Certificate = Guid.NewGuid();
            vaccine.VaccinationCenter = p.vaccCenterId;
            vaccine.PatientId = p.patientId;
            vaccine.DateTaken = p.vaccinationDate;
            vaccine.Center = _centerRepository.Get(p.vaccCenterId);
            _vaccineRepository.Insert(vaccine);
            return true;


        }

        public VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter p)
        {
            return _centerRepository.Insert(p);
        }

        public VaccinationCenter DeleteCenter(Guid id)
        {
            VaccinationCenter center = _centerRepository.Get(id);
            return _centerRepository.Delete(center);
        }

        public List<VaccinationCenter> GetAllVaccinationCenter()
        {
            return _centerRepository.GetAll().ToList();
        }

        public VaccinationCenter GetDetailsForVaccinationCenter(Guid? id)
        {
            return _centerRepository.Get(id);
        }

        public List<Vaccine> GetVaccinesForCenter(Guid? id)
        {
            return _vaccineRepository.GetAll().Where(x => x.VaccinationCenter == id).ToList();
        }

        public void LowerCapacity(Guid id)
        {
            var center = _centerRepository.Get(id);
            center.MaxCapacity--;
            _centerRepository.Update(center);
        }

        public VaccinationCenter UpdeteExistingCenter(VaccinationCenter p)
        {
            return _centerRepository.Update(p);
        }
    }
}
