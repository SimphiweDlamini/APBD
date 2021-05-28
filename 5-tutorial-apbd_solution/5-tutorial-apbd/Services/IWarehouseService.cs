using _5_tutorial_apbd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_tutorial_apbd.Services
{
    public interface IWarehouseService
    {
        public Task<int> addWarehouseProduct(WarehouseProduct warehouseProduct);
    }
}
