using Autofac;
using SecurityWeakness.Core.Extensibility;

namespace SecurityWeakness.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HtmlSanitizer>().As<IHtmlSanitizer>();
        }
    }
}