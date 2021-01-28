using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SIPIweb.Models;
using SIPIweb.Procedimientos;

namespace SIPIweb.Controllers
{
    public class precargaUsuarioController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public precargaUsuarioController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: Listado de Usuario Temporales
        public async Task<IActionResult> Index()
        {
            return View(await _context.my_usuario_tmps.ToListAsync());
        }

        // GET: Detalle de Usuario Temporales
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario_tmp = await _context.my_usuario_tmps.FirstOrDefaultAsync(m => m.id_usuario_tmp == id);
            if (tbl_usuario_tmp == null)
            {
                return NotFound();
            }

            return View(tbl_usuario_tmp);
        }

        // GET: Carga de Usuario Temporal Manualmente
        public IActionResult Create()
        {
            ViewData["id_usuarioTipo"] = new SelectList(_context.tbl_usuarioTipos, "id_usuarioTipo", "usuarioTipo_nombre");

            return View();
        }

        // POST: Realiza Guardado de Usuario Temporal Manualmente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuario_tmp,id_usuarioTipo,usuario_login,usuario_pass,usuario_email,Estatus,Observaciones")] tbl_usuario_tmp tbl_usuario_tmp)
        {
            // **** Direcciona a Tablas Temporales y Definitiva **** //
            tbl_usuario_tmp.Estatus = false;
            tbl_usuario_tmp.usuario_createdDay = DateTime.Now;
            tbl_usuario_tmp.usuario_origen = "SIPI_WEB";
            
            var _tablaFinal = new tbl_usuario();
            var _tablaTMP = tbl_usuario_tmp;

            if (ModelState.IsValid)
            {
                // **** Variables de Control de Temporal creado **** //
                _context.Add(_tablaTMP);
                await _context.SaveChangesAsync();

                #region "// **** Guarda Definitiva **** //"
                migradores _guarda = new migradores(_context);
                var _resultado = _guarda.migraGeneral(_tablaTMP.id_usuario_tmp, _context, _tablaFinal, _tablaTMP,true);
                if (_resultado.Item1 == false)
                {
                    if (_tablaFinal.id_usuario > 0)
                    {
                        _context.Remove(_tablaTMP);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("details", "usuario", new { @id = _tablaFinal.id_usuario });
                    }
                }
                else
                {
                    _tablaTMP.Estatus = _resultado.Item1;
                    _tablaTMP.Observaciones = _resultado.Item2;
                    _context.Update(_tablaTMP);
                    await _context.SaveChangesAsync();
                }
                #endregion
            }
            return View(tbl_usuario_tmp);
        }

        // GET: Modifica Usuario Temporal
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario_tmp = await _context.my_usuario_tmps.FindAsync(id);
            if (tbl_usuario_tmp == null)
            {
                return NotFound();
            }
            return View(tbl_usuario_tmp);
        }

        // POST: Realiza Modificacion Usuario Temporal 
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
                    tbl_usuario_tmp.Estatus = false;
                    tbl_usuario_tmp.usuario_createdDay = DateTime.Now;
                    tbl_usuario_tmp.usuario_origen = "SIPI_WEB";
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

        // GET: Elimina Usuario Temporal
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuario_tmp = await _context.my_usuario_tmps
                .FirstOrDefaultAsync(m => m.id_usuario_tmp == id);
            if (tbl_usuario_tmp == null)
            {
                return NotFound();
            }

            return View(tbl_usuario_tmp);
        }

        // POST: Realiza Elimina Usuario Temporal
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_usuario_tmp = await _context.my_usuario_tmps.FindAsync(id);
            _context.my_usuario_tmps.Remove(tbl_usuario_tmp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuario_tmpExists(long id)
        {
            return _context.my_usuario_tmps.Any(e => e.id_usuario_tmp == id);
        }

        // POST: Realiza Elimina Usuario Temporal
        public async Task<IActionResult> limpiaUsuarioTMP()
        {
            var tbl_usuario_tmp = await _context.my_usuario_tmps.ToListAsync();
            _context.my_usuario_tmps.RemoveRange(tbl_usuario_tmp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Carga Masiva CSV Usuario Temporales
        public async Task<IActionResult> cargaUsuarioTMPlote()
        {
            migradores _guarda = new migradores(_context);
            var _archivo = "CIT_GUACARA.csv";
            var records = _guarda.leeCSVUsuario(_archivo);

            ViewData["archivo"] = _archivo;
            return View(records.ToList());
        }

        // GET: Guarda Masiva CSV Usuario en Usuario Definitivo
        public async Task<IActionResult> grabarUsuarioDefinitivo()
        {
            migradores _guarda = new migradores(_context);
            var _archivo = "CIT_GUACARA.csv";
            var records = _guarda.leeCSVUsuario(_archivo);
            var _errores = 0;
             foreach (var usuario in records)
            {
                var _tablaFinal = new tbl_usuario();
                var _resultado = _guarda.migraGeneral(usuario.id_usuario_tmp, _context, _tablaFinal, usuario, false);

                if (_resultado.Item1 == false)
                {
                    if (_tablaFinal.id_usuario > 0)
                    {
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _errores = _errores + 1;
                    usuario.Estatus = _resultado.Item1;
                    usuario.Observaciones = _resultado.Item2;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
            }
            if (_errores == 0) {
                return RedirectToAction("index", "usuario");
            }
            else
            {
                return RedirectToAction("index", "precargaUsuario");
            }
            
        }


    }
}
