using System.Collections.Generic;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaOptions
    {
        public string League { get; set; }

        public string BaseUrl { get; set; }

        public Dictionary<string, string> Endpoints { get; set; }
    }
}
