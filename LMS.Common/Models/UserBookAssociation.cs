//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.Models
    {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UserBookAssociation
        {
        [Key]
        public int Id 
            { 
            get; 
            set; 
            }
        public int UserId 
            { 
            get; 
            set; 
            }
        public int BookLibraryAssociationId 
            { 
            get; 
            set; 
            }
        [Column(TypeName = "date")]
        public DateTime? DueDate 
            { 
            get; 
            set; 
            }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserBookAssociation")]
        public virtual User User 
            { 
            get; 
            set; 
            }
        }
    }
