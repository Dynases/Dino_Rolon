using ENTITY.adm.ValidacioinPrograma;
using ENTITY.DiSoft.Libreria;
using ENTITY.Libreria.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;

namespace LOGIC.Class
{
   public class LLibreria
    {
        protected ILibreria iLibreria;
        public LLibreria()
        {
            iLibreria = new RLibreria();
        }
        #region Consultas
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        public List<VLibreria> Listar(int idGrupo, int idOrden)
        {
            try
            {
                return iLibreria.Listar(idGrupo, idOrden);
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
                return iLibreria.TraerProgramas();
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
                return iLibreria.TraerCategorias(idPrograma);
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
                return iLibreria.TraerLibreriasXCategoria(idGrupo, idOrden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Transacciones
        public bool Guardar(VLibreriaLista vlibreria)
        {
            try
            {

                using (var scope = new TransactionScope())
                {
                    var result = iLibreria.Guardar(vlibreria);
                    if (vlibreria.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO)
                    {
                        VLibreriaD vLibreriaDisoft = new VLibreriaD()
                        {
                           cecon = 101,
                           cenum = vlibreria.IdLibrer,
                           cefact = vlibreria.Fecha,
                           cehact = vlibreria.Hora,
                           ceuact =  vlibreria.Usuario
                        };
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
        public bool Modificar( List<VLibreriaLista> vlibreria, ref List<string> mensaje)
        {
            try
            {
                FValidacionPrograma validacionPrograma = new FValidacionPrograma();
                validacionPrograma.tablaOrigen = "ADM.Libreria";
                using (var scope = new TransactionScope())
                {
                    bool resultado = false;
                    foreach (var fila in vlibreria)
                    {
                        if (fila.estado == (int)ENEstado.MODIFICAR)
                        {
                            resultado = iLibreria.Modificar(fila);
                        }
                        if (fila.estado == (int)ENEstado.ELIMINAR)
                        {
                            validacionPrograma.IdGrupo = fila.IdGrupo;
                            validacionPrograma.IdOrden = fila.IdOrden;
                            if (new LValidacionPrograma().ValidadrEliminacion(fila.IdLibrer, validacionPrograma, ref mensaje, true))
                            {
                                resultado = iLibreria.Eliminar(fila);
                            }
                        }
                    }
                    if (mensaje.Count > 0)
                    {
                        return false;
                    }
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(VLibreriaLista vlibreria)
        {
            try
            {

                using (var scope = new TransactionScope())
                {
                    var result = iLibreria.Eliminar(vlibreria);
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


    }
}
