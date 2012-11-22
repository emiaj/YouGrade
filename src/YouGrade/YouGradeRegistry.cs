using System.ComponentModel.DataAnnotations;
using Bottles;
using FubuCore.Reflection;
using FubuMVC.Core;
using FubuMVC.Spark;
using YouGrade.Attributes;
using YouGrade.Behaviors;
using YouGrade.Controllers.Account;
using YouGrade.Controllers.Home;
using YouGrade.Controllers.Home.Models;
using YouGrade.Model;
using YouGrade.Services;

namespace YouGrade
{
    public class YouGradeRegistry : FubuRegistry
    {
        public YouGradeRegistry()
        {
            Actions.IncludeClassesSuffixedWithController();
            
            Routes.IgnoreMethodSuffix("Query");
            Routes.IgnoreMethodSuffix("Command");
            Routes.IgnoreControllerNamespaceEntirely();
            Routes.IgnoreClassNameForType<HomeController>();
            Routes.IgnoreClassNameForType<AccountController>();

            Routes.ConstrainToHttpMethod(x => x.Method.Name.EndsWith("Command"), "POST");
            Routes.ConstrainToHttpMethod(x => x.Method.Name.EndsWith("Query"), "GET");
            
            Routes.HomeIs<HomeController>(x => x.Index(null));

            Policies
                .ConditionallyWrapBehaviorChainsWith<IsAuthenticatedBehavior>(call => call.HandlerType.HasAttribute<AuthenticatedOnlyAttribute>());

            Services(services =>
            {
                services.AddService<IActivator, AutoMapperActivator>();

                services.SetServiceIfNone<IExamManager, ExamManager>();
                services.SetServiceIfNone<IYouGradeService, YouGradeService>();
                services.SetServiceIfNone<YouGradeEntities, YouGradeEntities>();
                services.DefaultServiceFor<YouGradeEntities>().DependencyByValue("name=YouGradeEntities");

                services.SetServiceIfNone<IAuthenticationService, AuthenticationService>();
                services.SetServiceIfNone<IUserService, UserService>();
            });

            HtmlConvention(x => x.Editors.AddClassForAttribute<RequiredAttribute>("required"));
            HtmlConvention(x => x.Editors
                                    .If(accessor => accessor.Accessor.InnerProperty.Name.Equals("Password"))
                                    .Attr("type", "password"));

            this.UseSpark();
            Views.TryToAttachWithDefaultConventions();

            Output.ToJson
             .WhenTheOutputModelIs<GetQuestionQueryOutput>()
             .WhenTheOutputModelIs<SaveAnswerCommandOutput>()
             .WhenTheOutputModelIs<EndExamQueryOutput>();

            IncludeDiagnostics(true);

        }
    }
}