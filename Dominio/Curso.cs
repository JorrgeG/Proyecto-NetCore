using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Curso
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }

        //COMANDO PARA MIGRACION: dontnet ef migrations add AgregarColumnasFecha -p .\Persistencia\ -s .\WebAPI\
        public DateTime? FechaCreacion { get; set; }
        public Precio PrecioPromocion { get; set; }
        public ICollection<Comentario> ComentarioLista { get; set; }

        public ICollection<CursoInstructor> InstructorsLink { get; set; }
    }
}