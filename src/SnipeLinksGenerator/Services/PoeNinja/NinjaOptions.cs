using System.Collections.Generic;
using SnipeLinksGenerator.Services.Core;
using SnipeLinksGenerator.Services.PoeNinja.Models;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaOptions : BaseApiClientOptions
    {
        public Endpoint CurrencyEndpoint { get; set; }

        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
