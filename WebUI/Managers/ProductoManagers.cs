using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebUI.Areas.Usuario.Models;
using WebUI.Entities;
using WebUI.Utilities;

namespace WebUI.Managers
{
    public class ProductoManagers : Managers
    {
        #region ABM
        /// <summary>
        /// Agrega un nuevo Producto en la BD.
        /// </summary>
        /// <param name="prod"></param>
        public static void Nuevo(Producto prod)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("insert into Producto(Nombre,Codigo,Descripcion,ImagenUri,Precio,CategoriaId,Destacado) values (@nombre,@Codigo,@Descripcion,@ImagenUri,@Precio,@CategoriaId,0)", con);

                query.Parameters.AddWithValue("@nombre", prod.Nombre);
                query.Parameters.AddWithValue("@Codigo", prod.Codigo);
                query.Parameters.AddWithValue("@Descripcion", prod.Descripcion);
                query.Parameters.AddWithValue("@Precio", prod.Precio);
                query.Parameters.AddWithValue("@ImagenUri", prod.ImagenUri);
                query.Parameters.AddWithValue("@CategoriaId", prod.TipoCategoria.Id);

                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Borrar un producto de la BD
        /// </summary>
        /// <param name="idProducto"></param>
        public static void Borrar(int idProducto)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();
                var query = new SqlCommand("DELETE FROM Producto WHERE Id = @id", con);
                query.Parameters.AddWithValue("@id", idProducto);
                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Edita un producto en la BD.
        /// </summary>
        /// <param name="prod"></param>
        public static void Editar(Producto prod)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("UPDATE Producto set Nombre = @nombre, Codigo = @Codigo, Descripcion = @Descripcion, Precio = @Precio, ImagenUri = @ImagenUri, CategoriaId = @CategoriaId WHERE Id = @id", con);

                query.Parameters.AddWithValue("@id", prod.Id);
                query.Parameters.AddWithValue("@nombre", prod.Nombre);
                query.Parameters.AddWithValue("@Codigo", prod.Codigo);
                query.Parameters.AddWithValue("@Descripcion", prod.Descripcion);
                query.Parameters.AddWithValue("@Precio", prod.Precio);
                query.Parameters.AddWithValue("@ImagenUri", prod.ImagenUri);
                query.Parameters.AddWithValue("@CategoriaId", prod.TipoCategoria.Id);

                query.ExecuteNonQuery();
            }
        }
        #endregion

        #region ProductosDestacados
        /// <summary>
        /// Retorna todos los productos destacados ordenados por el nombre.
        /// </summary>
        /// <returns></returns>
        public static List<Producto> GetProductosDestacados()
        {
            var productos = new List<Producto>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select p.ImagenUri,p.Nombre,p.Codigo,p.Descripcion,p.Precio,p.Id,p.Destacado, c.Nombre as NombreCategoria, c.Id as IdCategoria from Producto p inner join Categoria c on (p.CategoriaId = c.Id) where (p.Destacado = 1) order by Nombre", con);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var prod = MapearProducto(dr);
                        productos.Add(prod);
                    }
                }
            }
            return productos;
        }

        /// <summary>
        /// Agrega un nuevo Producto Destacado en la BD.
        /// </summary>
        /// <param name="idProducto"></param>
        public static void DestacarProducto(int idProducto)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("update Producto set Destacado = 1 where Id = @idProducto", con);

                query.Parameters.AddWithValue("@idProducto", idProducto);

                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Borrar un producto Destacado de la BD
        /// </summary>
        /// <param name="idProducto"></param>
        public static void QuitarProductoDestacado(int idProducto)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();
                var query = new SqlCommand("update Producto set Destacado = 0 where Id = @id", con);
                query.Parameters.AddWithValue("@id", idProducto);
                query.ExecuteNonQuery();
            }
        }
        #endregion

        /// <summary>
        /// Retorna un producto a partir de un id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Producto GetById(int id)
        {
            Producto prod = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select p.ImagenUri,p.Nombre,p.Codigo,p.Descripcion,p.Precio,p.Id,p.Destacado, c.Nombre as NombreCategoria, c.Id as IdCategoria from Producto p inner join Categoria c on (p.CategoriaId = c.Id) where p.id = @id", con);
                query.Parameters.AddWithValue("@id", id);

                using (var dr = query.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        prod = MapearProducto(dr);
                    }
                }
            }
            return prod;
        }

        /// <summary>
        /// Retorna todos los productos ordenados por el nombre.
        /// </summary>
        /// <returns></returns>
        public static List<Producto> GetTodos()
        {
            var productos = new List<Producto>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select p.ImagenUri,p.Nombre,p.Codigo,p.Descripcion,p.Precio,p.Id,p.Destacado, c.Nombre as NombreCategoria, c.Id as IdCategoria from Producto p inner join Categoria c on (p.CategoriaId = c.Id) order by Nombre", con);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var prod = MapearProducto(dr);
                        productos.Add(prod);
                    }
                }
            }
            return productos;
        }

        /// <summary>
        /// Retorna todos los productos de una categoria dada por parametro.
        /// </summary>
        /// <returns></returns>
        public static List<Producto> GetPorCategoria(int idCateg)
        {
            var productos = new List<Producto>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select p.ImagenUri,p.Nombre,p.Codigo,p.Descripcion,p.Precio,p.Id,p.Destacado, c.Nombre as NombreCategoria, c.Id as IdCategoria from Producto p inner join Categoria c on (p.CategoriaId = c.Id) where c.Id = @id", con);
                query.Parameters.AddWithValue("@id", idCateg);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var prod = MapearProducto(dr);
                        productos.Add(prod);
                    }
                }
            }
            return productos;
        }


        /// <summary>
        /// Retorna una lista de productos con la cantidad de cada uno para la vista de carrito
        /// </summary>
        /// <param name="idCliente"></param>
        public static List<Producto> ListaDelCarrito(int idCliente)
        {
            var carrito = new List<Producto>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select Producto_Pedido.Cantidad, p.Id, p.Nombre, p.Precio, p.Descripcion,p.ImagenUri from Pedido inner join Producto_Pedido on (Pedido.Id = Producto_Pedido.PedidoId) inner join Producto p on (p.Id = Producto_Pedido.ProductoId)where Pedido.IdCliente = @ClienteId", con);
                query.Parameters.AddWithValue("@ClienteId", idCliente);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var car = new Producto
                        {
                            Cantidad = Convert.ToInt32(dr["Cantidad"]),
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            ImagenUri = dr["ImagenUri"].ToString(),
                            Precio = Convert.ToDouble(dr["Precio"]),
                            Descripcion = dr["Descripcion"].ToString()
                        };
                        carrito.Add(car);
                    }
                }
            }
            return carrito;
        }

        /// <summary>
        /// Retorna todos los productos de un pedido ordenados por el nombre.
        /// </summary>
        /// <returns>name:"id"</returns>
        public static List<Producto> GetProductosPedidos(int id)
        {
            var listaProductos = new List<Producto>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select p.Id,p.Codigo,p.Nombre,p.Descripcion,p.Precio,pp.Cantidad from Producto_Pedido pp inner join Producto p on (pp.ProductoId = p.Id) where (pp.PedidoId = @id)", con);
                query.Parameters.AddWithValue("@id", id);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var prod = new Producto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Codigo = dr["Codigo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDouble(dr["Precio"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            Cantidad = Convert.ToInt32(dr["Cantidad"])
                        };
                        listaProductos.Add(prod);
                    }
                }
            }
            return listaProductos;
        }

        private static Producto MapearProducto(SqlDataReader dr)
        {
            var producto = new Producto
            {
                Id = Convert.ToInt32(dr["Id"]),
                Codigo = dr["Codigo"].ToString(),
                Nombre = dr["Nombre"].ToString(),
                Precio = Convert.ToDouble(dr["Precio"]),
                Descripcion = dr["Descripcion"].ToString(),
                ImagenUri = dr["ImagenUri"].ToString(),
                Destacado = Convert.ToInt32(dr["Destacado"]),
                TipoCategoria = new Categoria
                {
                    Id = Convert.ToInt32(dr["IdCategoria"]),
                    Nombre = dr["NombreCategoria"].ToString()
                }
            };
            return producto;

        }
    }
}