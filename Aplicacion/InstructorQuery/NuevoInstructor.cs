using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.InstructorQuery
{
    public class NuevoInstructor
    {
        public class Ejecuta : IRequest
        {
            public Guid InstructorId { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Titulo { get; set; }
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>
        {
            public EjecutaValida()
            {
                RuleFor(x => x.Nombre)
                .NotEmpty();
                RuleFor(x => x.Apellido)
                .NotEmpty();
                RuleFor(x => x.Titulo)
                .NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _instructor;

            public Manejador(IInstructor instructor)
            {
                _instructor = instructor;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructor.Nuevo(request.Nombre, request.Apellido, request.Titulo);

                if (result <= 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puedo realizar la accion de insertar el Instructor");
            }
        }
    }
}