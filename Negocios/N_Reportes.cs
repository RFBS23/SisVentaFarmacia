using Datos;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class N_Reportes
    {
        private D_Reportes objDatos = new D_Reportes();

        public List<ReportesVenta> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objDatos.Ventas(fechainicio, fechafin, idtransaccion);
        }

        public Reportes verReportes()
        {
            return objDatos.verReportes();
        }
    }
}
