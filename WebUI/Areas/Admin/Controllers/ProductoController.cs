using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Admin.Models;
using WebUI.Entities;
using WebUI.Managers;
using WebUI.Utilities;

namespace WebUI.Areas.Admin.Controllers
{
    public class ProductoController : AdminBaseController
    {
        // GET: Admin/Producto
        public ActionResult Index()
        {
            //paso por viewbag el listado de categorias..
            ViewBag.Categorias = CategoriaManagers.GetCategorias();
            List<Producto> productos = ProductoManagers.GetTodos();
            return View(productos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? categoriaId) //int? significa que es un int nulleable.
        {
            ViewBag.Categorias = CategoriaManagers.GetCategorias();
            List<Producto> productos;
            if (categoriaId.HasValue) //HasValue nos dice si el nulleable tiene valor.
            {
                productos = ProductoManagers.GetPorCategoria(categoriaId.Value); //.Value sobre un nulleable nos retorna el valor.
            }
            else
            {
                productos = ProductoManagers.GetTodos();
            }
            return View(productos);
        }

        #region ProductosDestacados
        public ActionResult ProductosDestacados()
        {
            List<Producto> prodDestacados = ProductoManagers.GetProductosDestacados();
            return View(prodDestacados);
        }
        public ActionResult Destacar(int id)
        {
            ProductoManagers.DestacarProducto(id);
            TempData[Strings.KeyMensajeDeAccion] = "El producto ha sido destacado.";
            return RedirectToAction("Index");
        }
        public ActionResult QuitarDestacado(int id)
        {
            ProductoManagers.QuitarProductoDestacado(id);
            TempData[Strings.KeyMensajeDeAccion] = "El producto ha sido quitado de los destacados.";
            return RedirectToAction("Index");
        }
        #endregion

        #region ABMs
        public ActionResult Nuevo()
        {
            ProductoViewModel model = new ProductoViewModel
            {
                //NOTA: Categorias NO es un listado de categorías, List<Categoria>, sino que es un IEnumerable<SelectListItem>
                Categorias = new SelectList(CategoriaManagers.GetCategorias(), "Id", "Nombre")
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(ProductoViewModel model)
        {
            //verifico, si subio imagen, que la imagen sea jpg
            if (model.Imagen != null && !model.Imagen.ContentType.Equals("image/jpeg"))
            {
                ModelState.AddModelError("Imagen", "La imagen debe ser jpg.");
            }

            if (ModelState.IsValid)
            {
                string imageUri = "";
                if (model.Imagen != null && model.Imagen.ContentLength > 0)
                {
                    var uploadDir = "~/uploads/productos";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.Imagen.FileName);
                    model.Imagen.SaveAs(imagePath);
                    imageUri = string.Format("{0}/{1}", uploadDir, model.Imagen.FileName);
                }

                //Creo mi entidad a partir del ViewModel.
                Producto prod = new Producto
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    TipoCategoria = new Categoria { Id = model.CategoriaId },
                    ImagenUri = imageUri,
                    Codigo = model.Codigo,
                    Precio = model.Precio
                };
                ProductoManagers.Nuevo(prod);

                TempData[Strings.KeyMensajeDeAccion] = "El producto ha sido dado de alta.";
                return RedirectToAction("Index");
            }
            else
            {
                //si el Model no es valido, busco el list de categorias nuevamente..
                model.Categorias = new SelectList(CategoriaManagers.GetCategorias(), "Id", "Nombre");
            }

            return View(model);
        }

        public ActionResult Editar(int id)
        {
            //obtengo la entidad
            Producto prod = ProductoManagers.GetById(id);

            //a partir de la entidad, armo el viewModel que necesita la vista.
            ProductoViewModel model = new ProductoViewModel
            {
                Id = prod.Id, //nuevo campo en el viewModel, IMPORTANTE, nos va a servir para luego tener el Id.. 
                CategoriaId = prod.TipoCategoria.Id,
                Nombre = prod.Nombre,
                Codigo = prod.Codigo,
                Precio = prod.Precio,
                Descripcion = prod.Descripcion,
                ImagenUri = prod.ImagenUri, //nuevo campo en el viewModel, IMPORTANTE, que me va a servir a fines de mostrar la imagen actual y tb para volver a tener el dato..
                Categorias = new SelectList(CategoriaManagers.GetCategorias(), "Id", "Nombre")
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ProductoViewModel model)
        {
            //verifico, si subio imagen, que la imagen sea jpg
            if (model.Imagen != null && !model.Imagen.ContentType.Equals("image/jpeg"))
            {
                ModelState.AddModelError("Imagen", "La imagen debe ser jpg.");
            }

            if (ModelState.IsValid)
            {
                string imageUri = ""; //inicializo.. 
                if (!string.IsNullOrEmpty(model.ImagenUri))
                {
                    //Si no es vacio, la inicializo con el valor q tenia..
                    imageUri = model.ImagenUri;
                }
                if (model.Imagen != null && model.Imagen.ContentLength > 0)
                {
                    var uploadDir = "~/uploads/productos";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.Imagen.FileName);
                    model.Imagen.SaveAs(imagePath);
                    imageUri = string.Format("{0}/{1}", uploadDir, model.Imagen.FileName);
                }

                //Obtengo mi entidad, y la actualizo mi entidad a partir del ViewModel.
                Producto prod = ProductoManagers.GetById(model.Id);
                prod.Nombre = model.Nombre;
                prod.Codigo = model.Codigo;
                prod.Precio = model.Precio;
                prod.Descripcion = model.Descripcion;
                prod.TipoCategoria = new Categoria { Id = model.CategoriaId };
                prod.ImagenUri = imageUri;

                //edito pasandole la entidad modificada...
                ProductoManagers.Editar(prod);

                TempData[Strings.KeyMensajeDeAccion] = "El plato ha sido modificado.";
                return RedirectToAction("Index");
            }
            else
            {
                model.Categorias = new SelectList(CategoriaManagers.GetCategorias(), "Id", "Nombre");
            }
            return View(model);
        }

        public ActionResult Baja(int id)
        {
            ProductoManagers.Borrar(id);
            TempData[Strings.KeyMensajeDeAccion] = "El plato ha sido borrado.";
            return RedirectToAction("Index");
        }
    #endregion
    }
}