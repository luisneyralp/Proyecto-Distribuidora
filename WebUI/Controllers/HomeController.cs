using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebUI.Managers;
using WebUI.Entities;
using WebUI.Models;
using WebUI.Utilities;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //A este Action entra cuando viene por POST información de campos del form de contacto.
        [HttpPost] //indice que el action debe ser de un request con datos de formulario
        [ValidateAntiForgeryToken] //indica que espera un token del cliente (que se debe insertar en la vista)
        public ActionResult Index(ContactoViewModel model) //aca el model se bindea automaticamente, ya que machea por los nombres de los campos del form, que son los mismos nombres q las propiedades del viewmodel.
        {
            //ModelState contiene el estado del viewModel. Es como que se realiza una nueva verificiación pero desde el servidor.. 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Usuarios user = UsuarioManagers.Login(model.Email,model.Password);
            if (user==null){
                ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                return View(model);
            }
            Session[Strings.KeyCurrentUser] = user;
            switch (user.TipoDeUsuario)
            {
                case ElTipoUsuario.Cliente:
                    return RedirectToAction("index", "VistaGeneral", new { area = "Usuario" });
                case ElTipoUsuario.Empresa:
                    return RedirectToAction("index", "VistaGeneral", new { area = "Admin" });
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Remove(Strings.KeyCurrentUser);
            return RedirectToAction("Index");
        }
        public ActionResult Recuperar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Recuperar(RecuperarViewModel model)
        {
            Usuarios user = UsuarioManagers.GetByEmail(model.Email);
            if (ModelState.IsValid && user != null)
            {
                string password = Membership.GeneratePassword(10, 1); //Se genera un password aleatoria
                string encriptado = Strings.Encriptar(password); //Se encripta la nueva pass
                UsuarioManagers.EditarContraseña(user.Id, encriptado); // Se guarda la pass encriptada
                var body = "<p>Email From: {0} </p><p>Hola {1}, esta es ahora tu nueva password:</p><b>{2}</b>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("luisneyralp@gmail.com")); //A este mail llegan los mensajes enviados (al final escribir user.email)
                message.Subject = "Recuperar Contraseña";
                message.Body = string.Format(body,"luisneyralp@gmail.com", user.Nombre, password);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Gracias");
                }
            }
            ModelState.AddModelError("Email", "No se encontro ningun usuario con ese Email.");
            return View(model);
        }
        public ActionResult Gracias()
        {
            return View();
        }
    }
}