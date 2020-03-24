using DevComponents.DotNetBar;
using ENTITY.Producto.View;
using ENTITY.reg.Precio.View;
using ENTITY.reg.PrecioCategoria.View;
using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

using UTILITY.Global;
using UTILITY.Enum.EnEstado;
using System.Transactions;

namespace PRESENTER.reg
{
    public partial class f1_Precio : MODEL.ModeloF1
    {
        #region Variables Locales
        string _NombreFormulario = "PRECIOS";
        DataTable tPrecio = new DataTable();
        GridEX Dgv_PrecioImport = new GridEX();
        DataTable PrecioImport = new DataTable();
        int _IdCategoria = 0;
        List<VPrecioCategoria> lCategoria = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
        #endregion
        #region Eventos
        public f1_Precio()
        {
            InitializeComponent();
            SuperTabBuscar.Visible = false;
            MP_Iniciar();
        }
        private void Dgv_Categoria_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        private void Cb_Almacen_ValueChanged(object sender, EventArgs e)
        {
            MP_CargarPrecio(true);
        }
        private void BtnExportar_Click(object sender, EventArgs e)
        {
            MP_ImportarExelLista();
            MP_PasarRegistrosImportados();
        }
        private void Btn_Agregar_Click(object sender, EventArgs e)
        {
            MP_GrabarCategoria();
        }
        private void Dgv_Precio_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (BtnGrabar.Enabled)
                {

                    foreach (var categoria in lCategoria)
                    {
                        if (e.Column.Index == Dgv_Precio.RootTable.Columns[categoria.Cod].Index)
                        {
                            e.Cancel = false;
                            break;
                        }
                        else
                            e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }    
    #endregion
    #region Metodos Privados
    private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                //Carga las librerias al combobox desde una lista
                UTGlobal.MG_ArmarComboSucursal(Cb_Almacen,
                                                new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());

                MP_CargarCategoria();
                //MP_InHabilitar();
                BtnExportar.Visible = true;
                BtnNuevo.Visible = false;
                BtnEliminar.Visible = false;
                BtnImprimir.Visible = false;
                BtnExportar.Text = "IMPORTAR";
                MP_InHabilitar();
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
        private void MP_CargarCategoria()
        {
            try
            {
                var lresult = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
                if (lresult.Count() > 0)
                {
                    DataTable result = ListaATabla(lresult);
                    Dgv_Categoria.DataSource = result;
                    Dgv_Categoria.RetrieveStructure();
                    Dgv_Categoria.AlternatingColors = true;

                    Dgv_Categoria.RootTable.Columns["id"].Visible = false;

                    Dgv_Categoria.RootTable.Columns["Cod"].Key = "Cod";
                    Dgv_Categoria.RootTable.Columns["Cod"].Caption = "CODIGO";
                    Dgv_Categoria.RootTable.Columns["Cod"].Width = 80;
                    Dgv_Categoria.RootTable.Columns["Cod"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns["Cod"].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns["Cod"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Categoria.RootTable.Columns["Cod"].Visible = true;

                    Dgv_Categoria.RootTable.Columns["Descrip"].Key = "Descrip";
                    Dgv_Categoria.RootTable.Columns["Descrip"].Caption = "DESCRIPCION";
                    Dgv_Categoria.RootTable.Columns["Descrip"].Width = 150;
                    Dgv_Categoria.RootTable.Columns["Descrip"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns["Descrip"].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns["Descrip"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Categoria.RootTable.Columns["Descrip"].Visible = true;

                    Dgv_Categoria.RootTable.Columns["Tipo"].Visible = false;

                    Dgv_Categoria.RootTable.Columns["NombreTipo"].Caption = "ESTADO";
                    Dgv_Categoria.RootTable.Columns["NombreTipo"].Width = 120;
                    Dgv_Categoria.RootTable.Columns["NombreTipo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns["NombreTipo"].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns["NombreTipo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Categoria.RootTable.Columns["NombreTipo"].Visible = true;

                    Dgv_Categoria.RootTable.Columns["Margen"].Caption = "MARGEN";
                    Dgv_Categoria.RootTable.Columns["Margen"].Width = 120;
                    Dgv_Categoria.RootTable.Columns["Margen"].FormatString = "0.00";
                    Dgv_Categoria.RootTable.Columns["Margen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns["Margen"].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns["Margen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Categoria.RootTable.Columns["Margen"].Visible = true;

                    Dgv_Categoria.RootTable.Columns["Fecha"].Visible = true;
                    Dgv_Categoria.RootTable.Columns["Usuario"].Visible = true;
                    Dgv_Categoria.RootTable.Columns["Hora"].Visible = true;

                    Dgv_Categoria.RootTable.Columns["Estado"].Visible = true;

                    //Habilitar filtradores
                    Dgv_Categoria.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Categoria.FilterMode = FilterMode.Automatic;
                    Dgv_Categoria.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Precio.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Categoria.GroupByBoxVisible = false;
                    Dgv_Categoria.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        public static DataTable ListaATabla(List<VPrecioCategoria> lista)
        {
            DataTable tabla = new DataTable();
            //Crear la Estructura de la Tabla a partir de la Lista de Objetos
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                tabla.Columns.Add(propiedades[i].Name, propiedades[i].PropertyType);
            }
            //Llenar la Tabla desde la Lista de Objetos
            DataRow fila = null;
            for (int i = 0; i < lista.Count; i++)
            {
                propiedades = lista[i].GetType().GetProperties();
                fila = tabla.NewRow();
                for (int j = 0; j < propiedades.Length; j++)
                {
                    fila[j] = propiedades[j].GetValue(lista[i], null);
                }
                tabla.Rows.Add(fila);
            }
            return (tabla);
        }
        public static DataTable ListaATabla2(List<VPrecioLista> lista)
        {
            DataTable tabla = new DataTable();
            //Crear la Estructura de la Tabla a partir de la Lista de Objetos
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                tabla.Columns.Add(propiedades[i].Name, propiedades[i].PropertyType);
            }
            //Llenar la Tabla desde la Lista de Objetos
            DataRow fila = null;
            for (int i = 0; i < lista.Count; i++)
            {
                propiedades = lista[i].GetType().GetProperties();
                fila = tabla.NewRow();
                for (int j = 0; j < propiedades.Length; j++)
                {
                    fila[j] = propiedades[j].GetValue(lista[i], null);
                }
                tabla.Rows.Add(fila);
            }
            return (tabla);
        }

        public static DataTable ListaATabla3(List<VProductoLista> lista)
        {
            DataTable tabla = new DataTable();
            //Crear la Estructura de la Tabla a partir de la Lista de Objetos
            PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
            for (int i = 0; i < propiedades.Length; i++)
            {
                tabla.Columns.Add(propiedades[i].Name, propiedades[i].PropertyType);
            }
            //Llenar la Tabla desde la Lista de Objetos
            DataRow fila = null;
            for (int i = 0; i < lista.Count; i++)
            {
                propiedades = lista[i].GetType().GetProperties();
                fila = tabla.NewRow();
                for (int j = 0; j < propiedades.Length; j++)
                {
                    fila[j] = propiedades[j].GetValue(lista[i], null);
                }
                tabla.Rows.Add(fila);
            }
            return (tabla);
        }
        private void MP_CargarPrecioTabla()
        {
            tPrecio = new ServiceDesktop.ServiceDesktopClient().PrecioProductoListar2(Convert.ToInt32(Cb_Almacen.Value));
           
        }
        private void MP_CargarPrecio(bool bandera)
        {
            try
            {
                if (Convert.ToUInt32(Cb_Almacen.Value) >= 0)
                {
                    var lProducto = new ServiceDesktop.ServiceDesktopClient().ProductoListar().ToList();
                    DataTable tProducto = ListaATabla3(lProducto);
                    if (bandera)
                    {
                        MP_CargarPrecioTabla();
                    }
                    var lPrecitoCat = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
                    DataTable tPrecioCat = ListaATabla(lPrecitoCat);

                    foreach (DataRow rPrecioCat in tPrecioCat.Rows)
                    {
                        tProducto.Columns.Add("estado_" + rPrecioCat.Field<string>("Cod"));
                        tProducto.Columns.Add(rPrecioCat.Field<string>("Cod").ToString().Trim());
                    }
                    for (int j = 0; j < tProducto.Rows.Count; j++)
                    {
                        int idProdcuto = tProducto.Rows[j].Field<int>("Id");
                        DataRow[] resultado = tPrecio.Select("IdProducto=" + idProdcuto.ToString());
                        for (int i = 0; i < resultado.Length; i++)
                        {
                            int rowIndex = tPrecio.Rows.IndexOf(resultado[i]);
                            string columnprecio = resultado[i].Field<string>("Cod").ToString();
                            string columnEstado = "estado_" + resultado[i].Field<string>("Cod");
                            tProducto.Rows[j][columnprecio] = Math.Round(resultado[i].Field<decimal>("Precio"), 2);
                            tProducto.Rows[j][columnEstado] = resultado[i].Field<int>("Estado").ToString() + "_" + rowIndex.ToString().Trim();
                        }
                    }
                    Dgv_Precio.DataSource = tProducto;
                    Dgv_Precio.RetrieveStructure();
                    if (tProducto.Rows.Count > 0)
                    {
                        foreach (DataRow rPrecioCat in tPrecioCat.Rows)
                        {
                            string columnprecio = rPrecioCat.Field<string>("Cod").ToString().Trim();
                            string columnestado = "estado_" + rPrecioCat.Field<string>("Cod").ToString().Trim();

                            Dgv_Precio.RootTable.Columns[columnestado].Visible = false;

                            Dgv_Precio.RootTable.Columns[columnprecio].Caption = rPrecioCat.Field<string>("Cod").ToString();
                            Dgv_Precio.RootTable.Columns[columnprecio].Width = 70;
                            Dgv_Precio.RootTable.Columns[columnprecio].FormatString = "0.00";
                            Dgv_Precio.RootTable.Columns[columnprecio].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                            Dgv_Precio.RootTable.Columns[columnprecio].CellStyle.FontSize = 8;
                            Dgv_Precio.RootTable.Columns[columnprecio].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                            Dgv_Precio.RootTable.Columns[columnprecio].Visible = true;
                        }
                        // Dgv_Precio.RetrieveStructure();
                        Dgv_Precio.AlternatingColors = true;
                        Dgv_Precio.RootTable.Columns["Id"].Visible = false;

                        Dgv_Precio.RootTable.Columns["Codigo"].Caption = "Codigo";
                        Dgv_Precio.RootTable.Columns["Codigo"].Width = 100;
                        Dgv_Precio.RootTable.Columns["Codigo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_Precio.RootTable.Columns["Codigo"].CellStyle.FontSize = 8;
                        Dgv_Precio.RootTable.Columns["Codigo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_Precio.RootTable.Columns["Codigo"].Visible = true;

                        Dgv_Precio.RootTable.Columns["Descripcion"].Caption = "Descripcion";
                        Dgv_Precio.RootTable.Columns["Descripcion"].Width = 150;
                        Dgv_Precio.RootTable.Columns["Descripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_Precio.RootTable.Columns["Descripcion"].CellStyle.FontSize = 8;
                        Dgv_Precio.RootTable.Columns["Descripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_Precio.RootTable.Columns["Descripcion"].Visible = true;

                        Dgv_Precio.RootTable.Columns["Grupo1"].Caption = "División";
                        Dgv_Precio.RootTable.Columns["Grupo1"].Width = 150;
                        Dgv_Precio.RootTable.Columns["Grupo1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_Precio.RootTable.Columns["Grupo1"].CellStyle.FontSize = 8;
                        Dgv_Precio.RootTable.Columns["Grupo1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_Precio.RootTable.Columns["Grupo1"].Visible = false;

                        Dgv_Precio.RootTable.Columns["Grupo2"].Caption = "Tipo";
                        Dgv_Precio.RootTable.Columns["Grupo2"].Width = 120;
                        Dgv_Precio.RootTable.Columns["Grupo2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_Precio.RootTable.Columns["Grupo2"].CellStyle.FontSize = 8;
                        Dgv_Precio.RootTable.Columns["Grupo2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_Precio.RootTable.Columns["Grupo2"].Visible = true;

                        Dgv_Precio.RootTable.Columns["Grupo3"].Caption = "CategorIas";
                        Dgv_Precio.RootTable.Columns["Grupo3"].Width = 200;
                        Dgv_Precio.RootTable.Columns["Grupo3"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_Precio.RootTable.Columns["Grupo3"].CellStyle.FontSize = 8;
                        Dgv_Precio.RootTable.Columns["Grupo3"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_Precio.RootTable.Columns["Grupo3"].Visible = false;

                        Dgv_Precio.RootTable.Columns["Tipo"].Visible = false;
                        Dgv_Precio.RootTable.Columns["Usuario"].Visible = false;
                        Dgv_Precio.RootTable.Columns["Hora"].Visible = false;
                        Dgv_Precio.RootTable.Columns["Fecha"].Visible = false;

                        //Habilitar filtradores
                        Dgv_Precio.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                        Dgv_Precio.FilterMode = FilterMode.Automatic;
                        Dgv_Precio.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                        //Dgv_Precio.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                        Dgv_Precio.GroupByBoxVisible = false;
                        Dgv_Precio.VisualStyle = VisualStyle.Office2007;
                    }
                }
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }

        }

        private void MP_ImportarExelLista()
        {
            try
            {
                //String name = "LISTA";
                //String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                //               @"D:\DINASES\DiAvi\ARCHIVOS\Plantilla.xlsx" +
                //                ";Extended Properties='Excel 8.0;HDR=SI;';";

                //OleDbConnection con = new OleDbConnection(constr);
                //OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                //con.Open();

                //OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                //DataTable data = new DataTable();
                //sda.Fill(data);
                //Dgv_Precio.DataSource = data;
                //con.Close();
                string folder = "";
                string doc = "LISTA";
                OpenFileDialog openfile1 = new OpenFileDialog();
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folder = openfile1.FileName;
                }
                {
                    string pathconn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + folder + @";Extended Properties=""Excel 12.0 Xml;HDR=YES;""";

                    OleDbConnection con = new OleDbConnection(pathconn);
                    //OleDbCommand oconn = new OleDbCommand("Select * from [" + doc + "$]", con);             
                    con.Open();
                    OleDbDataAdapter MyDataAdapter = new OleDbDataAdapter("Select * from [" + doc + "$]", con);

                    //DataTable dt = new DataTable();
                    MyDataAdapter.Fill(PrecioImport);
                    //Dgv_PrecioImport.DataSource = dt;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_PasarRegistrosImportados()
        {
            try
            {
                for (int i = 0; i < Dgv_Precio.RowCount - 1; i++)
                {
                    foreach (DataRow filaImport in PrecioImport.Rows)
                    {
                        string a = filaImport["Codigo"].ToString();
                        if (((DataTable)Dgv_Precio.DataSource).Rows[i]["Codigo"].ToString() == filaImport["Codigo"].ToString())
                        {
                            var lresult = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
                            foreach (var categoria in lresult)
                            {
                                string e = categoria.Cod;
                                string z = ((DataTable)Dgv_Precio.DataSource).Rows[i][categoria.Cod].ToString();
                                string idProducto = ((DataTable)Dgv_Precio.DataSource).Rows[i]["Id"].ToString();
                                string x = filaImport[categoria.Cod].ToString();
                                ((DataTable)Dgv_Precio.DataSource).Rows[i][categoria.Cod] = x;

                                DataRow[] resultado = tPrecio.Select("IdProducto=" + idProducto + "and Cod='" + categoria.Cod + "'");

                                int rowIndex = tPrecio.Rows.IndexOf(resultado[0]);
                                string columna= resultado[0].Field<int>("Estado").ToString() + "_" + rowIndex.ToString().Trim();
                                MP_ActualizarPrecio(columna, null, x, 2);
                            }
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_GrabarCategoria()
        {
            try
            {
                if (!MP_Validar())
                {
                    bool resultado = false;
                    string mensaje = "";

                    VPrecioCategoria vPrecioCategoria = new VPrecioCategoria()
                    {
                        Cod = Tb_CodCategoria.Text,
                        Descrip = Tb_Descripcion.Text,
                        Estado = (int)ENEstado.GUARDADO,
                        Tipo = Sw_Tipo.Value ? 1 : 0,
                        Margen = Convert.ToDecimal(Tb_Margen.Text),
                        Fecha = DateTime.Now.Date,
                        Hora = DateTime.Now.ToString("hh:mm"),
                        Usuario = UTGlobal.Usuario
                    };
                    int id = _IdCategoria;
                    int idAux = id;
                    resultado = new ServiceDesktop.ServiceDesktopClient().precioCategoria_Guardar(vPrecioCategoria, ref id);
                    if (resultado)
                    {
                        if (idAux == 0)//Registar
                        {

                            MP_CargarCategoria();
                            MP_CargarTablaPrecios();
                            lCategoria = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
                            mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                        }
                        else//Modificar
                        {
                            MP_CargarCategoria();
                            MP_CargarTablaPrecios();
                            lCategoria = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
                            mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                            MH_Habilitar();//El menu                   
                        }
                    }
                    //Resultado
                    if (resultado)
                    {
                        ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                    }
                    else
                    {
                        mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                        ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        public bool MP_Validar()
        {
            bool _Error = false;
            try
            {              
                if (Tb_CodCategoria.Text == "")
                {
                    Tb_CodCategoria.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Tb_CodCategoria.BackColor = Color.White;

                if (Tb_Descripcion.Text == "")
                {
                    Tb_Descripcion.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Tb_Descripcion.BackColor = Color.White;
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }           
        }
        private void MP_CargarTablaPrecios()
        {
            try
            {
                DataRow[] result = tPrecio.Select("estado > 1");
                MP_CargarPrecioTabla();
                for (int i = 0; i < result.Length - 1; i++)
                {
                    DataRow r = result[i];
                    DataRow[] dr;
                    dr = tPrecio.Select("IdProducto=" + r["IdProducto"].ToString() + " and IdPrecioCat=" + r["IdPrecioCat"].ToString());
                    if (dr != null)
                    {
                        dr[0]["Precio"] = r["Precio"];
                        dr[0]["estado"] = r["estado"];
                    }
                }
                MP_CargarPrecio(false);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }
        private void MP_Habilitar()
        {         
            Tb_CodCategoria.ReadOnly = false;
            Tb_Descripcion.ReadOnly = false;
            Tb_Margen.ReadOnly = false;
            Cb_Almacen.ReadOnly = false;
            Sw_Tipo.IsReadOnly = false;
            BtnExportar.Enabled = true;
        }
        private void MP_InHabilitar()
        {
            Tb_CodCategoria.ReadOnly = true;
            Tb_Descripcion.ReadOnly = true;
            Tb_Margen.ReadOnly = true;
            Cb_Almacen.ReadOnly = true;
            Sw_Tipo.IsReadOnly = true;
            BtnExportar.Enabled = false;
        }
        #endregion

        #region Metodos Heredados     

        public override bool MH_NuevoRegistro()
        {
            try
            {                
                bool resultado = false;
                string mensaje = "";
                List<VPrecioLista> lPrecio = new List<VPrecioLista>();   
                foreach (DataRow item in tPrecio.Rows)
                {
                    VPrecioLista vPrecio= new VPrecioLista
                    {
                        Id = item.Field<int>("Id"),
                        IdProducto = item.Field<int>("IdProducto"),
                        IdPrecioCat = item.Field<int>("IdPrecioCat"),
                        COd = item.Field<string>("Cod"),
                        Precio = item.Field<decimal>("Precio"),
                        Estado = item.Field<int>("Estado"),
                    };
                    lPrecio.Add(vPrecio);
                }
                var lista = lPrecio.ToArray();
                using (var scope = new TransactionScope())
                {
                    foreach (var item in lPrecio)
                    {
                        if (item.Estado == 3)
                        {
                            resultado = new ServiceDesktop.ServiceDesktopClient().PrecioNuevo(item, Convert.ToInt32(Cb_Almacen.Value), UTGlobal.Usuario);
                        }
                        else
                        {
                            if (item.Estado == 2)
                            {

                                resultado = new ServiceDesktop.ServiceDesktopClient().PrecioModificar(item, UTGlobal.Usuario);
                            }
                        }
                    }
                    scope.Complete();
                    resultado = true;
                }
                //resultado = new ServiceDesktop.ServiceDesktopClient().PrecioGuardar(lista, Convert.ToInt32(Cb_Almacen.Value), UTGlobal.Usuario);
                if (resultado)
                {
                    MP_CargarPrecio(true);
                    MP_InHabilitar();//El formulario
                    mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, "");
                }
                //Resultado
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
                return false;
            }        
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {
                if (Cb_Almacen.SelectedIndex == -1)
                {
                    _Error = true;
                    throw new Exception("Debe seleccionar una sucursal");
                }
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }          
        }
   

        public override void MH_Modificar()
        {
            MP_Habilitar();           
        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
        }
        #endregion

        private void Dgv_Precio_CellEdited(object sender, ColumnActionEventArgs e)
        {
            if (Tb_CodCategoria.ReadOnly == false)
            {
                if (e.Column.Index > 1)
                {
                    var precio =  Dgv_Precio.GetValue(e.Column.Index);
                    MP_ActualizarPrecio(e.Column.Index.ToString(), precio,"",1);
                }
            }
        }

        private void MP_ActualizarPrecio(string columna, object precio, string precio2, int tipo )
        {
            try
            {
                string data = tipo == 1? Dgv_Precio.GetValue(Convert.ToInt32(columna) - 1).ToString().Trim(): columna;
                string estado = data.Substring(0, 1).Trim();
                string pos = data.Substring(2, data.Length - 2);
                if (estado == "1" || estado == "2")
                {
                    tPrecio.Rows[Convert.ToInt32(pos)]["estado"] = 2;
                    tPrecio.Rows[Convert.ToInt32(pos)]["precio"] = tipo == 1 ? precio : precio2;
                }
                else
                {
                    if (estado == "0" || estado == "3")
                    {
                        tPrecio.Rows[Convert.ToInt32(pos)]["estado"] = 3;
                        tPrecio.Rows[Convert.ToInt32(pos)]["precio"] = tipo == 1 ? precio : precio2;
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }        
    }
}
