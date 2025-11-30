using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa æwiczenia jest wymagana")]
        [MaxLength(255, ErrorMessage = "Nazwa nie mo¿e przekraczaæ 255 znaków")]
        [Display(Name = "Nazwa æwiczenia")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Opis nie mo¿e przekraczaæ 1000 znaków")]
        [Display(Name = "Opis", Description = "Opcjonalny opis æwiczenia")]
        public string? Description { get; set; }

        public ICollection<CompletedExercise> CompletedExercises { get; set; } = new List<CompletedExercise>();
    }
}