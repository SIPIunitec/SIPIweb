using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPIweb.Models;

namespace SIPIweb.Controllers
{
    public class precargaUsuarioController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public precargaUsuarioController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: precargaUsuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbl_usuario_tmps.ToListAsync());
        }

        // GET: precargaUsuario/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario_tmp = await _context.tbl_usuario_tmps
                .FirstOrDefaultAsync(m => m.id_usuario_tmp == id);
            if (tbl_usuario_tmp == null)
            {
                return NotFound();
            }

            return View(tbl_usuario_tmp);
        }

        // GET: precargaUsuario/Create
        public IActionResult Create()
        {
            ViewData["id_usuarioTipo"] = new SelectList(_context.tbl_usuarioTipos, "id_usuarioTipo", "usuarioTipo_nombre");

            return View();
        }

        // POST: precargaUsuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuario_tmp,id_usuarioTipo,usuario_login,usuario_pass,usuario_email,Estatus,Observaciones")] tbl_usuario_tmp tbl_usuario_tmp)
        {
            if (ModelState.IsValid)
            {
                tbl_usuario_tmp.Estatus = false;
                _context.Add(tbl_usuario_tmp);

                await _context.SaveChangesAsync();
                validaUsuario(tbl_usuario_tmp.id_usuario_tmp);
                return RedirectToAction(nameof(Index));
            }
            return View(tbl_usuario_tmp);
        }

        // GET: precargaUsuario/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario_tmp = await _context.tbl_usuario_tmps.FindAsync(id);
            if (tbl_usuario_tmp == null)
            {
                return NotFound();
            }
            return View(tbl_usuario_tmp);
        }

        // POST: precargaUsuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_usuario_tmp,id_usuarioTipo,usuario_login,usuario_pass,usuario_email,Estatus,Observaciones")] tbl_usuario_tmp tbl_usuario_tmp)
        {
            if (id != tbl_usuario_tmp.id_usuario_tmp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuario_tmp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuario_tmpExists(tbl_usuario_tmp.id_usuario_tmp))
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
            return View(tbl_usuario_tmp);
        }

        // GET: precargaUsuario/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario_tmp = await _context.tbl_usuario_tmps
                .FirstOrDefaultAsync(m => m.id_usuario_tmp == id);
            if (tbl_usuario_tmp == null)
            {
                return NotFound();
            }

            return View(tbl_usuario_tmp);
        }

        // POST: precargaUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_usuario_tmp = await _context.tbl_usuario_tmps.FindAsync(id);
            _context.tbl_usuario_tmps.Remove(tbl_usuario_tmp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuario_tmpExists(long id)
        {
            return _context.tbl_usuario_tmps.Any(e => e.id_usuario_tmp == id);
        }

        private string validaUsuario(long id) 
        {
            var usuario = new tbl_usuario();
            //System.Threading.Tasks.Task<Models.tbl_usuario_tmp> tbl_usuario_tmp;
            var usuario_tmp = _context.tbl_usuario_tmps.FirstOrDefault(m => m.id_usuario_tmp == id);
            
            var propInfo = usuario.GetType().GetProperties();
            foreach (var item in propInfo)
            {
                var key = Attribute.GetCustomAttribute(item, typeof(KeyAttribute)) as KeyAttribute;

                if (key == null)
                {
                    var campo = item.Name;
                    //var valor = tbl_usuario_tmp.GetType().GetProperty(campo).GetValue(tbl_usuario_tmp, null);

                    try
                    {
                        var values = usuario_tmp.GetType().GetProperty(campo).GetValue(usuario_tmp, null);
                        usuario.GetType().GetProperty(campo).SetValue(usuario, values, null);
                    }
                    catch (System.NullReferenceException e)
                    {

                        throw;
                    }


                }
            }
            _context.tbl_usuarios.Add(usuario);


            //tbl_usuario_tmp.GetType().GetProperty(item.Name).SetValue(tbl_usuario_tmp, item.GetValue(tbl_usuario, null), null);
            return "0";



        }

    }
}
