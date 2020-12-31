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
                    //Filtrar solo el personal registrado, excluye el configurado por defecto
                    var listResult = (from a in db.TC002
                                      where a.cbcat != 2 && a.cbnumi != 1 && a.cbnumi != 2 && a.cbnumi != 3 && a.cbnumi != 4
                                      select new VPersonalCombo
                                      {
                                          Id = a.cbnumi,
                                          Descripcion = a.cbdesc,
                                          Categoria = a.cbcat.Value,
                                          AlmacenRelacionado = a.cbreloj.Value
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
