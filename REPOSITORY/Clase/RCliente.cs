
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Cliente.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum;
using ENTITY.Telefono.View;
using UTILITY.Enum.EnEstaticos;
using System.Data.Entity;
using System.Data;

namespace REPOSITORY.Clase
{

    public class RCliente : BaseConexion, ICliente
    {
        #region Consultas
        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VCliente> ListarCliente(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Cliente
                                      where a.Id.Equals(id)
                                      select new VCliente
                                      {
                                          Id = a.Id,
                                          IdSpyre = a.IdSpyre,
                                          Descripcion = a.Descrip,
                                          RazonSocial = a.RazonSo,
                                          TipoCliente = a.TipoCli,
                                          Nit = a.Nit,
                                          Direcccion = a.Direcc,
                                          Contacto1 = a.Contac1,
                                          Contacto2 = a.Contac2,
                                          Telfono1 = a.Telfo1,
                                          Telfono2 = a.Telfo2,
                                          Email1 = a.Email1,
                                          Email2 = a.Email2,
                                          Ciudad = a.Ciudad,
                                          Facturacion = a.Factur,
                                          Latitud = a.Latitud,
                                          Longittud = a.Longit,
                                          Imagen = a.Imagen,
                                          TotalCred = a.TotalCred,
                                          Dias = a.Dias,
                                          IdCategoria = a.IdCategoria,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal TraerLimiteCredito(int clienteId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    //var limiteCredito = Convert.ToDecimal( 
                    //                        db.Cliente
                    //                        .Where(x => x.Id == clienteId && x.TipoCli == 2)
                    //                        .Select(x=> x.TotalCred));
                    var limiteCredito = db.Cliente
                                       .Where(x => x.Id == clienteId && x.TipoCli == 2)
                                       .Count() == 0? 
                                        0: 
                                        db.Cliente
                                          .Where(x => x.Id == clienteId && x.TipoCli == 2)
                                          .First().TotalCred;
                    return limiteCredito;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VCliente> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Cliente
                                      select new VCliente
                                      {
                                          Id = a.Id,
                                          IdSpyre = a.IdSpyre,
                                          Descripcion = a.Descrip,
                                          RazonSocial = a.RazonSo,
                                          TipoCliente = a.TipoCli,
                                          Nit = a.Nit,
                                          Direcccion = a.Direcc,
                                          Contacto1 = a.Contac1,
                                          Contacto2 = a.Contac2,
                                          Telfono1 = a.Telfo1,
                                          Telfono2 = a.Telfo2,
                                          Email1 = a.Email1,
                                          Email2 = a.Email2,
                                          Ciudad = a.Ciudad,
                                          Facturacion = a.Factur
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VClienteLista> ListarClientes()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Cliente
                                      select new VClienteLista
                                      {
                                          Id = a.Id,
                                          Descripcion = a.Descrip,
                                          RazonSocial = a.RazonSo,
                                          Ciudad = a.Ciudad,
                                          NombreCiudad = "",
                                          Contacto1 = a.Contac1,
                                          Contacto2 = a.Contac2,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado()
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = @"SELECT
                                     c.Id as 'COD',
                                     C.IdSpyre as 'CÓDIGO SPYRE',
                                     c.Descrip as 'Nombre y Apellido', 
                                     c.RazonSo as 'Razon Social', 
                                     C.Nit as 'NIT',
                                     c.Direcc as 'Direccion',
                                     b.Descrip as 'Ciudad',
                                     c.Factur as 'Facturacion',
                                     c.IdCategoria
                                    FROM 
                                        REG.Cliente c
                                        JOIN ADM.Libreria b ON b.IdGrupo = 2 AND b.IdOrden = 1 AND b.IdLibrer = c.Ciudad
                                    GROUP BY
                                      c.Id, c.IdSpyre, c.Descrip, c.RazonSo, c.Nit, c.Direcc, b.Descrip, c.Factur, c.IdCategoria";
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VClienteCombo> TraerClienteCombo()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Cliente
                      .Select(v => new VClienteCombo
                      {
                          Id = v.Id,
                          Cliente = v.Descrip,
                          Nit = v.Nit,
                          EmpresaProveedora = db.Libreria.FirstOrDefault(a => a.IdGrupo == (int)ENEstaticosGrupo.CLIENTE &&
                                                                              a.IdOrden == (int)ENEstaticosOrden.FACTURACION_CLIENTE &&
                                                                              a.IdLibrer == v.Factur).Descrip,
                          IdCategoriaPrecio = v.IdCategoria  ,
                          FacturaEmpresa = v.Factur,
                          tipoCliente = v.TipoCli
                      }).ToList();                   
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        public bool Guardar(VCliente vcliente, ref int idCLiente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var cliente = new Cliente();
                    cliente.IdSpyre = vcliente.IdSpyre;
                    cliente.Estado = vcliente.Estado;
                    cliente.Descrip = vcliente.Descripcion;
                    cliente.RazonSo = vcliente.RazonSocial;
                    cliente.Nit = vcliente.Nit;
                    cliente.TipoCli = vcliente.TipoCliente;
                    cliente.Direcc = vcliente.Direcccion;
                    cliente.Contac1 = vcliente.Contacto1;
                    cliente.Contac2 = vcliente.Contacto2;
                    cliente.Telfo1 = vcliente.Telfono1;
                    cliente.Telfo2 = vcliente.Telfono2;
                    cliente.Email1 = vcliente.Email1;
                    cliente.Email2 = vcliente.Email2;
                    cliente.Ciudad = vcliente.Ciudad;
                    cliente.Factur = vcliente.Facturacion;
                    cliente.Longit = vcliente.Longittud;
                    cliente.Latitud = vcliente.Latitud;
                    cliente.Imagen = vcliente.Imagen;
                    cliente.TotalCred = vcliente.TotalCred;
                    cliente.Dias = vcliente.Dias;
                    cliente.Fecha = vcliente.Fecha;
                    cliente.Hora = vcliente.Hora;
                    cliente.IdCategoria = vcliente.IdCategoria;
                    cliente.Usuario = vcliente.Usuario;
                    db.Cliente.Add(cliente);
                    db.SaveChanges();
                    idCLiente = cliente.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCliente vcliente, int idCLiente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var cliente = db.Cliente.FirstOrDefault(b => b.Id == idCLiente);
                    cliente.IdSpyre = vcliente.IdSpyre;
                    cliente.Estado = vcliente.Estado;
                    cliente.Descrip = vcliente.Descripcion;
                    cliente.RazonSo = vcliente.RazonSocial;
                    cliente.Nit = vcliente.Nit;
                    cliente.TipoCli = vcliente.TipoCliente;
                    cliente.Direcc = vcliente.Direcccion;
                    cliente.Contac1 = vcliente.Contacto1;
                    cliente.Contac2 = vcliente.Contacto2;
                    cliente.Telfo1 = vcliente.Telfono1;
                    cliente.Telfo2 = vcliente.Telfono2;
                    cliente.Email1 = vcliente.Email1;
                    cliente.Email2 = vcliente.Email2;
                    cliente.Ciudad = vcliente.Ciudad;
                    cliente.Factur = vcliente.Facturacion;
                    cliente.Longit = vcliente.Longittud;
                    cliente.Latitud = vcliente.Latitud;
                    cliente.Imagen = vcliente.Imagen;
                    cliente.TotalCred = vcliente.TotalCred;
                    cliente.Dias = vcliente.Dias;
                    cliente.Fecha = vcliente.Fecha;
                    cliente.Hora = vcliente.Hora;
                    cliente.Usuario = vcliente.Usuario;
                    cliente.IdCategoria = vcliente.IdCategoria;
                    db.Cliente.Attach(cliente);
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                    idCLiente = cliente.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
     
        public bool Eliminar(int IdCliente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var cliente = db.Cliente.FirstOrDefault(b => b.Id == IdCliente);
                    db.Cliente.Remove(cliente);
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
        public bool ExisteEnVenta(int idCliente)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Cliente
                                     join b in db.Venta on a.Id equals b.IdCliente
                                     where b.IdCliente.Equals(idCliente) && b.Estado != -1
                                     select a).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EsClienteCredito(int clienteId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.Cliente
                                .Where(x => x.Id == clienteId && x.TipoCli == 2)
                                .Count() > 0;
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