using DATA.EntityDataModel.DiAvi;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Base
{
    public abstract class BaseConexionPrincipal <T> where T: DbContext
    {
        protected abstract T GetEsquema();
    }
}
