using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class RolController : MiControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> CrearAsync(RolNuevo.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<IdentityRole>>> GetRoles()
        {
            return await Mediator.Send(new RolLista.Ejecuta());
        }

        [HttpPost("agregarRoleUsuario")]
        public async Task<ActionResult<Unit>> AgregarRolUser(UsuarioRolAgregar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpPost("eliminarRoleUsuario")]
        public async Task<ActionResult<Unit>> EliminarRolUser(UsuarioRolEliminar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpGet("obtenerRolesPorUsuario/{username}")]
        public async Task<ActionResult<List<string>>> GetRolesPorUsuarios(string username)
        {
            return await Mediator.Send(new ObtenerRolesPorUsuario.Ejecuta { UserName = username });
        }
    }
}
