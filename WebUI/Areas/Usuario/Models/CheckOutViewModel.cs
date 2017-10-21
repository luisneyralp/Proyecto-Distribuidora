using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebUI.Entities;

namespace WebUI.Areas.Usuario.Models
{
    public class CheckOutViewModel
    {
        public PedidoViewModel ModeloPedido { get; set; }

        public Producto Producto { get; set; }
    }
}