using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Modelos;
using Aplicacion.Seguridad;
using Dominio;
using HateoasNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;

namespace WebAPI.Controllers
{
    [AllowAnonymous]//Para desautorizar y para que sea publico
    public class UsuarioController : MiControllerBase
    {
        private readonly ILinksService _linksService;
        private readonly IHateoas _hateoas;
        public UsuarioController(ILinksService linksService, IHateoas hateoas)
        {
            _linksService = linksService;
            _hateoas = hateoas;
        }
        //http:localhost:5000/api/Usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [Links(Policy = "FullInfoPolicy")]
        [HttpGet(Name = "DevovlerUsuario")]
        public async Task<ActionResult<UsuarioData>> DevovlerUsuario()
        {
            var result = await Mediator.Send(new UsuarioActual.Ejecutar());
            //var links = CreateLinksUser();

            //result.Links = links;
            //result.Direccion.LinksDire = links;

            var hateoas = new ItemHateoasReponse { Data = result };
            await _linksService.AddLinksAsync(result);

            return Ok(result);
        }

        [HttpGet("all-user", Name = "get-members")]
        public async Task<ActionResult<List<UsuarioData>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsers.Ejecutar());
            //var links = CreateLinksUser();
            // foreach (var item in result)
            // {
            //     item.Links = links;

            // }


            var links = _hateoas.Generate(result);




            /*var hateoasresult = new List<ItemHateoasReponse>();
            foreach (var item in result)
            {

                var hateoas = new ItemHateoasReponse { Data = item };
                await _linksService.AddLinksAsync(hateoas);
                hateoasresult.Add(hateoas);
            }*/

            return Ok(new { data = result, Heateoas = links });
        }

        private List<LinksGetCurse> CreateLinksUser()
        {
            var var = "hola feo";
            var result = 123123;
            var link = new List<LinksGetCurse>();
            link.Add(
                new LinksGetCurse(
                    Url.Link("Devolver", new { var, result }),
                    "Self",
                    "GET"
                )
            );
            return link;
        }
    }
}