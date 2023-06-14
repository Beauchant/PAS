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
    public class AnneeAcademiquesController : Controller
    {
        private readonly PASDbContext _context;

        public AnneeAcademiquesController(PASDbContext context)
        {
            _context = context;
        }

        // GET: AnneeAcademiques
        public async Task<IActionResult> Index()
        {
              return _context.AnneeAcademiques != null ? 
                          View(await _context.AnneeAcademiques.ToListAsync()) :
                          Problem("Entity set 'PASDbContext.AnneeAcademiques'  is null.");
        }

        // GET: AnneeAcademiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnneeAcademiques == null)
            {
                return NotFound();
            }

            var anneeAcademique = await _context.AnneeAcademiques
                .FirstOrDefaultAsync(m => m.AnneeAcademiqueId == id);
            if (anneeAcademique == null)
            {
                return NotFound();
            }

            return View(anneeAcademique);
        }

        // GET: AnneeAcademiques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnneeAcademiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnneeAcademiqueId,AnneeScolaire,DateDebut,DateFin,Statut")] AnneeAcademique anneeAcademique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anneeAcademique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anneeAcademique);
        }

        // GET: AnneeAcademiques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnneeAcademiques == null)
            {
                return NotFound();
            }

            var anneeAcademique = await _context.AnneeAcademiques.FindAsync(id);
            if (anneeAcademique == null)
            {
                return NotFound();
            }
            return View(anneeAcademique);
        }

        // POST: AnneeAcademiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnneeAcademiqueId,AnneeScolaire,DateDebut,DateFin,Statut")] AnneeAcademique anneeAcademique)
        {
            if (id != anneeAcademique.AnneeAcademiqueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anneeAcademique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnneeAcademiqueExists(anneeAcademique.AnneeAcademiqueId))
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
            return View(anneeAcademique);
        }

        // GET: AnneeAcademiques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnneeAcademiques == null)
            {
                return NotFound();
            }

            var anneeAcademique = await _context.AnneeAcademiques
                .FirstOrDefaultAsync(m => m.AnneeAcademiqueId == id);
            if (anneeAcademique == null)
            {
                return NotFound();
            }

            return View(anneeAcademique);
        }

        // POST: AnneeAcademiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnneeAcademiques == null)
            {
                return Problem("Entity set 'PASDbContext.AnneeAcademiques'  is null.");
            }
            var anneeAcademique = await _context.AnneeAcademiques.FindAsync(id);
            if (anneeAcademique != null)
            {
                _context.AnneeAcademiques.Remove(anneeAcademique);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnneeAcademiqueExists(int id)
        {
          return (_context.AnneeAcademiques?.Any(e => e.AnneeAcademiqueId == id)).GetValueOrDefault();
        }
    }
}
