using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Usuario.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System.Data;
using System.Transactions;
using UTILITY.Enum.EnEstado;

namespace LOGIC.Class
{
    public class LUsuario
    {
        protected IUsuario iUsuario;
        public LUsuario()
        {
            iUsuario = new RUsuario();
        }
        #region Transacciones
        public bool Guardar(VUsuario vUsuario, List<VUsuario_01> detalle, ref int IdUsuario, string usuario)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = IdUsuario;
                    result = iUsuario.Guardar(vUsuario, ref IdUsuario);

                    if (aux == 0)//Nuevo 
                    {
                        var resultDetalle = new LUsuario_01().Nuevo(detalle, IdUsuario, usuario);
                    }
                    else//Modificar          
                    {
                        foreach (var i in detalle)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VUsuario_01> detalleNuevo = new List<VUsuario_01>();
                                detalleNuevo.Add(i);
                                var resultDetalle = new LUsuario_01().Nuevo(detalleNuevo, IdUsuario, usuario);
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {
                                var resultDetalle = new LUsuario_01().Modificar(i, IdUsuario, usuario);
                                if (resultDetalle == false)
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {
                                var resultDetalle = new LUsuario_01().Eliminar(i.IdUsuario_01);
                                if (resultDetalle == false)
                                {
                                    return false;
                                }
                            }

                        }
                    }
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EliminarUsuario(int IdUsuario, List<VUsuario_01> detalle)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    var resultDetalle = new LUsuario_01().EliminarTodoDetalle(IdUsuario, detalle);
                    if (resultDetalle == false)
                    {
                        return false;
                    }

                    result = iUsuario.Eliminar(IdUsuario);


                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        public List<VUsuario> Listar()
        {
            try
            {
                return iUsuario.ListaUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VUsuario> ValidarUsuario(string user, string password)
        {
            try
            {
                return iUsuario.ValidarUsuario(user, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
