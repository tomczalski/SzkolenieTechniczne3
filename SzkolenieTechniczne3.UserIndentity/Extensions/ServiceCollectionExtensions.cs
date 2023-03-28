using Microsoft.Extensions.DependencyInjection;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Mapping;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Services.Interfaces;
using SzkolenieTechniczne3.UserIdentity.CrossCutting.Services;

namespace SzkolenieTechniczne3.UserIndentity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection) 
        {
            serviceCollection.AddSingleton<RoleToRoleDtoMapping>();
            serviceCollection.AddSingleton<RoleDtoToRoleMapping>();
            serviceCollection.AddSingleton<UserToUserDtoMapping>();
            serviceCollection.AddSingleton<UserDtoToUserMapping>();

            serviceCollection.AddTransient<IRoleService, RoleService>();
            return serviceCollection;
        }
    }
}
