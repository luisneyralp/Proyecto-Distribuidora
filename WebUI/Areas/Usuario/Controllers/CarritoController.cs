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
    public class CarritoController : UsuarioBaseController
    {
        //GET: Usuario/Carrito
        public ActionResult Index()
        {
            Usuarios cliente = (Usuarios)Session[Strings.KeyCurrentUser];
            List<Producto> carrito = ProductoManagers.ListaDelCarrito(cliente.Id);
            return View(carrito);
        }
        public ActionResult QuitarProducto(int id)
        {
            Usuarios cliente = (Usuarios)Session[Strings.KeyCurrentUser];
            int idPedido = PedidoManagers.GetById(cliente.Id).Id;
            PedidoManagers.BorrarProducto(id, idPedido);
            TempData[Strings.KeyMensajeDeAccion] = "El producto ha sido borrado.";
            return RedirectToAction("Index");
        }
        public ActionResult CheckOut(double monto)
        {
            Usuarios cliente = (Usuarios)Session[Strings.KeyCurrentUser];
            PedidoManagers.ConfirmarPedido(cliente.Id,monto);
            TempData[Strings.KeyMensajeDeAccion] = "Su carrito ha sido cerrado.";
            return RedirectToAction("Index");
        }
        public ActionResult CheckIn()
        {
            Usuarios cliente = (Usuarios)Session[Strings.KeyCurrentUser];
            PedidoManagers.ReabrirPedido(cliente.Id);
            TempData[Strings.KeyMensajeDeAccion] = "Se ha reabierto el carrito";
            return RedirectToAction("Index");
        }
    }
}