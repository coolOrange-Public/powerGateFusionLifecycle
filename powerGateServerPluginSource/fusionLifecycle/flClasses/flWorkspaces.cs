using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusionLifecycle
{
    public partial class FlWorkspaces
    {
        [JsonProperty("list")]
        public FlWsList List { get; set; }
    }

    public partial class FlWsList
    {
        [JsonProperty("data")]
        public FlWsDataArray[] Data { get; set; }
    }

    public partial class FlWsDataArray
    {
        [JsonProperty("data")]
        public FlWsData Data { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public partial class FlWsData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public enum Category { BasicWorkspace, BasicWorkspaceWithWorkflow, RevisionControlledWorkspace, RevisioningWorkspace, SupplierManagementWithWorkflow };
}
