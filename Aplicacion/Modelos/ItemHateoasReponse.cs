using System.Collections.Generic;
using Aplicacion.Seguridad;
using Newtonsoft.Json;
using RiskFirst.Hateoas.Models;

namespace Aplicacion.Modelos
{
    public class ItemHateoasReponse : ILinkContainer
    {
        public UsuarioData Data;
        private Dictionary<string, Link> _links;

        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links
        {
            get => _links ?? (_links = new Dictionary<string, Link>());
            set => _links = value;
        }

        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }

    }
}