using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.TI002.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTILITY.Enum.ENConcepto;

namespace REPOSITORY.Clase
{
  
    public class RTI002 : BaseConexion, ITI002
    {
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public VTI002 TraerMovimiento(int idDetalle, int idConcepto)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.TI002
                       .Where(v => v.ibiddc == idDetalle && v.ibconcep == idConcepto)
                       .Select(v => new VTI002
                       {
                           IdManual = v.ibid,
                           fechaDocumento = v.ibfdoc,
                           IdConecpto = v.ibconcep,
                           Observacion = v.ibobs,                          
                           Estado = v.ibest,
                           IdAlmacenOrigen = v.ibalm,
                           idAlmacenDestino = v.ibdepdest,
                           IdDestino = v.ididdestino,
                           IdDetalle = v.ibiddc

                       }).FirstOrDefault();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/

        /********** REPORTE ***********/
        #endregion
        #region Trasancciones

        public bool Guardar(int idAlmacenOrigen,                            
                            int idAlmacenDestino,                           
                            int idDetalle,
                            string usuario,
                            string observacion,
                            int concepto,
                            ref int idTI2,
                            int idDestino)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti002 = new TI002
                    {
                        ibalm = idAlmacenOrigen,
                        ibconcep = concepto,
                        ibdepdest = idAlmacenDestino,
                        ibest = 2,
                        ibfact = DateTime.Now,
                        ibfdoc = DateTime.Now,
                        ibhact = DateTime.Now.ToShortTimeString(),
                        ibid = db.TI002.Select(a => a.ibid).DefaultIfEmpty(0).Max() + 1,
                        ididdestino = idDestino,
                        ibiddc = idDetalle, 
                        ibobs = observacion,
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
                           int idDetalle,
                           string usuario,
                           string observaciones,
                           int concepto,
                           int idDestino)
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
                    ti002.ididdestino = idDestino;
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
        public bool ModificarCampoDestinoTraspaso(int idTraspaso)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var IdmovimientoOrigen = db.TI002.FirstOrDefault(b => b.ibiddc == idTraspaso &&
                                                                b.ibconcep == (int)ENConcepto.TRASPASO_SALIDA).ibid;

                    var IdmovimientoDestino= db.TI002.FirstOrDefault(b => b.ibiddc == idTraspaso &&
                                                              b.ibconcep == (int)ENConcepto.TRASPASO_INGRESO).ibid;
                    var movimientos = db.TI002.Where(b => b.ibiddc == idTraspaso).ToList();
                    foreach (var fila in movimientos)
                    {
                        if (fila.ibconcep == (int)ENConcepto.TRASPASO_SALIDA)
                        {
                            fila.ididdestino = IdmovimientoDestino;
                        }
                        if (fila.ibconcep == (int)ENConcepto.TRASPASO_INGRESO)
                        {
                            fila.ididdestino = IdmovimientoOrigen;
                        }                        
                    }
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
                                     where a.ibiddc == idDetalle && a.ibconcep == concepto
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
