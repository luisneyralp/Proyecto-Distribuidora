using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Usuario.Models;
using WebUI.Entities;
using WebUI.Managers;
using WebUI.Utilities;

namespace WebUI.Areas.Usuario.Controllers
{
    public class CheckOutController : UsuarioBaseController
    {
        // GET: Usuario/CheckOut
        public ActionResult Index(int id)
        {
            Usuarios cliente = (Usuarios)Session[Strings.KeyCurrentUser];
            Producto prod = ProductoManagers.GetById(id);
            PedidoViewModel pedMod = new PedidoViewModel
            {
                Id = PedidoManagers.GetById(cliente.Id).Id,
                Cantidad = 0
            };
            CheckOutViewModel model = new CheckOutViewModel
            {
                Producto = prod,
                ModeloPedido = pedMod
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckOutViewModel model)
        {
            Usuarios cliente = (Usuarios)Session[Strings.KeyCurrentUser];
            Producto prod = ProductoManagers.GetById(model.Producto.Id);
            List<Producto> listaProductos = ProductoManagers.GetProductosPedidos(model.ModeloPedido.Id);
            var esta = listaProductos.Find(p => p.Id == model.Producto.Id);
            if (ModelState.IsValid)
            {
                if (esta == null)
                {
                    PedidoManagers.AgregarProducto(model.ModeloPedido.Id, prod.Id, model.ModeloPedido.Cantidad);
                }
                else
                {
                    PedidoManagers.AumentarCantidad(model.ModeloPedido.Id, prod.Id, model.ModeloPedido.Cantidad);
                }
                TempData[Strings.KeyMensajeDeAccion] = "Su compra ha sido cargada al carrito";
                return RedirectToAction("Index", "Carrito");
            }
            model.Producto = prod;
            return View(model);
        }
    }
}