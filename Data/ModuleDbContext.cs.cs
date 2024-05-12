// MIT License

using DnnReactModule.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace DnnReactModule.Data
{
    /// <summary>
    /// The data context for this module.
    /// </summary>
    public class ModuleDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDbContext"/> class.
        /// </summary>
        public ModuleDbContext() : base("name=SiteSqlServer")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ModuleDbContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDbContext"/> class.
        /// </summary>
        /// <param name="connection">An existing <see cref="DbConnection"/>.</param>
        public ModuleDbContext(DbConnection connection)
            : base(connection, true)
        {
        }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        public DbSet<Mission> Missions { get; set; }

    }
}
