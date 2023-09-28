using Autofac;
using Ardalis.SharedKernel;
using ContactManagement.Infrastructure.Data;

namespace ContactManagement.Infrastructure;

public class AutofacInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterEf(builder);
    }

    private void RegisterEf(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .InstancePerLifetimeScope();
    }
}