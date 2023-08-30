using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Datos
{
    public class D_Reportes
    {
        public List<ReportesVenta> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<ReportesVenta> lista = new List<ReportesVenta>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    SqlCommand cmd = new SqlCommand("spu_reporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReportesVenta()
                            {
                                FechaVenta = dr["FechaVenta"].ToString(),
                                Clientes = dr["Clientes"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                precio = Convert.ToDecimal( dr["precio"], new CultureInfo("es-PE")),
                                cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                total = Convert.ToDecimal(dr["total"], new CultureInfo("es-PE")),
                                idtransaccion = dr["estado"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ReportesVenta>();
            }

            return lista;
        }

        /* visualizamos la contidad de reportes en cards */
        public Reportes verReportes()
        {
            Reportes objeto = new Reportes();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    SqlCommand cmd = new SqlCommand("spu_reporte", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Reportes()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new Reportes();
            }

            return objeto;
        }
    }
}
