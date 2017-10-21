using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Entities;

namespace WebUI.Areas.Admin.Models
{
    public class ProductoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public double Precio { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }
        
        [Display(Name = "Imagen JPG")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Imagen { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImagenUri { get; set; }
    }
}