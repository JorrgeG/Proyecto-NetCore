using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDto>>
        {

        }

        //HANDLER
        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {
            private readonly CursoOnlineContext _context;
            public Manejador(CursoOnlineContext context)
            {
                _context = context;
            }

            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.Include(x => x.InstructorsLink).ThenInclude(x => x.Instructor).ToListAsync();
                return curso;
            }
        }
    }
}