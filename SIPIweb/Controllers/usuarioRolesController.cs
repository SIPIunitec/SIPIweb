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
    public class usuarioRolesController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public usuarioRolesController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: usuarioRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_usuarioRoles.ToListAsync());
        }

        // GET: usuarioRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioRole = await _context.tbl_usuarioRoles
                .FirstOrDefaultAsync(m => m.id_usuarioRoles == id);
            if (tbl_usuarioRole == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioRole);
        }

        // GET: usuarioRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usuarioRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuarioRoles,usuarioRol_nombre")] tbl_usuarioRole tbl_usuarioRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbl_usuarioRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_usuarioRole);
        }

        // GET: usuarioRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioRole = await _context.tbl_usuarioRoles.FindAsync(id);
            if (tbl_usuarioRole == null)
            {
                return NotFound();
            }
            return View(tbl_usuarioRole);
        }

        // POST: usuarioRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_usuarioRoles,usuarioRol_nombre")] tbl_usuarioRole tbl_usuarioRole)
        {
            if (id != tbl_usuarioRole.id_usuarioRoles)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuarioRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioRoleExists(tbl_usuarioRole.id_usuarioRoles))
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
            return View(tbl_usuarioRole);
        }

        // GET: usuarioRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioRole = await _context.tbl_usuarioRoles
                .FirstOrDefaultAsync(m => m.id_usuarioRoles == id);
            if (tbl_usuarioRole == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioRole);
        }

        // POST: usuarioRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbl_usuarioRole = await _context.tbl_usuarioRoles.FindAsync(id);
            _context.tbl_usuarioRoles.Remove(tbl_usuarioRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioRoleExists(int id)
        {
            return _context.tbl_usuarioRoles.Any(e => e.id_usuarioRoles == id);
        }
    }
}
