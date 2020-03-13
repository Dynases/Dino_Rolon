using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Compra.View;
using DATA.EntityDataModel.DiAvi;

namespace REPOSITORY.Clase
{
    public class RCompra : BaseConexion, ICompra
    {

        public bool Guardar(VCompra vCompraIngreso, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Compra Compra;
                    if (id > 0)
                    {
                        Compra = db.Compra.Where(a => a.Id == idAux).FirstOrDefault();
                        if (Compra == null)
                            throw new Exception("No existe la compra con id " + idAux);
                    }
                    else
                    {
                        Compra = new Compra();
                        db.Compra.Add(Compra);
                    }
                    Compra.IdAlmacen = vCompraIngreso.IdAlmacen;
                    Compra.IdProvee = vCompraIngreso.IdProvee;
                    Compra.Estado = vCompraIngreso.Estado;
                    Compra.FechaDoc = vCompraIngreso.FechaDoc;
                    Compra.TipoVenta = vCompraIngreso.TipoVenta;
                    Compra.FechaVen = vCompraIngreso.FechaVen;
                    Compra.Observ = vCompraIngreso.Observ;
                    Compra.Descu = vCompraIngreso.Descu;
                    Compra.Total = vCompraIngreso.Total;
                    Compra.Fecha = vCompraIngreso.Fecha;
                    Compra.Hora = vCompraIngreso.Hora;
                    Compra.Usuario = vCompraIngreso.Usuario;
                    db.SaveChanges();
                    id = Compra.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region CONSULTA
        public List<VCompraLista> Lista()
        {

            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Compra
                                      join b in db.Proveed on a.IdProvee equals b.Id
                                      select new VCompraLista
                                      {
                                          Id = a.Id,
                                          IdAlmacen = a.IdAlmacen,
                                          IdProvee = a.IdProvee,
                                          Proveedor = b.Descrip,
                                          Estado = a.Estado,
                                          FechaDoc = a.FechaDoc,
                                          TipoVenta = a.TipoVenta,
                                          NombreTipo = a.TipoVenta == 1 ? "CONTADO" : "CREDITO",
                                          Descu = a.Descu,
                                          Total = a.Total,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario
                                      }).ToList();
                    return listResult;
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
