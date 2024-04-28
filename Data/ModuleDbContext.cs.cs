// MIT License

using DnnReactDemo.Entities;
using System.Data.Entity;

namespace DnnReactDemo.Data
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
        /// Gets or sets the module.
        /// </summary>
        public DbSet<Mission> Missions { get; set; }

    }
}
