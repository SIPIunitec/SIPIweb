using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPIweb.Models;

namespace SIPIweb.Controllers
{
    public class usuarioEstudianteController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public usuarioEstudianteController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: usuarioEstudiante
        public async Task<IActionResult> Index()
        {
            var sipiunitec_dbContext = _context.my_usuarioEstudiante.Include(t => t.id_usuarioEstudianteNavigation);
            return View(await sipiunitec_dbContext.ToListAsync());
        }

        // GET: usuarioEstudiante/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioEstudiante = await _context.my_usuarioEstudiante
                .Include(t => t.id_usuarioEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.id_usuarioEstudiante == id);
            if (tbl_usuarioEstudiante == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioEstudiante);
        }

        // GET: usuarioEstudiante/Create
        public IActionResult Create()
        {
            ViewData["id_usuarioEstudiante"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email");
            return View();
        }

        // POST: usuarioEstudiante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuarioEstudiante,usuarioEstudiante_ingreso")] tbl_usuarioEstudiante tbl_usuarioEstudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_usuarioEstudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuarioEstudiante"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email", tbl_usuarioEstudiante.id_usuarioEstudiante);
            return View(tbl_usuarioEstudiante);
        }

        // GET: usuarioEstudiante/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioEstudiante = await _context.my_usuarioEstudiante.FindAsync(id);
            if (tbl_usuarioEstudiante == null)
            {
                return NotFound();
            }
            ViewData["id_usuarioEstudiante"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email", tbl_usuarioEstudiante.id_usuarioEstudiante);
            return View(tbl_usuarioEstudiante);
        }

        // POST: usuarioEstudiante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_usuarioEstudiante,usuarioEstudiante_ingreso")] tbl_usuarioEstudiante tbl_usuarioEstudiante)
        {
            if (id != tbl_usuarioEstudiante.id_usuarioEstudiante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuarioEstudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioEstudianteExists(tbl_usuarioEstudiante.id_usuarioEstudiante))
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
            ViewData["id_usuarioEstudiante"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email", tbl_usuarioEstudiante.id_usuarioEstudiante);
            return View(tbl_usuarioEstudiante);
        }

        // GET: usuarioEstudiante/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioEstudiante = await _context.my_usuarioEstudiante
                .Include(t => t.id_usuarioEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.id_usuarioEstudiante == id);
            if (tbl_usuarioEstudiante == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioEstudiante);
        }

        // POST: usuarioEstudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_usuarioEstudiante = await _context.my_usuarioEstudiante.FindAsync(id);
            _context.my_usuarioEstudiante.Remove(tbl_usuarioEstudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioEstudianteExists(long id)
        {
            return _context.my_usuarioEstudiante.Any(e => e.id_usuarioEstudiante == id);
        }
    }
}
