using Autofac;
using SecurityWeakness.Domain.Extensibility.Services;
using SecurityWeakness.Domain.Services;

namespace SecurityWeakness.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SecureProductService>().As<ISecureProductService>();
            builder.RegisterType<NotSecureProductService>().As<INotSecureProductService>();
        }
    }
}