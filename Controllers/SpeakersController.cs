using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sacrament_Meeting_Planner.Data;
using Sacrament_Meeting_Planner.Models;

namespace Sacrament_Meeting_Planner
{
    public class SpeakersController : Controller
    {
        private readonly Sacrament_Meeting_PlannerContext _context;

        public SpeakersController(Sacrament_Meeting_PlannerContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            int newId = (int)TempData["id"];
            TempData.Keep("id");

            var sacrament_Meeting_PlannerContext = _context.Speakers.Include(s => s.SacramentMeetingPlan).Where(i => i.SacramentMeetingPlanId == newId);
            return View(await sacrament_Meeting_PlannerContext.ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .Include(s => s.SacramentMeetingPlan)
                .FirstOrDefaultAsync(m => m.SpeakersId == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["SacramentMeetingPlanId"] = new SelectList(_context.SacramentMeetingPlan, "SacramentMeetingPlanId", "SacramentMeetingPlanId");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakersId,MemberName,Topic,SacramentMeetingPlanId")] Speakers speakers)
        {
            if (ModelState.IsValid)
            {
                int newId = (int)TempData["id"];
                speakers.SacramentMeetingPlanId = newId;
                _context.Add(speakers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SacramentMeetingPlanId"] = new SelectList(_context.SacramentMeetingPlan, "SacramentMeetingPlanId", "SacramentMeetingPlanId", speakers.SacramentMeetingPlanId);
            return View(speakers);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers.FindAsync(id);
            if (speakers == null)
            {
                return NotFound();
            }
            ViewData["SacramentMeetingPlanId"] = new SelectList(_context.SacramentMeetingPlan, "SacramentMeetingPlanId", "SacramentMeetingPlanId", speakers.SacramentMeetingPlanId);
            return View(speakers);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakersId,MemberName,Topic,SacramentMeetingPlanId")] Speakers speakers)
        {
            if (id != speakers.SpeakersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speakers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakersExists(speakers.SpeakersId))
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
            ViewData["SacramentMeetingPlanId"] = new SelectList(_context.SacramentMeetingPlan, "SacramentMeetingPlanId", "SacramentMeetingPlanId", speakers.SacramentMeetingPlanId);
            return View(speakers);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .Include(s => s.SacramentMeetingPlan)
                .FirstOrDefaultAsync(m => m.SpeakersId == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speakers = await _context.Speakers.FindAsync(id);
            _context.Speakers.Remove(speakers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakersExists(int id)
        {
            return _context.Speakers.Any(e => e.SpeakersId == id);
        }
    }
}
