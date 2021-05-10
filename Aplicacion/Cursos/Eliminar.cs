using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
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
                //Toda la lista de instructores
                var instructorDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach (var instructor in instructorDB)
                {
                    _context.CursoInstructor.Remove(instructor);
                }
                var curso = await _context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    //throw new Exception("No se puede eliminar");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Nose encontro el Curso" });
                }
                _context.Remove(curso);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudieron guardar los cambios");

            }
        }
    }
}