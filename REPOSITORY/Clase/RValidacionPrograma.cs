using ENTITY.adm.ValidacioinPrograma;
using Microsoft.SqlServer.Server;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
   public class RValidacionPrograma:BaseConexion, IValidacionPrograma
    {
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        public List<VValidacionPrograma> TrerValidacionProgramas(FValidacionPrograma validacionPrograma)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.ValidacionPrograma
                                      where a.TablaOrigen.Equals(validacionPrograma.tablaOrigen) &&                                           
                                            a.Estado == 1
                                      select new VValidacionPrograma
                                      {
                                          Id = a.Id,
                                          TablaOrigen = a.TablaOrigen,
                                          CampoOrigen = a.CampoOrigen,
                                          TableDestino = a.TableDestino,
                                          CampoDestino = a.CampoDestino,
                                          Programa = a.Programa
                                      }).ToList();
                    return listResult;
                }
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
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.ValidacionPrograma
                                      where a.TablaOrigen.Equals(validacionPrograma.tablaOrigen) &&
                                            a.IdGrupo == validacionPrograma.IdGrupo &&
                                            a.IdOrden == validacionPrograma.IdOrden &&
                                            a.Estado == 1
                                      select new VValidacionPrograma
                                      {
                                          Id = a.Id,
                                          TablaOrigen = a.TablaOrigen,
                                          CampoOrigen = a.CampoOrigen,
                                          TableDestino = a.TableDestino,
                                          CampoDestino = a.CampoDestino,
                                          Programa = a.Programa
                                      }).ToList();
                    return listResult;
                }
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
        public bool ExisteEnProgramaDestino(FValidacionPrograma validacionPrograma)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    List<SqlParameter> lPars = new List<SqlParameter>();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format( @"SELECT	
	                                COUNT(*) as Cantidad
                                FROM 
	                                {0}
                                WHERE
                                    {1} = @idOrigen ", validacionPrograma.tablaDestino, validacionPrograma.campoDestino));

                    lPars.Add(BD.CrearParametro("idOrigen", SqlDbType.Int, 0, validacionPrograma.idOrigen));
                    var resultado = BD.EjecutarConsulta(sb.ToString(), lPars.ToArray()).Tables[0];
                    var rResultado = resultado.Rows[0];
                    return Convert.ToInt32(rResultado["Cantidad"]) > 0 ? true : false;
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
