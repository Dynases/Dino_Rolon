using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using ENTITY.Rol.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using System.Data.Entity;



namespace REPOSITORY.Clase
{
    public class RRol_01 :BaseConexion, IRol_01
    {
        #region Transacciones
        public bool Nuevo(List<VRol_01> Lista, int IdRol, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    //var rol = db.Rol.Where(t => t.IdRol == IdRol).FirstOrDefault();
                    
                    var idAux = IdRol;
                    Rol_01 rol_01 = new Rol_01();
                    foreach (var i in Lista)
                    {                        
                        rol_01.IdRol = IdRol;
                        rol_01.IdPrograma = i.IdPrograma;
                        rol_01.Show = i.Show;
                        rol_01.Add = i.Add;
                        rol_01.Mod = i.Mod;
                        rol_01.Del = i.Del;
                        rol_01.Fecha = DateTime.Now.Date;
                        rol_01.Hora = DateTime.Now.ToString("HH:mm");
                        rol_01.Usuario = usuario;
                  
                        db.Rol_01.Add(rol_01);
                        db.SaveChanges();                    
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VRol_01 Lista, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    Rol_01 rol_01 = db.Rol_01.Where(a => a.IdRol_01 == Lista.IdRol_01).FirstOrDefault();
                    if (rol_01 == null)
                    { throw new Exception("No existe el rol con id " + Lista.IdRol_01); }


                    rol_01.IdRol = Lista.IdRol;
                    rol_01.IdPrograma = Lista.IdPrograma;
                    rol_01.Show = Lista.Show;
                    rol_01.Add = Lista.Add;
                    rol_01.Mod = Lista.Mod;
                    rol_01.Del = Lista.Del;
                    rol_01.Fecha = DateTime.Now.Date;
                    rol_01.Hora = DateTime.Now.ToString("HH:mm");
                    rol_01.Usuario = usuario;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarDetalle(int IdRol, List<VRol_01> detalle)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    //Rol_01 rol_01 = new Rol_01();
                    //Rol_01 rol_01 = db.Rol_01.Where(b => b.IdRol == IdRol).ToList();
                    foreach (var i in detalle)
                    {
                        var query = (from a in db.Rol_01
                                     where a.IdRol==IdRol
                                     select a);

                        db.Rol_01.RemoveRange(query);
                        db.SaveChanges();
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdDetalle)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    
                    Rol_01 rol_01 = db.Rol_01.Where(a => a.IdRol_01 == IdDetalle).FirstOrDefault();
                    if (rol_01 == null)
                    { throw new Exception("No existe el rol con id " + IdDetalle); }
                                        
                    db.Rol_01.Remove(rol_01);
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
        public List<VRol_01> Lista(int IdRol)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Rol_01
                                      join b in db.Programa on a.IdPrograma equals b.IdPrograma                                      
                                      where a.IdRol==IdRol
                                      select new VRol_01
                                      {
                                          IdRol_01=a.IdRol_01,
                                          IdRol=a.IdRol,
                                          IdPrograma=a.IdPrograma,
                                          NombreProg=b.NombreProg,
                                          Descripcion=b.Descripcion,
                                          IdModulo= b.IdModulo,
                                          Show=a.Show,
                                          Add=a.Add,
                                          Mod=a.Mod,
                                          Del=a.Del,
                                          Estado=1                                       

                                      })
                                      .Union(from a in db.Programa                                             
                                             where !(from b in db.Rol_01 where b.IdRol == IdRol select b.IdPrograma).Contains(a.IdPrograma)                                            
                                             select new VRol_01
                                             {
                                                 IdRol_01 =0,
                                                 IdRol = IdRol,
                                                 IdPrograma = a.IdPrograma,
                                                 NombreProg = a.NombreProg,
                                                 Descripcion = a.Descripcion,
                                                 IdModulo = a.IdModulo,
                                                 Show = false,
                                                 Add = false,
                                                 Mod = false,
                                                 Del = false,
                                                 Estado = 0
                                                
                                             }

                                        ).ToList();
                    
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
