//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.Models
    {
    using System.ComponentModel.DataAnnotations;

    public partial class Book
        {
            [Required]
            [Key]
            public int BookId 
            { 
            get; 
            set; 
            }
            [Required]
            [StringLength(200)]
            public string Title { 
            get; 
            set; 
            }
            [Required]
            [StringLength(100)]
            public string Author 
            { 
            get; 
            set; 
            }
            public double Price 
            { 
            get; 
            set; 
            }
            [Required]
            [StringLength(50)]
            public string Genre 
            { 
            get; 
            set; 
            }
            [StringLength(2083)]
            public string ImageUrl 
            { 
            get; 
            set; 
            }
        }
        }