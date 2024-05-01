// MIT License
using DotNetNuke.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace DnnReactDemo.Entities
{
    /// <summary>
    /// Represents an Mission entity.
    /// </summary>
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
