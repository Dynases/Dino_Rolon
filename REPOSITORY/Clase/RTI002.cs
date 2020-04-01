using DATA.EntityDataModel.DiAvi;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Data.Entity;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTI002 : BaseConexion, ITI002
    {
        #region Trasancciones

        public bool Guardar(int idAlmacenSalida,
                            string almacenSalida,
                            int idAlmacenDestino,
                            string almacenDestino,
                            int idTraspaso,
                            string usuario,
                            string observaciones,
                            int? concepto,
                            ref int idTI2)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti002 = new TI002
                    {
                        ibalm = idAlmacenSalida,
                        ibconcep = concepto,//FROM TCI001 id 6 TRASPASO SALIDA
                        ibdepdest = idAlmacenDestino,
                        ibest = 2,//NO SE SABE PORQUE
                        ibfact = DateTime.Now,
                        ibfdoc = DateTime.Now,
                        ibhact = DateTime.Now.ToShortTimeString(),
                        ibid = db.TI002.Max(t => t.ibid) + 1,
                        ididdestino = 0,
                        ibiddc = idTraspaso, //JOIN IMPLICITO A TABLA TRASPASO
                        ibobs = observaciones,
                        ibuact = usuario
                    };

                    db.TI002.Add(ti002);
                    db.SaveChanges();
                    idTI2 = ti002.ibid;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(int idAlmacenSalida,                          
                           int idAlmacenDestino,                          
                           int? idDetalle,
                           string usuario,
                           string observaciones,
                           int? concepto)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti002 = db.TI002.Where(t => t.ibiddc == idDetalle
                                               && t.ibconcep == concepto).FirstOrDefault();
                    ti002.ibfdoc = DateTime.Now;
                    ti002.ibconcep = concepto;
                    ti002.ibobs = observaciones;
                    ti002.ibest = 2;//NO SE SABE PORQUE
                    ti002.ibalm = idAlmacenSalida;                   
                    ti002.ibdepdest = idAlmacenDestino;
                    ti002.ididdestino = 0;
                    ti002.ibfact = DateTime.Now;
                    ti002.ibiddc = idDetalle; //JOIN IMPLICITO A TABLA TRASPASO
                    ti002.ibfdoc = DateTime.Now;
                    ti002.ibhact = DateTime.Now.ToShortTimeString();                  
                    ti002.ibuact = usuario;                 
                    db.TI002.Attach(ti002);
                    db.Entry(ti002).State = EntityState.Modified;
                    db.SaveChanges();                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdDetalle, int concepto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var tI002 = db.TI002.FirstOrDefault(b => b.ibiddc == IdDetalle &&
                                                                b.ibconcep == concepto);
                    db.TI002.Remove(tI002);
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
        #region Verificaciones

        public bool ExisteEnMovimiento(int idDetalle,int concepto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.TI002
                                     where a.ibiddc == idDetalle && a.ibconcep != concepto
                                     select a).Count();
                    return resultado != 0 ? true : false;
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
