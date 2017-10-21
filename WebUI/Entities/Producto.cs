using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public Categoria TipoCategoria { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUri { get; set; }
        public int Destacado { get; set; }
        public int Cantidad { get; set; }
    }
}