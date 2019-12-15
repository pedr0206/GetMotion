using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GetFitness02.Data;
using GetFitness02.Models.Activity;
using Microsoft.AspNetCore.Identity;
using GetFitness02.Models;

namespace GetFitness02.Controllers
{
    public class ActivityEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
      
       
        public ActivityEntriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           

        }

        // GET: ActivityEntries
        public async Task<IActionResult> Index()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var currentUser = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.ActivityEntry.Include(a => a.ActivityItem)
                //Include(a => a.ApplicationUser);
            .Where(a => a.ApplicationUserId == currentUser.Id);
        
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ActivityEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEntry = await _context.ActivityEntry
                .Include(a => a.ActivityItem)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ActivityEntryId == id);
            if (activityEntry == null)
            {
                return NotFound();
            }

            return View(activityEntry);
        }

        // GET: ActivityEntries/Create
        public IActionResult Create()
        {
            ViewData["ActivityItemId"] = new SelectList(_context.ActivityItem, "ActivityItemId", "ActivityName");
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        // POST: ActivityEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityEntryId,Date,Duration,Height,Weight,ActivityItemId")] ActivityEntry activityEntry)
        {
            if (ModelState.IsValid)
            {

                var currentUser = await _userManager.GetUserAsync(User);
                //var currentUser = await _userManager.GetUserAsync(User);
                //var applicationDbContext = _context.ActivityEntry.Include(a => a.ActivityItem).
                //    Include(a => a.ApplicationUser)
                //    .Where(a => a.ApplicationUserId == currentUser.Id);
                activityEntry.ApplicationUserId = currentUser.Id;
                _context.Add(activityEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityItemId"] = new SelectList(_context.ActivityItem, "ActivityItemId", "ActivityName", activityEntry.ActivityItemId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", activityEntry.ApplicationUserId);
            return View(activityEntry);
        }

        // GET: ActivityEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEntry = await _context.ActivityEntry.FindAsync(id);
            if (activityEntry == null)
            {
                return NotFound();
            }
            ViewData["ActivityItemId"] = new SelectList(_context.ActivityItem, "ActivityItemId", "ActivityName", activityEntry.ActivityItemId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", activityEntry.ApplicationUserId);
            return View(activityEntry);
        }

        // POST: ActivityEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityEntryId,Date,Duration,Height,Weight,ActivityItemId,ApplicationUserId")] ActivityEntry activityEntry)
        {
            if (id != activityEntry.ActivityEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityEntryExists(activityEntry.ActivityEntryId))
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
            ViewData["ActivityItemId"] = new SelectList(_context.ActivityItem, "ActivityItemId", "ActivityName", activityEntry.ActivityItemId);
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", activityEntry.ApplicationUserId);
            return View(activityEntry);
        }

        // GET: ActivityEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityEntry = await _context.ActivityEntry
                .Include(a => a.ActivityItem)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.ActivityEntryId == id);
            if (activityEntry == null)
            {
                return NotFound();
            }

            return View(activityEntry);
        }

        // POST: ActivityEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityEntry = await _context.ActivityEntry.FindAsync(id);
            _context.ActivityEntry.Remove(activityEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityEntryExists(int id)
        {
            return _context.ActivityEntry.Any(e => e.ActivityEntryId == id);
        }
    }
}
