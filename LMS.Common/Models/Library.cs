//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.Models
    { 
    using System.ComponentModel.DataAnnotations;
 
    public partial class Library
            {
            [Key]
            public int LibraryId { get; set; }
            public int LocationId { get; set; }
            }
        }

