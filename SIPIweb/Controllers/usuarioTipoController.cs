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
    public class usuarioTipoController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public usuarioTipoController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: usuarioTipo
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_usuarioTipos.ToListAsync());
        }

        // GET: usuarioTipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioTipo = await _context.tbl_usuarioTipos
                .FirstOrDefaultAsync(m => m.id_usuarioTipo == id);
            if (tbl_usuarioTipo == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioTipo);
        }

        // GET: usuarioTipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usuarioTipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuarioTipo,usuarioTipo_nombre")] tbl_usuarioTipo tbl_usuarioTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_usuarioTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_usuarioTipo);
        }

        // GET: usuarioTipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioTipo = await _context.tbl_usuarioTipos.FindAsync(id);
            if (tbl_usuarioTipo == null)
            {
                return NotFound();
            }
            return View(tbl_usuarioTipo);
        }

        // POST: usuarioTipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_usuarioTipo,usuarioTipo_nombre")] tbl_usuarioTipo tbl_usuarioTipo)
        {
            if (id != tbl_usuarioTipo.id_usuarioTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuarioTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioTipoExists(tbl_usuarioTipo.id_usuarioTipo))
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
            return View(tbl_usuarioTipo);
        }

        // GET: usuarioTipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioTipo = await _context.tbl_usuarioTipos
                .FirstOrDefaultAsync(m => m.id_usuarioTipo == id);
            if (tbl_usuarioTipo == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioTipo);
        }

        // POST: usuarioTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_usuarioTipo = await _context.tbl_usuarioTipos.FindAsync(id);
            _context.tbl_usuarioTipos.Remove(tbl_usuarioTipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioTipoExists(int id)
        {
            return _context.tbl_usuarioTipos.Any(e => e.id_usuarioTipo == id);
        }
    }
}
