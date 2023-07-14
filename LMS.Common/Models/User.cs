//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.Models
    {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
        {
        public User()
            {
            UserBookAssociation = new HashSet<UserBookAssociation>();
            }

        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int LocationId { get; set; }
        public string EmailId { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserBookAssociation> UserBookAssociation { get; set; }
        }
    }
