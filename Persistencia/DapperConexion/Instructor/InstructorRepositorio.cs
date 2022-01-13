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
        public async Task<int> Actualizar(Guid instructorId, string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_editar";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        InstructorId = instructorId,
                        Nombre = nombre,
                        Apellidos = apellidos,
                        Titulo = titulo
                    },
                    commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo editar la data del instructor. ", e);
            }
        }

        public async Task<int> Eliminar(Guid id)
        {
            var storeProcedure = "usp_instructor_eliminar";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new
                    {
                        InstructorId = id
                    },
                    commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return result;
            }
            catch (System.Exception e)
            {
                throw new Exception("No se puedo eliminar el instructor " + e.Message);
            }
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

        public async Task<InstructorModel> ObtenerInstructorPorId(Guid id)
        {
            var storeProcedure = "usp_obtener_instructor_por_id";
            InstructorModel instructorList = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                instructorList = await connection.QueryFirstAsync<InstructorModel>(
                    storeProcedure,
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return instructorList;
            }
            catch (System.Exception e)
            {
                throw new Exception("No se puedo eliminar el instructor " + e.Message);
            }
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