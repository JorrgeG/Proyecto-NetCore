using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.Nombre)
                .NotEmpty();
                RuleFor(x => x.Apellido)
                .NotEmpty();
                RuleFor(x => x.Email)
                .NotEmpty();
                RuleFor(x => x.Password)
                .NotEmpty();
            }
        }

        public class Mnejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursoOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Mnejador(CursoOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync(); //comaparamos que ingresa el usuario con lo que esta en ala bd
                if (existe)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El email ingresado ya existe" });
                }
                var existeUserName = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (existeUserName)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe ya un usuariocon este username" });
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellido,
                    Email = request.Email,
                    UserName = request.UserName
                };

                //Metodo para inserter 
                var result = await _userManager.CreateAsync(usuario, request.Password);
                if (result.Succeeded)
                {
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        UserName = usuario.UserName,
                        Email = usuario.Email
                    };
                }
                throw new Exception("No se pudo agregar al nuevo usuario");
            }
        }
    }
}