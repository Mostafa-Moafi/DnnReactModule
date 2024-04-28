// MIT License
using System.ComponentModel.DataAnnotations;

namespace DnnReactDemo.Entities
{
    /// <summary>
    /// Represents an Mission entity.
    /// </summary>
    public class Mission : BaseEntity
    {
        /// <summary>
        /// Gets or sets the item name.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the item description.
        /// </summary>
        public string Description { get; set; }
    }
}
