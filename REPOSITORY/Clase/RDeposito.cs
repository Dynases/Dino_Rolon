using ENTITY.inv.Deposito;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase
{
    public class RDeposito : BaseConexion, IDeposito
    {
        public List<VDepositoCombo> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from d in db.Deposito
                                      select new VDepositoCombo
                                      {
                                          Id = d.Id,
                                          Descripcion = d.Descrip
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
