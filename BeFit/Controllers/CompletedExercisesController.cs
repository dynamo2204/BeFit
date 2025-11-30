using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BeFit.Controllers
{
    [Authorize]
    public class CompletedExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompletedExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercises = await _context.CompletedExercise
                .Include(c => c.CreatedBy)
                .Include(c => c.ExerciseType)
                .Include(c => c.TrainingSession)
                .Where(c => c.CreatedById == userId)
                .OrderByDescending(c => c.TrainingSession.StartTime)
                .ToListAsync();
            return View(exercises);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var completedExercise = await _context.CompletedExercise
                .Include(c => c.CreatedBy)
                .Include(c => c.ExerciseType)
                .Include(c => c.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (completedExercise == null)
            {
                return NotFound();
            }

            return View(completedExercise);
        }

        public IActionResult Create(int? sessionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name");
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSession.Where(t => t.CreatedById == userId), 
                "Id", 
                "StartTime",
                sessionId);

            return View(new CompletedExercise { TrainingSessionId = sessionId ?? 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExerciseTypeId,TrainingSessionId,Weight,Sets,Reps,Notes")] CompletedExercise completedExercise)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            

            completedExercise.CreatedById = userId;

            var session = await _context.TrainingSession
                .FirstOrDefaultAsync(t => t.Id == completedExercise.TrainingSessionId && t.CreatedById == userId);

            if (session == null)
            {
                ModelState.AddModelError("TrainingSessionId", "Nieprawid³owa sesja treningowa");
            }

            ModelState.Remove("CreatedById");

            if (ModelState.IsValid)
            {
                _context.Add(completedExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "TrainingSessions", new { id = completedExercise.TrainingSessionId });
            }

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", completedExercise.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSession.Where(t => t.CreatedById == userId), 
                "Id", 
                "StartTime", 
                completedExercise.TrainingSessionId);
            return View(completedExercise);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var completedExercise = await _context.CompletedExercise
                .FirstOrDefaultAsync(c => c.Id == id && c.CreatedById == userId);

            if (completedExercise == null)
            {
                return NotFound();
            }

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", completedExercise.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSession.Where(t => t.CreatedById == userId), 
                "Id", 
                "StartTime", 
                completedExercise.TrainingSessionId);
            return View(completedExercise);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExerciseTypeId,TrainingSessionId,Weight,Sets,Reps,Notes")] CompletedExercise completedExercise)
        {
            if (id != completedExercise.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingExercise = await _context.CompletedExercise
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.CreatedById == userId);

            if (existingExercise == null)
            {
                return NotFound();
            }

            completedExercise.CreatedById = userId;


            var session = await _context.TrainingSession
                .FirstOrDefaultAsync(t => t.Id == completedExercise.TrainingSessionId && t.CreatedById == userId);

            if (session == null)
            {
                ModelState.AddModelError("TrainingSessionId", "Nieprawid³owa sesja treningowa");
            }

            ModelState.Remove("CreatedById");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(completedExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompletedExerciseExists(completedExercise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "TrainingSessions", new { id = completedExercise.TrainingSessionId });
            }

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseType, "Id", "Name", completedExercise.ExerciseTypeId);
            ViewData["TrainingSessionId"] = new SelectList(
                _context.TrainingSession.Where(t => t.CreatedById == userId), 
                "Id", 
                "StartTime", 
                completedExercise.TrainingSessionId);
            return View(completedExercise);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var completedExercise = await _context.CompletedExercise
                .Include(c => c.CreatedBy)
                .Include(c => c.ExerciseType)
                .Include(c => c.TrainingSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (completedExercise == null)
            {
                return NotFound();
            }

            return View(completedExercise);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var completedExercise = await _context.CompletedExercise
                .FirstOrDefaultAsync(c => c.Id == id && c.CreatedById == userId);

            if (completedExercise != null)
            {
                var sessionId = completedExercise.TrainingSessionId;
                _context.CompletedExercise.Remove(completedExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "TrainingSessions", new { id = sessionId });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CompletedExerciseExists(int id)
        {
            return _context.CompletedExercise.Any(e => e.Id == id);
        }
    }
}