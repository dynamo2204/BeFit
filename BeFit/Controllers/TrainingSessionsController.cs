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
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _context.TrainingSession
                .Include(t => t.CreatedBy)
                .Where(t => t.CreatedById == userId)
                .OrderByDescending(t => t.StartTime)
                .ToListAsync();
            return View(sessions);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSession
                .Include(t => t.CreatedBy)
                .Include(t => t.CompletedExercises)
                    .ThenInclude(ce => ce.ExerciseType)
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }


        public IActionResult Create()
        {
            return View(new TrainingSession { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,Notes")] TrainingSession trainingSession)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            trainingSession.CreatedById = userId;

            if (trainingSession.EndTime <= trainingSession.StartTime)
            {
                ModelState.AddModelError("EndTime", "Czas zakoñczenia musi byæ póŸniejszy ni¿ czas rozpoczêcia");
            }

            ModelState.Remove("CreatedById");

            if (ModelState.IsValid)
            {
                _context.Add(trainingSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingSession);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSession
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedById == userId);

            if (trainingSession == null)
            {
                return NotFound();
            }
            return View(trainingSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,Notes")] TrainingSession trainingSession)
        {
            if (id != trainingSession.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingSession = await _context.TrainingSession
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedById == userId);

            if (existingSession == null)
            {
                return NotFound();
            }

            trainingSession.CreatedById = userId;

            if (trainingSession.EndTime <= trainingSession.StartTime)
            {
                ModelState.AddModelError("EndTime", "Czas zakoñczenia musi byæ póŸniejszy ni¿ czas rozpoczêcia");
            }


            ModelState.Remove("CreatedById");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingSessionExists(trainingSession.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainingSession);
        }

        // GET: TrainingSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSession
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id && m.CreatedById == userId);

            if (trainingSession == null)
            {
                return NotFound();
            }

            return View(trainingSession);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainingSession = await _context.TrainingSession
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedById == userId);

            if (trainingSession != null)
            {
                _context.TrainingSession.Remove(trainingSession);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TrainingSessionExists(int id)
        {
            return _context.TrainingSession.Any(e => e.Id == id);
        }
    }
}