using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Entities;
using System.Configuration;
using System.Data.SqlClient;
using WebUI.Utilities;
using WebUI.Areas.Usuario.Models;
using System.Data;

namespace WebUI.Managers
{
    public class PedidoManagers : Managers
    {
        /// <summary>
        /// Retorna un Pedido a partir del Id de un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Pedido GetById(int id)
        {
            Pedido pedido = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("select * from Pedido where IdCliente = @id", con);
                query.Parameters.AddWithValue("@id", id);

                using (var dr = query.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        pedido = MapearPedido(dr);
                    }
                }
            }
            return pedido;
        }

        /// <summary>
        /// Agrega un nuevo producto del pedido en la BD.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public static void AgregarProducto(int idPedido, int idProducto, int cantidad)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("insert into Producto_Pedido (ProductoId,PedidoId,Cantidad) values (@ProductoId,@PedidoId,@Cantidad)", con);

                query.Parameters.AddWithValue("@PedidoId", idPedido);
                query.Parameters.AddWithValue("@ProductoId", idProducto);
                query.Parameters.AddWithValue("@Cantidad", cantidad); ;

                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Borrar un producto del pedido del cliente.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idPedido"></param>
        public static void BorrarProducto(int idProducto, int idPedido)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();
                var query = new SqlCommand("delete from Producto_Pedido where ProductoId = @idProducto and PedidoId = @idPedido", con);
                query.Parameters.AddWithValue("@idProducto", idProducto);
                query.Parameters.AddWithValue("@idPedido", idPedido);
                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Incrementa la cantidad de un producto ya existente en la lista de productos de un cliente.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <param name="idProducto"></param>
        /// <param name="cant"></param>
        public static void AumentarCantidad(int idPedido, int idProducto, int cant)
        {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("update Producto_Pedido set Cantidad = Cantidad+@cant where ProductoId = @idProducto and PedidoId = @idPedido", con);

                query.Parameters.AddWithValue("@idPedido", idPedido);
                query.Parameters.AddWithValue("@idProducto", idProducto);
                query.Parameters.AddWithValue("@cant", cant);

                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cierra el carrito, quedando el pedido confirmado.
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="monto"></param>
        public static void ConfirmarPedido(int idCliente, double monto)
        {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("update Pedido set Definitivo = 1,MontoTotal = @monto where IdCliente = @idCliente", con);
                query.Parameters.AddWithValue("@idCliente", idCliente);
                query.Parameters.AddWithValue("@monto", monto);

                query.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Reabre el carrito, el usuario puede volver hacer uso del carrito.
        /// </summary>
        /// <param name="idCliente"></param>
        public static void ReabrirPedido(int idCliente)
        {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("update Pedido set Definitivo = 0 where IdCliente = @idCliente", con);
                query.Parameters.AddWithValue("@idCliente", idCliente);

                query.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// Se abren los carritos de todos los clientes.
        /// </summary>
        /// <param></param>
        public static void SetearPedidos()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand(@"delete from Producto_Pedido
                                            update Pedido set Definitivo = 0", con);
                query.ExecuteNonQuery();
            }
        }

        private static Pedido MapearPedido(SqlDataReader dr)
        {
            var pedido = new Pedido
            {
                Id = Convert.ToInt32(dr["Id"]),
                Definitivo = Convert.ToInt32(dr["Definitivo"]),
                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                MontoTotal = Convert.ToDouble(dr["MontoTotal"])
            };
            return pedido;
        }







    }
}