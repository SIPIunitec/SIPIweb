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
    public class usuarioPersonaController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public usuarioPersonaController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: usuarioPersona
        public async Task<IActionResult> Index()
        {
            var sipiunitec_dbContext = _context.my_usuarioPersonas.Include(t => t.id_ciudad_nacimientoNavigation).Include(t => t.id_ciudad_ubicacionNavigation).Include(t => t.id_personaNavigation);
            return View(await sipiunitec_dbContext.ToListAsync());
        }

        // GET: usuarioPersona/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioPersona = await _context.my_usuarioPersonas
                .Include(t => t.id_ciudad_nacimientoNavigation)
                .Include(t => t.id_ciudad_ubicacionNavigation)
                .Include(t => t.id_personaNavigation)
                .FirstOrDefaultAsync(m => m.id_persona == id);
            if (tbl_usuarioPersona == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioPersona);
        }

        // GET: usuarioPersona/Create
        public IActionResult Create()
        {
            ViewData["id_ciudad_nacimiento"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre");
            ViewData["id_ciudad_ubicacion"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre");
            ViewData["id_persona"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email");
            return View();
        }

        // POST: usuarioPersona/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_persona,persona_nombres,persona_apellidos,persona_nombreCompleto,persona_nacimiento,persona_sangre,id_ciudad_nacimiento,id_ciudad_ubicacion")] tbl_usuarioPersona tbl_usuarioPersona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_usuarioPersona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_ciudad_nacimiento"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre", tbl_usuarioPersona.id_ciudad_nacimiento);
            ViewData["id_ciudad_ubicacion"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre", tbl_usuarioPersona.id_ciudad_ubicacion);
            ViewData["id_persona"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email", tbl_usuarioPersona.id_persona);
            return View(tbl_usuarioPersona);
        }

        // GET: usuarioPersona/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioPersona = await _context.my_usuarioPersonas.FindAsync(id);
            if (tbl_usuarioPersona == null)
            {
                return NotFound();
            }
            ViewData["id_ciudad_nacimiento"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre", tbl_usuarioPersona.id_ciudad_nacimiento);
            ViewData["id_ciudad_ubicacion"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre", tbl_usuarioPersona.id_ciudad_ubicacion);
            ViewData["id_persona"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email", tbl_usuarioPersona.id_persona);
            return View(tbl_usuarioPersona);
        }

        // POST: usuarioPersona/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_persona,persona_nombres,persona_apellidos,persona_nombreCompleto,persona_nacimiento,persona_sangre,id_ciudad_nacimiento,id_ciudad_ubicacion")] tbl_usuarioPersona tbl_usuarioPersona)
        {
            if (id != tbl_usuarioPersona.id_persona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuarioPersona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioPersonaExists(tbl_usuarioPersona.id_persona))
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
            ViewData["id_ciudad_nacimiento"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre", tbl_usuarioPersona.id_ciudad_nacimiento);
            ViewData["id_ciudad_ubicacion"] = new SelectList(_context.tbl_geografiaCiudads, "id_ciudad", "ciudad_nombre", tbl_usuarioPersona.id_ciudad_ubicacion);
            ViewData["id_persona"] = new SelectList(_context.my_usuarios, "id_usuario", "usuario_email", tbl_usuarioPersona.id_persona);
            return View(tbl_usuarioPersona);
        }

        // GET: usuarioPersona/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioPersona = await _context.my_usuarioPersonas
                .Include(t => t.id_ciudad_nacimientoNavigation)
                .Include(t => t.id_ciudad_ubicacionNavigation)
                .Include(t => t.id_personaNavigation)
                .FirstOrDefaultAsync(m => m.id_persona == id);
            if (tbl_usuarioPersona == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioPersona);
        }

        // POST: usuarioPersona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_usuarioPersona = await _context.my_usuarioPersonas.FindAsync(id);
            _context.my_usuarioPersonas.Remove(tbl_usuarioPersona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioPersonaExists(long id)
        {
            return _context.my_usuarioPersonas.Any(e => e.id_persona == id);
        }
    }
}
