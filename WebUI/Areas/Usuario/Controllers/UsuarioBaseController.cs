using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.Utilities;

namespace WebUI.Areas.Usuario.Controllers
{
    public class UsuarioBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            //reviso la sesion.. Si es nueva, o no existe el usuario cargado en sesion, o el usuario no es Empresa, se lo expulsa.
            if (session.IsNewSession || Session[Strings.KeyCurrentUser] == null || ((Entities.Usuarios)Session[Strings.KeyCurrentUser]).TipoDeUsuario != Entities.ElTipoUsuario.Cliente)
            {
                //Si el request es AJAX..
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Unauthorized, Strings.UIMessageUnauthorized);
                }
                else
                {
                    //Si el request es normal, lo redirigo al Index.
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "Index" },
                        { "Area", "" }
                    });
                }
            }
        }
    }
}