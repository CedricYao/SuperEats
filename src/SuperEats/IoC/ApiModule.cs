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

            var userRepo = new UserRepository();
            builder.RegisterInstance(userRepo).As<IUserRepository>();
            
            var menuItemRepo = new MenuItemRepository();
            builder.RegisterInstance(menuItemRepo).As<IMenuItemRepository>();

            var orderRepo = new OrderRepository();
            builder.RegisterInstance(orderRepo).As<IOrderRepository>();

            var orderItemRepo = new OrderItemRepository();
            builder.RegisterInstance(orderItemRepo).As<IOrderItemRepository>();

            var deliveryDriverRepo = new DeliveryDriverRepository();
            builder.RegisterInstance(deliveryDriverRepo).As<IDeliveryDriverRepository>();

            var deliveryRepo = new DeliveryRepository();
            builder.RegisterInstance(deliveryRepo).As<IDeliveryRepository>();

            var addressRepo = new AddressRepository();
            builder.RegisterInstance(addressRepo).As<IAddressRepository>();

            var paymentRepo = new PaymentRepository();
            builder.RegisterInstance(paymentRepo).As<IPaymentRepository>();

            var ratingRepo = new RatingRepository();
            builder.RegisterInstance(ratingRepo).As<IRatingRepository>();

            var restaurantDiscountRepo = new RestaurantDiscountRepository();
            builder.RegisterInstance(restaurantDiscountRepo).As<IRestaurantDiscountRepository>();

            var SuperEatsDiscountRepo = new SuperEatsDiscountRepository();
            builder.RegisterInstance(SuperEatsDiscountRepo).As<ISuperEatsDiscountRepository>();

            var couponRepo = new CouponRepository();
            builder.RegisterInstance(couponRepo).As<ICouponRepository>();
        }
    }
}