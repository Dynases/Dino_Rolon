using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.inv.Transformacion.View;

namespace REPOSITORY.Clase
{
    public class RTransformacion : BaseConexion, ITransformacion
    {
        public List<VTransformacion> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Transformacion
                                      join b in db.Sucursal on a.IdSucIngreso equals b.Id
                                      join c in db.Sucursal on a.IdSucSalida equals c.Id
                                      select new VTransformacion
                                      {
                                          Id = a.Id,
                                          IdSucSalida = a.IdSucSalida,
                                          Sucursal2 = c.Descrip,
                                          IdSucIngreso = a.IdSucIngreso,
                                          Sucursal1 =  b.Descrip,                                         
                                          Observ = a.Observ,                                         
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
    }
}
