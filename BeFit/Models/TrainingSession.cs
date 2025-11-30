using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data i czas rozpoczêcia s¹ wymagane")]
        [Display(Name = "Rozpoczêcie treningu")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Data i czas zakoñczenia s¹ wymagane")]
        [Display(Name = "Zakoñczenie treningu")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [MaxLength(500, ErrorMessage = "Notatki nie mog¹ przekraczaæ 500 znaków")]
        [Display(Name = "Notatki", Description = "Opcjonalne notatki z treningu")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Required]
        [Display(Name = "Utworzono przez")]
        public string CreatedById { get; set; }

        [Display(Name = "Utworzono przez")]
        public virtual AppUser? CreatedBy { get; set; }

        public ICollection<CompletedExercise> CompletedExercises { get; set; } = new List<CompletedExercise>();
    }
}