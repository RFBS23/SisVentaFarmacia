using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Carrito
    {
        public int idcarrito { get; set; }
        public Cliente ocliente { get; set; }
        public Producto oproducto { get; private set; }
        public int cantidad { get; set; }
    }
}
