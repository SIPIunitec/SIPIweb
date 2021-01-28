using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPIweb.Models;
using SIPIweb.Procedimientos;

namespace SIPIweb.Controllers
{
    public class precargaPersonaController : Controller
    {
        private readonly sipiunitec_dbContext _context;

        public precargaPersonaController(sipiunitec_dbContext context)
        {
            _context = context;
        }

        // GET: precargaPersona
        public async Task<IActionResult> Index()
        {
            return View(await _context.my_usuarioPersona_tmps.ToListAsync());
        }

        // GET: precargaPersona/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioPersona_tmp = await _context.my_usuarioPersona_tmps
                .FirstOrDefaultAsync(m => m.id_persona_tmp == id);
            if (tbl_usuarioPersona_tmp == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioPersona_tmp);
        }

        // GET: precargaPersona/Create
        public IActionResult Create(long? id)
        {
            var _datosUsuario = _context.my_usuarios.Find(id);
            ViewData["usuario"] = _datosUsuario;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("persona_nombres,persona_apellidos,persona_login,persona_email,persona_origen,Observaciones,Estatus")] long id_usuario, tbl_usuarioPersona_tmp tbl_usuarioPersona_tmp)
        {
            tbl_usuarioPersona_tmp.Estatus = false;
            tbl_usuarioPersona_tmp.persona_createdDay = DateTime.Now;
            tbl_usuarioPersona_tmp.persona_origen = "SIPI_WEB";

            var _tablaFinal = new tbl_usuarioPersona();
            var _tablaTMP = tbl_usuarioPersona_tmp;

            if (ModelState.IsValid)
            {
                // **** Variables de Control de Temporal creado **** //
                _context.Add(_tablaTMP);
                await _context.SaveChangesAsync();

                #region "// **** Guarda Definitiva **** //"
                migradores _guarda = new migradores(_context);
                var _id = id_usuario;
                _tablaFinal.id_persona = _id;
                 var _resultado = _guarda.migraGeneral(_id, _context, _tablaFinal, _tablaTMP, true);
                if (_resultado.Item1 == false)
                {
                    if (_id > 0)
                    {
                        _context.Remove(_tablaTMP);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("details", "persona", new { @id = _tablaFinal.id_persona });
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
            return View(tbl_usuarioPersona_tmp);
        }

        // GET: precargaPersona/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioPersona_tmp = await _context.my_usuarioPersona_tmps.FindAsync(id);
            if (tbl_usuarioPersona_tmp == null)
            {
                return NotFound();
            }
            return View(tbl_usuarioPersona_tmp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id_persona_tmp,persona_nombres,persona_apellidos,persona_login,persona_email,persona_origen,Observaciones,Estatus")] tbl_usuarioPersona_tmp tbl_usuarioPersona_tmp)
        {
            if (id != tbl_usuarioPersona_tmp.id_persona_tmp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbl_usuarioPersona_tmp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_usuarioPersona_tmpExists(tbl_usuarioPersona_tmp.id_persona_tmp))
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
            return View(tbl_usuarioPersona_tmp);
        }

        // GET: precargaPersona/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbl_usuarioPersona_tmp = await _context.my_usuarioPersona_tmps
                .FirstOrDefaultAsync(m => m.id_persona_tmp == id);
            if (tbl_usuarioPersona_tmp == null)
            {
                return NotFound();
            }

            return View(tbl_usuarioPersona_tmp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tbl_usuarioPersona_tmp = await _context.my_usuarioPersona_tmps.FindAsync(id);
            _context.my_usuarioPersona_tmps.Remove(tbl_usuarioPersona_tmp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_usuarioPersona_tmpExists(long id)
        {
            return _context.my_usuarioPersona_tmps.Any(e => e.id_persona_tmp == id);
        }

        // GET: Carga Masiva CSV Usuario Temporales
        public async Task<IActionResult> cargaPersonaTMPlote()
        {
            migradores _guarda = new migradores(_context);
            var _archivo = "CIT_GUACARA_persona.csv";
            var records = _guarda.leeCSVpersona(_archivo);

            ViewData["archivo"] = _archivo;
            return View(records.ToList());
        }

        public async Task<IActionResult> grabarPersonaDefinitivo()
        {
            migradores _guarda = new migradores(_context);
            var _archivo = "CIT_GUACARA_persona.csv";
            var records = _guarda.leeCSVpersona(_archivo);
            var _errores = 0;

            foreach (var persona in records)
            {
                var _tablaFinal = new tbl_usuarioPersona();
                var _resultado = _guarda.migraGeneral(persona.id_persona_tmp, _context, _tablaFinal, persona,false);

                if (_resultado.Item1 == false)
                {
                    if (_tablaFinal.id_persona > 0)
                    {
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _errores = _errores + 1;
                    persona.Estatus = _resultado.Item1;
                    persona.Observaciones = _resultado.Item2;
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
            }
            if (_errores == 0)
            {
                return RedirectToAction("index", "usuario");
            }
            else
            {
                return RedirectToAction("index", "precargaUsuario");
            }

        }
    }
}
