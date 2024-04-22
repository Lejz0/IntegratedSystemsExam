using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.DTO
{
    public class CenterDTO
    {
        public List<Vaccine> vaccines {  get; set; }
        public VaccinationCenter center { get; set; }
    }
}
