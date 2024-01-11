using Autofac;
using FirstDemo.Areas.Admin.Models;
using FirstDemo.Models;

namespace FirstDemo
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnicodeSmsSender>().As<ISmsSender>();
            builder.RegisterType<CourseCreateModel>().AsSelf();
            builder.RegisterType<CourseListModel>().AsSelf();
            builder.RegisterType<CourseUpdateModel>().AsSelf();
        }
    }
}
