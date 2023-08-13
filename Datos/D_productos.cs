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
    public class D_productos
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.idproducto, p.nombre, p.Descripcion, m.idmarca, m.descripcion[desmarca], c.idcategoria, c.descripcion[descategoria], p.precio, p.stock, p.rutaimg, p.nombreimg, p.estado from productos p");
                    sb.AppendLine("inner join marcas m on m.idmarca = p.idmarca");
                    sb.AppendLine("inner join categorias c on c.idcategoria = p.idcategoria");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                idproducto = Convert.ToInt32(dr["idproducto"]),
                                nombre = dr["nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                omarca = new Marca() { idmarca = Convert.ToInt32(dr["idmarca"]), descripcion = dr["desmarca"].ToString() },
                                ocategoria = new Categoria() { idcategoria = Convert.ToInt32(dr["idcategoria"]), descripcion = dr["descategoria"].ToString() },
                                precio = Convert.ToDecimal(dr["precio"], new CultureInfo("es-PE")),
                                stock = Convert.ToInt32(dr["stock"]),
                                rutaimg = dr["rutaimg"].ToString(),
                                nombreimg = dr["nombreimg"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Producto>();
            }

            return lista;
        }

        //registrar
        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    SqlCommand cmd = new SqlCommand("spu_registrar_productos", oconexion);
                    cmd.Parameters.AddWithValue("idmarca", obj.omarca.idmarca);
                    cmd.Parameters.AddWithValue("idcategoria", obj.ocategoria.idcategoria);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("precio", obj.precio);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idautogenerado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        //editar
        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    SqlCommand cmd = new SqlCommand("spu_editar_productos", oconexion);
                    cmd.Parameters.AddWithValue("idproducto", obj.idproducto);
                    cmd.Parameters.AddWithValue("idmarca", obj.omarca.idmarca);
                    cmd.Parameters.AddWithValue("idcategoria", obj.ocategoria.idcategoria);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("precio", obj.precio);
                    cmd.Parameters.AddWithValue("stock", obj.stock);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        //guardar ruta y nombre de imagen
        public bool GuardarImg(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    string query = "update productos set rutaimg = @rutaimg, nombreimg = @nombreimg where idproducto = @idproducto";

                    SqlCommand cmd = new SqlCommand("spu_registrar_productos", oconexion);
                    cmd.Parameters.AddWithValue("rutaimg", obj.rutaimg);
                    cmd.Parameters.AddWithValue("nombreimg", obj.nombreimg);
                    cmd.Parameters.AddWithValue("idproducto", obj.idproducto);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    } else
                    {
                        Mensaje = "No se Pudo Actualiza la Imagen 🖼️";
                    }
                }
            } catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        //eliminar
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cndb))
                {
                    SqlCommand cmd = new SqlCommand("spu_eliminar_producto", oconexion);
                    cmd.Parameters.AddWithValue("idproducto", id);
                    cmd.Parameters.Add("resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
