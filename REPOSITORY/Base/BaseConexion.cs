using DATA.EntityDataModel.DiAvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Base
{
    public class BaseConexion : BaseConexionPrincipal<DiAviEntities>
    {
        protected override DiAviEntities GetEsquema()
        {
            return new DiAviEntities();
        }
        private static BaseConexion _instancia;
        public static BaseConexion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new BaseConexion();
                }
                return _instancia;
            }
        }
    }
}
