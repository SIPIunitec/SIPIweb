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
    public class informacionController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public informacionController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: informacion
        public async Task<IActionResult> Index()
        {
            var sipiunitec_dbContext = _context.tbl_informacions.Include(t => t.id_usuarioNavigation);
            return View(await sipiunitec_dbContext.ToListAsync());
        }

        // GET: informacion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_informacion = await _context.tbl_informacions
                .Include(t => t.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_informacion == id);
            if (tbl_informacion == null)
            {
                return NotFound();
            }

            return View(tbl_informacion);
        }

        // GET: informacion/Create
        public IActionResult Create()
        {
            ViewData["id_usuario"] = new SelectList(_context.tbl_usuarios, "id_usuario", "usuario_email");
            return View();
        }

        // POST: informacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_informacion,informacion_titulo,informacion_cuerpo,informacion_fechaPublicacion,informacion_fechaLimite,id_usuario")] tbl_informacion tbl_informacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_informacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuario"] = new SelectList(_context.tbl_usuarios, "id_usuario", "usuario_email", tbl_informacion.id_usuario);
            return View(tbl_informacion);
        }

        // GET: informacion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_informacion = await _context.tbl_informacions.FindAsync(id);
            if (tbl_informacion == null)
            {
                return NotFound();
            }
            ViewData["id_usuario"] = new SelectList(_context.tbl_usuarios, "id_usuario", "usuario_email", tbl_informacion.id_usuario);
            return View(tbl_informacion);
        }

        // POST: informacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_informacion,informacion_titulo,informacion_cuerpo,informacion_fechaPublicacion,informacion_fechaLimite,id_usuario")] tbl_informacion tbl_informacion)
        {
            if (id != tbl_informacion.id_informacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_informacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_informacionExists(tbl_informacion.id_informacion))
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
            ViewData["id_usuario"] = new SelectList(_context.tbl_usuarios, "id_usuario", "usuario_email", tbl_informacion.id_usuario);
            return View(tbl_informacion);
        }

        // GET: informacion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_informacion = await _context.tbl_informacions
                .Include(t => t.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_informacion == id);
            if (tbl_informacion == null)
            {
                return NotFound();
            }

            return View(tbl_informacion);
        }

        // POST: informacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_informacion = await _context.tbl_informacions.FindAsync(id);
            _context.tbl_informacions.Remove(tbl_informacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_informacionExists(long id)
        {
            return _context.tbl_informacions.Any(e => e.id_informacion == id);
        }
    }
}
