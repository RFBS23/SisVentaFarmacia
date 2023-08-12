using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Entidad;

namespace Negocios
{
    public class N_Usuarios
    {
        private D_Usuarios objDatos = new D_Usuarios();
        public List<Usuario> Listar()
        {
            return objDatos.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombreusuario) || string.IsNullOrWhiteSpace(obj.nombreusuario))
            {
                Mensaje = "Debes Colocar un Usuario pe Bateria 🤨";
            } else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "Debe Completar este Campo pe Chistoso 🤨";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string claveAcceso = N_Recursos.GenerarClave(); //clave generada
                string asunto = "Creacion de Cuenta";
                string mensajeEmail = "<h3>Su cuenta fue Creada Correctamente</h3><br><p>Su Contraseña 🔑 para Acceder al sistema es: ! contraseña ¡ </p>";
                mensajeEmail = mensajeEmail.Replace("! contraseña ¡", claveAcceso);
                bool respuesta = N_Recursos.EnviarEmail(obj.correo, asunto, mensajeEmail);
                if (respuesta)
                {
                    obj.clave = N_Recursos.ConvertitSha256(claveAcceso);
                    return objDatos.Registrar(obj, out Mensaje);
                } else
                {
                    Mensaje = "No se pudo enviar el correo 📧 ❌";
                    return 0;
                }

            } else
            {
                return 0;
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.nombreusuario) || string.IsNullOrWhiteSpace(obj.nombreusuario))
            {
                Mensaje = "Debes Editar Bonito pe Mascota 🤨";
            }
            else if (string.IsNullOrEmpty(obj.correo) || string.IsNullOrWhiteSpace(obj.correo))
            {
                Mensaje = "Debes Colocar un Correo Diferente papu 🤨";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objDatos.Editar(obj, out Mensaje);
            } else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objDatos.Eliminar(id, out Mensaje);
        }

    }
}
