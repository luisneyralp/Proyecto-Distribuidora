using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Entities;
using WebUI.Managers;

namespace WebUI.Areas.Usuario.Controllers
{
    public class CategoriaController : UsuarioBaseController
    {
        // GET: Usuario/Categoria
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Almacen()
        {
            List<Producto> ProductosAlmacen = ProductoManagers.GetPorCategoria(1);
            return View(ProductosAlmacen);
        }
        public ActionResult Limpieza()
        {
            List<Producto> ProductosLimpieza = ProductoManagers.GetPorCategoria(2);
            return View(ProductosLimpieza);
        }
        public ActionResult Bebidas()
        {
            List<Producto> ProductosBebidas = ProductoManagers.GetPorCategoria(3);
            return View(ProductosBebidas);
        }
        public ActionResult Frescos()
        {
            List<Producto> ProductosFrescos = ProductoManagers.GetPorCategoria(4);
            return View(ProductosFrescos);
        }
    }
}