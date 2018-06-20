using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusionLifecycle
{
    public partial class FlLogin
    {
        [JsonProperty("userid")]
        public string Userid { get; set; }

        [JsonProperty("sessionid")]
        public string Sessionid { get; set; }

        [JsonProperty("customerToken")]
        public string CustomerToken { get; set; }

        [JsonProperty("authStatus")]
        public AuthStatus AuthStatus { get; set; }
    }

    public partial class AuthStatus
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
