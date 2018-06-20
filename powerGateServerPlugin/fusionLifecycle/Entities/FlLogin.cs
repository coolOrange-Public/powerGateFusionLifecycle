using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using powerGateServer.SDK;

namespace fusionLifecycle
{
    [DataServiceKey("tenant")]
    [DataServiceEntity]
    public class FusionLifecycleLoginClass
    {
        public string tenant { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        //public FlCredentials credentials { get; set; }
    }

    public partial class FlCredentials
    {
        public string Userid { get; set; }
        public string Sessionid { get; set; }
        public string CustomerToken { get; set; }
        public AuthStatus AuthStatus { get; set; }
    }

    public partial class FlAuthStatus
    {
        public long Id { get; set; }
        public string Description { get; set; }
    }

    public class FusionLifecycleLogin : ServiceMethod<FusionLifecycleLoginClass>
    {
        public override string Name
        {
            get { return "FlLogin"; }
        }

        public override IEnumerable<FusionLifecycleLoginClass> Query(IExpression<FusionLifecycleLoginClass> expression)
        {
            return new List<FusionLifecycleLoginClass>();
        }

        public override void Update(FusionLifecycleLoginClass entity)
        {
        }

        public override void Create(FusionLifecycleLoginClass entity)
        {
            FlHelper.instance.Login(entity.tenant, entity.user, entity.password);
        }

        public override void Delete(FusionLifecycleLoginClass entity)
        {
        }
    }
}
