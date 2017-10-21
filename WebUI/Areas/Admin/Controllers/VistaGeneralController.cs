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
    public class VistaGeneralController : AdminBaseController
    {

        public ActionResult Index()
        {
            //Ejemplo de cómo recuperar un objeto de una sesion. 
            //Debo indicar de que tipo es la sesion (Casting -> unboxing)
            Usuarios user = (Usuarios)Session[Strings.KeyCurrentUser];
            return View(UsuarioManagers.GetClientes());
        }
        public ActionResult MiCuenta()
        {
            Usuarios user = (Usuarios)Session[Strings.KeyCurrentUser];
            return View(user);
        }
#region ABMs
        public ActionResult DarBaja(int id)
        {
            UsuarioManagers.DarBaja(id);
            TempData[Strings.KeyMensajeDeAccion] = "El usuario ha sido dado de baja.";
            return RedirectToAction("Index");
        }
        public ActionResult Editar(int id)
        {
            Usuarios user = UsuarioManagers.GetById(id);
            //a partir de la entidad, armo el viewModel que necesita la vista.
            RegistroViewModel model = new RegistroViewModel
            {
                Id = user.Id, //nuevo campo en el viewModel, IMPORTANTE, nos va a servir para luego tener el Id.. 
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Password = user.Password,
                Email = user.Email,
                Direccion = user.Direccion,
                TipoDeUsuario = user.TipoDeUsuario
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Obtengo mi entidad, y la actualizo mi entidad a partir del ViewModel.
                Usuarios user = UsuarioManagers.GetById(model.Id);
                user.Email = model.Email;
                user.Nombre = model.Nombre;
                user.Apellido = model.Apellido;
                user.Direccion = model.Direccion;
                user.Id = model.Id;
                user.Password = model.Password;
                user.TipoDeUsuario = model.TipoDeUsuario;

                //edito pasandole la entidad modificada...
                UsuarioManagers.Editar(user);

                TempData[Strings.KeyMensajeDeAccion] = "El usuario ha sido modificado.";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult DarAlta()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DarAlta(RegistroViewModel alta)
        {
            List < Usuarios > listaUsers = UsuarioManagers.GetClientes();
            Usuarios userPrueba = listaUsers.Find(us => us.Email == alta.Email);
            if (!ModelState.IsValid || userPrueba != null)
            {
                ModelState.AddModelError("", "Email ya registrado papu.");
                return View(alta);
            }
            string passwordEncriptada = Strings.Encriptar(alta.Password);
            Usuarios user = new Usuarios
            {   Apellido = alta.Apellido,
                Nombre = alta.Nombre,
                Email = alta.Email,
                Direccion = alta.Direccion,
                Password = passwordEncriptada, 
                TipoDeUsuario = ElTipoUsuario.Cliente
            };
            UsuarioManagers.DarAlta(user);
            Usuarios usuarioCreado = UsuarioManagers.GetByEmail(alta.Email);
            TempData[Strings.KeyMensajeDeAccion] = "El usuario ha sido dado de alta.";
            return RedirectToAction("Index");
        }
#endregion
    }
}
