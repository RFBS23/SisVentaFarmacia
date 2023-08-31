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

        //cambiar y restablecer contraseña
        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return objDatos.Cambiarclave(idusuario, nuevaclave,out Mensaje);
        }

        public bool RestablecerClave(int idusuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = N_Recursos.GenerarClave(); //clave generada
            bool resultado = objDatos.RestablecerClave(idusuario, N_Recursos.ConvertitSha256(nuevaclave), out Mensaje);

            if (resultado)
            {
                string asunto = "FarmaDev - Restablecer contraseña";
                string mensajeEmail = "<h3>Su contraseña fue restablecida Correctamente</h3><br><p>Para Acceder al sistema Debe utilizar la siguiente contraseña 🔑: ! contraseña ¡ </p>";
                mensajeEmail = mensajeEmail.Replace("! contraseña ¡", nuevaclave);
                bool respuesta = N_Recursos.EnviarEmail(correo, asunto, mensajeEmail);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se puedo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se puedo restablecer la contraseña :( ";
                return false;
            }
        }

    }
}
