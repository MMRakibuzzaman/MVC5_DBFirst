using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.CustomValidation;

namespace WebApplication1.Models.ViewModels
{
    public class PlayerVM
    {
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Player Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateLessThanToday(ErrorMessage = "You cannot select a future date!")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(18, 45, ErrorMessage = "Player must be between 18 and 45 years old.")]
        public int Age { get; set; }

        public string Picture { get; set; }

        [Display(Name = "Upload Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Captain")]
        public bool IsCaptain { get; set; }

        public List<int> TeamList { get; set; } = new List<int>();
    }

}