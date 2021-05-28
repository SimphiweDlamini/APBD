using _8_Tutorial.DTOs;
using _8_Tutorial.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Models
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IMedicinalService _medicinalService;

        public DoctorsController(IMedicinalService medicinalService)
        {
            _medicinalService = medicinalService;
        }

        [HttpGet]
        public async Task<IActionResult> getDoctors()
        {
            var result = await _medicinalService.GetDoctors();
            return Ok(result);
        }

        [HttpGet("{docid}")]
        public async Task<IActionResult> getDoctor(int docid) {
            var result = await _medicinalService.GetDoctor(docid);
            if (result == null) {
                return BadRequest("Doctor with this id does not exist");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorRequest toAdd) {
            if (await _medicinalService.DoctorExist(toAdd)) {
                return BadRequest("A doctor with this Id already exists");
            }
            await _medicinalService.AddDoctor(toAdd);
            return StatusCode(201, "Doctor Successfully added");
        }

        [HttpPut("{docid}")]
        public async Task<IActionResult> UpdateDoctor(DoctorRequest toUpdate) {
            if (!await _medicinalService.DoctorExist(toUpdate)) {
                return BadRequest("This Doctor Does not exist");
            }
            await _medicinalService.UpdateDoctor(toUpdate);
                return StatusCode(201, "Doctor Update Succesfully");
            
        }

        [HttpDelete("{docid}")]
        public async Task<IActionResult> DeleteDoctor(int docid) {
            if (!await _medicinalService.DoctorExist(docid))
            {
                return BadRequest("This Doctor Does not exist");
            }
            await _medicinalService.DeleteDoctor(docid);
            return StatusCode(201, "Doctor has succesffully been deleted");
        }
    }
}
