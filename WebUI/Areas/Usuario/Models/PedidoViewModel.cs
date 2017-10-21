using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Entities;

namespace WebUI.Areas.Usuario.Models
{
    public class PedidoViewModel
    {
        [Range(1, 500)]
        [Required]
        public int Cantidad { get; set; }

        public int Id { get; set; }
    }
}