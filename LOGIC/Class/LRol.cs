using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Rol.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System.Data;
using System.Transactions;
using UTILITY.Enum.EnEstado;


namespace LOGIC.Class
{
    public class LRol
    {
        protected IRol iRol;
        public LRol()
        {
            iRol = new RRol();            
        }
        #region Transacciones
        public bool Guardar(VRol vRol, List<VRol_01> detalle, ref int IdRol, string usuario)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = IdRol;
                    result = iRol.Guardar(vRol, ref IdRol);
                    
                    if (aux == 0)//Nuevo 
                    {
                        var resultDetalle = new LRol_01().Nuevo(detalle, IdRol, usuario);
                    }
                    else//Modificar          
                    {
                        foreach (var i in detalle)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VRol_01> detalleNuevo = new List<VRol_01>();
                                detalleNuevo.Add(i);
                                var resultDetalle = new LRol_01().Nuevo(detalleNuevo, IdRol, usuario);
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {
                                var resultDetalle = new LRol_01().Modificar(i, IdRol, usuario);
                                if (resultDetalle == false)
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {
                                var resultDetalle = new LRol_01().Eliminar(i.IdRol_01);
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

        public bool EliminarRol(int IdRol, List<VRol_01> detalle)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    var resultDetalle = new LRol_01().EliminarTodoDetalle(IdRol, detalle);
                    if (resultDetalle == false)
                    {
                        return false;
                    }

                    result = iRol.Eliminar(IdRol);
                                      
                   
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
        public List<VRol> Listar()
        {
            try
            {
                return iRol.ListaRol();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
