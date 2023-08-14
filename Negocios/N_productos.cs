using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidad;
namespace Negocios
{
    public class N_productos
    {
        private D_productos objDatos = new D_productos();
        public List<Producto> Listar()
        {
            return objDatos.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.omarca.idmarca == 0)
            {
                Mensaje = "Debes Seleccionar una marca 🤨";
            }
            else if (obj.ocategoria.idcategoria == 0)
            {
                Mensaje = "Debes Seleccionar una categoria 🤨";
            }
            else if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "Debes Colocar una Decripcion pe Bateria 🤨";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Debes Colocar una Decripcion pe Bateria 🤨";
            }
            else if (obj.precio == 0)
            {
                Mensaje = "Debes Colocar un precio 🤨";
            }
            else if (obj.stock == 0)
            {
                Mensaje = "Debes Colocar el stock del producto 🤨";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        //editar
        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.omarca.idmarca == 0)
            {
                Mensaje = "Debes Seleccionar una marca 🤨";
            }
            else if (obj.ocategoria.idcategoria == 0)
            {
                Mensaje = "Debes Seleccionar una categoria 🤨";
            }
            else if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                Mensaje = "Debes Colocar una Decripcion pe Bateria 🤨";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "Debes Colocar una Decripcion pe Bateria 🤨";
            }
            else if (obj.precio == 0)
            {
                Mensaje = "Debes Colocar un precio 🤨";
            }
            else if (obj.stock == 0)
            {
                Mensaje = "Debes Colocar el stock del producto 🤨";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        //img
        public bool GuardarImg(Producto obj, out string Mensaje)
        {
            return objDatos.GuardarImg(obj, out Mensaje);
        }

        //eliminar
        public bool Eliminar(int id, out string Mensaje)
        {
            return objDatos.Eliminar(id, out Mensaje);
        }
    }
}
