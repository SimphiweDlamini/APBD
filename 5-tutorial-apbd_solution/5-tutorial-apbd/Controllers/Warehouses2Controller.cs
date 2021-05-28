using _5_tutorial_apbd.Models;
using _5_tutorial_apbd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_tutorial_apbd.Controllers
{
    [Route("api/warehouse2")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IWarehouse2Service _service;
        public Warehouses2Controller(IWarehouse2Service service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrationProduct(WarehouseProduct warehouseProduct) {
            var result = _service.add2WarehouseProduct(warehouseProduct);

            return StatusCode(201, result);
        
        }
    }
}
