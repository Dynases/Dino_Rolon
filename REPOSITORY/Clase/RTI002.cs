using DATA.EntityDataModel.DiAvi;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase
{
    public class RTI002 : BaseConexion, ITI002
    {
        #region Trasancciones

        public bool GuardarTransaccion()
        {
            try
            {
                //var t2 = new TI002
                //{
                //    ibalm = 1,
                //    ibconcep = 
                //};

                using (var db = this.GetEsquema())
                {


                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
