using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            public List<Guid> LisaInstructor { get; set; }
            public decimal Precio { get; set; }
            public decimal Promocion { get; set; }

        }

        //Clase que ejecuta la validacion de los campos
        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.Titulo)
                .NotEmpty();
                RuleFor(x => x.Descripcion)
                .NotEmpty();
                RuleFor(x => x.FechaPublicacion)
                .NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursoOnlineContext _context;
            public Manejador(CursoOnlineContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var _curso = Guid.NewGuid();
                var curso = new Curso
                {
                    CursoId = _curso,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };
                _context.Curso.Add(curso);

                if (request.LisaInstructor != null)
                {
                    foreach (var id in request.LisaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor
                        {
                            CursoId = _curso,
                            InstructorId = id
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                //Logica para agregar precio en el curso
                var precioEntidad = new Precio
                {
                    CursoId = _curso,
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion,
                    PrecioId = Guid.NewGuid()
                };

                _context.Precio.Add(precioEntidad);

                var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el curso");
            }
        }

    }
}