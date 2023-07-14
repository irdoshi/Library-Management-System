namespace LMS.Common.ViewModels
    {
    using System;
 
    using System.ComponentModel.DataAnnotations;

    public class ViewBookWithDate
            {

        [Required]

        public int BookId { get; set; }
        
        [StringLength(2083)]
        public string ImageUrl { get; set; }
       
        public string Title { get; set; }

            [Required]

            public string Author { get; set; }

            [RegularExpression(@"^\d+\.\d{0,2}$")]
            public double Price { get; set; }

            [Required]
            public string Genre { get; set; }

            [Required]
            public DateTime DueDate { get; set; }

            }
        }
