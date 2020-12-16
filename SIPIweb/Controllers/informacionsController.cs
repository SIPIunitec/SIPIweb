using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPIweb.Models;

namespace SIPIweb.Views
{
    public class informacionsController : Controller
    {
        private readonly SIPIContext _context;

        public informacionsController(SIPIContext context)
        {
            _context = context;
        }

        // GET: informacions
        public async Task<IActionResult> Index()
        {
            var sIPIContext = _context.informacions.Include(i => i.id_usuarioNavigation);
            return View(await sIPIContext.ToListAsync());
        }

        // GET: Main Principal
        public async Task<IActionResult> main()
        {
            var sIPIContext = _context.informacions.Include(i => i.id_usuarioNavigation);
            return View(await sIPIContext.ToListAsync());
        }

        // GET: informacions/detalleNoticia/5
        [Route("informacions/detalleNoticia/{id}")]
        public async Task<IActionResult> detalleNoticia(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion = await _context.informacions
                .Include(i => i.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_informacion == id);
            if (informacion == null)
            {
                return NotFound();
            }

            return View(informacion);
        }

        // GET: informacions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion = await _context.informacions
                .Include(i => i.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_informacion == id);
            if (informacion == null)
            {
                return NotFound();
            }

            return View(informacion);
        }

        // GET: informacions/Create
        public IActionResult Create()
        {
            ViewData["id_usuario"] = new SelectList(_context.usuarios, "id_usuario", "usuario_email");
            return View();
        }

        // POST: informacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_informacion,informacion_titulo,informacion_cuerpo,informacion_fechaPublicacion,informacion_fechaLimite,id_usuario")] informacion informacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_usuario"] = new SelectList(_context.usuarios, "id_usuario", "usuario_email", informacion.id_usuario);
            return View(informacion);
        }

        // GET: informacions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion = await _context.informacions.FindAsync(id);
            if (informacion == null)
            {
                return NotFound();
            }
            ViewData["id_usuario"] = new SelectList(_context.usuarios, "id_usuario", "usuario_email", informacion.id_usuario);
            return View(informacion);
        }

        // POST: informacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_informacion,informacion_titulo,informacion_cuerpo,informacion_fechaPublicacion,informacion_fechaLimite,id_usuario")] informacion informacion)
        {
            if (id != informacion.id_informacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!informacionExists(informacion.id_informacion))
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
            ViewData["id_usuario"] = new SelectList(_context.usuarios, "id_usuario", "usuario_email", informacion.id_usuario);
            return View(informacion);
        }

        // GET: informacions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacion = await _context.informacions
                .Include(i => i.id_usuarioNavigation)
                .FirstOrDefaultAsync(m => m.id_informacion == id);
            if (informacion == null)
            {
                return NotFound();
            }

            return View(informacion);
        }

        // POST: informacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var informacion = await _context.informacions.FindAsync(id);
            _context.informacions.Remove(informacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool informacionExists(long id)
        {
            return _context.informacions.Any(e => e.id_informacion == id);
        }
    }
}
