
using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Concepto.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum.ENConcepto;

namespace REPOSITORY.Clase
{
   public class RConcepto : BaseConexion, IConcepto
    {
        #region Transacciones
        public void Guardar(VConcepto concepto, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    TCI001 data;
                    if (id > 0)
                    {
                        data = db.TCI001.Where(a => a.cpnumi == idAux).FirstOrDefault();
                        if (data == null)
                            throw new Exception("No existe el concepto con id " + idAux);
                    }
                    else
                    {
                        data = new TCI001();
                        db.TCI001.Add(data);
                        data.cpnumi = db.TCI001.Select(a => a.cpnumi).DefaultIfEmpty(0).Max() + 1;                       
                    }
                    data.cpdesc = concepto.Descripcion;
                    data.cpmov = concepto.TipoMovimientoValor ? 1 : -1;
                    data.cpmovcli = concepto.AjusteCliente ? 1 : 2;
                    data.cptipo = 1;
                    data.cpest = concepto.Estado ? 1 : 2;
                    data.cpfact = DateTime.Today;
                    data.cphact = DateTime.Now.ToString("HH:mm");
                    data.cpuact = concepto.Usuario;
                    db.SaveChanges();
                    id = data.cpnumi;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var concepto = db.TCI001.Where(a => a.cpnumi == id).FirstOrDefault();
                    db.TCI001.Remove(concepto);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VConceptoCombo> ListaComboAjuste()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TCI001
                        .Where(a => a.cptipo == (int)ENConcepto.CONCEPTO_TIPO_AJUSTE)
                        .OrderBy(a => a.cpnumi);
                    return query.Select(a => new VConceptoCombo
                    {
                        Id = a.cpnumi,
                        Descripcion = a.cpdesc,
                        TipoMovimiento = a.cpmov,
                        AjusteCliente = a.cpmovcli
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VConceptoCombo ObternerPorId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TCI001
                        .Where(a => a.cpnumi == id)
                        .Select(a => new VConceptoCombo
                        {
                            Id = a.cpnumi,
                            Descripcion = a.cpdesc,
                            TipoMovimiento = a.cpmov
                        })
                        .FirstOrDefault();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VConcepto> ListaConcepto()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TCI001
                        .OrderBy(a => a.cpnumi);
                    return query.Select(a => new VConcepto
                    {
                        Id = a.cpnumi,
                        Descripcion = a.cpdesc,
                        TipoMovimiento = a.cpmov,
                        TipoMovimientoValor = (a.cpmov == 1 ? true : false),
                        AjusteCliente = (a.cpmovcli == 1 ? true : false),
                        Estado =  (a.cpest == 1 ? true : false),
                        Fecha = a.cpfact,
                        Hora = a.cphact,
                        Usuario = a.cpuact

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VConceptoLista> ObtenerListaConcepto()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TCI001
                        .Where(a=> a.cptipo == (int)ENConcepto.CONCEPTO_TIPO_AJUSTE)
                        .OrderBy(a => a.cpnumi);
                    return query.Select(a => new VConceptoLista
                    {
                        Id = a.cpnumi,
                        Descripcion = a.cpdesc,
                        TipoMovimiento = (a.cpmov == 1 ? "INGRESO" : "SALIDA"),                       
                        AjusteCliente = (a.cpmovcli == 1 ? "SI" : "NO"),
                        Estado = (a.cpest == 1 ? "HABILITADO" : "DESHABILITADO")                      
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion    
    }
 }
