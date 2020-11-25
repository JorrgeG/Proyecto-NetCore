using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int Id { get; set; }
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
                var curso = await _context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    throw new System.Exception("No se puede eliminar");
                }
                _context.Remove(curso);

                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new System.Exception("No se pudieron guardar los cambios");

            }
        }
    }
}