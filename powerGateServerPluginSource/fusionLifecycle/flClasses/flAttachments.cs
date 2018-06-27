using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusionLifecycle
{
    public partial class FlAttachment
    {
        [JsonProperty("list")]
        public FlList List { get; set; }
    }

    public partial class FlList
    {
        [JsonProperty("data")]
        public FlDatum[] Data { get; set; }
    }

    public partial class FlDatum
    {
        [JsonProperty("file")]
        public FlFile File { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public partial class FlFile
    {
        [JsonProperty("workspaceID")]
        public long WorkspaceId { get; set; }

        [JsonProperty("dmsID")]
        public long DmsId { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("versionID")]
        public long VersionId { get; set; }

        [JsonProperty("fileID")]
        public long FileId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("resourceName")]
        public string ResourceName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("fileVersion")]
        public long FileVersion { get; set; }

        [JsonProperty("fileStatus")]
        public FlFileStatus FileStatus { get; set; }

        [JsonProperty("createdUserID")]
        public string CreatedUserId { get; set; }

        [JsonProperty("createdDisplayName")]
        public string CreatedDisplayName { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("folderId")]
        public long FolderId { get; set; }

        [JsonProperty("fileStorageKeyHash")]
        public long FileStorageKeyHash { get; set; }
    }

    public partial class FlFileStatus
    {
        [JsonProperty("statusID")]
        public long StatusId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class FlNewAttachement
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("resourceName")]
        public string ResourceName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

}
