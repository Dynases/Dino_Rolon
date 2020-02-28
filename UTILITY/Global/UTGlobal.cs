using Janus.Windows.GridEX.EditControls;
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
using ENTITY.inv.Sucursal.View;

namespace UTILITY.Global
{
    public class UTGlobal
    {
        public static string Usuario = "DEFAULT";
        public static int Mayusculas =0;
        public static int UsuarioRol = 0;
        public static string NombreButton = "";
        public static Visualizador visualizador;
        //**Carpetas
        #region Carpetas
        public static  string RutaTemporal = @"C:\Temporal";
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
        public static void MG_ArmarComboSucursal(MultiColumnCombo combo, List<VSucursalCombo> lSucursal)
        {
            try
            {
                combo.DropDownList.Columns.Clear();
                combo.DropDownList.Columns.Add("idLibreria").Width = 70;
                combo.DropDownList.Columns[0].Caption = "Cod";
                combo.DropDownList.Columns[0].Visible = false;

                combo.DropDownList.Columns.Add("Descripcion").Width = 150;
                combo.DropDownList.Columns[1].Caption = "Descripcion";
                combo.DropDownList.Columns[1].Visible = true;

                combo.ValueMember = "idLibreria";
                combo.DisplayMember = "Descripcion";
                combo.DropDownList.DataSource = lSucursal;
                combo.DropDownList.Refresh();
                combo.Value = 2;
            }
            catch (Exception)
            {
                throw new Exception();
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
            catch ( Exception)
            {

                throw new Exception();
            }
           
        }
        public static void MG_CrearCarpetaTemporal()
        {
            if (System.IO.Directory.Exists(RutaTemporal)==false)
            {
                System.IO.Directory.CreateDirectory(RutaTemporal);
            }
            else
            {
                try
                {
                    //this.Computer.FileSystem.DeleteDirectory(RutaTemporal, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    System.IO.Directory.Delete(RutaTemporal,true);
                    System.IO.Directory.CreateDirectory(RutaTemporal);
                }
                catch (Exception)
                {

                    throw new Exception();
                }
            }
        }
        public static void MG_MoverImagenRuta(string _Folder, string _Nombre, PictureBox imagen )
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
