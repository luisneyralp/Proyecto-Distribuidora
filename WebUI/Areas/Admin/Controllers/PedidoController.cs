using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Admin.Models;
using WebUI.Managers;
using WebUI.Entities;
using WebUI.Models;
using WebUI.Utilities;

namespace WebUI.Areas.Admin.Controllers
{
    public class PedidoController : AdminBaseController
    {
        public ActionResult Index()
        {
            return View(UsuarioManagers.GetPedidoClientes());
        }
        public ActionResult VerPedido(int id, string nombreCompleto, double montoTotal)
        {
            ViewBag.MontoTotal = montoTotal;
            ViewBag.NombreCompleto = nombreCompleto;
            return View(ProductoManagers.GetProductosPedidos(id));
        }
        public ActionResult PedidoGestor()
        {
            return View();
        }
        public ActionResult SetearPedido()
        {
            PedidoManagers.SetearPedidos();
            TempData[Strings.KeyMensajeDeAccion] = "Los pedidos han sido eliminados.";
            return RedirectToAction("Index");
        }
    }
}