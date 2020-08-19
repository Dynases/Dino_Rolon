using DevComponents.DotNetBar;
using ENTITY.inv.Almacen.View;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Janus.Windows.GridEX;
using PRESENTER.frm;
using PRESENTER.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UTILITY.Enum.EnEstado;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Almacen : MODEL.ModeloF1
    {
        public F1_Almacen()
        {
            InitializeComponent();
            this.MP_IniciarMapa();
            this.MP_InHabilitar();
            this.MP_CargarTiposAlmacen();
            this.MP_CargarSucursales();
            this.MP_CargarAlmacenes(); 
            this.MP_AsignarPermisos();

        }

        //==================================
        #region Variables de entorno

        private GMapOverlay _overlay;
        private Double _latitud = 0;
        private Double _longitud = 0;
        private string _imagen = "Default.jpg";
        private bool _modificarImagen = false;
        private string _NombreFormulario = "ALMACEN";
        private static int index;
        private static List<VAlmacenLista> listaAlmacen;

        #endregion

        //==================================
        #region Metodos Privados
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarAlmacenes();
            if (Dgv_Almacenes.RowCount > 0)
            {
                index = 0;
                MP_MostrarRegistro(tipo == 1 ? index : Dgv_Almacenes.RowCount - 1);
            }
            else
            {
                MP_Reiniciar();
                LblPaginacion.Text = "0/0";
            }
        }
        private void MP_IniciarMapa()
        {
            _overlay = new GMapOverlay("points");
            Gmc_Almacen.Overlays.Add(_overlay);
            MP_Map();
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
        private void MP_Map()
        {
            Gmc_Almacen.DragButton = MouseButtons.Left;
            Gmc_Almacen.CanDragMap = true;
            Gmc_Almacen.MapProvider = GMapProviders.GoogleMap;
            if (_latitud != 0 && _longitud != 0)
            {
                Gmc_Almacen.Position = new PointLatLng(_latitud, _longitud);
            }
            else
            {
                _overlay.Markers.Clear();
                Gmc_Almacen.Position = new PointLatLng(-17.3931784, -66.1738852);
            }
            Gmc_Almacen.MinZoom = 0;
            Gmc_Almacen.MaxZoom = 24;
            Gmc_Almacen.Zoom = 15.5;
            Gmc_Almacen.AutoScroll = true;
            GMapProvider.Language = LanguageType.Spanish;
        }

        private void MP_Reiniciar()
        {
            this.MP_Limpiar();
            this.MP_IniciarMapa();
            this.MP_InHabilitar();
            this.MP_CargarTiposAlmacen();
            this.MP_CargarSucursales();
            this.MP_CargarAlmacenes();
        }

        private void MP_Limpiar()
        {
            this.Tb_Descrip.Text = "";
            this.Tb_Direcc.Text = "";
            this.Tb_Telef.Text = "";
            this.Tb_Encargado.Text = "";
        }

        private void MP_InHabilitar()
        {
            this.Tb_Descrip.ReadOnly = true;
            this.Tb_Direcc.ReadOnly = true;
            this.Tb_Telef.ReadOnly = true;
            this.Tb_Encargado.ReadOnly = true;
            this.BtAdicionar.Enabled = false;
            this.Cb_Sucursales.Enabled = false;
            this.Cb_TipoAlmacen.Enabled = false;
            this.lblId.Visible = false;
            this.Dgv_Almacenes.Enabled = true;
        }
        private void MP_Habilitar()
        {
            this.Tb_Descrip.ReadOnly = false;
            this.Tb_Direcc.ReadOnly = false;
            this.Tb_Telef.ReadOnly = false;
            this.Tb_Encargado.ReadOnly = false;
            this.Cb_Sucursales.Enabled = true;
            this.Cb_TipoAlmacen.Enabled = true;
            this.BtAdicionar.Enabled = true;
            this.Dgv_Almacenes.Enabled = false;
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

        private void MP_CargarAlmacenes()
        {
            try
            {
                listaAlmacen = new ServiceDesktop.ServiceDesktopClient().AlmacenListar().ToList();
                if (listaAlmacen.Count() > 0)
                {
                    index = 0;
                    this.MP_MostrarRegistro(index);

                    Dgv_Almacenes.DataSource = listaAlmacen;
                    Dgv_Almacenes.RetrieveStructure();
                    Dgv_Almacenes.AlternatingColors = true;

                    Dgv_Almacenes.RootTable.Columns[0].Key = "Id";
                    Dgv_Almacenes.RootTable.Columns[0].Caption = "Id";
                    Dgv_Almacenes.RootTable.Columns[0].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[1].Key = "Descripcion";
                    Dgv_Almacenes.RootTable.Columns[1].Caption = "Sucursal";
                    Dgv_Almacenes.RootTable.Columns[1].Width = 200;
                    Dgv_Almacenes.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[1].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[2].Key = "Direccion";
                    Dgv_Almacenes.RootTable.Columns[2].Caption = "Direccion";
                    Dgv_Almacenes.RootTable.Columns[2].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[2].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[3].Key = "Telefono";
                    Dgv_Almacenes.RootTable.Columns[3].Caption = "Telefono";
                    Dgv_Almacenes.RootTable.Columns[3].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[3].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[4].Key = "Sucursal";
                    Dgv_Almacenes.RootTable.Columns[4].Caption = "Sucursal";
                    Dgv_Almacenes.RootTable.Columns[4].Width = 100;
                    Dgv_Almacenes.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[4].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[5].Key = "SucursalId";
                    Dgv_Almacenes.RootTable.Columns[5].Caption = "SucursalId";
                    Dgv_Almacenes.RootTable.Columns[5].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[6].Key = "TipoAlmacen";
                    Dgv_Almacenes.RootTable.Columns[6].Caption = "TipoAlmacen";
                    Dgv_Almacenes.RootTable.Columns[6].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[6].Visible = true;

                    Dgv_Almacenes.RootTable.Columns[7].Key = "TipoAlmacenId";
                    Dgv_Almacenes.RootTable.Columns[7].Caption = "TipoAlmacenId";
                    Dgv_Almacenes.RootTable.Columns[7].Visible = false;

                    Dgv_Almacenes.RootTable.Columns[8].Key = "Encargado";
                    Dgv_Almacenes.RootTable.Columns[8].Caption = "Encargado";
                    Dgv_Almacenes.RootTable.Columns[8].Width = 150;
                    Dgv_Almacenes.RootTable.Columns[8].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Almacenes.RootTable.Columns[8].CellStyle.FontSize = 8;
                    Dgv_Almacenes.RootTable.Columns[8].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Almacenes.RootTable.Columns[8].Visible = true;

                    //Habilitar filtradores
                    Dgv_Almacenes.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Almacenes.FilterMode = FilterMode.Automatic;
                    Dgv_Almacenes.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Almacenes.GroupByBoxVisible = false;
                    Dgv_Almacenes.VisualStyle = VisualStyle.Office2007;
                }

            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_CargarSucursales()
        {
            try
            {
                UTGlobal.MG_ArmarComboSucursal(Cb_Sucursales,
                                                new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_CargarTiposAlmacen()
        {
            try
            {
                UTGlobal.MG_ArmarComboTipoAlmacen(Cb_TipoAlmacen,
                                                new ServiceDesktop.ServiceDesktopClient().TipoAlmacenListarCombo().ToList());
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_MostrarRegistro(int index)
        {
            var almacen = listaAlmacen[index];
            lblId.Text = almacen.Id.ToString();
            Tb_Descrip.Text = almacen.Descripcion;
            Tb_Direcc.Text = almacen.Direccion;
            Tb_Telef.Text = almacen.Telefono;
            Tb_Encargado.Text = almacen.Encargado;
            Cb_TipoAlmacen.Value = almacen.TipoAlmacenId;
            Cb_Sucursales.Value = almacen.SucursalId;

            this.LblPaginacion.Text = (index + 1) + "/" + listaAlmacen.Count;
        }

        #endregion

        //===========
        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
            this.MP_Limpiar();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_Reiniciar();
            this.MP_MostrarRegistro(0);
        }

        public override bool MH_NuevoRegistro()
        {
            var Almacen = new VAlmacen
            {
                Descripcion = Tb_Descrip.Text,
                Direccion = Tb_Direcc.Text,
                IdSucursal = Convert.ToInt32(Cb_Sucursales.Value),
                Imagen = _imagen,
                Latitud = Convert.ToDecimal(_latitud),
                Longitud = Convert.ToDecimal(_longitud),
                Telefono = Tb_Telef.Text,
                Usuario = UTGlobal.Usuario,
                Encargado = Tb_Encargado.Text,
                TipoAlmacenId = Convert.ToInt32(Cb_TipoAlmacen.Value)
            };
            try
            {
                var Id = lblId.Text == "" ? 0 : Convert.ToInt32(lblId.Text);
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    servicio.AlmacenGuardar(Almacen, ref Id);
                }
                if (Id == 0)
                {
                    base.MH_Habilitar();
                    this.MP_Reiniciar();
                    this.MP_MostrarRegistro(0);  
                }
                else
                {
                    this.MP_Filtrar(2);
                    this.MP_InHabilitar();
                    MH_Habilitar();//El menu  
                }
                ToastNotification.Show(this, GLMensaje.Nuevo_Exito("TIPO ALMACEN", Id.ToString()), Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                return true;
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
     
        public override bool MH_Validar()
        {
            if (string.IsNullOrEmpty(Tb_Descrip.Text))
            {
                Tb_Descrip.BackColor = Color.Red;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void MH_Modificar()
        {
            base.MH_Modificar();
            this.MP_Habilitar();
        }
        public override bool MH_Eliminar()
        {
            try
            {
                int IdAlmacen = Convert.ToInt32(lblId.Text);
                Efecto efecto = new Efecto();
                efecto.Tipo = 2;
                efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
                efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
                efecto.ShowDialog();
                bool resul = false;
                if (efecto.Band)
                {
                    List<string> Mensaje = new List<string>();
                    var LMensaje = Mensaje.ToArray();
                    using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                    {
                        resul = servicio.EliminarAlmacen(IdAlmacen, ref LMensaje);
                    }
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, lblId.Text));
                    }
                    else
                    {
                        //Obtiene los codigos de productos sin stock
                        var mensajeLista = LMensaje.ToList();
                        if (mensajeLista.Count > 0)
                        {
                            var mensaje = "";
                            foreach (var item in mensajeLista)
                            {
                                mensaje = mensaje + "- " + item + "\n";
                            }
                            MP_MostrarMensajeError(mensaje);
                            return false;
                        }
                        else
                        {
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, lblId.Text));
                        }
                    }
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        #endregion

        //==================================
        #region Eventos
        private void Dgv_Almacenes_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_Almacenes.Row >= 0 && Dgv_Almacenes.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_Almacenes.Row);
            }
        }
        private void F1_Almacen_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "ALMACENES";
        }

        private void BtAdicionar_Click(object sender, EventArgs e)
        {
            this.MP_CopiarImagenRutaDefinida();
            BtnGrabar.Focus();
        }

        private void Tb_Descrip_TextChanged_1(object sender, EventArgs e)
        {
            if (Tb_Descrip.BackColor == Color.Red)
            {
                Tb_Descrip.BackColor = Color.White;
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            index = 0;
            this.MP_MostrarRegistro(index);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index -= 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (index < listaAlmacen.Count - 1)
            {
                index += 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            index = listaAlmacen.Count - 1;
            this.MP_MostrarRegistro(index);
        }
        private void Dgv_Almacenes_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion


    }
}