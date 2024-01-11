using Autofac;
using FirstDemo.Application.Features.Training;
using FirstDemo.Domain.Features.Training;

namespace FirstDemo.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseManagementService>().As<ICourseManagementService>()
                 .InstancePerLifetimeScope();
        }
    }
}
