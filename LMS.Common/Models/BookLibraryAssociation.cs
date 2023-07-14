namespace LMS.Common.Models
    {
    using System.ComponentModel.DataAnnotations;
    public partial class BookLibraryAssociation
        {
        [Key]
        public int BookLibraryAssociationId { get; set; }
        public int BookId { get; set; }
        public int LibraryId { get; set; }
        [Required]
        [StringLength(5)]
        public string IsAvailable { get; set; }
        [Required]
        [StringLength(5)]
        public string IsCheckedOut { get; set; }
        }
    }