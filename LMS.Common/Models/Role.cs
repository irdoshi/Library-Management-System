//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.Models
    { 
    using System.ComponentModel.DataAnnotations;
 
    public partial class Role
    { 
    [Key]
    public int RoleId { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    }
}
