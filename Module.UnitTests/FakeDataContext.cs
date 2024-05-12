using DnnReactModule.Data;
using DnnReactModule.Entities;
using Effort.Provider;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;

namespace Module.UnitTests
{
    public class FakeDataContext : IDisposable
    {
        public EffortConnection connection;
        public ModuleDbContext dataContext;

        private bool _disposed = false;

        public FakeDataContext()
        {
            this.connection = Effort.DbConnectionFactory.CreateTransient();
            this.dataContext = new ModuleDbContext(this.connection);
        }
        public class TestDataContext : ModuleDbContext
        {
            public TestDataContext(DbConnection connection)
                : base(connection)
            {
            }

            public DbSet<Mission> Missions { get; set; }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                this.dataContext.Dispose();
                this.connection.Dispose();
            }

            this.dataContext = null;
            this.connection = null;

            _disposed = true;
        }

    }
    public class Mission : BaseEntity
    {
        /// <summary>
        /// Gets or sets the item Title.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the item Title.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the item description.
        /// </summary>
        public string Description { get; set; }
    }

}
