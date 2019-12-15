using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GetFitness02.Data;
using GetFitness02.Models.Activity;

namespace GetFitness02.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActivityItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ActivityItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActivityItem.ToListAsync());
        }

        // GET: Admin/ActivityItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityItem = await _context.ActivityItem
                .FirstOrDefaultAsync(m => m.ActivityItemId == id);
            if (activityItem == null)
            {
                return NotFound();
            }

            return View(activityItem);
        }

        // GET: Admin/ActivityItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ActivityItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityItemId,ActivityName,Calorie")] ActivityItem activityItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityItem);
        }

        // GET: Admin/ActivityItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityItem = await _context.ActivityItem.FindAsync(id);
            if (activityItem == null)
            {
                return NotFound();
            }
            return View(activityItem);
        }

        // POST: Admin/ActivityItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityItemId,ActivityName,Calorie")] ActivityItem activityItem)
        {
            if (id != activityItem.ActivityItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityItemExists(activityItem.ActivityItemId))
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
            return View(activityItem);
        }

        // GET: Admin/ActivityItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityItem = await _context.ActivityItem
                .FirstOrDefaultAsync(m => m.ActivityItemId == id);
            if (activityItem == null)
            {
                return NotFound();
            }

            return View(activityItem);
        }

        // POST: Admin/ActivityItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityItem = await _context.ActivityItem.FindAsync(id);
            _context.ActivityItem.Remove(activityItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityItemExists(int id)
        {
            return _context.ActivityItem.Any(e => e.ActivityItemId == id);
        }
    }
}
