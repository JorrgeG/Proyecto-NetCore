using System;
using System.Collections.Generic;
using Dominio;

namespace Aplicacion.Cursos
{
    public class CursoDto
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }
        public Precio PrecioPromocion { get; set; }

        public ICollection<InstructorDto> CursoLink { get; set; }
    }
}