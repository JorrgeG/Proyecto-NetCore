using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepositorio : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;
        public InstructorRepositorio(IFactoryConnection connection)
        {
            _factoryConnection = connection;
        }
        public Task<int> Actualizar(InstructorModel instructorModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Nuevo(string nombre, string apellido, string titulo)
        {
            try
            {
                var storeProcedure = "usp_instructor_nuevo";
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(storeProcedure, new
                {
                    InstructorId = Guid.NewGuid(),
                    Nombre = nombre,
                    Apellidos = apellido,
                    Titulo = titulo
                },
                commandType: CommandType.StoredProcedure);
                _factoryConnection.CloseConnection();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo guardar el nuevo instructor", ex);
            }
        }

        public Task<InstructorModel> ObtenerInstructorPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";
            try
            {
                var connections = _factoryConnection.GetConnection();
                instructorList = await connections.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos Obtner instrctor", e);
            }
            finally // este Finally siempre se va a ejecutar asi entre al Try o al Catch,, siempre se va a ejuctar despues de los anteriores
            {
                _factoryConnection.CloseConnection();
            }
            return instructorList;
        }
    }
}