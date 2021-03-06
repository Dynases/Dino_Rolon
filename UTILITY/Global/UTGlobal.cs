﻿using Janus.Windows.GridEX.EditControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using ENTITY.Libreria.View;
using System.IO;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Windows.Forms;
using ENTITY.inv.Almacen.View;
using ENTITY.inv.Sucursal.View;
using ENTITY.inv.TipoAlmacen.view;
using ENTITY.reg.PrecioCategoria.View;
using ENTITY.DiSoft.Zona;
using ENTITY.com.CompraIngreso_02;
using ENTITY.Proveedor.View;
using ENTITY.com.CompraIngreso.View;
using UTILITY.Enum.EnEstado;
using ENTITY.Cliente.View;
using System.Linq.Expressions;
using ENTITY.inv.Concepto.View;
using ENTITY.DiSoft.Personal;

namespace UTILITY.Global
{
    public static class UTGlobal
    {
        public static string Usuario = "DEFAULT";
        public static int Mayusculas = 0;
        public static int UsuarioRol = 0;
        public static int UsuarioId = 0;
        public static string NombreButton = "";
        public static Visualizador visualizador;
        public static string lote = "20170101";
        public static DateTime fechaVencimiento = Convert.ToDateTime("2017-01-01");
        //**Carpetas
        #region Carpetas
        public static string RutaTemporal = @"C:\Temporal";
        #endregion

        #region Metodos Globales

        public static void MG_ArmarCombo(MultiColumnCombo combo, List<VLibreria> lLibreria)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("idLibreria").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("Descripcion").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "idLibreria";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = lLibreria;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarComboPersonal(MultiColumnCombo combo, List<VPersonalCombo> Personal)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Descripcion").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "Id";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = Personal;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarCombos<T>(MultiColumnCombo combo, T Lista)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Descripcion").Width = 280;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "Id";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = Lista;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarComboConcepto(MultiColumnCombo combo, List<VConceptoCombo> conceptos)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 100;
                combo.DropDownList.Columns[0].Caption = "Id";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Descripcion").Width = 280;
                combo.DropDownList.Columns[1].Caption = "Descripción";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("TipoMovimiento").Visible = false;

                combo.ValueMember = "Id";
                combo.DisplayMember = "Descripcion";
                combo.DataSource = conceptos;
                combo.Refresh();
                combo.Value = 1;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarComboConPrimerFila(MultiColumnCombo combo, List<VLibreria> lLibreria)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("idLibreria").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("Descripcion").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                VLibreria PrimerFile = new VLibreria()
                {
                    IdLibreria = 0,
                    Descripcion = "TODOS"
                };
                lLibreria.Insert(0, PrimerFile);
                combo.ValueMember = "idLibreria";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = lLibreria;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarComboProveedores(MultiColumnCombo combo, List<VProveedorCombo> lProveedores, ENEstado cargarPrimerFila)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("Descripcion").Width = 180;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("EdadSemana").Width = 80;
                combo.DropDownList.Columns[2].Caption = "Edad Semana";
                combo.DropDownList.Columns[2].Visible = true;
                if (cargarPrimerFila == ENEstado.CARGARPRIMERFILA)
                {
                    VProveedorCombo PrimerFile = new VProveedorCombo()
                    {
                        Id = 0,
                        Descripcion = "TODOS",
                        EdadSemana = "TODOS",
                    };
                    lProveedores.Insert(0, PrimerFile);
                }              
                combo.ValueMember = "Id";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = lProveedores;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarComboClientes(MultiColumnCombo combo, List<VClienteCombo> lClientes, ENEstado cargarPrimerFila)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("Cliente").Width = 350;
                combo.DropDownList.Columns[1].Caption = "Cliente";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("Nit").Width = 80;
                combo.DropDownList.Columns[2].Caption = "Nit";
                combo.DropDownList.Columns[2].Visible = false;

                combo.DropDownList.Columns.Add("EmpresaProveedora").Width = 80;
                combo.DropDownList.Columns[3].Caption = "EmpresaProveedora";
                combo.DropDownList.Columns[3].Visible = false;

                combo.DropDownList.Columns.Add("IdCategoriaPrecio").Width = 80;
                combo.DropDownList.Columns[4].Caption = "IdCategoriaPrecio";
                combo.DropDownList.Columns[4].Visible = false;
                if (cargarPrimerFila == ENEstado.CARGARPRIMERFILA)
                {
                    VClienteCombo PrimerFile = new VClienteCombo()
                    {
                        Id = 0,
                        Cliente = "TODOS",
                        Nit = "TODOS",
                        EmpresaProveedora = "",
                        IdCategoriaPrecio = 0
                    };
                    lClientes.Insert(0, PrimerFile);
                }
                combo.ValueMember = "Id";
                combo.DisplayMember = "Cliente";
                combo.DropDownList.DataSource = lClientes;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarMultiComboPlaca(MultiColumnCombo combo, List<VCompraIngreso_02> lLibreria)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Id";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("IdLibreria").Width = 150;
                combo.DropDownList.Columns[1].Caption = "IdLibreria";
                combo.DropDownList.Columns[1].Visible = false;

                combo.DropDownList.Columns.Add("Descripcion").Width = 180;
                combo.DropDownList.Columns[2].Caption = "Nombre";
                combo.DropDownList.Columns[2].Visible = true;

                combo.DropDownList.Columns.Add("Libreria").Width = 150;
                combo.DropDownList.Columns[3].Caption = "Placa";
                combo.DropDownList.Columns[3].Visible = true;

                combo.ValueMember = "IdLibreria";
                combo.DisplayMember = "Descripcion";
                combo.DisplayMember = "Libreria";
                combo.DropDownList.DataSource = lLibreria;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarMultiComboCompraIngreso(MultiColumnCombo combo, List<VCompraIngresoCombo> lLibreria)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 80;
                combo.DropDownList.Columns[0].Caption = "Num. Rec.";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("NumGranja").Width = 100;
                combo.DropDownList.Columns[1].Caption = "Num. Granja";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("Proveedor").Width = 180;
                combo.DropDownList.Columns[2].Caption = "Proveedor";
                combo.DropDownList.Columns[2].Visible = true;             

                combo.ValueMember = "Id";
                combo.DisplayMember = "NumGranja";
                combo.DropDownList.DataSource = lLibreria;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarMultiComboCompraIngreso2(MultiColumnCombo combo, List<VCompraIngresoCombo> lLibreria)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 80;
                combo.DropDownList.Columns[0].Caption = "Num. Rec.";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("NumGranja").Width = 100;
                combo.DropDownList.Columns[1].Caption = "Num. Granja";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("Proveedor").Width = 180;
                combo.DropDownList.Columns[2].Caption = "Proveedor";
                combo.DropDownList.Columns[2].Visible = true;

                VCompraIngresoCombo PrimerFile = new VCompraIngresoCombo()
                {
                    Id = 0,
                    NumGranja = "TODOS",
                    Proveedor = "TODOS",
                };
                lLibreria.Insert(0, PrimerFile);
                combo.ValueMember = "Id";
                combo.DisplayMember = "NumGranja";
                combo.DropDownList.DataSource = lLibreria;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_ArmarCombo_CatPrecio(MultiColumnCombo combo, List<VPrecioCategoria> lLibreria)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Id";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Cod").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "Id";
                combo.DisplayMember = "Cod";
                combo.DropDownList.DataSource = lLibreria;
                combo.DropDownList.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public static void MG_ArmarComboAlmacen(MultiColumnCombo combo, List<VAlmacenCombo> lAlmacen)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("idLibreria").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Descripcion").Width = 500;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "idLibreria";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = lAlmacen;
                combo.DropDownList.Refresh();
                combo.Value = 1;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public static void MG_SeleccionarCombo_Almacen(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VAlmacenCombo>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void MG_ArmarComboSucursal(MultiColumnCombo combo, List<VSucursalCombo> lSucursal)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 70;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("Descripcion").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "Id";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = lSucursal;
                combo.DropDownList.Refresh();
                combo.Value = 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void MG_ArmarComboZona(MultiColumnCombo combo, List<VZona> lZona)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 50;
                combo.DropDownList.Columns[0].Caption = "Id";
                combo.DropDownList.Columns[0].Visible = true;

                combo.DropDownList.Columns.Add("Ciudad").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Ciudad";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("Provincia").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Provincia";
                combo.DropDownList.Columns[1].Visible = true;

                combo.DropDownList.Columns.Add("Zona").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Zona";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "Id";
                combo.DisplayMember = "Zona";
                combo.DropDownList.DataSource = lZona;
                combo.DropDownList.Refresh();
                combo.Value = 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void MG_ArmarComboTipoAlmacen(MultiColumnCombo combo, List<VTipoAlmacenCombo> lTipoAlmacen)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("Id").Width = 70;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("TipoAlmacen").Width = 150;
                combo.DropDownList.Columns[1].Caption = "TipoAlmacen";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "Id";
                combo.DisplayMember = "TipoAlmacen";
                combo.DropDownList.DataSource = lTipoAlmacen;
                combo.DropDownList.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void MG_SeleccionarCombo(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VLibreria>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarCombos(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VPersonalCombo>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarComboCompraIngreso(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VCompraIngresoCombo>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarComboPlaca(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VCompraIngreso_02>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarComboProveedor(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VProveedorCombo>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarComboCliente(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VClienteCombo>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarCombo_Zona(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VZona>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public static void MG_SeleccionarCombo_CatPrecio(MultiColumnCombo combo)
        {
            try
            {
                if (((List<VPrecioCategoria>)combo.DataSource).Count() > 0)
                {
                    combo.SelectedIndex = 0;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public static void MG_CrearCarpetaImagenes(string _CarpetaRaiz, string _NombreCarpeta)
        {
            try
            {
                string[] ScarpetaRaiz = { ConexionGlobal.gs_CarpetaRaiz, _CarpetaRaiz };
                string carpetaRaiz = Path.Combine(ScarpetaRaiz);
                if (System.IO.Directory.Exists(Path.Combine(carpetaRaiz, _NombreCarpeta)) == false)
                {
                    if (System.IO.Directory.Exists(carpetaRaiz) == false)
                    {
                        System.IO.Directory.CreateDirectory(carpetaRaiz);
                        if (System.IO.Directory.Exists(Path.Combine(carpetaRaiz, _NombreCarpeta)) == false)
                        {
                            System.IO.Directory.CreateDirectory(Path.Combine(carpetaRaiz, _NombreCarpeta));
                        }
                    }
                    else
                    {
                        if (System.IO.Directory.Exists(Path.Combine(carpetaRaiz, _NombreCarpeta)) == false)
                        {
                            System.IO.Directory.CreateDirectory(Path.Combine(carpetaRaiz, _NombreCarpeta));
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }

        }

        public static void MG_CrearCarpetaTemporal()
        {
            if (System.IO.Directory.Exists(RutaTemporal) == false)
            {
                System.IO.Directory.CreateDirectory(RutaTemporal);
            }
            else
            {
                try
                {
                    //this.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    System.IO.Directory.Delete(RutaTemporal, true);
                    System.IO.Directory.CreateDirectory(RutaTemporal);
                }
                catch (Exception)
                {

                    throw new Exception();
                }
            }
        }

        public static void MG_MoverImagenRuta(string _Folder, string _Nombre, PictureBox imagen)
        {
            try
            {
                if (!_Nombre.Equals("Default.jpg") && File.Exists(RutaTemporal + _Nombre))
                {
                    Bitmap img = new Bitmap(new Bitmap(RutaTemporal + _Nombre), 500, 300);
                    imagen.Image.Dispose();
                    imagen.Image = null;
                    try
                    {
                        System.IO.File.Copy(RutaTemporal + _Nombre, _Folder + _Nombre, true);

                    }
                    catch (System.IO.IOException)
                    {
                        throw new System.IO.IOException();
                    }

                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }       
        #endregion
    }
}
