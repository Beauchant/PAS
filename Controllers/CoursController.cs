using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAS.Data;
using PAS.Models;

namespace PAS.Controllers
{
    public class CoursController : Controller
    {
        private readonly PASDbContext _context;

        public CoursController(PASDbContext context)
        {
            _context = context;
        }

        // GET: Cours
        public async Task<IActionResult> Index()
        {
            var pASDbContext = _context.Cours.Include(c => c.Classe);
            return View(await pASDbContext.ToListAsync());
        }

        // GET: Cours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cours == null)
            {
                return NotFound();
            }

            var cour = await _context.Cours
                .Include(c => c.Classe)
                .FirstOrDefaultAsync(m => m.CourId == id);
            if (cour == null)
            {
                return NotFound();
            }

            return View(cour);
        }

        // GET: Cours/Create
        public IActionResult Create()
        {
            ViewData["ClasseId"] = new SelectList(_context.Classes, "ClasseId", "ClasseId");
            return View();
        }

        // POST: Cours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourId,NomCours,Description,ClasseId")] Cour cour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClasseId"] = new SelectList(_context.Classes, "ClasseId", "ClasseId", cour.ClasseId);
            return View(cour);
        }

        // GET: Cours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cours == null)
            {
                return NotFound();
            }

            var cour = await _context.Cours.FindAsync(id);
            if (cour == null)
            {
                return NotFound();
            }
            ViewData["ClasseId"] = new SelectList(_context.Classes, "ClasseId", "ClasseId", cour.ClasseId);
            return View(cour);
        }

        // POST: Cours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourId,NomCours,Description,ClasseId")] Cour cour)
        {
            if (id != cour.CourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourExists(cour.CourId))
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
            ViewData["ClasseId"] = new SelectList(_context.Classes, "ClasseId", "ClasseId", cour.ClasseId);
            return View(cour);
        }

        // GET: Cours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cours == null)
            {
                return NotFound();
            }

            var cour = await _context.Cours
                .Include(c => c.Classe)
                .FirstOrDefaultAsync(m => m.CourId == id);
            if (cour == null)
            {
                return NotFound();
            }

            return View(cour);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cours == null)
            {
                return Problem("Entity set 'PASDbContext.Cours'  is null.");
            }
            var cour = await _context.Cours.FindAsync(id);
            if (cour != null)
            {
                _context.Cours.Remove(cour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourExists(int id)
        {
          return (_context.Cours?.Any(e => e.CourId == id)).GetValueOrDefault();
        }
    }
}
