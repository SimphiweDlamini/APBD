using _5_tutorial_apbd.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _5_tutorial_apbd.Services
{
    public class Warehouse2service : IWarehouse2Service
    {
        private IConfiguration _configuration;

        public Warehouse2service(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> add2WarehouseProduct(WarehouseProduct warehouseProduct)
        {
            var result = null;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                SqlCommand insert = new SqlCommand("AddProducttToWarehouse",con);
                insert.CommandType= CommandType.StoredProcedure;
                insert.Parameters.AddWithValue("@IdProduct", warehouseProduct.IdProduct);
                insert.Parameters.AddWithValue("@IdWarehouse", warehouseProduct.IdWarehouse);
                insert.Parameters.AddWithValue("@Amount", warehouseProduct.Amount);
                insert.Parameters.AddWithValue("@CreatedAt", warehouseProduct.CreatedAt);
                await con.OpenAsync();

                using (var dr = await insert.ExecuteReaderAsync()) {
                    result = dr.ReadAsync();
                }


               
            }
            return result;
    }
}
