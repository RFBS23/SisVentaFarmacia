using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Producto
    {
        public int idproducto { get; set; }
        public Marca omarca { get; set; }
        public Categoria ocategoria { get; set; }
        public string nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal precio { get; set; }
        public string precioTexto{ get; set; }
        public int stock { get; set; }
        public string rutaimg { get; set; }
        public string nombreimg { get; set; }
        public bool estado { get; set; }

        public string base64 { get; set; }
        public string Extension { get; set; }
    }
}
