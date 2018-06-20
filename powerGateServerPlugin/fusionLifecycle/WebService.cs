using powerGateServer.SDK;

namespace fusionLifecycle
{
    [WebServiceData("coolOrange", "FL_SVC")]
    public class FLService : WebService
    {
        public FLService()
        {
            AddMethod(new FusionLifecycleWorkspaces());
            AddMethod(new FusionLifecycleLogin());
            AddMethod(new FusionLifecycleItems());
            AddMethod(new FusionLifecycleItemProperties());
            AddMethod(new FusionLifecycleItemRelations());
        }
    }
}