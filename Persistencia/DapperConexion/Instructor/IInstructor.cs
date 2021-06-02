using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();

        Task<InstructorModel> ObtenerInstructorPorId(Guid id);

        Task<int> Nuevo(string nombre, string apellido, string titulo);
        Task<int> Actualizar(InstructorModel instructorModel);
        Task<int> Eliminar(Guid id);
    }
}