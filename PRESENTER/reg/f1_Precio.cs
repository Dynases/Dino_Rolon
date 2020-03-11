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

namespace PRESENTER.reg
{
    public partial class f1_Precio : MODEL.ModeloF1
    {
        #region Variables Locales
        string _NombreFormulario = "PRECIOS";
        DataTable tPrecio = new DataTable();
        GridEX Dgv_PrecioImport = new GridEX();
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

                    Dgv_Categoria.RootTable.Columns[0].Key = "id";
                    Dgv_Categoria.RootTable.Columns[0].Visible = false;

                    Dgv_Categoria.RootTable.Columns[1].Key = "Cod";
                    Dgv_Categoria.RootTable.Columns[1].Caption = "CODIGO";
                    Dgv_Categoria.RootTable.Columns[1].Width = 80;
                    Dgv_Categoria.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Categoria.RootTable.Columns[1].Visible = true;

                    Dgv_Categoria.RootTable.Columns[2].Key = "Descrip";
                    Dgv_Categoria.RootTable.Columns[2].Caption = "DESCRIPCION";
                    Dgv_Categoria.RootTable.Columns[2].Width = 150;
                    Dgv_Categoria.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Categoria.RootTable.Columns[2].Visible = true;

                    Dgv_Categoria.RootTable.Columns[3].Key = "Tipo";
                    Dgv_Categoria.RootTable.Columns[3].Visible = false;


                    Dgv_Categoria.RootTable.Columns[4].Key = "NombreTipo";
                    Dgv_Categoria.RootTable.Columns[4].Caption = "ESTADO";
                    Dgv_Categoria.RootTable.Columns[4].Width = 120;
                    Dgv_Categoria.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Categoria.RootTable.Columns[4].Visible = true;

                    Dgv_Categoria.RootTable.Columns[5].Key = "Margen";
                    Dgv_Categoria.RootTable.Columns[5].Caption = "MARGEN";
                    Dgv_Categoria.RootTable.Columns[5].Width = 120;
                    Dgv_Categoria.RootTable.Columns[5].FormatString = "0.00";
                    Dgv_Categoria.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Categoria.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_Categoria.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Categoria.RootTable.Columns[5].Visible = true;

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
            var precio = new ServiceDesktop.ServiceDesktopClient().PrecioProductoListar(Convert.ToInt32(Cb_Almacen.Value)).ToList();
            tPrecio= ListaATabla2(precio);
        }
        private void MP_CargarPrecio(bool bandera)
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
                    tProducto.Columns.Add(rPrecioCat.Field<int>("Id").ToString().Trim());
                }
                for (int j = 0; j < tProducto.Rows.Count ; j++)
                {
                    int idProdcuto = tProducto.Rows[j].Field<int>("Id");
                    DataRow[] resultado = tPrecio.Select("IdProducto=" + idProdcuto.ToString());
                    for (int i = 0; i < resultado.Length ; i++)
                    {
                        int rowIndex = tPrecio.Rows.IndexOf(resultado[i]);
                        string columnprecio = resultado[i].Field<int>("IdPrecioCat").ToString();
                        string columnEstado = "estado_" + resultado[i].Field<string>("Cod");
                        tProducto.Rows[j][columnprecio] = Math.Round(resultado[i].Field<decimal>("Precio"), 2);
                        tProducto.Rows[j][columnEstado] = resultado[i].Field<int>("Estado").ToString() + "_" + rowIndex.ToString().Trim();
                    }
                }
                //Dgv_Precio.BoundMode = Janus.Data.BoundMode.Bound;
                Dgv_Precio.DataSource = tProducto;
                Dgv_Precio.RetrieveStructure();
                if (tProducto.Rows.Count > 0)
                {
                    foreach (DataRow rPrecioCat in tPrecioCat.Rows)
                    {
                        string columnprecio = rPrecioCat.Field<int>("Id").ToString().Trim();
                        string columnestado = "estado_" + rPrecioCat.Field<string>("Cod").ToString().Trim();

                        //Dgv_Precio.RootTable.Columns[columnestado].Key = "Id";
                        //Dgv_Precio.RootTable.Columns[columnestado].Caption = "";
                        //Dgv_Precio.RootTable.Columns[columnestado].Width = 150;            
                        Dgv_Precio.RootTable.Columns[columnestado].Visible = false;
                       // Dgv_Precio.RootTable.Columns[columnestado].FormatString = "0";

                        //Dgv_Precio.RootTable.Columns[columnprecio].Key = "Cod";
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

                    Dgv_Precio.RootTable.Columns["Cod_Producto"].Visible = false;

                    Dgv_Precio.RootTable.Columns["Descripcion"].Caption = "Descripcion";
                    Dgv_Precio.RootTable.Columns["Descripcion"].Width = 210;
                    Dgv_Precio.RootTable.Columns["Descripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns["Descripcion"].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns["Descripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns["Descripcion"].Visible = true;

                    Dgv_Precio.RootTable.Columns["Grupo1"].Caption = "División";
                    Dgv_Precio.RootTable.Columns["Grupo1"].Width = 150;
                    Dgv_Precio.RootTable.Columns["Grupo1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns["Grupo1"].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns["Grupo1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns["Grupo1"].Visible = true;

                    Dgv_Precio.RootTable.Columns["Grupo2"].Caption = "Marca/Tipo";
                    Dgv_Precio.RootTable.Columns["Grupo2"].Width = 250;
                    Dgv_Precio.RootTable.Columns["Grupo2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns["Grupo2"].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns["Grupo2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns["Grupo2"].Visible = false;

                    Dgv_Precio.RootTable.Columns["Grupo3"].Caption = "CategorIas/tipo";
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
                 
                    DataTable dt = new DataTable();
                    MyDataAdapter.Fill(dt);
                    Dgv_PrecioImport.DataSource = dt;
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
            var lresult = new ServiceDesktop.ServiceDesktopClient().PrecioCategoriaListar().ToList();
            foreach (var fila in Dgv_Precio.GetRows())
            {
                foreach (var filaImport in Dgv_PrecioImport.GetRows())
                {
                    foreach (var categoria in lresult)
                    {
                        if (fila.Cells[categoria.Descrip].Value.ToString() == filaImport.Cells[categoria.Descrip].Value.ToString())
                        {
                            fila.Cells[categoria.Descrip].Value = filaImport.Cells[categoria.Descrip].Value;
                        }
                    }
                }              
            }
        }

        #endregion

        #region Metodos Heredados      

        #endregion

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            MP_ImportarExelLista();
        }
    }
}
