using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IList<InstructorModel>> ObtenerLista();

        Task<InstructorModel> ObtenerInstructorPorId(Guid id);

        Task<int> Nuevo(InstructorModel instructorModel);
        Task<int> Actualizar(InstructorModel instructorModel);
        Task<int> Eliminar(Guid id);
    }
}