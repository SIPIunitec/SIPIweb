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
    public class usuarioController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public usuarioController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: usuario
        public async Task<IActionResult> Index()
        {
            var sipiunitec_dbContext = _context.tbl_usuarios.Include(t => t.id_usuarioTipoNavigation);
            return View(await sipiunitec_dbContext.ToListAsync());
        }

        // GET: usuario/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios
                .Include(t => t.id_usuarioTipoNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // GET: usuario/Create
        public IActionResult Create()
        {
            ViewData["id_usuarioTipo"] = new SelectList(_context.tbl_usuarioTipos, "id_usuarioTipo", "usuarioTipo_nombre");
            return View();
        }

        // POST: usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuario,id_usuarioTipo,usuario_login,usuario_pass,usuario_email,usuario_createdDay")] tbl_usuario tbl_usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuarioTipo"] = new SelectList(_context.tbl_usuarioTipos, "id_usuarioTipo", "usuarioTipo_nombre", tbl_usuario.id_usuarioTipo);
            return View(tbl_usuario);
        }

        // GET: usuario/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios.FindAsync(id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }
            ViewData["id_usuarioTipo"] = new SelectList(_context.tbl_usuarioTipos, "id_usuarioTipo", "usuarioTipo_nombre", tbl_usuario.id_usuarioTipo);
            return View(tbl_usuario);
        }

        // POST: usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_usuario,id_usuarioTipo,usuario_login,usuario_pass,usuario_email,usuario_createdDay")] tbl_usuario tbl_usuario)
        {
            if (id != tbl_usuario.id_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioExists(tbl_usuario.id_usuario))
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
            ViewData["id_usuarioTipo"] = new SelectList(_context.tbl_usuarioTipos, "id_usuarioTipo", "usuarioTipo_nombre", tbl_usuario.id_usuarioTipo);
            return View(tbl_usuario);
        }

        // GET: usuario/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario = await _context.tbl_usuarios
                .Include(t => t.id_usuarioTipoNavigation)
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (tbl_usuario == null)
            {
                return NotFound();
            }

            return View(tbl_usuario);
        }

        // POST: usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_usuario = await _context.tbl_usuarios.FindAsync(id);
            _context.tbl_usuarios.Remove(tbl_usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioExists(long id)
        {
            return _context.tbl_usuarios.Any(e => e.id_usuario == id);
        }
    }
}
