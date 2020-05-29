using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Rol.View;
using DATA.EntityDataModel.DiAvi;
using System.Data;
using REPOSITORY.Base;
using REPOSITORY.Interface;

namespace REPOSITORY.Clase
{
    public class RRol : BaseConexion, IRol
    {
        #region Transacciones
        
        public bool Guardar(VRol vRol, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Rol rol;
                    if (id > 0)
                    {
                        rol = db.Rol.Where(a => a.IdRol == idAux).FirstOrDefault();
                        if (rol == null)
                            throw new Exception("No existe el rol con id " + idAux);
                    }
                    else
                    {
                        rol = new Rol();
                        db.Rol.Add(rol);
                    }

                    rol.Rol1 = vRol.Rol1;
                    rol.Fecha = DateTime.Now.Date;
                    rol.Hora = DateTime.Now.ToString("HH:mm");
                    rol.Usuario = vRol.Usuario;

                    db.SaveChanges();
                    id = rol.IdRol;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdRol)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var rol = db.Rol.FirstOrDefault(b => b.IdRol == IdRol);
                    db.Rol.Remove(rol);
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
        public List<VRol> ListaRol()
        {

            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Rol                         
                                      select new VRol
                                      {
                                          IdRol = a.IdRol,
                                          Rol1 = a.Rol1,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario= a.Usuario                                          
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable AsignarPermisos(string idRol, string NombreProg)
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = "SELECT * " +
                                  "FROM " + " ADM.Rol_01 a " +
                                  "JOIN ADM.Programa b ON a.IdPrograma=b.IdPrograma " +
                                   "WHERE " +
                                        "a.IdRol= " + idRol + " and b.NombreProg = '" + NombreProg + "'";
                                    
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region Verificaciones
        public bool ExisteEnUsuario(int IdRol)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Rol
                                     join b in db.Usuario on  a.IdRol equals b.IdRol
                                     where a.IdRol.Equals(IdRol) && b.Estado > 0
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
