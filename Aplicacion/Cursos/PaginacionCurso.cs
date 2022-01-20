using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using Persistencia.DapperConexion.Paginacion;

namespace Aplicacion.Cursos
{
    public class PaginacionCurso
    {
        public class Ejecuta : IRequest<PaginacionModel>
        {
            public string Titulo { get; set; }
            public int NumeroPagina { get; set; }
            public int CantidadElementos { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {
            private readonly IPaginacion _paginacion;
            public Manejador(IPaginacion paginacion)
            {
                _paginacion = paginacion;
            }
            public Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var StoredProcedure = "usp_obtener_paginacion";
                var Ordenamiento = "Titulo";
                var parametros = new Dictionary<string, object>();
                parametros.Add("NombreCurso", request.Titulo);

                return _paginacion.devolverPaginacion(StoredProcedure, request.NumeroPagina, request.CantidadElementos, parametros, Ordenamiento);
            }
        }
    }
}