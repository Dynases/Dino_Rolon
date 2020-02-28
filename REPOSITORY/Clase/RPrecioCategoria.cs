using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.reg.PrecioCategoria.View;

namespace REPOSITORY.Clase
{
    public class RPrecioCategoria : BaseConexion, IPrecioCategoria
    {
        #region Consulta
        public List<VPrecioCategoria> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.PrecioCat
                                      select new VPrecioCategoria
                                      {
                                          Id = a.Id,
                                          Cod = a.Cod,
                                          Descrip = a.Descrip,
                                          Tipo = a.Tipo,
                                          NombreTipo = a.Tipo == 1 ? "Venta" : "Compra",
                                          Margen = a.Margen
                                      }).ToList();
                    return listResult;
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
