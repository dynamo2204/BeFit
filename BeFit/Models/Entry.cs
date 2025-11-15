using System.ComponentModel.DataAnnotations;

    namespace BeFit.Models
    {
        public class Entry
        {
            public int Id { get; set; }
            [MaxLength(255)]
            public string Name { get; set; }
            [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
            public float Amount { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
            public DateOnly Date { get; set; }
            [Display(Name = "Category")]
            public int CategoryId { get; set; }
            [Display(Name = "Category")]
            public virtual Category? Category { get; set; }
            [Display(Name = "Created by")]
            public string CreatedById { get; set; }
            [Display(Name = "Created by")]
            public virtual AppUser? CreatedBy { get; set; }
        }

    }