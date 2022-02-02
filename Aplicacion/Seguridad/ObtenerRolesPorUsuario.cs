using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class ObtenerRolesPorUsuario
    {
        public class Ejecuta : IRequest<List<string>>
        {
            public string UserName { get; set; }
        }


        public class Manejador : IRequestHandler<Ejecuta, List<string>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<Usuario> _userManager;
            public Manejador(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuarioIden = await _userManager.FindByNameAsync(request.UserName);
                if (usuarioIden == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Usuario no existe" });
                }

                var result = await _userManager.GetRolesAsync(usuarioIden);
                return new List<string>(result);
            }
        }
    }
}