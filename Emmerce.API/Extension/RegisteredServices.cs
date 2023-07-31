using Ecommerce.API.Profiles;
using Ecommerce.API.Service.Implementation;
using Ecommerce.API.Service.Interface;
using Ecommerce.Data.Repository.Implementation;
using Ecommerce.Data.Repository.Interface;
using Emmerce.API.Service.Implementation;
using Emmerce.API.Service.Interface;

namespace DipoleBank.Extension
{
    public static class RegisteredServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IDeliveryRepo, DeliveryRepo>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddAutoMapper(typeof(ProjectProfile));
            services.AddScoped<IEmailServices, EmailService>();
        }
    }
}
