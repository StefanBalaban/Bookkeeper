using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tulumba.Data;
using Volo.Abp.DependencyInjection;

namespace Tulumba.EntityFrameworkCore
{
    public class EntityFrameworkCoreTulumbaDbSchemaMigrator
        : ITulumbaDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreTulumbaDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the TulumbaDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<TulumbaDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}