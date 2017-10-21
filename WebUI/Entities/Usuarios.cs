using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Entities
{
    public enum ElTipoUsuario
    {
        Empresa,
        Cliente
    }
    public class Usuarios
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Apellido { get; set; }
        public ElTipoUsuario TipoDeUsuario { get; set; }
    }
}