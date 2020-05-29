using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using ENTITY.Usuario.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using System.Data.Entity;

namespace REPOSITORY.Clase
{
    public class RUsuario_01 : BaseConexion, IUsuario_01
    {
        #region Transacciones
        public bool Nuevo(List<VUsuario_01> Lista, int IdUsuario, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {             

                    var idAux = IdUsuario;
                    Usuario_01 usuario_01 = new Usuario_01();
                    foreach (var i in Lista)
                    {
                        usuario_01.IdUsuario = IdUsuario;
                        usuario_01.IdAlmacen = i.IdAlmacen;
                        usuario_01.Acceso = i.Acceso;
                        usuario_01.Fecha = DateTime.Now.Date;
                        usuario_01.Hora = DateTime.Now.ToString("HH:mm");
                        usuario_01.Usuario = usuario;                        

                        db.Usuario_01.Add(usuario_01);
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
        public bool Modificar(VUsuario_01 Lista, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    Usuario_01 usuario_01 = db.Usuario_01.Where(a => a.IdUsuario_01 == Lista.IdUsuario_01).FirstOrDefault();
                    if (usuario_01 == null)
                    { throw new Exception("No existe el usuario con id " + Lista.IdUsuario_01); }


                    usuario_01.IdUsuario = Lista.IdUsuario;
                    usuario_01.IdAlmacen = Lista.IdAlmacen;
                    usuario_01.Acceso = Lista.Acceso;
                    usuario_01.Fecha = DateTime.Now.Date;
                    usuario_01.Hora = DateTime.Now.ToString("HH:mm");
                    usuario_01.Usuario = usuario;
                    db.SaveChanges();
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

                    Usuario_01 usuario_01 = db.Usuario_01.Where(a => a.IdUsuario_01 == IdDetalle).FirstOrDefault();
                    if (usuario_01 == null)
                    { throw new Exception("No existe el usuario con id " + IdDetalle); }

                    db.Usuario_01.Remove(usuario_01);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarDetalle(int IdUsuario, List<VUsuario_01> detalle)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    
                    foreach (var i in detalle)
                    {
                        var query = (from a in db.Usuario_01
                                     where a.IdUsuario == IdUsuario
                                     select a);

                        db.Usuario_01.RemoveRange(query);
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

        #endregion

        #region Consultas
        public List<VUsuario_01> Lista(int IdUsuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Usuario_01
                                      join b in db.Almacen on a.IdAlmacen equals b.Id
                                      join c in db.Sucursal on b.IdSuc equals c.Id
                                      where a.IdUsuario == IdUsuario
                                      select new VUsuario_01
                                      {
                                          IdUsuario_01=a.IdUsuario_01,
                                          IdUsuario=a.IdUsuario,
                                          IdAlmacen=a.IdAlmacen,
                                          DescripAlm=b.Descrip,
                                          IdSuc=b.IdSuc,
                                          DescripSuc=c.Descrip,
                                          Acceso = a.Acceso.Value,
                                          Estado=1,                                    

                                      })
                                      .Union(from a in db.Almacen
                                             join b in db.Sucursal on a.IdSuc equals b.Id
                                             where !(from c in db.Usuario_01 where c.IdUsuario == IdUsuario select c.IdAlmacen).Contains(a.Id)
                                             select new VUsuario_01
                                             {
                                                 IdUsuario_01 = 0,
                                                 IdUsuario = IdUsuario,
                                                 IdAlmacen = a.Id,
                                                 DescripAlm = a.Descrip,
                                                 IdSuc = a.IdSuc,
                                                 DescripSuc = b.Descrip,
                                                 Acceso = false,
                                                 Estado = 0,

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
