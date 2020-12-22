using Microsoft.AspNetCore.Identity;
namespace Dominio
{
    public class Usuario : IdentityUser
    {
        public string NombreCompleto { get; set; }
        public string Barrio { get; set; }
        public string DireccionCasa { get; set; }
    }
}