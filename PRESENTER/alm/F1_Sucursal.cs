using DevComponents.DotNetBar;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Janus.Windows.GridEX;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Sucursal : MODEL.ModeloF1
    {
        public F1_Sucursal()
        {
            InitializeComponent();
            this.MP_IniciarMapa();
            this.MP_InHabilitar();
            this.MP_CargarSucursales();
        }

        //==================================
        #region Variables de entorno

        private GMapOverlay _overlay;
        private Double _latitud = 0;
        private Double _longitud = 0;
        private string _imagen = "Default.jpg";
        private bool _modificarImagen = false;

        #endregion

        //==================================
        #region Metodos

        private void MP_IniciarMapa()
        {
            _overlay = new GMapOverlay("points");
            Gmc_Sucursal.Overlays.Add(_overlay);
            MP_Map();
        }

        private void MP_Map()
        {
            Gmc_Sucursal.DragButton = MouseButtons.Left;
            Gmc_Sucursal.CanDragMap = true;
            Gmc_Sucursal.MapProvider = GMapProviders.GoogleMap;
            if (_latitud != 0 && _longitud != 0)
            {
                Gmc_Sucursal.Position = new PointLatLng(_latitud, _longitud);
            }
            else
            {
                _overlay.Markers.Clear();
                Gmc_Sucursal.Position = new PointLatLng(-17.3931784, -66.1738852);
            }
            Gmc_Sucursal.MinZoom = 0;
            Gmc_Sucursal.MaxZoom = 24;
            Gmc_Sucursal.Zoom = 15.5;
            Gmc_Sucursal.AutoScroll = true;
            GMapProvider.Language = LanguageType.Spanish;
        }

        private void MP_Reiniciar()
        {
            this.Tb_Descrip.Text = "";
            this.Tb_Direcc.Text = "";
            this.Tb_Telef.Text = "";

            this.MP_InHabilitar();
        }

        private void MP_InHabilitar()
        {
            this.Tb_Descrip.ReadOnly = true;
            this.Tb_Direcc.ReadOnly = true;
            this.Tb_Telef.ReadOnly = true;
            this.BtAdicionar.Enabled = false;

            this.lblId.Visible = false;
        }

        private void MP_Habilitar()
        {
            this.Tb_Descrip.ReadOnly = false;
            this.Tb_Direcc.ReadOnly = false;
            this.Tb_Telef.ReadOnly = false;

            this.BtAdicionar.Enabled = true;
        }

        private bool MP_AccionResult()
        {
            return lblId.Text == string.Empty ? true : false;
        }

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private string MP_CopiarImagenRutaDefinida()
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog()
                {
                    Filter = "Ficheros JPG o JPEG o PNG|*.jpg;*.jpeg;*.png" +
                              "|Ficheros GIF|*.gif" +
                              "|Ficheros BMP|*.bmp" +
                              "|Ficheros PNG|*.png" +
                              "|Ficheros TIFF|*.tif"
                };
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
                            mayor = 5;
                            _imagen = @"\Imagen_" + Convert.ToString(mayor + 1).Trim() + ".jpg";
                            Pc_Img.SizeMode = PictureBoxSizeMode.StretchImage;
                            Pc_Img.Image = Image.FromStream(Bin);

                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                        }
                        else
                        {
                            _imagen = @"\Imagen_" + lblId.Text.Trim() + ".jpg";
                            Pc_Img.Image = Image.FromStream(Bin);
                            img.Save(UTGlobal.RutaTemporal + _imagen, System.Drawing.Imaging.ImageFormat.Jpeg);
                            img.Dispose();
                            _modificarImagen = true;
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

        private void MP_CargarSucursales()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().SucursalListar();
                if (ListaCompleta.Count() > 0)
                {
                    Dgv_Sucursales.DataSource = ListaCompleta;
                    Dgv_Sucursales.RetrieveStructure();
                    Dgv_Sucursales.AlternatingColors = true;

                    Dgv_Sucursales.RootTable.Columns[0].Key = "Id";
                    Dgv_Sucursales.RootTable.Columns[0].Caption = "Id";
                    Dgv_Sucursales.RootTable.Columns[0].Visible = false;

                    Dgv_Sucursales.RootTable.Columns[1].Key = "Descripcion";
                    Dgv_Sucursales.RootTable.Columns[1].Caption = "Descripcion";
                    Dgv_Sucursales.RootTable.Columns[1].Width = 300;
                    Dgv_Sucursales.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[1].Visible = true;

                    Dgv_Sucursales.RootTable.Columns[2].Key = "Direccion";
                    Dgv_Sucursales.RootTable.Columns[2].Caption = "Direccion";
                    Dgv_Sucursales.RootTable.Columns[2].Width = 250;
                    Dgv_Sucursales.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[2].Visible = true;

                    Dgv_Sucursales.RootTable.Columns[3].Key = "Telefono";
                    Dgv_Sucursales.RootTable.Columns[3].Caption = "Telefono";
                    Dgv_Sucursales.RootTable.Columns[3].Width = 280;
                    Dgv_Sucursales.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[3].Visible = true;

                    //Habilitar filtradores
                    Dgv_Sucursales.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Sucursales.FilterMode = FilterMode.Automatic;
                    Dgv_Sucursales.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Sucursales.GroupByBoxVisible = false;
                    Dgv_Sucursales.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_CargarDetalle(int id)
        {
            try
            {
                var lresult = new ServiceDesktop.ServiceDesktopClient().Transformacion_01_Lista().Where(a => a.IdTransformacion == id).ToList();
                if (lresult.Count() > 0)
                {
                    Dgv_Sucursales.DataSource = lresult;
                    Dgv_Sucursales.RetrieveStructure();
                    Dgv_Sucursales.AlternatingColors = true;

                    Dgv_Sucursales.RootTable.Columns["Id"].Visible = false;
                    //Dgv_Sucursales.RootTable.Columns["IdTransformacion"].Visible = false;
                    //Dgv_Sucursales.RootTable.Columns["IdProducto"].Visible = false;

                    Dgv_Sucursales.RootTable.Columns["Descripcion"].Caption = "DESCRIPCION";
                    Dgv_Sucursales.RootTable.Columns["Descripcion"].Width = 150;
                    Dgv_Sucursales.RootTable.Columns["Descripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns["Descripcion"].CellStyle.FontSize = 9;
                    Dgv_Sucursales.RootTable.Columns["Descripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns["Descripcion"].Visible = true;

                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].Caption = "TOTAL PROD";
                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].FormatString = "0";
                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].Width = 120;
                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].CellStyle.FontSize = 9;
                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    //Dgv_Sucursales.RootTable.Columns["TotalProd"].Visible = true;

                    //Dgv_Sucursales.RootTable.Columns["Producto2"].Caption = "M. PRIMA";
                    //Dgv_Sucursales.RootTable.Columns["Producto2"].Width = 150;
                    //Dgv_Sucursales.RootTable.Columns["Producto2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    //Dgv_Sucursales.RootTable.Columns["Producto2"].CellStyle.FontSize = 9;
                    //Dgv_Sucursales.RootTable.Columns["Producto2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    //Dgv_Sucursales.RootTable.Columns["Producto2"].Visible = true;

                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].Caption = "CANT.";
                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].FormatString = "0.00";
                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].Width = 90;
                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    //Dgv_Sucursales.RootTable.Columns["Cantidad"].Visible = true;

                    //Dgv_Sucursales.RootTable.Columns["Total"].Caption = "TOTAL";
                    //Dgv_Sucursales.RootTable.Columns["Total"].FormatString = "0.00";
                    //Dgv_Sucursales.RootTable.Columns["Total"].Width = 100;
                    //Dgv_Sucursales.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    //Dgv_Sucursales.RootTable.Columns["Total"].CellStyle.FontSize = 9;
                    //Dgv_Sucursales.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    //Dgv_Sucursales.RootTable.Columns["Total"].Visible = true;

                    Dgv_Sucursales.GroupByBoxVisible = false;
                    Dgv_Sucursales.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }

        //===========
        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_Reiniciar();
        }

        public override bool MH_NuevoRegistro()
        {
            if (this.MH_Validar())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool MH_Validar()
        {
            return string.IsNullOrEmpty(this.Tb_Descrip.Text) &&
                   string.IsNullOrEmpty(this.Tb_Direcc.Text) &&
                   string.IsNullOrEmpty(this.Tb_Telef.Text) ? false : true;
        }

        #endregion
        //===========

        #endregion

        //==================================
        #region Eventos

        private void F1_Sucursal_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "SUCURSALES";
        }

        private void BtAdicionar_Click(object sender, EventArgs e)
        {
            this.MP_CopiarImagenRutaDefinida();
            BtnGrabar.Focus();
        }

        #endregion
    }
}
