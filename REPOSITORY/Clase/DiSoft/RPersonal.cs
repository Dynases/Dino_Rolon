using ENTITY.DiSoft.Personal;
using REPOSITORY.Base;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase.DiSoft
{
   public  class RPersonal: BaseConexion2, IPersonal
    {
        public List<VPersonalCombo> PersonalCombo()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.TC002
                                      where a.cbcat != 2
                                      select new VPersonalCombo
                                      {
                                          Id = a.cbnumi,
                                          Descripcion = a.cbdesc,                                        
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
