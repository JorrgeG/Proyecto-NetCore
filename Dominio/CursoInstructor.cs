using System;

namespace Dominio
{
    public class CursoInstructor
    {
        public Guid CursoId { get; set; }

        public Curso Curso { get; set; }
        public Guid InstructorId { get; set; }

        //COMANDO PARA MIGRACION: dontnet ef migrations add AgregarColumnasFecha -p .\Persistencia\ -s .\WebAPI\
        //public DateTime? FechaCreacion { get; set; }

        public Instructor Instructor { get; set; }
    }
}
