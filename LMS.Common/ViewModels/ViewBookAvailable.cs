//-----------------------------------------------------------------------
// <copyright file="ViewModels.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.ViewModels
    {
    using System.ComponentModel.DataAnnotations;
    public class ViewBookAvailable
        {
        [Required]
        public int BookId { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public double Price { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string IsAvailable { get; set; }
        }
    }



