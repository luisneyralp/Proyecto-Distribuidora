using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebUI.Entities;
using WebUI.Utilities;

namespace WebUI.Managers
{
    public class CategoriaManagers: Managers
    {
        /// <summary>
        /// Retorna todas las categorias.
        /// </summary>
        /// <returns></returns>
        public static List<Categoria> GetCategorias()
        {
            var categorias = new List<Categoria>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("SELECT * FROM Categoria", con);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var categ = new Categoria {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString()
                        };
                        // Agregamos la categoria a la lista
                        categorias.Add(categ);
                    }
                }
            }
            return categorias;
        }
    }
}