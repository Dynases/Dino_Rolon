using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Libreria.View;
using DATA.EntityDataModel.DiAvi;

namespace REPOSITORY.Clase
{
    public class RLibreria : BaseConexion, ILibreria
    {
        #region Transacciones
        public bool Guardar(VLibreriaLista vLibreria)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var libreria = new Libreria();
                    libreria.IdGrupo = vLibreria.IdGrupo;
                    libreria.IdOrden = vLibreria.IdOrden;
                    libreria.IdLibrer = vLibreria.IdLibrer;
                    libreria.Descrip = vLibreria.Descrip;
                    libreria.Fecha = vLibreria.Fecha;
                    libreria.Usuario = vLibreria.Usuario;
                    libreria.Hora = vLibreria.Hora;
                    db.Libreria.Add(libreria);
                    db.SaveChanges();
                    return true;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region MyRegion
        public List<VLibreria> Listar(int idGrupo, int idOrden)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Libreria
                                      where a.IdGrupo.Equals(idGrupo) & a.IdOrden.Equals(idOrden)
                                      select new VLibreria
                                      {
                                          IdLibreria = a.IdLibrer,
                                          Descripcion = a.Descrip
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VLibreria> ObtenerUltimo(int idGrupo, int idOrden)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Libreria
                                      where a.IdGrupo.Equals(idGrupo) & a.IdOrden.Equals(idOrden)
                                      select  new VLibreria
                                      {
                                          IdLibreria = a.IdLibrer
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
        #region Verficaciones
       
        #endregion
    }
}
