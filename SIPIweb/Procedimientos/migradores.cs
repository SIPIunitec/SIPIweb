using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using SIPIweb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SIPIweb.Procedimientos
{
    public class migradores
    {
        private readonly sipiunitec_dbContext _context;

        public migradores(sipiunitec_dbContext context)
        {
            _context = context;
        }

        #region "// *** Usuarios *** //
        public (bool, string) migraGeneral(sipiunitec_dbContext _MiContexto, object _tablaDestino, object _tablaTemporal, bool _saltaClave, [Optional] string _clave, [Optional] long _valor)
        {
            #region "*** Lee Valor TMP y Crea objeto a guardar ***"
            var propInfo = _tablaDestino.GetType().GetProperties();
            foreach (var item in propInfo)
            {
                KeyAttribute key = Attribute.GetCustomAttribute(item, typeof(KeyAttribute)) as KeyAttribute;
                if (key == null || _saltaClave==false)
                {
                    try
                    {
                        var campo = item.Name;
                        var _valida = _tablaTemporal.GetType().GetProperty(campo);
                        if (_valida != null)
                        {
                            var values = _tablaTemporal.GetType().GetProperty(campo).GetValue(_tablaTemporal, null);
                            _tablaDestino.GetType().GetProperty(campo).SetValue(_tablaDestino, values, null);
                        }
                    }
                    catch (DbUpdateException) { }
                }else if (_clave != null)
                {
                    var values = _valor;
                    _tablaDestino.GetType().GetProperty(_clave).SetValue(_tablaDestino, values, null);
                }
            }
            #endregion

            #region "*** Guarda el objeto en la tabla ***"
            try
            {
                _MiContexto.Add(_tablaDestino);
                _context.SaveChanges();
                return (false, "");
            }
            catch (DbUpdateException ex)
            {
                _context.Entry(_tablaDestino).State = EntityState.Detached;
                return (true, ex.InnerException.Message.ToString());
            }
            #endregion
            
        }
        public List<tbl_usuario_tmp> leeCSVUsuario(string _ubicacionCSV)
        {
            var records = new List<tbl_usuario_tmp>();
            var _archivo = _ubicacionCSV;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = (string header, int index) => header.ToLower(),
                Delimiter = ";"
            };

            using (var reader = new System.IO.StreamReader("csv\\" + _archivo))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new tbl_usuario_tmp
                    {
                        id_usuarioTipo = csv.GetField<int>("id_usuarioTipo"),
                        usuario_login = csv.GetField<string>("usuario_login"),
                        usuario_pass = csv.GetField<string>("usuario_pass"),
                        usuario_email = csv.GetField<string>("usuario_email"),
                        usuario_origen = csv.GetField<string>("Origen"),
                        usuario_createdDay = DateTime.Now,
                        Estatus = false

                    };
                    records.Add(record);
                }
            }

            return records;
        }
        #endregion

        #region "// *** Persona *** //
        public List<tbl_usuarioPersona_tmp> leeCSVpersona(string _ubicacionCSV)
        {
            var records = new List<tbl_usuarioPersona_tmp>();
            var _archivo = _ubicacionCSV;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = (string header, int index) => header.ToLower(),
                Delimiter = ";"
            };

            using (var reader = new System.IO.StreamReader("csv\\" + _archivo))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new tbl_usuarioPersona_tmp
                    {
                        persona_nombres = csv.GetField<string>("persona_nombres"),
                        persona_apellidos = csv.GetField<string>("persona_apellidos"),
                        persona_login = csv.GetField<string>("persona_login"),
                        persona_email = csv.GetField<string>("persona_email"),
                        persona_origen = csv.GetField<string>("persona_origen"),
                        persona_createdDay = DateTime.Now,
                        Estatus = false
                    };
                    //var _usuario = _context.my_usuarios.FirstOrDefault(t => t.usuario_login.Equals(record.persona_login) || t.usuario_login.Equals(record.persona_login));
                   
                   // if (_usuario!=null) record.id_persona = _usuario.id_usuario;
                    records.Add(record);
                }
            }

            return records;
        }
        #endregion
    }


}
