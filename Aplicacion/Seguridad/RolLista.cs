using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class RolLista
    {
        public class Ejecuta : IRequest<List<IdentityRole>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<IdentityRole>>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly CursoOnlineContext _context;
            public Manejador(RoleManager<IdentityRole> roleManager, CursoOnlineContext context)
            {
                _roleManager = roleManager;
                _context = context;
            }
            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var roles = await _context.Roles.ToListAsync();
                return roles;
            }
        }
    }
}