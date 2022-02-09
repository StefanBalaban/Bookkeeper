using System.Threading.Tasks;

namespace Tulumba.Data
{
    public interface ITulumbaDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}