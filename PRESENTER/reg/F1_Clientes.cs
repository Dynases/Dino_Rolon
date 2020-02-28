using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MODEL;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using Janus.Windows.GridEX;
using ENTITY.Cliente.View;
using UTILITY.Global;
using UTILITY.Enum;
using UTILITY.Enum.EnEstaticos;
using ENTITY.Libreria.View;
using Janus.Windows.GridEX.EditControls;
using MetroFramework.Controls;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.IO;
using UTILITY.Enum.EnCarpetas;
using PRESENTER.frm;

namespace PRESENTER.reg
{
    public partial class F1_Clientes : ModeloF1
    {
        #region Variables
        string _NombreFormulario = "CLIENTES";
        string _imagen = "Default";
        int _idOriginal=0;
        int _MPos;
        Double _latitud = 0;
        Double _longitud = 0;
        GMapOverlay _Overlay;
        bool _Limpiar = false;
        bool _ModificarImagen = false;
        #endregion
        #region Manejo de eventos
        public F1_Clientes()
        {
            InitializeComponent();
            SuperTabBuscar.Visible = false;          
        }
        private void F1_Clientes_Load(object sender, EventArgs e)
        {
            MP_Iniciar();
        }
        private void F1_Clientes_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void Dgv_Buscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_Buscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_Buscador2.Row >= 0 && Dgv_Buscador2.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_Buscador2.Row);
            }
        }     
        private void ButtonX3_Click(object sender, EventArgs e)
        {
            if (Gmc_Cliente.Zoom >= Gmc_Cliente.MaxZoom)
                Gmc_Cliente.Zoom = Gmc_Cliente.Zoom - 1;
        }
        private void BtnSubir_Click(object sender, EventArgs e)
        {
            if (Gmc_Cliente.Zoom <= Gmc_Cliente.MaxZoom)
                Gmc_Cliente.Zoom = Gmc_Cliente.Zoom + 1;
        }
        private void Gmc_Cliente_DoubleClick(object sender, EventArgs e)
        {
            _Overlay.Markers.Clear();
            GMapControl gm = ((GMapControl)sender);
            MouseEventArgs hj = ((MouseEventArgs)e);
            PointLatLng plg = gm.FromLocalToLatLng(hj.X, hj.Y);
            _latitud = plg.Lat;
            _longitud = plg.Lng;
            MP_AgregarPunto(plg, "", "");
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            if (Dgv_Buscador2.RowCount > 0)
            {
                _MPos = 0;
                Dgv_Buscador2.Row = _MPos;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_Buscador2.Row;
            if (_MPos > 0 && Dgv_Buscador2.RowCount > 0)
            {
                _MPos = _MPos - 1;
                Dgv_Buscador2.Row = _MPos;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_Buscador2.Row;
            if (_MPos < Dgv_Buscador2.RowCount - 1 && _MPos >= 0)
            {
                _MPos = Dgv_Buscador2.Row + 1;
                Dgv_Buscador2.Row = _MPos;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            _MPos = Dgv_Buscador2.Row;
            if (Dgv_Buscador2.RowCount > 0)
            {
                _MPos = Dgv_Buscador2.RowCount - 1;
                Dgv_Buscador2.Row = _MPos;
            }
        }

        private void BtAdicionar_Click(object sender, EventArgs e)
        {
            MP_CopiarImagenRutaDefinida();
            BtnGrabar.Focus();
        }
        #endregion
        #region Metodos Privados
        private void MP_Iniciar()
        {
            LblTitulo.Text = _NombreFormulario;
            //Carga las librerias al combobox desde una lista
            UTGlobal.MG_ArmarCombo(Cb_CliCiudad,
                                   new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo (Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                 Convert.ToInt32(ENEstaticosOrden.CIUDAD_CLIENTE)).ToList());
            UTGlobal.MG_ArmarCombo(Cb_CliFacturacion,
                                   new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                 Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE)).ToList());
            MP_IniciarMapa();
            btnMax.Visible = false;
            MP_CargarEncabezado();
            MP_InHabilitar();
        
        }
        private void MP_Map()
        {
            Gmc_Cliente.DragButton = MouseButtons.Left;
            Gmc_Cliente.CanDragMap = true;
            Gmc_Cliente.MapProvider = GMapProviders.GoogleMap;
            if (_latitud != 0 && _longitud != 0)
            {
                Gmc_Cliente.Position = new PointLatLng(_latitud, _longitud);
            }
            else
            {
                _Overlay.Markers.Clear();
                Gmc_Cliente.Position = new PointLatLng(-17.3931784, -66.1738852);
            }
            Gmc_Cliente.MinZoom = 0;
            Gmc_Cliente.MaxZoom = 24;
            Gmc_Cliente.Zoom = 15.5;
            Gmc_Cliente.AutoScroll = true;
            GMapProvider.Language = LanguageType.Spanish;
        }
        private void MP_IniciarMapa()
        {     
            _Overlay = new GMapOverlay("points");
            Gmc_Cliente.Overlays.Add(_Overlay);
            MP_Map();
        }
        private void MP_ArmarCombo(MultiColumnCombo combo,int idGrupo, int idOrden)
        {
            var tabla = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(idGrupo,idOrden);

            combo.DropDownList.Columns.Clear();
            combo.DropDownList.Columns.Add("idLibreria").Width = 70;
            combo.DropDownList.Columns[0].Caption = "Cod";
            combo.DropDownList.Columns[0].Visible = false;

            combo.DropDownList.Columns.Add("Descricion").Width = 150;
            combo.DropDownList.Columns[1].Caption = "Descripcion";
            combo.DropDownList.Columns[1].Visible = true;           

            combo.ValueMember = "idLibreria";
            combo.DisplayMember = "Descripcion";
            combo.DropDownList.DataSource = tabla;
            combo.DropDownList.Refresh();
        }
        private void MP_CargarEncabezado()
        {
            var result = new ServiceDesktop.ServiceDesktopClient().ClientesListar();
            Dgv_Buscador2.DataSource = result;
            Dgv_Buscador2.RetrieveStructure();
            Dgv_Buscador2.AlternatingColors = true;

            Dgv_Buscador2.RootTable.Columns[0].Key = "id";
            Dgv_Buscador2.RootTable.Columns[0].Visible = false;

            Dgv_Buscador2.RootTable.Columns[1].Key = "Descripcion";
            Dgv_Buscador2.RootTable.Columns[1].Caption = "Descripcion";
            Dgv_Buscador2.RootTable.Columns[1].Width = 250;
            Dgv_Buscador2.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Buscador2.RootTable.Columns[1].CellStyle.FontSize = 8;
            Dgv_Buscador2.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Buscador2.RootTable.Columns[1].Visible = true;


            Dgv_Buscador2.RootTable.Columns[2].Key = "RazonSocial";
            Dgv_Buscador2.RootTable.Columns[2].Caption = "RazonSocial";
            Dgv_Buscador2.RootTable.Columns[2].Width = 250;
            Dgv_Buscador2.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Buscador2.RootTable.Columns[2].CellStyle.FontSize = 8;
            Dgv_Buscador2.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Buscador2.RootTable.Columns[2].Visible = true;

            Dgv_Buscador2.RootTable.Columns[3].Key = "Ciudad";
            Dgv_Buscador2.RootTable.Columns[3].Caption = "Ciudad";
            Dgv_Buscador2.RootTable.Columns[3].Width = 200;
            Dgv_Buscador2.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Buscador2.RootTable.Columns[3].CellStyle.FontSize = 8;
            Dgv_Buscador2.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Buscador2.RootTable.Columns[3].Visible = false;


            Dgv_Buscador2.RootTable.Columns[4].Key = "NombreCiudad";
            Dgv_Buscador2.RootTable.Columns[4].Caption = "Ciudad";
            Dgv_Buscador2.RootTable.Columns[4].Width = 200;
            Dgv_Buscador2.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Buscador2.RootTable.Columns[4].CellStyle.FontSize = 8;
            Dgv_Buscador2.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Buscador2.RootTable.Columns[4].Visible = true;

            Dgv_Buscador2.RootTable.Columns[5].Key = "Contacto1";
            Dgv_Buscador2.RootTable.Columns[5].Caption = "Contacto1";
            Dgv_Buscador2.RootTable.Columns[5].Width = 200;
            Dgv_Buscador2.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Buscador2.RootTable.Columns[5].CellStyle.FontSize = 8;
            Dgv_Buscador2.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Buscador2.RootTable.Columns[5].Visible = true;

            Dgv_Buscador2.RootTable.Columns[6].Key = "Contacto2";
            Dgv_Buscador2.RootTable.Columns[6].Caption = "Contacto2";
            Dgv_Buscador2.RootTable.Columns[6].Width = 200;
            Dgv_Buscador2.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Buscador2.RootTable.Columns[6].CellStyle.FontSize = 8;
            Dgv_Buscador2.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Buscador2.RootTable.Columns[6].Visible = true;

            //Habilitar filtradores
            Dgv_Buscador2.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            Dgv_Buscador2.FilterMode = FilterMode.Automatic;
            Dgv_Buscador2.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            //Dgv_Buscador22.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
            Dgv_Buscador2.GroupByBoxVisible = false;
            Dgv_Buscador2.VisualStyle = VisualStyle.Office2007;
        }
        private void MP_Habilitar()
        {
            Txb_CliCod.ReadOnly = false;
            Txb_CliCodSpyre.ReadOnly = false;
            Txb_CliDescripcion.ReadOnly = false;
            Txb_CliRazonSoc.ReadOnly = false;
            Txb_CliNit.ReadOnly = false;
            Chb_CliContado.Enabled = true;
            Chb_CliCredito.Enabled = true;
            Txb_CliDireccion.ReadOnly = false;
            Txb_CliContacto1.ReadOnly = false;
            Txb_CliContacto2.ReadOnly = false;
            Txb_CliTel1.ReadOnly = false;
            Txb_CliTel2.ReadOnly = false;
            Txb_CliEmail1.ReadOnly = false;
            Txb_CliEmail2.ReadOnly = false;
            Cb_CliCiudad.Enabled = true;
            Cb_CliFacturacion.Enabled = true;
            BtAdicionar.Enabled = true;
            UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente);
            UTGlobal.MG_CrearCarpetaTemporal();
        }
        private void MP_InHabilitar()
        {
            Txb_CliCod.ReadOnly = true;
            Txb_CliCodSpyre.ReadOnly = true;
            Txb_CliDescripcion.ReadOnly = true;
            Txb_CliRazonSoc.ReadOnly = true;
            Txb_CliNit.ReadOnly = true;
            Chb_CliContado.Enabled = false;
            Chb_CliCredito.Enabled = false;
            Txb_CliDireccion.ReadOnly = true;
            Txb_CliContacto1.ReadOnly = true;
            Txb_CliContacto2.ReadOnly = true;
            Txb_CliTel1.ReadOnly = true;
            Txb_CliTel2.ReadOnly = true;
            Txb_CliEmail1.ReadOnly = true;
            Txb_CliEmail2.ReadOnly = true;
            Cb_CliCiudad.Enabled = false;
            Cb_CliFacturacion.Enabled = false;
            BtAdicionar.Enabled = false;
            _Limpiar = false;
        }
        private void MP_Limpiar()
        {
            Txb_CliCod.Clear();
            Txb_CliCodSpyre.Clear();
            Txb_CliDescripcion.Clear();
            Txb_CliRazonSoc.Clear();
            Txb_CliNit.Clear();
            Chb_CliContado.Checked = true;
            Chb_CliCredito.Checked = false;
            Txb_CliDireccion.Clear();
            Txb_CliContacto1.Clear();
            Txb_CliContacto2.Clear();
            Txb_CliTel1.Clear();
            Txb_CliTel2.Clear();
            Txb_CliEmail1.Clear();
            Txb_CliEmail2.Clear();
            if (_Limpiar == false)
            {
                UTGlobal.MG_SeleccionarCombo(Cb_CliCiudad);
                UTGlobal.MG_SeleccionarCombo(Cb_CliFacturacion);
            }
        }
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_Buscador2.Row = _Pos;
                _idOriginal = (int)Dgv_Buscador2.GetValue("id");
                var tabla = new ServiceDesktop.ServiceDesktopClient().ClienteListar1(_idOriginal).ToArray();
                Txb_CliCod.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Id.ToString())).Count() > 0 ? tabla.Select(x => x.Id).First().ToString() : "";
                Txb_CliCodSpyre.Text = tabla.Where(x => !string.IsNullOrEmpty(x.IdSpyre)).Count() > 0 ? tabla.Select(x => x.IdSpyre).First().ToString() : "";
                Txb_CliDescripcion.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Descripcion)).Count() > 0 ? tabla.Select(x => x.Descripcion).First().ToString() : "";
                Txb_CliRazonSoc.Text = tabla.Where(x => !string.IsNullOrEmpty(x.RazonSocial)).Count() > 0 ? tabla.Select(x => x.RazonSocial).First().ToString() : "";
                Txb_CliNit.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Nit)).Count() > 0 ? tabla.Select(x => x.Nit).First().ToString() : "";
                Chb_CliContado.Checked = tabla.Select(x => x.Id).First() == 1 ? true : false;
                Chb_CliCredito.Checked = tabla.Select(x => x.Id).First() != 1 ? true : false;
                //Chb_CliContado.Text = tabla.Select(x => x.Id).First().ToString();
                //Chb_CliCredito.Text = tabla.Select(x => x.Id).First().ToString();
                Txb_CliDireccion.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Direcccion)).Count() > 0 ? tabla.Select(x => x.Direcccion).First().ToString() : "";
                Txb_CliContacto1.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Contacto1)).Count() > 0 ? tabla.Select(x => x.Contacto1).First().ToString() : "";
                Txb_CliContacto2.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Contacto2)).Count() > 0 ? tabla.Select(x => x.Contacto2).First().ToString() : "";
                Txb_CliTel1.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Telfono1)).Count() > 0 ? tabla.Select(x => x.Telfono1).First().ToString() : "";
                Txb_CliTel2.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Telfono2)).Count() > 0 ? tabla.Select(x => x.Telfono2).First().ToString() : "";
                Txb_CliEmail1.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Email1)).Count() > 0 ? tabla.Select(x => x.Email1).First().ToString() : "";
                Txb_CliEmail2.Text = tabla.Where(x => !string.IsNullOrEmpty(x.Email2)).Count() > 0 ? tabla.Select(x => x.Email2).First().ToString() : "";        
                Cb_CliCiudad.Value = tabla.Select(x => x.Ciudad).First();               
                Cb_CliFacturacion.Value = tabla.Select(x => x.Facturacion).First();
                _latitud = Convert.ToDouble(tabla.Select(x => x.Latitud).First());
                _longitud = Convert.ToDouble(tabla.Select(x => x.Longittud).First());
                MP_DibujarUbicacion(Txb_CliDescripcion.Text, Txb_CliNit.Text);
                //Mostrar Imagenes
                MP_MostrarImagen(tabla.Select(x => x.Imagen).First());
                LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_Buscador2.RowCount.ToString();
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.StackTrace, "Comuniquece con el administrador del sistema");
            }                    
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarEncabezado();
            if (Dgv_Buscador2.RowCount > 0)
            {
                _MPos = 0;
                MP_MostrarRegistro(tipo == 1 ? _MPos : Dgv_Buscador2.RowCount - 1);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }

        private void MP_MostrarImagen(string _NombreImagen)
        {
            if (_NombreImagen.Equals("Default.jpg") || !File.Exists(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente) + _NombreImagen))
            {
                Bitmap img = new Bitmap(PRESENTER.Properties.Resources.PANTALLA);
                Pc_Img.Image = img;
            }
            else
            {
                if (File.Exists(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente) + _NombreImagen))
                {
                    MemoryStream Bin = new MemoryStream();
                    Bitmap img = new Bitmap(new Bitmap(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente) + _NombreImagen));
                    img.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Pc_Img.SizeMode = PictureBoxSizeMode.StretchImage;
                    Pc_Img.Image = Image.FromStream(Bin);
                    Bin.Dispose();
                }
            }
        }
        private string MP_CopiarImagenRutaDefinida()
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" +
                              "|Ficheros GIF|*.gif" +
                              "|Ficheros BMP|*.bmp" +
                              "|Ficheros PNG|*.png" +
                              "|Ficheros TIFF|*.tif";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string ruta = file.FileName;
                    if (file.CheckFileExists)
                    {
                        Bitmap img = new Bitmap(new Bitmap(ruta));
                        Bitmap imgM = new Bitmap(new Bitmap(ruta));
                        MemoryStream Bin = new MemoryStream();
                        imgM.Save(Bin, System.Drawing.Imaging.ImageFormat.Jpeg);

                        if (MP_AccionResult())
                        {
                            int mayor;
                            mayor = Convert.ToInt32(Dgv_Buscador2.GetTotal(Dgv_Buscador2.RootTable.Columns[0], AggregateFunction.Max));
                            _imagen = @"\Imagen_" + Convert.ToString(mayor + 1).Trim() + ".jpg";
                            Pc_Img.SizeMode = PictureBoxSizeMode.StretchImage;
                            Pc_Img.Image = Image.FromStream(Bin);

                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                        }
                        else
                        {
                            _imagen = @"\Imagen_" + Txb_CliCod.Text.Trim() + ".jpg";
                            Pc_Img.Image = Image.FromStream(Bin);
                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                            _ModificarImagen = true;
                        }
                    }
                    return _imagen;
                }
                return "default.jpg";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
                return "";
            }
        }
        private bool MP_AccionResult()
        {
            return Txb_CliCod.Text == string.Empty ? true : false;
        }
        #endregion
        #region Metodos Heredados
        public override bool MH_NuevoRegistro()
        {
            int id = 0;
            bool resultado =false;
            string mensaje = "";
            
            VCliente Cliente = new VCliente()
            {               
                IdSpyre = Txb_CliCodSpyre.Text,
                Descripcion = Txb_CliDescripcion.Text,
                RazonSocial = Txb_CliRazonSoc.Text,
                Nit = Txb_CliNit.Text,
                TipoCliente = Chb_CliContado.Checked ? 1 : 0,
                Direcccion = Txb_CliDireccion.Text,
                Contacto1 = Txb_CliContacto1.Text,
                Contacto2 = Txb_CliContacto2.Text,
                Telfono1 = Txb_CliTel1.Text,
                Telfono2 = Txb_CliTel2.Text,
                Email1 = Txb_CliEmail1.Text,
                Email2 =  Txb_CliEmail2.Text,
                Ciudad = Convert.ToInt32(Cb_CliCiudad.Value),
                Facturacion = Convert.ToInt32(Cb_CliFacturacion.Value),
                Latitud = Convert.ToDecimal( _latitud),
                Longittud = Convert.ToDecimal(_longitud),
                Imagen = _imagen,
                Fecha = DateTime.Now.Date,
                Hora = DateTime.Now.ToString("hh:mm"),
                Usuario = UTGlobal.Usuario,
            };
            if (VM_Nuevo) //Nuevo
            {
                resultado = new ServiceDesktop.ServiceDesktopClient().ClienteGuardar(Cliente,ref id);
                if (resultado)
                {
                    Txb_CliCodSpyre.Focus();
                    UTGlobal.MG_MoverImagenRuta(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente), _imagen, Pc_Img);
                    MP_Filtrar(1);
                    MP_Limpiar();
                    _Limpiar = true;
                    _imagen = "Default.jpg";
                    _ModificarImagen = false;
                    mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                    
                }               
            }
            else //Modifcar
            {
                id = Convert.ToInt32(Txb_CliCod.Text);
                resultado = new ServiceDesktop.ServiceDesktopClient().ClienteModificar(Cliente, id);
                if (resultado)
                {
                    if (_ModificarImagen)
                    {
                        UTGlobal.MG_MoverImagenRuta(Path.Combine(ConexionGlobal.gs_CarpetaRaiz, EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente), _imagen, Pc_Img);
                        _ModificarImagen = false;
                    }
                    Txb_CliCodSpyre.Focus();
                    MP_Filtrar(1);
                    MP_InHabilitar();//El formulario
                    _Limpiar = true;
                    _imagen = "Default.jpg";
                    mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                    MH_Habilitar();//El menu
                }
            }
            if (resultado)
            {
                ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
            }
            else
            {
                mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
            }
            return resultado;
        }
        public override bool MH_Eliminar()
        {
            int IdCliente = Convert.ToInt32(Txb_CliCod.Text);
            bool resultadoRegistro = false;
            //Pregunta si eliminara el registro
            Efecto efecto = new Efecto();
            efecto.Tipo = 2;
            efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
            efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
            efecto.ShowDialog();
            bool bandera = efecto.Band;
            if (bandera)
            {
                var resul = new ServiceDesktop.ServiceDesktopClient().ClienteEliminar(IdCliente);
                if (resul)
                {
                    ToastNotification.Show(this, GLMensaje.Eliminar_Exito(_NombreFormulario, IdCliente.ToString()),
                                           PRESENTER.Properties.Resources.GRABACION_EXITOSA,
                                           (int)GLMensajeTamano.Chico,
                                            eToastGlowColor.Green,
                                           eToastPosition.TopCenter);
                    MP_Filtrar(1);
                    resultadoRegistro = true;
                }
                else
                {
                    Bitmap img = new Bitmap(PRESENTER.Properties.Resources.WARNING, 50, 50);
                    ToastNotification.Show(this, GLMensaje.Eliminar_Error(_NombreFormulario, IdCliente.ToString()),
                                                img, (int)GLMensajeTamano.Mediano,
                                                eToastGlowColor.Green,
                                                eToastPosition.TopCenter);
                    resultadoRegistro = false;
                }
            }
            return resultadoRegistro;
        }
        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();
            
        }
        public override void MH_Modificar()
        {
            MP_Habilitar();

        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(1);
        }     
        private void MP_AgregarPunto(PointLatLng pointLatLng, string _nombre, string _ci)
        {
            if (_Overlay!=null)
            {
                GMarkerGoogle marker = new GMarkerGoogle(pointLatLng, PRESENTER.Properties.Resources.MARKERICONO);
                //añadir tooltip
                MarkerTooltipMode mode = MarkerTooltipMode.OnMouseOver;
                marker.ToolTip = new GMapBaloonToolTip(marker);
                marker.ToolTipMode = mode;
                SolidBrush ToolTipBackColor = new SolidBrush(Color.Blue);
                marker.ToolTip.Fill = ToolTipBackColor;
                marker.ToolTip.Foreground = Brushes.White;
                _Overlay.Markers.Add(marker);
                Gmc_Cliente.Position = pointLatLng;
            }
        }
        private void MP_DibujarUbicacion(string _nombre, string _ci)
        {
            if (_latitud !=0 && _longitud!=0)
            {
                PointLatLng plg = new PointLatLng(_latitud, _longitud);
                _Overlay.Markers.Clear();
                MP_AgregarPunto(plg, _nombre, _ci);
            }
            else
            {
                _Overlay.Markers.Clear();
                Gmc_Cliente.Position = new PointLatLng(-17.3931784, -66.1738852);
            }
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            if (Txb_CliDescripcion.Text == "")
            {
                Txb_CliDescripcion.BackColor = Color.Red;
                _Error = true;
            }
            else
                Txb_CliDescripcion.BackColor = Color.White;

            if (Cb_CliCiudad.SelectedIndex == -1)
            {
                Cb_CliCiudad.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_CliCiudad.BackColor = Color.White;
            if (Cb_CliFacturacion.SelectedIndex == -1)
            {
                Cb_CliFacturacion.BackColor = Color.Red;
                _Error = true;
            }
            else
                Cb_CliFacturacion.BackColor = Color.White;
            return _Error;
        }


        #endregion

    }
}
