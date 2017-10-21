using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int Definitivo { get; set; }
        public int IdCliente { get; set; }
        public double MontoTotal { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string DireccionCliente { get; set; }
    }
}