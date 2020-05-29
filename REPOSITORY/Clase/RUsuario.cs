using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Usuario.View;
using DATA.EntityDataModel.DiAvi;
using System.Data;
using REPOSITORY.Base;
using REPOSITORY.Interface;



namespace REPOSITORY.Clase
{
    public class RUsuario:BaseConexion, IUsuario
    {
        #region Transacciones
        public bool Guardar(VUsuario vUsuario, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Usuario usuario;
                    if (id > 0)
                    {
                        usuario = db.Usuario.Where(a => a.IdUsuario == idAux).FirstOrDefault();
                        if (usuario == null)
                            throw new Exception("No existe el usuario con id " + idAux);
                    }
                    else
                    {
                        usuario = new Usuario();
                        db.Usuario.Add(usuario);
                    }

                    usuario.User = vUsuario.User;
                    usuario.Password = vUsuario.Password;
                    usuario.IdRol = vUsuario.IdRol;
                    usuario.Estado = vUsuario.Estado;
                    usuario.Fecha= DateTime.Now.Date;                   
                    usuario.Hora = DateTime.Now.ToString("HH:mm");
                    usuario.Usuario1 = vUsuario.Usuario1;

                    db.SaveChanges();
                    id =usuario.IdUsuario;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdUsuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var usuario = db.Usuario.FirstOrDefault(b => b.IdUsuario == IdUsuario);
                    db.Usuario.Remove(usuario);
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
        public List<VUsuario> ListaUsuarios()
        {

            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Usuario
                                      select new VUsuario
                                      {
                                          IdUsuario=a.IdUsuario,
                                          User=a.User,
                                          Password=a.Password,
                                          IdRol = a.IdRol,
                                          Estado=a.Estado.Value,                             
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario1 = a.Usuario1
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
