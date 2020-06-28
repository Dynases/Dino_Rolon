using ENTITY.adm.ValidacioinPrograma;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
   public class LValidacionPrograma
    {
        protected IValidacionPrograma iValidacionPrograma;
        public LValidacionPrograma()
        {
            iValidacionPrograma = new RValidacionPrograma();
        }
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        public List<VValidacionPrograma> TrerValidacionProgramas(FValidacionPrograma validacionPrograma)
        {
            try
            {
                return iValidacionPrograma.TrerValidacionProgramas(validacionPrograma);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VValidacionPrograma> TrerValidacionProgramasLibreria(FValidacionPrograma validacionPrograma)
        {
            try
            {
                return iValidacionPrograma.TrerValidacionProgramasLibreria(validacionPrograma);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTE ***********/
        #endregion
        #region Transacciones

        #endregion
        #region Verificaciones
        public bool ValidadrEliminacion(int idTablaDestino, FValidacionPrograma validacionPrograma, ref List<string> respuesta, bool esLibreria)
        {
            try
            {
                List<VValidacionPrograma> programas = new List<VValidacionPrograma>();
                if (esLibreria)                
                    programas = this.TrerValidacionProgramasLibreria(validacionPrograma);                
                else
                    programas = this.TrerValidacionProgramas(validacionPrograma);

                if (programas.Count > 0)
                {
                    foreach (var fila in programas)
                    {
                        validacionPrograma.tablaDestino = fila.TableDestino;
                        validacionPrograma.campoDestino = fila.CampoDestino;
                        validacionPrograma.idOrigen = idTablaDestino;
                        if (ExisteEnProgramaDestino(validacionPrograma))
                        {
                            respuesta.Add("Esta asociado a una transacción:" + fila.Programa);
                        }
                    }
                     
                }
                return respuesta.Count == 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnProgramaDestino(FValidacionPrograma validacionPrograma)
        {
            try
            {

                return iValidacionPrograma.ExisteEnProgramaDestino(validacionPrograma);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

