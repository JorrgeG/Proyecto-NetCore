using System.Collections.Generic;
using Aplicacion.Modelos;
using RiskFirst.Hateoas.Models;

namespace Aplicacion.Seguridad
{
    public class UsuarioData : LinkContainer
    {
        public string NombreCompleto { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Imagen { get; set; }
        public Direccion Direccion { get; set; }
    }
    public class Direccion
    {
        public string Barrio { get; set; }
        public string DireccionCasa { get; set; }
        public List<LinksGetCurse> LinksDire { get; set; }

    }

}