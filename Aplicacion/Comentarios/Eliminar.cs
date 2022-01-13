using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursoOnlineContext _cursoOnlineContext;
            public Manejador(CursoOnlineContext cursoOnlineContext)
            {
                _cursoOnlineContext = cursoOnlineContext;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _cursoOnlineContext.Comentario.FindAsync(request.Id);
                if (result == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "no se encontro comentario" });
                }
                _cursoOnlineContext.Remove(result);
                var num = await _cursoOnlineContext.SaveChangesAsync();
                if (num > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puedo eliminar comentaio");
            }
        }
    }
}