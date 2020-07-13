
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
        #region Consulta
        public List<VConceptoCombo> ListaComboAjuste()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.TCI001
                        .Where(a => a.cpnumi == (int)ENConcepto.INGRESO || a.cpnumi == (int)ENConcepto.SALIDA)
                        .OrderBy(a => a.cpnumi);
                    return query.Select(a => new VConceptoCombo
                    {
                        Id = a.cpnumi,
                        Descripcion = a.cpdesc,
                        TipoMovimiento = a.cpmov.Value
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
                            TipoMovimiento = a.cpmov.Value
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
        #endregion    
    }
    }
