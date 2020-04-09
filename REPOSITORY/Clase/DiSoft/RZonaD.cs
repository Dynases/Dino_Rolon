using ENTITY.Cliente.View;
using ENTITY.DiSoft.Zona;
using REPOSITORY.Base;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase.DiSoft
{
    public class RZonaD : BaseConexion2, IZonaD
    {
        public List<VZona> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.TL001
                                      join c in db.TC0051 on a.lazona equals c.cenum
                                      join cc in db.TC0051 on a.laprovi equals cc.cenum
                                      join ccc in db.TC0051 on a.lacity equals ccc.cenum
                                      where c.cecon == 2 && cc.cecon ==3 && ccc.cecon ==4
                                      select new VZona
                                      {
                                          Id = a.lanumi,
                                          Ciudad = ccc.cedesc,
                                          Provincia = cc.cedesc,
                                          Zona = c.cedesc
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VClienteLista> ListarClienteAdicionarZona(List<VClienteLista> cliente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var zona = this.Listar();
                    foreach (var i in cliente)
                    {
                        i.NombreCiudad = zona.Where(z => z.Id == i.Ciudad).Select(z => z.Zona).FirstOrDefault();
                    }
                    return cliente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
