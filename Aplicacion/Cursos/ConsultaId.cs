using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            public Guid Id { get; set; }
        }

        //Handler
        public class Manejador : IRequestHandler<CursoUnico, CursoDto>
        {
            private readonly CursoOnlineContext _context;
            private readonly IMapper _mapper;
            public Manejador(CursoOnlineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.Include(x => x.InstructorsLink).ThenInclude(y => y.Instructor).FirstOrDefaultAsync(a => a.CursoId == request.Id);
                if (curso == null)
                {
                    //throw new Exception("No se puede eliminar");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Nose encontro el Curso" });
                }

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);
                return cursoDto;
            }
        }
    }
}