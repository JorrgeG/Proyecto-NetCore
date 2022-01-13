using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.InstructorQuery
{
    public class ConsultaPorId
    {
        public class Ejecuta : IRequest<InstructorModel>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, InstructorModel>
        {
            private readonly IInstructor _instructorR;
            public Manejador(IInstructor instructor)
            {
                _instructorR = instructor;
            }
            public async Task<InstructorModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructorR.ObtenerInstructorPorId(request.Id);
                if (result == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el instrucor" });
                }
                return result;
            }
        }
    }
}