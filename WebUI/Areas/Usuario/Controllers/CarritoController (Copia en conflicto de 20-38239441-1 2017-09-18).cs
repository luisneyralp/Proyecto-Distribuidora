using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Managers;
using WebUI.Utilities;

namespace WebUI.Areas.Usuario.Controllers
{
    public class CarritoController : UsuarioBaseController
    {
        // GET: Usuario/Carrito
        public ActionResult Index()
        {
            return View(ProductoManagers.GetMasPedidos());
        }
        public ActionResult Delete(int id)
        {
            ProductoManagers.Borrar(id);
            TempData[Strings.KeyMensajeDeAccion] = "El plato ha sido borrado correctamente.";
            return RedirectToAction("Index");
        }
    }
}