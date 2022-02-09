using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tulumba.Data
{
    /* This is used if database provider does't define
     * ITulumbaDbSchemaMigrator implementation.
     */
    public class NullTulumbaDbSchemaMigrator : ITulumbaDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}