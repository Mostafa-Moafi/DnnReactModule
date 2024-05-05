// MIT License

using System;

namespace DnnReactModule.Entities
{
    /// <summary>
    /// Ensures entities have some common properties.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was first created.
        /// </summary>
        DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was last updated.
        /// </summary>
        DateTime? UpdatedDate { get; set; }
    }
}
