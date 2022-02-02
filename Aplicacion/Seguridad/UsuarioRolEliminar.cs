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
    public class UsuarioRolEliminar
    {
        public class Ejecuta : IRequest
        {
            public string UserName { get; set; }
            public string RolNombre { get; set; }
        }
        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<Usuario> _userManager;
            public Manejador(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if (role == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Rol no existe" });
                }

                var usuarioIden = await _userManager.FindByNameAsync(request.UserName);
                if (usuarioIden == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Usuario no existe" });
                }

                var result = await _userManager.RemoveFromRoleAsync(usuarioIden, request.RolNombre);
                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se pudo el Rol al usuario" });
            }
        }
    }
}