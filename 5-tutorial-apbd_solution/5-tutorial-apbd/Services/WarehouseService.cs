using _5_tutorial_apbd.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _5_tutorial_apbd.Services
{
    public class WarehouseService : IWarehouseService
    {
        private IConfiguration _configuration;
        public WarehouseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task <int> addWarehouseProduct(WarehouseProduct warehouseProduct)
        {
            int result =-4;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("default"))) {
                SqlCommand insert = new SqlCommand();
                insert.Connection = con;
                insert.Parameters.AddWithValue("@pid", warehouseProduct.IdProduct);
                insert.CommandText = "Select idProduct from Product where idProduct = @pid;";
                await con.OpenAsync();
                DbTransaction transaction = await con.BeginTransactionAsync();
                insert.Transaction = (SqlTransaction)transaction;

                try
                {
                    using (var dr = await insert.ExecuteReaderAsync())
                    {
                        await dr.ReadAsync();
                        if (!dr.HasRows)
                        {
                            return -1;
                        }

                    }
                    insert.Parameters.AddWithValue("@amount", warehouseProduct.Amount);
                    insert.Parameters.AddWithValue("@createdat", warehouseProduct.CreatedAt);
                    insert.CommandText = "Select idProduct,Amount,CreatedAt from Order where" +
                        "idProduct = @pid and Amount = @amount and CreatedAt < @createdat;";
                    using (var dr = await insert.ExecuteReaderAsync())
                    {
                        await dr.ReadAsync();
                        if (!dr.HasRows)
                        {
                            return -2;
                        }

                    }
                    insert.CommandText = "Select FulfilledAt from order where" +
                        "idProduct = @pid and Amount = @amount and CreatedAt < @createdat;";
                    using (var dr = await insert.ExecuteReaderAsync())
                    {
                        await dr.ReadAsync();
                        if (dr["FulfilledAt"].ToString() == null)
                        {
                            return -3;
                        }

                    }
                    insert.CommandText = "Select IdOrder from Product_Warehouse where" +
                         "idProduct = @pid and Amount = @amount and CreatedAt < @createdat;";
                    int idOrder;
                    using (var dr = await insert.ExecuteReaderAsync())
                    {
                        await dr.ReadAsync();
                        if (!dr.HasRows)
                        {
                            return -3;
                        }
                        idOrder = int.Parse(dr["IdOrder"].ToString());

                    }
                    insert.CommandText = "Update Order Set FulfilledAt = @createdat;";
                    await insert.ExecuteNonQueryAsync();
                    int price;
                    insert.CommandText = "Select price from Product where idProduct = @pid";
                    using (var dr = await insert.ExecuteReaderAsync())
                    {
                        await dr.ReadAsync();
                        price = int.Parse(dr["Price"].ToString());
                    }
                    int pwprice = price * warehouseProduct.Amount;
                    insert.Parameters.AddWithValue("@wid", warehouseProduct.IdWarehouse);
                    insert.Parameters.AddWithValue("@idorder", idOrder);
                    insert.Parameters.AddWithValue("@price", pwprice);
                    insert.CommandText = "Insert into Product_Warehouse(IdWarehouse,IdProduct,IdOrder,Amount,Price,CreatedAt) " +
                        "values (@wid,@pid,@idorder,@amount,@price,@createdat); ";
                    await insert.ExecuteNonQueryAsync();
                    insert.CommandText = "SELECT * FROM Product_Warehouse WHERE IdProductWarehouse=(SELECT max(IdProductWarehouse) FROM ProductWarehouse);";
                    using (var dr = await insert.ExecuteReaderAsync())
                    {
                        await dr.ReadAsync();
                       // if (!dr.HasRows)
                        //{
                          //  return -4;
                       // }
                        result = int.Parse(dr["IdProductWarehouse"].ToString());

                    }
                    await transaction.CommitAsync();

                }
                catch (SqlException e)
                {

                    await transaction.RollbackAsync();
                }
                catch (Exception tce) {
                    await transaction.RollbackAsync();
                }
                
            }
            return result;
        }
    }
}
