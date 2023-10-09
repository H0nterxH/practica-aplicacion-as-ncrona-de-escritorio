    using System.Data;
using System.Web.Http;
using System.Data.SqlClient;
using System;

namespace Aplicación_asíncrona_Web.Controllers
{
    public class controlador : ControllerBase
    {
        private string connectionString = "Data Source=DESKTOP-PTBLD39\\SQLEXPRESS;Initial Catalog=AdventureWorks2012;Integrated Security=True;";

        [System.Web.Http.HttpGet]
        public IHttpActionResult Consulta1()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT po.PurchaseOrderID, po.VendorID, po.SubTotal, " +
                        "pod.UnitPrice, pod.OrderQty, p.ProductID, p.Name AS ProductName, p.Color, " +
                        "v.Name AS VendorName, v.AccountNumber " +
                        "FROM Purchasing.PurchaseOrderHeader AS po " +
                        "INNER JOIN Purchasing.PurchaseOrderDetail AS pod ON po.PurchaseOrderID = pod.PurchaseOrderID " +
                        "INNER JOIN Production.Product AS p ON pod.ProductID = p.ProductID " +
                        "INNER JOIN Purchasing.Vendor AS v ON po.VendorID = v.VendorID " +
                        "WHERE po.VendorID BETWEEN 1568 AND 1572;", connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return Ok(dataTable); // Devuelve los resultados en formato JSON
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Consulta2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT po.PurchaseOrderID, po.VendorID, pod.ProductID, " +
                        "pod.UnitPrice, pod.StockedQty, " +
                        "(SELECT Name FROM Purchasing.ShipMethod WHERE ShipMethodID = po.ShipMethodID) AS ShipMethodName, " +
                        "(SELECT ShipBase FROM Purchasing.ShipMethod WHERE ShipMethodID = po.ShipMethodID) AS ShipBase " +
                        "FROM Purchasing.PurchaseOrderDetail AS pod " +
                        "INNER JOIN Purchasing.PurchaseOrderHeader AS po ON pod.PurchaseOrderID = po.PurchaseOrderID " +
                        "WHERE po.VendorID = 1662;", connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return Ok(dataTable); // Devuelve los resultados en formato JSON
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
