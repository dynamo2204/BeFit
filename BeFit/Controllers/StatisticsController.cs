using BeFit.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var exercises = await _context.CompletedExercise
                .Include(ce => ce.ExerciseType)
                .Include(ce => ce.TrainingSession)
                .Where(ce => ce.CreatedById == userId && ce.TrainingSession.StartTime >= fourWeeksAgo)
                .ToListAsync();

            var stats = exercises
                .GroupBy(ce => new { ce.ExerciseTypeId, ce.ExerciseType.Name })
                .Select(g => new ExerciseStatistics
                {
                    ExerciseName = g.Key.Name,
                    TotalOccurrences = g.Count(),
                    TotalReps = g.Sum(ce => ce.Sets * ce.Reps),
                    AverageWeight = g.Average(ce => ce.Weight),
                    MaxWeight = g.Max(ce => ce.Weight)
                })
                .OrderByDescending(s => s.TotalOccurrences)
                .ToList();

            return View(stats);
        }
    }

    public class ExerciseStatistics
    {
        public string ExerciseName { get; set; }
        public int TotalOccurrences { get; set; }
        public int TotalReps { get; set; }
        public decimal AverageWeight { get; set; }
        public decimal MaxWeight { get; set; }
    }
}