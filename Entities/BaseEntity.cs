// MIT License

using System;
using System.ComponentModel.DataAnnotations;

namespace DnnReactDemo.Entities
{
    /// <summary>
    /// Base entity to provide common properties to other entities and allow it's usage in generic repositories.
    /// </summary>
    public class BaseEntity : IEntity
    {
        /// <inheritdoc/>
        [Key]
        public int Id { get; set; }

        /// <inheritdoc/>
        public DateTime? UpdatedDate { get; set; }

        /// <inheritdoc/>
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
