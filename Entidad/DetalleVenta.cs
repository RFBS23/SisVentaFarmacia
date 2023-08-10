using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DetalleVenta
    {
        public int iddetventa { get; set; }
        public int idventa { get; set; }
        public Producto oproducto { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string idtransaccion { get; set; }
    }
}
