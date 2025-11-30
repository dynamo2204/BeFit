using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class CompletedExercise
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Typ æwiczenia jest wymagany")]
        [Display(Name = "Typ æwiczenia")]
        public int ExerciseTypeId { get; set; }

        [Display(Name = "Typ æwiczenia")]
        public virtual ExerciseType? ExerciseType { get; set; }

        [Required(ErrorMessage = "Sesja treningowa jest wymagana")]
        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }

        [Display(Name = "Sesja treningowa")]
        public virtual TrainingSession? TrainingSession { get; set; }

        [Required(ErrorMessage = "Obci¹¿enie jest wymagane")]
        [Range(0, 1000, ErrorMessage = "Obci¹¿enie musi byæ w zakresie 0-1000 kg")]
        [Display(Name = "Obci¹¿enie (kg)", Description = "U¿yte obci¹¿enie w kilogramach")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Liczba serii jest wymagana")]
        [Range(1, 100, ErrorMessage = "Liczba serii musi byæ w zakresie 1-100")]
        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Liczba powtórzeñ jest wymagana")]
        [Range(1, 1000, ErrorMessage = "Liczba powtórzeñ musi byæ w zakresie 1-1000")]
        [Display(Name = "Liczba powtórzeñ", Description = "Liczba powtórzeñ w jednej serii")]
        public int Reps { get; set; }

        [MaxLength(500, ErrorMessage = "Notatki nie mog¹ przekraczaæ 500 znaków")]
        [Display(Name = "Notatki", Description = "Opcjonalne notatki o æwiczeniu")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }

        [Required]
        [Display(Name = "Utworzono przez")]
        public string CreatedById { get; set; }

        [Display(Name = "Utworzono przez")]
        public virtual AppUser? CreatedBy { get; set; }
    }
}