using System;
using System.Web.Routing;
using FubuMVC.Core;
using FubuMVC.StructureMap;

namespace YouGrade
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs args)
        {
            FubuApplication
                .For<YouGradeRegistry>()
                .StructureMapObjectFactory(x => x.AddRegistry<IoCRegistry>())
                .Bootstrap(RouteTable.Routes);
        }
    }
}
