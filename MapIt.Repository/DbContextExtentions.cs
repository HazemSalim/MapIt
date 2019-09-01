using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace MapIt.Repository
{
    public static class DbContextExtentions
    {
        /// <summary>
        /// Exposes the ObjectContext from DbContext
        /// </summary>
        public static ObjectContext ToObjectContext(this DbContext dbContext)
        {
            return (dbContext as IObjectContextAdapter).ObjectContext;
        }
    }
}
