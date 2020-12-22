using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Modelos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class GetAllUsers
    {
        public class Ejecutar : IRequest<List<UsuarioData>> { }

        public class Manejador : IRequestHandler<Ejecutar, List<UsuarioData>>
        {
            private readonly CursoOnlineContext _context;
            public Manejador(CursoOnlineContext context)
            {
                _context = context;
            }

            public async Task<List<UsuarioData>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var usurario = await _context.Users.ToListAsync();
                List<UsuarioData> List = new List<UsuarioData>();
                foreach (var items in usurario)
                {
                    List.Add(new UsuarioData
                    {
                        NombreCompleto = items.NombreCompleto,
                        UserName = items.UserName,
                        Email = items.Email,
                        Direccion = new Direccion { Barrio = items.Barrio, DireccionCasa = items.DireccionCasa }

                    });
                }

                return List;
            }
        }
    }
}