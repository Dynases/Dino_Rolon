using ENTITY.Plantilla;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;

namespace LOGIC.Class
{
    public class LPlantilla
    {
        protected IPlantilla iPlantilla;

        public LPlantilla()
        {
            iPlantilla = new RPlantilla();
        }

        #region Trasancciones

        public bool Guardar(VPlantilla VPlantilla)
        {
            try
            {
                return this.iPlantilla.Guardar(VPlantilla);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consultas

        public List<VPlantilla> Listar()
        {
            try
            {
                return this.iPlantilla.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
