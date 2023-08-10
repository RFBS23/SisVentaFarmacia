using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Usuario
    {
        public int idusuario { get; set; }
        public string nombreusuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public bool restablecer { get; set; }
        public bool estado { get; set; }
    }
}
