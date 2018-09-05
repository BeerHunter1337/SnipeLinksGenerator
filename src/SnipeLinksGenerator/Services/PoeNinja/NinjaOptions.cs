using System.Collections.Generic;
using SnipeLinksGenerator.Services.Core;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaOptions : BaseApiClientOptions
    {
        public Dictionary<string, string> Endpoints { get; set; }
    }
}
