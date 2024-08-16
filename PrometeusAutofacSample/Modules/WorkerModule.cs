using Autofac;
using PrometeusAutofacSample.Services;
using PrometeusAutofacSample.Services.Abstract;

namespace PrometeusAutofacSample.Modules
{
    public class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var env = ctx.Resolve<IHostEnvironment>();
                return env.IsDevelopment()
                    ? (IWorkerService)new WorkerServiceA()
                    : new WorkerServiceB();
            }).As<IWorkerService>().InstancePerLifetimeScope();

            builder.RegisterType<BackgroundMetricsService>().As<IHostedService>().SingleInstance();
        }
    }
}

