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
using UTILITY.Enum.EnEstado;

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
            this.Text = _NombreFormulario;
            //Carga las librerias al combobox desde una lista
            MP_CargarCombo();
            MP_IniciarMapa();
            btnMax.Visible = false;
            MP_CargarEncabezado();
            MP_InHabilitar();
            MP_AsignarPermisos();


        }
        private void MP_AsignarPermisos()
        {
            try
            {
                DataTable dtRolUsu = new ServiceDesktop.ServiceDesktopClient().AsignarPermisos((UTGlobal.UsuarioRol).ToString(), _NombreProg);
                if (dtRolUsu.Rows.Count > 0)
                {
                    bool show = Convert.ToBoolean(dtRolUsu.Rows[0]["Show"]);
                    bool add = Convert.ToBoolean(dtRolUsu.Rows[0]["Add"]);
                    bool modif = Convert.ToBoolean(dtRolUsu.Rows[0]["Mod"]);
                    bool del = Convert.ToBoolean(dtRolUsu.Rows[0]["Del"]);

                    if (add == false)
                        BtnNuevo.Visible = false;
                    if (modif == false)
                        BtnModificar.Visible = false;
                    if (del == false)
                        BtnEliminar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarCombo()
        {
            try
            {
                UTGlobal.MG_ArmarCombo(Cb_CliCiudad,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                Convert.ToInt32(ENEstaticosOrden.CIUDAD_CLIENTE)).ToList());
                UTGlobal.MG_ArmarCombo(Cb_CliFacturacion,
                                       new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                     Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE)).ToList());
                var comboCatPrecio = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().Where(a => a.Tipo.Equals((int)EnTipoCategoria.VENTA)).ToList();
                UTGlobal.MG_ArmarCombo_CatPrecio(Cb_CatPrecio, comboCatPrecio);

                var LZona = new ServiceDesktop.ServiceDesktopClient().ZonaListar().ToList();
                UTGlobal.MG_ArmarComboZona(Cb_CliCiudad, LZona);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }

        private void MP_Map()
        {
            try
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
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_IniciarMapa()
        {
            try
            {
                _Overlay = new GMapOverlay("points");
                Gmc_Cliente.Overlays.Add(_Overlay);
                MP_Map();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void MP_ArmarCombo(MultiColumnCombo combo,int idGrupo, int idOrden)
        {
            try
            {
                var tabla = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(idGrupo, idOrden);

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
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }
        private void MP_CargarEncabezado()
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ClientesListar().ToList();
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
            catch (Exception EX)
            {
                MP_MostrarMensajeError(EX.Message);
            }
            
        }
        private void MP_Habilitar()
        {
            try
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
                Cb_CatPrecio.Enabled = true;
                Cb_CliFacturacion.Enabled = true;
                Tb_Dias.IsInputReadOnly = false;
                Tb_TotalCred.IsInputReadOnly = false;
                BtAdicionar.Enabled = true;
                UTGlobal.MG_CrearCarpetaImagenes(EnCarpeta.Imagen, ENSubCarpetas.ImagenesCliente);
                UTGlobal.MG_CrearCarpetaTemporal();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }
        private void MP_InHabilitar()
        {
            try
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
                Cb_CatPrecio.Enabled = false;
                Tb_Dias.IsInputReadOnly = true;
                Tb_TotalCred.IsInputReadOnly = true;
                Cb_CliFacturacion.Enabled = false;
                BtAdicionar.Enabled = false;
                _Limpiar = false;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void MP_Limpiar()
        {
            try
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
                Tb_Dias.Value = 0;
                Tb_TotalCred.Value = 0;
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo_Zona(Cb_CliCiudad);
                    UTGlobal.MG_SeleccionarCombo(Cb_CliFacturacion);
                    UTGlobal.MG_SeleccionarCombo_CatPrecio(Cb_CatPrecio);
                }
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
          
        }
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_Buscador2.Row = _Pos;
                _idOriginal = (int)Dgv_Buscador2.GetValue("id");
                var tabla = new ServiceDesktop.ServiceDesktopClient().ClienteListar1(_idOriginal).FirstOrDefault();
                Txb_CliCod.Text = tabla.Id.ToString();
                Txb_CliCodSpyre.Text = tabla.IdSpyre;
                Txb_CliDescripcion.Text = tabla.Descripcion;
                Txb_CliRazonSoc.Text = tabla.RazonSocial;
                Txb_CliNit.Text = tabla.Nit;
                Chb_CliContado.Checked = tabla.TipoCliente == 1 ? true : false;
                Chb_CliCredito.Checked = tabla.TipoCliente == 2 ? true : false;
                Txb_CliDireccion.Text = tabla.Direcccion;
                Txb_CliContacto1.Text = tabla.Contacto1;
                Txb_CliContacto2.Text = tabla.Contacto2;
                Txb_CliTel1.Text = tabla.Telfono1;
                Txb_CliTel2.Text = tabla.Telfono2;
                Txb_CliEmail1.Text = tabla.Email1;
                Txb_CliEmail2.Text = tabla.Email2;
                Cb_CliCiudad.Value = tabla.Ciudad;
                Cb_CatPrecio.Value = tabla.IdCategoria;
                Cb_CliFacturacion.Value = tabla.Facturacion;
                _latitud = Convert.ToDouble(tabla.Latitud);
                _longitud = Convert.ToDouble(tabla.Longittud);
                Tb_TotalCred.Value = Convert.ToDouble( tabla.TotalCred);
                Tb_Dias.Value = Convert.ToDouble(tabla.Dias);
                _imagen = tabla.Imagen;
                MP_DibujarUbicacion(Txb_CliDescripcion.Text, Txb_CliNit.Text);
                //Mostrar Imagenes
                MP_MostrarImagen(tabla.Imagen);
                LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_Buscador2.RowCount.ToString();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_Filtrar(int tipo)
        {
            try
            {
                MP_CargarEncabezado();
                if (Dgv_Buscador2.RowCount > 0)
                {
                    MP_MostrarRegistro(tipo == 1 ? 0 : _MPos);
                }
                else
                {
                    MP_Limpiar();
                    LblPaginacion.Text = "0/0";
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
          
        }

        private void MP_MostrarImagen(string _NombreImagen)
        {
            try
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
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
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
                MP_MostrarMensajeError(ex.Message);
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
            bool resultado = false;
            try
            {
                int id = 0;               
                string mensaje = "";

                VCliente Cliente = new VCliente()
                {
                    IdSpyre = Txb_CliCodSpyre.Text,
                    Descripcion = Txb_CliDescripcion.Text,
                    RazonSocial = Txb_CliRazonSoc.Text,
                    Estado = (int)ENEstado.GUARDADO,
                    Nit = Txb_CliNit.Text,
                    TipoCliente = Chb_CliContado.Checked ? 1 : 2,
                    Direcccion = Txb_CliDireccion.Text,
                    Contacto1 = Txb_CliContacto1.Text,
                    Contacto2 = Txb_CliContacto2.Text,
                    Telfono1 = Txb_CliTel1.Text,
                    Telfono2 = Txb_CliTel2.Text,
                    Email1 = Txb_CliEmail1.Text,
                    Email2 = Txb_CliEmail2.Text,
                    Ciudad = Convert.ToInt32(Cb_CliCiudad.Value),
                    Facturacion = Convert.ToInt32(Cb_CliFacturacion.Value),
                    IdCategoria = Convert.ToInt32(Cb_CatPrecio.Value),
                    Latitud = Convert.ToDecimal(_latitud),
                    Longittud = Convert.ToDecimal(_longitud),
                    Imagen = _imagen,
                    TotalCred = Convert.ToDecimal(Tb_TotalCred.Value),
                    Dias = Convert.ToDecimal(Tb_Dias.Value),
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (VM_Nuevo) //Nuevo
                {
                    resultado = new ServiceDesktop.ServiceDesktopClient().ClienteGuardar(Cliente, ref id);
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
                        MP_Filtrar(2);
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
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return resultado;
            }            
        }
        public override bool MH_Eliminar()
        {
            bool resultadoRegistro = false;
            try
            {
                int IdCliente = Convert.ToInt32(Txb_CliCod.Text);
                List<string> lMensaje = new List<string>();
                if (new ServiceDesktop.ServiceDesktopClient().ExisteClienteEnVenta(IdCliente))
                {
                    lMensaje.Add("El cliente esta asociado a una venta.");
                }
                if (lMensaje.Count > 0)
                {
                    var mensaje = "";
                    foreach (var item in lMensaje)
                    {
                        mensaje = mensaje + "- " + item + "\n";
                    }
                    MP_MostrarMensajeError(mensaje);
                    return false;
                }
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
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, Txb_CliCod.Text));
                        MP_Filtrar(1);
                        resultadoRegistro = true;
                    }
                    else
                    {
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Error(_NombreFormulario, Txb_CliCod.Text));
                        resultadoRegistro = false;
                    }
                }
                return resultadoRegistro;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return resultadoRegistro;
            }
           
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
            try
            {
                if (_Overlay != null)
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
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void MP_DibujarUbicacion(string _nombre, string _ci)
        {
            try
            {
                if (_latitud != 0 && _longitud != 0)
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
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {               
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
                if (Cb_CatPrecio.SelectedIndex == -1)
                {
                    Cb_CatPrecio.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_CatPrecio.BackColor = Color.White;
                if (new ServiceDesktop.ServiceDesktopClient().ClienteListar().Where(a => a.Descripcion == Txb_CliDescripcion.Text).Count() > 0)
                {
                    throw new Exception("El nombre ya existe");
                }
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error = true;
            }          
        }
        #endregion

        private void Chb_CliCredito_CheckedChanged(object sender, EventArgs e)
        {
            Tb_Dias.Visible = true;
            Tb_TotalCred.Visible = true;
            Lbl_TotalCred.Visible = true;
            Lbl_Dias.Visible = true;
        }

        private void Chb_CliContado_CheckedChanged(object sender, EventArgs e)
        {
            Tb_Dias.Visible = false;
            Tb_TotalCred.Visible = false;
            Lbl_TotalCred.Visible = false;
            Lbl_Dias.Visible = false;
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                         Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_CliFacturacion.Text == "" ? "" : Cb_CliFacturacion.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_CliFacturacion,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE)).ToList());
                    Cb_CliFacturacion.SelectedIndex = ((List<VLibreria>)Cb_CliFacturacion.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void btn_Ciudad_Click(object sender, EventArgs e)
        {
            try
            {
                int idLibreria = 0;
                var lLibreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                         Convert.ToInt32(ENEstaticosOrden.CIUDAD_CLIENTE));
                if (lLibreria.Count() > 0)
                {
                    idLibreria = lLibreria.Select(x => x.IdLibreria).Max();
                }
                VLibreriaLista libreria = new VLibreriaLista()
                {
                    IdGrupo = Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                    IdOrden = Convert.ToInt32(ENEstaticosOrden.CIUDAD_CLIENTE),
                    IdLibrer = idLibreria + 1,
                    Descrip = Cb_CliCiudad.Text == "" ? "" : Cb_CliCiudad.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                if (new ServiceDesktop.ServiceDesktopClient().LibreriaGuardar(libreria))
                {
                    UTGlobal.MG_ArmarCombo(Cb_CliCiudad,
                                  new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                Convert.ToInt32(ENEstaticosOrden.CIUDAD_CLIENTE)).ToList());
                    Cb_CliCiudad.SelectedIndex = ((List<VLibreria>)Cb_CliCiudad.DataSource).Count() - 1;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_SeleccionarButtonCombo(MultiColumnCombo combo, ButtonX btn)
        {
            try
            {
                if (combo.SelectedIndex < 0 && combo.Text != string.Empty)
                {
                    btn.Visible = true;
                }
                else
                {
                    btn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void Cb_CliCiudad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MP_SeleccionarButtonCombo(Cb_CliCiudad, btn_Ciudad);
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Cb_CliFacturacion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MP_SeleccionarButtonCombo(Cb_CliFacturacion, btnFacturacion);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Dgv_GBuscador_DoubleClick(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row > -1)
            {
                superTabControl1.SelectedTabIndex = 0;
            }
        }
    }
}
