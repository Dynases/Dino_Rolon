using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Libreria.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;

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
        public bool Modificar(VLibreriaLista vLibreria)
        {
            try
            {
                using (var db = GetEsquema())
                {

                    var libreria = db.Libreria
                                    .Where(a => a.IdGrupo == vLibreria.IdGrupo &&
                                                a.IdOrden == vLibreria.IdOrden &&
                                                a.IdLibrer == vLibreria.IdLibrer).FirstOrDefault();                    
                    libreria.Descrip = vLibreria.Descrip;
                    libreria.Fecha = vLibreria.Fecha;                 
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(VLibreriaLista vLibreria)
        {
            try
            {
                using (var db = GetEsquema())
                {

                    var libreria = db.Libreria
                                    .Where(a => a.IdGrupo == vLibreria.IdGrupo &&
                                                a.IdOrden == vLibreria.IdOrden &&
                                                a.IdLibrer == vLibreria.IdLibrer).FirstOrDefault();                  
                    db.Libreria.Remove(libreria);
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
        #region Consultas
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
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
        public List<VLibreria> TraerProgramas()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Estaticos
                      .Select(v => new VLibreria
                      {
                          IdLibreria = v.Id,
                          Descripcion = v.Grupo,
                      }).Distinct().ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VLibreria> TraerCategorias(int idPrograma)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Estaticos
                                    .Where(a=> a.Id == idPrograma)
                      .Select(v => new VLibreria
                      {
                          IdLibreria = v.Orden,
                          Descripcion = v.Descrip,
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
        public List<VLibreriaLista> TraerLibreriasXCategoria(int idGrupo, int idOrden)
        {
            try           
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Libreria
                                      where a.IdGrupo.Equals(idGrupo) & a.IdOrden.Equals(idOrden)
                                      select new VLibreriaLista
                                      {
                                          IdGrupo = a.IdGrupo,
                                          IdOrden = a.IdOrden,
                                          IdLibrer = a.IdLibrer,
                                          Descrip = a.Descrip,
                                          estado = (int)ENEstado.GUARDADO
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
        public bool ExisteEnClienteCiudad(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Libreria
                                     join b in db.Cliente on a.IdLibrer equals b.Ciudad
                                     where a.IdGrupo == (int)ENEstaticosGrupo.CLIENTE &&
                                           a.IdOrden == (int)ENEstaticosOrden.FACTURACION_CLIENTE  &&
                                           a.IdLibrer == id
                                     select a).Count();
                    return resultado != 0 ? true : false;
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
