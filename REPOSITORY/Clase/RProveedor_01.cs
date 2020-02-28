using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Proveedor.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
    public class RProveedor_01 : BaseConexion, IProveedor_01
    {
        public bool Guardar(List<VProveedor_01Lista> listProveedor_01, int idProveedor, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Proveed_01.Where(a => a.IdProveed == idProveedor).ToList();
                    if (listResult != null)
                        db.Proveed_01.RemoveRange(listResult);

                    foreach (var vProveedor_01 in listProveedor_01)
                    {
                        var proveedor = new Proveed_01();
                        proveedor.IdProveed = idProveedor;
                        proveedor.Estado = 1; //Estatico
                        proveedor.FechaNac = vProveedor_01.FechaNac;
                        proveedor.EdadSeman = vProveedor_01.EdadSeman;
                        proveedor.Cantidad = vProveedor_01.Cantidad;
                        proveedor.TipoAloja = vProveedor_01.IdTipoAloja;
                        proveedor.Linea = vProveedor_01.IdLinea;
                        proveedor.Fecha = DateTime.Now.Date;
                        proveedor.Hora = DateTime.Now.ToString("HH:mm");
                        proveedor.Usuario = usuario;
                        db.Proveed_01.Add(proveedor);
                    }
                    
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VProveedor_01Lista> ListarXId(int id)
        {
            try
            {
                int grupo = Convert.ToInt32(ENEstaticosGrupo.PROVEEDOR);
                int orden = Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_TIPO_ALOJAMIENTO);
                int orden2 = Convert.ToInt32(ENEstaticosOrden.PROVEEDOR_LINEA_GENETICA);
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Proveed_01
                                      join c in db.Libreria on 
                                      new { Grupo = grupo, Orden = orden, Libreria = a.TipoAloja }
                                        equals new { Grupo = c.IdGrupo, Orden = c.IdOrden, Libreria = c.IdLibrer }
                                      join d in db.Libreria on 
                                      new { Grupo = grupo, Orden = orden2, Libreria = a.Linea }
                                        equals new { Grupo = d.IdGrupo, Orden = d.IdOrden, Libreria = d.IdLibrer}
                                      where a.IdProveed.Equals(id)
                                      select new VProveedor_01Lista
                                      {
                                          Id = a.Id,
                                          IdLinea = a.Linea,
                                          Linea =d.Descrip,
                                          FechaNac = a.FechaNac,
                                          EdadSeman = a.EdadSeman,
                                          Cantidad = a.Cantidad,
                                          IdTipoAloja = a.TipoAloja,
                                          TipoAlojamiento = c.Descrip,
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
