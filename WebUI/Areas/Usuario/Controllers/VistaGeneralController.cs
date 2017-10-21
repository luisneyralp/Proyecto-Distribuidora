using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Utilities;
using WebUI.Entities;
using WebUI.Models;
using WebUI.Areas.Usuario.Models;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using WebUI.Managers;

namespace WebUI.Areas.Usuario.Controllers
{
    public class VistaGeneralController : UsuarioBaseController
    {
        // GET: Usuario/VistaGeneral
        public ActionResult Index()
        {
            //Ejemplo de cómo recuperar un objeto de una sesion. 
            //Debo indicar de que tipo es la sesion (Casting -> boxing & unboxing)
            Usuarios user = (Usuarios)Session[Strings.KeyCurrentUser];
            List<Producto> productosDestacados = ProductoManagers.GetProductosDestacados();
            return View(productosDestacados);
        }
        public ActionResult MiCuenta()
        {
            Usuarios user = (Usuarios)Session[Strings.KeyCurrentUser];
            return View(user);
        }
        public ActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contacto(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("luisneyralp@gmail.com")); //A ESTE MAIL LLEGAN LOS MENSAJES DE ENVIO DE CONTACTO
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.Nombre, model.Email, model.Mensaje);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    TempData[Strings.KeyMensajeDeAccion] = "Su mensaje ha sido enviado.";
                    return RedirectToAction("Contacto");
                }
            }
            return View(model);
        }
        public ActionResult CambiarContraseña(int id)
        {
            Usuarios user = UsuarioManagers.GetById(id);
            ContraseñaViewModel model = new ContraseñaViewModel
            {
                Id = user.Id,
                Password = user.Password
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarContraseña(ContraseñaViewModel model)
        {
            if (ModelState.IsValid && model.NewPassword == model.NewPasswordNew)
            {
                string encriptada = Strings.Encriptar(model.NewPassword);
                UsuarioManagers.EditarContraseña(model.Id,encriptada);
                TempData[Strings.KeyMensajeDeAccion] = "Su contraseña ha sido modificada.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Verifique que confirmar contraseña sea la misma.");
            return View(model);
        }
    }
}