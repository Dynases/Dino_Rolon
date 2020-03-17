using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_02;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstaticos;
using System.Data;

namespace REPOSITORY.Clase
{
    public class RCompraIngreso_02 : BaseConexion, ICompraIngreso_02
    {
        #region Transaccion
        public bool Guardar(VCompraIngreso_02 Lista)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var CompraIngreso_02 = new CompraIng_02();
                    CompraIngreso_02.Id = Lista.Id;
                    CompraIngreso_02.IdLibreria = Lista.IdLibreria;
                    CompraIngreso_02.Descripcion = Lista.Descripcion;
                    db.CompraIng_02.Add(CompraIngreso_02);
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
        #region Consulta
        public List<VCompraIngreso_02> Listar()
        {

            try
            {
                using (var db = GetEsquema())
                {
                    var grupo = (int)ENEstaticosGrupo.COMPRA_INGRESO;
                    var orden = (int)ENEstaticosOrden.COMPRA_INGRESO_PLACA;
                    var listResult = (from a in db.CompraIng_02
                                      join v in db.Libreria on a.IdLibreria equals v.IdLibrer
                                      where v.IdOrden.Equals(orden) && v.IdGrupo.Equals(grupo)
                                      select new VCompraIngreso_02
                                      {
                                         Id = a.Id,
                                         IdLibreria= a.IdLibreria,
                                         Descripcion= a.Descripcion,
                                         Libreria = v.Descrip,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarTabla()
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = @"SELECT
	                                     A.Id,
	                                     a.IdLIbreria,
	                                     B.Descrip Libreria,
	                                     a.Descripcion
                                    FROM 
	                                    COM.CompraIng_02 A  JOIN
	                                    ADM.Libreria B ON B.IdGrupo = 4 AND B.IdOrden = 1 AND B.IdLibrer = A.IdLibreria";
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
