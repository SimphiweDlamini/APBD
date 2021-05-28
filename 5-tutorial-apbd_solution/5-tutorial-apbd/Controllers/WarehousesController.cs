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
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _service;
        public WarehousesController(IWarehouseService service) {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> ProductRegistration(WarehouseProduct whouse) {
            int result =  await _service.addWarehouseProduct(whouse);
            if (result == -1)
            {
                return StatusCode(404, "The product/warehouse with the given id does not exist");
            }
            else if (result == -2)
            {
                return StatusCode(404, "There is no suitable order");
            }
            else if (result == -3)
            {
                return StatusCode(404, "The order has already been completed");
            }
            else if (result < 0)
            {
                return StatusCode(404, "An Error has occured with your request");
            }
            else {
                return StatusCode(201, result);
            }
           

        }
    }
}
