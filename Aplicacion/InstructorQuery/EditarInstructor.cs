using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.InstructorQuery
{
    public class EditarInstructor
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
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.Titulo).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _instructorR;

            public Manejador(IInstructor instructorR)
            {
                _instructorR = instructorR;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructorR.Actualizar(request.InstructorId, request.Nombre, request.Apellido, request.Titulo);
                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se puedo actualizar la data del instructor");
            }
        }

    }
}