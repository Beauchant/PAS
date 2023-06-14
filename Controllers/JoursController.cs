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
    public class JoursController : Controller
    {
        private readonly PASDbContext _context;

        public JoursController(PASDbContext context)
        {
            _context = context;
        }

        // GET: Jours
        public async Task<IActionResult> Index()
        {
            var pASDbContext = _context.Jours.Include(j => j.Professeur);
            return View(await pASDbContext.ToListAsync());
        }

        // GET: Jours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jours == null)
            {
                return NotFound();
            }

            var jour = await _context.Jours
                .Include(j => j.Professeur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jour == null)
            {
                return NotFound();
            }

            return View(jour);
        }

        // GET: Jours/Create
        public IActionResult Create()
        {
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId");
            return View();
        }

        // POST: Jours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,ProfesseurId")] Jour jour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId", jour.ProfesseurId);
            return View(jour);
        }

        // GET: Jours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jours == null)
            {
                return NotFound();
            }

            var jour = await _context.Jours.FindAsync(id);
            if (jour == null)
            {
                return NotFound();
            }
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId", jour.ProfesseurId);
            return View(jour);
        }

        // POST: Jours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,ProfesseurId")] Jour jour)
        {
            if (id != jour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JourExists(jour.Id))
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
            ViewData["ProfesseurId"] = new SelectList(_context.Professeurs, "ProfesseurId", "ProfesseurId", jour.ProfesseurId);
            return View(jour);
        }

        // GET: Jours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jours == null)
            {
                return NotFound();
            }

            var jour = await _context.Jours
                .Include(j => j.Professeur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jour == null)
            {
                return NotFound();
            }

            return View(jour);
        }

        // POST: Jours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jours == null)
            {
                return Problem("Entity set 'PASDbContext.Jours'  is null.");
            }
            var jour = await _context.Jours.FindAsync(id);
            if (jour != null)
            {
                _context.Jours.Remove(jour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JourExists(int id)
        {
          return (_context.Jours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
