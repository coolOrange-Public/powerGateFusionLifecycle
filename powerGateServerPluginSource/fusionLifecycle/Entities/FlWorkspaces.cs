using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using powerGateServer.SDK;

namespace fusionLifecycle
{
    [DataServiceKey("Id")]
    [DataServiceEntity]
    public class FusionLifecycleWorkspace
    {
        public long Id { get; set; }
        public string Category { get; set; }
        public string Label { get; set; }
        public string Uri { get; set; }
    }

    public class FusionLifecycleWorkspaces : ServiceMethod<FusionLifecycleWorkspace>
    {
        public override string Name
        {
            get { return "FlWorkspaces"; }
        }

        public override IEnumerable<FusionLifecycleWorkspace> Query(IExpression<FusionLifecycleWorkspace> expression)
        {
            List<FusionLifecycleWorkspace> ws = new List<FusionLifecycleWorkspace>();
            var flWorkspaces = FlHelper.instance.GetWorkspaces();
            foreach (var flws in flWorkspaces.List.Data)
                ws.Add(new FusionLifecycleWorkspace() { Category = flws.Data.Category.ToString(), Id = flws.Data.Id, Label = flws.Data.Label, Uri = flws.Uri });
            return ws;
        }

        public override void Update(FusionLifecycleWorkspace entity)
        {
        }

        public override void Create(FusionLifecycleWorkspace entity)
        {
        }

        public override void Delete(FusionLifecycleWorkspace entity)
        {
        }
    }
}
