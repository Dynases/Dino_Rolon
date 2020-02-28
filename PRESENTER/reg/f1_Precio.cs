using ENTITY.Producto.View;
using ENTITY.reg.Precio.View;
using ENTITY.reg.PrecioCategoria.View;
using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.reg
{
    public partial class f1_Precio : MODEL.ModeloF1
    {
        #region Variables Locales
        string _NombreFormulario = "PRECIOS";
        DataTable tPrecio = new DataTable();
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
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "Comuniquece con el administrador del sistema");
            }
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
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
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
                        Dgv_Precio.RootTable.Columns[columnestado].Caption = "";
                        Dgv_Precio.RootTable.Columns[columnestado].Width = 150;            
                        Dgv_Precio.RootTable.Columns[columnestado].Visible = false;
                        Dgv_Precio.RootTable.Columns[columnestado].FormatString = "0";

                        //Dgv_Precio.RootTable.Columns[columnprecio].Key = "Cod";
                        Dgv_Precio.RootTable.Columns[columnprecio].Caption = rPrecioCat.Field<string>("Cod").ToString();
                        Dgv_Precio.RootTable.Columns[columnprecio].Width = 70;
                        Dgv_Precio.RootTable.Columns[columnprecio].FormatString = "0.00";
                        Dgv_Precio.RootTable.Columns[columnprecio].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                        Dgv_Precio.RootTable.Columns[columnprecio].CellStyle.FontSize = 8;
                        Dgv_Precio.RootTable.Columns[columnprecio].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                        Dgv_Precio.RootTable.Columns[columnprecio].Visible = true;
                    }
                    Dgv_Precio.RetrieveStructure();
                    Dgv_Precio.AlternatingColors = true;

                    Dgv_Precio.RootTable.Columns[0].Key = "id";
                    Dgv_Precio.RootTable.Columns[0].Visible = false;

                    Dgv_Precio.RootTable.Columns[1].Key = "CodProducto";
                    Dgv_Precio.RootTable.Columns[1].Visible = false;


                    Dgv_Precio.RootTable.Columns[2].Key = "Descripcion";
                    Dgv_Precio.RootTable.Columns[2].Caption = "Descripcion";
                    Dgv_Precio.RootTable.Columns[2].Width = 210;
                    Dgv_Precio.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns[2].Visible = true;

                    Dgv_Precio.RootTable.Columns[3].Key = "División";
                    Dgv_Precio.RootTable.Columns[3].Caption = "División";
                    Dgv_Precio.RootTable.Columns[3].Width = 150;
                    Dgv_Precio.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns[3].Visible = true;


                    Dgv_Precio.RootTable.Columns[4].Key = "Marca/Tipo";
                    Dgv_Precio.RootTable.Columns[4].Caption = "Marca/Tipo";
                    Dgv_Precio.RootTable.Columns[4].Width = 250;
                    Dgv_Precio.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns[4].Visible = false;

                    Dgv_Precio.RootTable.Columns[5].Key = "Categorías/Tipo";
                    Dgv_Precio.RootTable.Columns[5].Caption = "CategorIas/tipo";
                    Dgv_Precio.RootTable.Columns[5].Width = 200;
                    Dgv_Precio.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns[5].Visible = false;

                    Dgv_Precio.RootTable.Columns[6].Key = "Contacto2";
                    Dgv_Precio.RootTable.Columns[6].Caption = "Contacto2";
                    Dgv_Precio.RootTable.Columns[6].Width = 200;
                    Dgv_Precio.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Precio.RootTable.Columns[6].CellStyle.FontSize = 8;
                    Dgv_Precio.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Precio.RootTable.Columns[6].Visible = false;

                    Dgv_Precio.RootTable.Columns[7].Key = "Usuario";
                    Dgv_Precio.RootTable.Columns[7].Visible = false;

                    Dgv_Precio.RootTable.Columns[8].Key = "Hora";
                    Dgv_Precio.RootTable.Columns[8].Visible = false;

                    Dgv_Precio.RootTable.Columns[9].Key = "Fecha";
                    Dgv_Precio.RootTable.Columns[9].Visible = false;
             
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


        #endregion

        #region Metodos Heredados

        #endregion


    }
}
