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
    public class CourEtudiantsController : Controller
    {
        private readonly PASDbContext _context;

        public CourEtudiantsController(PASDbContext context)
        {
            _context = context;
        }

        // GET: CourEtudiants
        public async Task<IActionResult> Index()
        {
            var pASDbContext = _context.CourEtudiants.Include(c => c.Cour).Include(c => c.Etudiant);
            return View(await pASDbContext.ToListAsync());
        }

        // GET: CourEtudiants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CourEtudiants == null)
            {
                return NotFound();
            }

            var courEtudiant = await _context.CourEtudiants
                .Include(c => c.Cour)
                .Include(c => c.Etudiant)
                .FirstOrDefaultAsync(m => m.CourId == id);
            if (courEtudiant == null)
            {
                return NotFound();
            }

            return View(courEtudiant);
        }

        // GET: CourEtudiants/Create
        public IActionResult Create()
        {
            ViewData["CourId"] = new SelectList(_context.Cours, "CourId", "CourId");
            ViewData["EtudiantId"] = new SelectList(_context.Etudiants, "EtudiantId", "EtudiantId");
            return View();
        }

        // POST: CourEtudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourId,EtudiantId")] CourEtudiant courEtudiant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courEtudiant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourId"] = new SelectList(_context.Cours, "CourId", "CourId", courEtudiant.CourId);
            ViewData["EtudiantId"] = new SelectList(_context.Etudiants, "EtudiantId", "EtudiantId", courEtudiant.EtudiantId);
            return View(courEtudiant);
        }

        // GET: CourEtudiants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CourEtudiants == null)
            {
                return NotFound();
            }

            var courEtudiant = await _context.CourEtudiants.FindAsync(id);
            if (courEtudiant == null)
            {
                return NotFound();
            }
            ViewData["CourId"] = new SelectList(_context.Cours, "CourId", "CourId", courEtudiant.CourId);
            ViewData["EtudiantId"] = new SelectList(_context.Etudiants, "EtudiantId", "EtudiantId", courEtudiant.EtudiantId);
            return View(courEtudiant);
        }

        // POST: CourEtudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourId,EtudiantId")] CourEtudiant courEtudiant)
        {
            if (id != courEtudiant.CourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courEtudiant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourEtudiantExists(courEtudiant.CourId))
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
            ViewData["CourId"] = new SelectList(_context.Cours, "CourId", "CourId", courEtudiant.CourId);
            ViewData["EtudiantId"] = new SelectList(_context.Etudiants, "EtudiantId", "EtudiantId", courEtudiant.EtudiantId);
            return View(courEtudiant);
        }

        // GET: CourEtudiants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CourEtudiants == null)
            {
                return NotFound();
            }

            var courEtudiant = await _context.CourEtudiants
                .Include(c => c.Cour)
                .Include(c => c.Etudiant)
                .FirstOrDefaultAsync(m => m.CourId == id);
            if (courEtudiant == null)
            {
                return NotFound();
            }

            return View(courEtudiant);
        }

        // POST: CourEtudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CourEtudiants == null)
            {
                return Problem("Entity set 'PASDbContext.CourEtudiants'  is null.");
            }
            var courEtudiant = await _context.CourEtudiants.FindAsync(id);
            if (courEtudiant != null)
            {
                _context.CourEtudiants.Remove(courEtudiant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourEtudiantExists(int id)
        {
          return (_context.CourEtudiants?.Any(e => e.CourId == id)).GetValueOrDefault();
        }
    }
}
