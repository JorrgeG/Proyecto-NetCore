using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursoOnlineContext context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Jorge GGG", UserName = "Kucu", Email = "jorge@gmail.com" };
                await userManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}