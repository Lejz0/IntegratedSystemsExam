using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;
using IntegratedSystems.Domain.DTO;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVaccinationCenterService vaccinationCenterService;
        private readonly IPatientService patientService;

        public VaccinationCentersController(IVaccinationCenterService vaccinationCenterService, IPatientService patientService)
        {
            this.vaccinationCenterService = vaccinationCenterService;
            this.patientService = patientService;
        }
        public IActionResult AddPatient(Guid id)
        {
            var center = vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if (center.MaxCapacity <= 0)
            {
                return Redirect(nameof(NoMoreCapacity));
            }

            VaccinationDTO dto = new VaccinationDTO();
            dto.vaccCenterId = center.Id;
            dto.patients = patientService.GetPatients();
            dto.manufacturers = new List<string>()
            {
                "Astrazeneka", "Pfizer"
            };
            return View(dto);
        }

        [HttpPost]
        public IActionResult AddPatientConfirmed(VaccinationDTO dto)
        {
            if (ModelState.IsValid)
            {
                vaccinationCenterService.LowerCapacity(dto.vaccCenterId);
                vaccinationCenterService.addPacientToCenter(dto);
                return RedirectToAction(nameof(Index));
            }

            return View(dto);

        }


        public IActionResult NoMoreCapacity()
        {
            return View();
        }

        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(vaccinationCenterService.GetAllVaccinationCenter());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetDetailsForVaccinationCenter(id);

            var dto = new CenterDTO();
            dto.center = vaccinationCenter;
            dto.vaccines = vaccinationCenterService.GetVaccinesForCenter(id);

            
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                vaccinationCenter.Id = Guid.NewGuid();
                vaccinationCenterService.CreateNewVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vaccinationCenterService.UpdeteExistingCenter(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = vaccinationCenterService.GetDetailsForVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
           vaccinationCenterService.DeleteCenter(id);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
