using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccinationCenterService
    {
        List<VaccinationCenter> GetAllVaccinationCenter();
        VaccinationCenter GetDetailsForVaccinationCenter(Guid? id);
        VaccinationCenter CreateNewVaccinationCenter(VaccinationCenter p);
        VaccinationCenter UpdeteExistingCenter(VaccinationCenter p);
        VaccinationCenter DeleteCenter(Guid id);
        List<Vaccine> GetVaccinesForCenter(Guid? id);
        bool addPacientToCenter(VaccinationDTO p);

        void LowerCapacity(Guid id);
      
    }
}
