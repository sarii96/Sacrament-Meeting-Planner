using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sacrament_Meeting_Planner.Data;
using Sacrament_Meeting_Planner.Models;
using System.Web;
using System.Dynamic;

namespace Sacrament_Meeting_Planner
{
    public class SacramentMeetingPlansController : Controller
    {
        private readonly Sacrament_Meeting_PlannerContext _context;

        public SacramentMeetingPlansController(Sacrament_Meeting_PlannerContext context)
        {
            _context = context;
        }

        // GET: SacramentMeetingPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.SacramentMeetingPlan.ToListAsync());
        }

        // GET: SacramentMeetingPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dynamic model = new ExpandoObject();

            var sacramentMeetingPlan = await _context.SacramentMeetingPlan
                .FirstOrDefaultAsync(m => m.SacramentMeetingPlanId == id);

            var speakersPlan = await _context.Speakers
             .Where(m => m.SacramentMeetingPlanId == id).ToListAsync();

            model.sacrament = sacramentMeetingPlan;
            model.speakers = speakersPlan;


            if (sacramentMeetingPlan == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: SacramentMeetingPlans/Create
        public IActionResult Create()
        {
            var response = _context.SacramentMeetingPlan.OrderByDescending(i => i.SacramentMeetingPlanId).FirstOrDefault();
            int id = response.SacramentMeetingPlanId + 1;
            TempData["id"] = id;

            return View();
        }

        // POST: SacramentMeetingPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SacramentMeetingPlanId,Date,BishopricName,OpeningSong,Invocation,SacramentHymn,IntermediateSong,ClosingSong,Benediction")] SacramentMeetingPlan sacramentMeetingPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sacramentMeetingPlan);
                await _context.SaveChangesAsync();
                
                TempData["id"] = sacramentMeetingPlan.SacramentMeetingPlanId;
                
                return RedirectToAction("Create", "Speakers");
                //return RedirectToAction(nameof(Index));
            }
            return View(sacramentMeetingPlan);
        }

        // GET: SacramentMeetingPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["id"] = id;

            if (id == null)
            {
                return NotFound();
            }

            var sacramentMeetingPlan = await _context.SacramentMeetingPlan.FindAsync(id);
            if (sacramentMeetingPlan == null)
            {
                return NotFound();
            }
            return View(sacramentMeetingPlan);
        }

        // POST: SacramentMeetingPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SacramentMeetingPlanId,Date,BishopricName,OpeningSong,Invocation,SacramentHymn,IntermediateSong,ClosingSong,Benediction")] SacramentMeetingPlan sacramentMeetingPlan)
        {
            if (id != sacramentMeetingPlan.SacramentMeetingPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sacramentMeetingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SacramentMeetingPlanExists(sacramentMeetingPlan.SacramentMeetingPlanId))
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
            return View(sacramentMeetingPlan);
        }

        // GET: SacramentMeetingPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentMeetingPlan = await _context.SacramentMeetingPlan
                .FirstOrDefaultAsync(m => m.SacramentMeetingPlanId == id);
            if (sacramentMeetingPlan == null)
            {
                return NotFound();
            }

            return View(sacramentMeetingPlan);
        }

        // POST: SacramentMeetingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sacramentMeetingPlan = await _context.SacramentMeetingPlan.FindAsync(id);
            _context.SacramentMeetingPlan.Remove(sacramentMeetingPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SacramentMeetingPlanExists(int id)
        {
            return _context.SacramentMeetingPlan.Any(e => e.SacramentMeetingPlanId == id);
        }
    }
}
