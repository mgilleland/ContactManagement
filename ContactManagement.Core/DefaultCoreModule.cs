using Autofac;

namespace ContactManagement.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //TODO: Delete this?
        //builder.RegisterType<DeleteContributorService>()
        //    .As<IDeleteContributorService>().InstancePerLifetimeScope();
    }
}