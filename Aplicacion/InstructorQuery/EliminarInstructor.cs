using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.InstructorQuery
{
    public class EliminarInstructor
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _instructorR;
            public Manejador(IInstructor instructor)
            {
                _instructorR = instructor;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructorR.Eliminar(request.Id);
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el instructor");
            }
        }
    }
}