using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenManager>().As<ITokenService>().InstancePerLifetimeScope();

            builder.RegisterType<RefreshTokenManager>().As<IRefreshTokenService>().InstancePerLifetimeScope();
            builder.RegisterType<EfRefreshTokenRepository>().As<IRefreshTokenRepository>().InstancePerLifetimeScope();

            builder.RegisterType<SliderManager>().As<ISliderService>().InstancePerLifetimeScope();
            builder.RegisterType<EfSliderRepository>().As<ISliderRepository>().InstancePerLifetimeScope();

            builder.RegisterType<BrandManager>().As<IBrandService>().InstancePerLifetimeScope();
            builder.RegisterType<EfBrandRepository>().As<IBrandRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AddressManager>().As<IAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<EfAddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ReviewManager>().As<IReviewService>().InstancePerLifetimeScope();
            builder.RegisterType<EfReviewRepository>().As<IReviewRepository>().InstancePerLifetimeScope();

            builder.RegisterType<OptionManager>().As<IOptionService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOptionRepository>().As<IOptionRepository>().InstancePerLifetimeScope();

            builder.RegisterType<OptionValueManager>().As<IOptionValueService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOptionValueRepository>().As<IOptionValueRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<EfCategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ProductManager>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<EfProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();

            builder.RegisterType<OrderManager>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ProductImageManager>().As<IProductImageService>().InstancePerLifetimeScope();
            builder.RegisterType<EfProductImageRepository>().As<IProductImageRepository>().InstancePerLifetimeScope();

            builder.RegisterType<BasketManager>().As<IBasketService>().InstancePerLifetimeScope();
            builder.RegisterType<EfBasketRepository>().As<IBasketRepository>().InstancePerLifetimeScope();

            builder.RegisterType<BasketItemManager>().As<IBasketItemService>().InstancePerLifetimeScope();
            builder.RegisterType<EfBasketItemRepository>().As<IBasketItemRepository>().InstancePerLifetimeScope();

            builder.RegisterType<OrderStatusManager>().As<IOrderStatusService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOrderStatusRepository>().As<IOrderStatusRepository>().InstancePerLifetimeScope();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg => {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                            {
                                Selector = new AspectInterceptorSelector()
                            }).SingleInstance().InstancePerDependency();
        }
    }
}
