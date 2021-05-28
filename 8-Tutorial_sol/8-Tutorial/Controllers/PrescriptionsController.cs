using _8_Tutorial.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Models
{
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IMedicinalService _medicinalService;

        public PrescriptionsController(IMedicinalService medicinalService)
        {
            _medicinalService = medicinalService;
        }

        [HttpGet("{prescriptionid}")]
        public async Task<IActionResult> getPrescription(int prescriptionid) {
            if (!(await _medicinalService.PrescriptionExist(prescriptionid))) {
               return BadRequest("The prescription with this ID does not exist");
            }
            return Ok(await _medicinalService.getPrescription(prescriptionid));
        }

    }
}
