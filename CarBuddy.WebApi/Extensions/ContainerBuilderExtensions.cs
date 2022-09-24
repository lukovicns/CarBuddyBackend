using Autofac;
using CarBuddy.Application.Contracts;
using CarBuddy.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;

namespace CarBuddy.WebApi.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterDependencies(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("CarBuddy.Application"))
               .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Validator"))
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("CarBuddy.Persistence"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("CarBuddy.WebApi"))
                .Where(t => t.Name.EndsWith("Service")
                    || t.Name.EndsWith("Handler")
                    || t.Name.EndsWith("Generator")
                    || t.Name.EndsWith("Validator")
                    || t.Name.EndsWith("Hasher"));

            builder.RegisterType<JwtSecurityTokenHandler>()
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtTokenGenerator>()
                .As<IJwtTokenGenerator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtTokenValidator>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PasswordHasher>()
                .As<IPasswordHasher>()
                .InstancePerLifetimeScope();
        }
    }
}
