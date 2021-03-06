﻿using FubuLocalization;
using FubuMVC.Authentication;
using FubuMVC.Core;
using FubuMVC.Core.UI;
using FubuMVC.Localization;
using FubuMVC.Navigation;
using YouGrade.Features.Home;
using YouGrade.Infrastructure;
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
                .ConstrainToHttpMethod(x => x.Method.Name == "Post", "POST")
                .IgnoreControllerNamespaceEntirely()
                .IgnoreClassSuffix("Endpoint")
                .IgnoreMethodsNamed("Get")
                .IgnoreMethodsNamed("Post")
                .HomeIs<HomeEndpoint>(x => x.Get(null));

            Policies
                .Add(x =>
                {
                    x.Where
                        .AnyActionMatches(action => action.Method.DeclaringType.Assembly == typeof(YouGradeRegistry).Assembly)
                        .Or.InputTypeIs<LoginRequest>();
                    x.Wrap.WithBehavior<IncludeBoostrapSet>();
                });


            Policies
                .Add<NavigationRegistry>(x =>
                    {
                        x.ForMenu(StringToken.FromKeyString("Navigation:Default", "Default"));
                        x.Add += MenuNode.ForInput<HomeInputModel>(StringToken.FromKeyString("Navigation:Default:Home", "Home"));

                        x.ForMenu(StringToken.FromKeyString("Navigation:Profile", "Profile"));
                        x.Add += MenuNode.ForInput<LogoutRequest>("Log Out");
                    });


            Services(x => x.SetServiceIfNone<IRandomStringGenerator, RandomStringGenerator>());

            AlterSettings<AuthenticationSettings>(x => x.ExcludeChains.ChainMatches(chain => chain.Route != null && chain.Route.Pattern != null && chain.Route.Pattern.StartsWith("_")));
            
            Import<DefaultHtmlConventions>(
                x => x.Editors.If(r => r.Accessor.InnerProperty.Name.Contains("Password"))
                         .ModifyWith(r => r.CurrentTag.Attr("type", "password")));

            Import<DefaultHtmlConventions>(x => x.Labels.Always.ModifyWith(e => e.CurrentTag.Text(e.Accessor.ToHeader())));

            Import<BasicLocalizationSupport>();

        }
    }
}