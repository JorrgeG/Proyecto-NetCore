using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConexion.Instructor;

namespace Aplicacion.InstructorQuery
{
    public class Consulta
    {
        public class Lista : IRequest<IEnumerable<InstructorModel>>
        {

        }

        public class Manejador : IRequestHandler<Lista, IEnumerable<InstructorModel>>
        {
            private readonly IInstructor _instructorRepository;

            public Manejador(IInstructor instructor)
            {
                _instructorRepository = instructor;
            }
            public async Task<IEnumerable<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                return await _instructorRepository.ObtenerLista();
            }
        }
    }
}