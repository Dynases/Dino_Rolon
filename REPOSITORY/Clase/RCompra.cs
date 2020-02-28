using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Compra.View;

namespace REPOSITORY.Clase
{
    public class RCompra : BaseConexion, ICompra
    {
        #region CONSULTA
        public List<VCompraLista> Lista()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Compra
                                      join b in db.Proveed on a.IdProvee equals b.Id
                                      select new VCompraLista
                                      {
                                          Id = a.Id,
                                          IdSuc = a.IdSuc,
                                          IdProvee = a.IdProvee,
                                          Proveedor = b.Descrip,
                                          Estado = a.Estado,
                                          FechaDoc = a.FechaDoc,
                                          TipoVenta = a.TipoVenta,
                                          NombreTipo = a.TipoVenta == 1 ? "CONTADO" : "CREDITO",
                                          Descu = a.Descu,
                                          Total = a.Total,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario
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
