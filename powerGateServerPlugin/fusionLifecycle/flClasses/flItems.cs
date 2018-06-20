using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusionLifecycle
{
    public partial class FlItems
    {
        [JsonProperty("list")]
        public FlItemList List { get; set; }
    }

    public partial class FlItemList
    {
        [JsonProperty("item")]
        public FlItem[] Item { get; set; }
    }

    public partial class FlListItem
    {
        [JsonProperty("item")]
        public FlItem Item { get; set; }
    }

    public partial class FlItem
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("details")]
        public FlDetails Details { get; set; }

        [JsonProperty("metaFields")]
        public FlMetaFields MetaFields { get; set; }

        [JsonProperty("relations")]
        public FlRelations Relations { get; set; }
    }

    public partial class FlDetails
    {
        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }

        [JsonProperty("workspaceID")]
        public long WorkspaceId { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("versionID")]
        public long VersionId { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("release")]
        public string Release { get; set; }

        [JsonProperty("timeStamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("owner")]
        public FlOwner Owner { get; set; }

        [JsonProperty("additionalOwners")]
        public FlAdditionalOwners AdditionalOwners { get; set; }

        [JsonProperty("lifecycleState")]
        public FlLifecycleState LifecycleState { get; set; }

        [JsonProperty("lifecycleStatus")]
        public string LifecycleStatus { get; set; }

        [JsonProperty("latest")]
        public bool Latest { get; set; }

        [JsonProperty("working")]
        public bool Working { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("dmsID")]
        public long DmsId { get; set; }
    }

    public partial class FlAdditionalOwners
    {
        [JsonProperty("group")]
        public object Group { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }
    }

    public partial class FlLifecycleState
    {
        [JsonProperty("stateID")]
        public long StateId { get; set; }

        [JsonProperty("stateName")]
        public string StateName { get; set; }

        [JsonProperty("effectivity")]
        public bool Effectivity { get; set; }
    }

    public partial class FlOwner
    {
        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userNumber")]
        public long UserNumber { get; set; }
    }

    public partial class FlMetaFields
    {
        [JsonProperty("entry")]
        public FlMetaFieldsEntry[] Entry { get; set; }
    }

    public partial class FlMetaFieldsEntry
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("fieldData")]
        public FlFieldData FieldData { get; set; }
    }

    public partial class FlFieldData
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("formattedValue", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedValue { get; set; }

        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
    }

    public partial class FlValueItem
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("details")]
        public FlDetails Details { get; set; }

        [JsonProperty("relations")]
        public FlRelations Relations { get; set; }
    }

    public partial class FlValue
    {
        [JsonProperty("item")]
        public FlValueItem[] Item { get; set; }
    }

    public partial class FlRelationsEntry
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public FlValue Value { get; set; }
    }

    public partial class FlRelations
    {
        [JsonProperty("entry")]
        public FlRelationsEntry[] Entry { get; set; }
    }

    public partial class NewFlItem
    {
        [JsonProperty("metaFields")]
        public NewFlItemMetaFields MetaFields { get; set; }
    }

    public partial class NewFlItemMetaFields
    {
        [JsonProperty("entry")]
        public List<NewFlItemMetaFieldsEntry> Entry { get; set; }
        public NewFlItemMetaFields()
        {
            Entry = new List<NewFlItemMetaFieldsEntry>();
        }
    }

    public partial class NewFlItemMetaFieldsEntry
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
