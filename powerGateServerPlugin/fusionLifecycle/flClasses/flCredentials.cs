using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusionLifecycle
{
    public partial class FlCredentials
    {
        [JsonProperty("userID")]
        public string UserId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
