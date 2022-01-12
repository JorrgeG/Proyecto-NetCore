using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.InstructorQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController : MiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<InstructorModel>> ObtenerInstructores()
        {
            return await Mediator.Send(new Consulta.Lista());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CrearInstructor(NuevoInstructor.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> ActualizarAsync(Guid id, EditarInstructor.Ejecuta data)
        {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }
    }
}