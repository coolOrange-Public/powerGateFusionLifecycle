using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Newtonsoft.Json;
using powerGateServer.SDK;
using RestSharp;

namespace fusionLifecycle
{
    [DataServiceKey("WorkspaceId", "Id")]
    [DataServiceEntity]
    public class FusionLifecycleItem
    {
        public long WorkspaceId { get; set; }
        public string Uri { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public string LifecycleStatus { get; set; }
        public DateTime LastModified { get; set; }
        public string Revision { get; set; }
        public long Version { get; set; }
        public string Owner { get; set; }
        public List<FusionLifecycleItemProperty> Properties { get; set; }
        public List<FusionLifecycleItemRelation> Relations { get; set; }
        
        public FusionLifecycleItem()
        {
            Properties = new List<FusionLifecycleItemProperty>();
            Relations = new List<FusionLifecycleItemRelation>();
        }
    }

    

    public class FusionLifecycleItems : ServiceMethod<FusionLifecycleItem>
    {
        public override string Name
        {
            get { return "FlItems"; }
        }

        private FusionLifecycleItem FlItem2PgItem(FlItem flItem)
        {
            var pgItem = new FusionLifecycleItem {
                WorkspaceId = flItem.Details.WorkspaceId,
                Id = flItem.Id,
                Description = flItem.Description,
                Uri = flItem.Uri,
                LifecycleStatus = flItem.Details.LifecycleStatus,
                LastModified = flItem.Details.LastModified,
                Revision = flItem.Details.Release,
                Owner = flItem.Details.Owner.Id,
                Version = flItem.Details.Version
            };
            foreach(var prop in flItem.MetaFields.Entry)
            {
                pgItem.Properties.Add(new FusionLifecycleItemProperty() { Name=prop.Key, Value=prop.FieldData.Value, WorkspaceId=flItem.Details.WorkspaceId, Id=flItem.Id });
            }
            foreach(var entry in flItem.Relations.Entry)
                if(entry.Value != null)
                foreach(var item in entry.Value.Item)
                {
                    pgItem.Relations.Add(new FusionLifecycleItemRelation() { Id=item.Id, WorkspaceId=item.Details.WorkspaceId });
                }
            return pgItem;
        }

        public override IEnumerable<FusionLifecycleItem> Query(IExpression<FusionLifecycleItem> expression)
        {
            List<FusionLifecycleItem> items = new List<FusionLifecycleItem>();
            var wsId = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("WorkspaceId"));
            if (wsId != null)
            {
                long workspaceId = long.Parse(wsId.Value.ToString());
                var iId = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("Id"));
                if (iId != null)
                {
                    long itemId = long.Parse(iId.Value.ToString());
                    var flitem = FlHelper.instance.GetItem(workspaceId, itemId);
                    items.Add(FlItem2PgItem(flitem.Item));
                }
                else
                {
                    var flItems = FlHelper.instance.GetItems(workspaceId);
                    foreach (var flitem in flItems.List.Item)
                        items.Add(FlItem2PgItem(flitem));
                }
            }
            return items;
        }

        public override void Update(FusionLifecycleItem entity)
        {
            
        }

        public override void Create(FusionLifecycleItem entity)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            foreach (var prop in entity.Properties)
                properties.Add(prop.Name, prop.Value);
            FlHelper.instance.AddItem(entity.WorkspaceId,properties);
        }

        public override void Delete(FusionLifecycleItem entity)
        {
        }
    }

    [DataServiceKey("WorkspaceId", "Id", "Name")]
    [DataServiceEntity]
    public class FusionLifecycleItemProperty
    {
        public long WorkspaceId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class FusionLifecycleItemProperties : ServiceMethod<FusionLifecycleItemProperty>
    {
        public override string Name
        {
            get { return "FlItemProperties"; }
        }

        public override IEnumerable<FusionLifecycleItemProperty> Query(IExpression<FusionLifecycleItemProperty> expression)
        {
            return null;
        }

        public override void Update(FusionLifecycleItemProperty entity)
        {
        }

        public override void Create(FusionLifecycleItemProperty entity)
        {
        }

        public override void Delete(FusionLifecycleItemProperty entity)
        {
        }
    }

    [DataServiceKey("WorkspaceId", "Id")]
    [DataServiceEntity]
    public class FusionLifecycleItemRelation
    {
        public long WorkspaceId { get; set; }
        public long Id { get; set; }
    }

    public class FusionLifecycleItemRelations : ServiceMethod<FusionLifecycleItemRelation>
    {
        public override string Name
        {
            get { return "FlItemRelations"; }
        }

        public override IEnumerable<FusionLifecycleItemRelation> Query(IExpression<FusionLifecycleItemRelation> expression)
        {
            return null;
        }

        public override void Update(FusionLifecycleItemRelation entity)
        {
        }

        public override void Create(FusionLifecycleItemRelation entity)
        {
        }

        public override void Delete(FusionLifecycleItemRelation entity)
        {
        }
    }
}
