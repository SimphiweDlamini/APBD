using _5_tutorial_apbd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_tutorial_apbd.Services
{
   public interface IWarehouse2Service
    {
        public Task<int> add2WarehouseProduct(WarehouseProduct warehouseProduct);
    }
}
