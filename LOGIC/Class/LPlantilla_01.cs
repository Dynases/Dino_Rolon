using ENTITY.Plantilla;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;

namespace LOGIC.Class
{
    public class LPlantilla_01
    {
        protected IPlantilla_01 iPlantilla01;

        public LPlantilla_01()
        {
            iPlantilla01 = new RPlantilla01();
        }

        #region Trasancciones

        public bool Guardar(List<VPlantilla01> lista, int PlantillaId)
        {
            try
            {
                return this.iPlantilla01.Guardar(lista, PlantillaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consultas

        public List<VPlantilla01> ListarDetallePlantilla(int PlantillaId)
        {
            try
            {
                return this.iPlantilla01.ListarDetallePlantilla(PlantillaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
