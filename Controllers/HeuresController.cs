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
    public class HeuresController : Controller
    {
        private readonly PASDbContext _context;

        public HeuresController(PASDbContext context)
        {
            _context = context;
        }

        // GET: Heures
        public async Task<IActionResult> Index()
        {
            var pASDbContext = _context.Heures.Include(h => h.Professeur);
            return View(await pASDbContext.ToListAsync());
        }

        // GET: Heures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Heures == null)
            {
                return NotFound();
            }

            var heure = await _context.Heures
                .Include(h => h.Professeur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heure == null)
            {
                return NotFound();
            }

            return View(heure);
        }

        // GET: Heures/Create
        public IActionResult Create()
        {
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId");
            return View();
        }

        // POST: Heures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,ProfesseurId")] Heure heure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId", heure.ProfesseurId);
            return View(heure);
        }

        // GET: Heures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Heures == null)
            {
                return NotFound();
            }

            var heure = await _context.Heures.FindAsync(id);
            if (heure == null)
            {
                return NotFound();
            }
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId", heure.ProfesseurId);
            return View(heure);
        }

        // POST: Heures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,ProfesseurId")] Heure heure)
        {
            if (id != heure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeureExists(heure.Id))
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
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId", heure.ProfesseurId);
            return View(heure);
        }

        // GET: Heures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Heures == null)
            {
                return NotFound();
            }

            var heure = await _context.Heures
                .Include(h => h.Professeur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heure == null)
            {
                return NotFound();
            }

            return View(heure);
        }

        // POST: Heures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Heures == null)
            {
                return Problem("Entity set 'PASDbContext.Heures'  is null.");
            }
            var heure = await _context.Heures.FindAsync(id);
            if (heure != null)
            {
                _context.Heures.Remove(heure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeureExists(int id)
        {
          return (_context.Heures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
