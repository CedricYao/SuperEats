using Autofac;
using Module = Autofac.Module;

namespace SuperEats.IoC
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var restaurantRepo = new RestaurantRepository();
            builder.RegisterInstance(restaurantRepo).As<IRestaurantRepository>();
        }
    }
}