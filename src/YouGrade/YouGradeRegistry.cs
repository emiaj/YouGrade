using FubuMVC.Authentication;
using FubuMVC.Core;
using FubuMVC.Core.UI;
using YouGrade.Features.Home;
using YouGrade.Policies.Asset;

namespace YouGrade
{
    public class YouGradeRegistry : FubuRegistry
    {
        public YouGradeRegistry()
        {
            Actions.IncludeClassesSuffixedWithEndpoint();

            Routes
                .ConstrainToHttpMethod(x => x.Method.Name == "Get", "GET")
                .IgnoreControllerNamespaceEntirely()
                .IgnoreClassSuffix("Endpoint")
                .IgnoreMethodsNamed("Get")
                .IgnoreMethodsNamed("Post")
                .HomeIs<HomeEndpoint>(x => x.Get(null));

            Policies.Add(x =>
                {
                    x.Where
                        .AnyActionMatches(action => action.Method.DeclaringType.Assembly == typeof(YouGradeRegistry).Assembly)
                        .Or.InputTypeIs<LoginRequest>();
                    x.Wrap.WithBehavior<IncludeBoostrapSet>();
                });

            Import<ApplyAuthentication>(x => x.Exclude(chain => chain.Route != null && chain.Route.Pattern != null && chain.Route.Pattern.StartsWith("_")));

            Import<DefaultHtmlConventions>(
                x =>
                x.Editors.If(r => r.Accessor.InnerProperty.Name.Contains("Password"))
                .ModifyWith(r => r.CurrentTag.Attr("type", "password")));

        }
    }
}