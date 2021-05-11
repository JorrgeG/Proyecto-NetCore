using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            private readonly IMapper _mapper;
            public Manejador(CursoOnlineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso
                .Include(x => x.ComentarioLista)
                .Include(x => x.PrecioPromocion)
                .Include(x => x.InstructorsLink).ThenInclude(x => x.Instructor).ToListAsync();

                var CursoDto = _mapper.Map<List<Curso>, List<CursoDto>>(curso);

                return CursoDto;
            }
        }
    }
}