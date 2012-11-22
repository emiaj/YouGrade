using FubuMVC.Authentication;
using FubuMVC.Core;

namespace YouGrade
{
    public class YouGradeRegistry : FubuRegistry
    {
        public YouGradeRegistry()
        {
            Import<ApplyAuthentication>(x => x.Exclude(chain => chain.Route != null && chain.Route.Pattern != null && chain.Route.Pattern.StartsWith("_")));
        }
    }
}