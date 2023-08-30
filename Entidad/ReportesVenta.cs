using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ReportesVenta
    {
        public string FechaVenta { get; set; }
        public string Clientes { get; set; }
        public string Producto { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string idtransaccion { get; set; }
    }
}
