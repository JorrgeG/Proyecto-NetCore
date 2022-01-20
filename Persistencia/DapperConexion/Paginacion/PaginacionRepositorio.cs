using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginacionRepositorio(IFactoryConnection connection)
        {
            _factoryConnection = connection;
        }
        public async Task<PaginacionModel> devolverPaginacion(string storeProcedureName, int numeroPagina, int cantidadElementos, IDictionary<string, object> parametrosFiltro, string ordenamientoColumna)
        {
            PaginacionModel paginacionModel = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters parameters = new DynamicParameters();

                foreach (var item in parametrosFiltro)
                {
                    parameters.Add("@" + item.Key, item.Value);
                }

                //Parametros de entrada
                parameters.Add("@NumeroPagina", numeroPagina);
                parameters.Add("@CantidadElementos", cantidadElementos);
                parameters.Add("@Ordenamiento", ordenamientoColumna);

                //Parametros de salida
                parameters.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.ListaRecords = listaReporte;
                paginacionModel.NumeroPaginas = parameters.Get<int>("@TotalPaginas");
                paginacionModel.TotalRecords = parameters.Get<int>("@TotalRecords");
            }
            catch (System.Exception e)
            {
                throw new System.Exception("No se puedo ejecutar el procedimiento almacenado" + e.Message);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return paginacionModel;
        }
    }
}