using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using System.Drawing;

namespace UTILITY.MetodosExtension
{
    public static class JanusExtension
    {
        #region GridEx
        #region Configuración inicial
        public static void ConfigInicialVinculado<T>(this GridEX grid, T list, string nombre)
        {
            grid.BoundMode = BoundMode.Bound;
            grid.SetDataBinding(list, nombre);
            grid.RetrieveStructure();
        }
        #endregion

        #region Columnas
        public static void ColNoVisible(this GridEX grid, string key)
        {
            grid.RootTable.Columns[key].Visible = false;
        }

        public static void ColAL(this GridEX grid, string key, string nombre, int ancho, int tamFuente = 8)
        {
            grid.RootTable.Columns[key].CellStyle.TextAlignment = TextAlignment.Near;
            ColPropiedadesComun(grid, key, nombre, ancho, tamFuente);
        }

        public static void ColAC(this GridEX grid, string key, string nombre, int ancho, int tamFuente = 8)
        {
            grid.RootTable.Columns[key].CellStyle.TextAlignment = TextAlignment.Center;
            ColPropiedadesComun(grid, key, nombre, ancho, tamFuente);
        }

        public static void ColAR(this GridEX grid, string key, string nombre, int ancho, int tamFuente = 8)
        {
            grid.RootTable.Columns[key].CellStyle.TextAlignment = TextAlignment.Far;
            ColPropiedadesComun(grid, key, nombre, ancho, tamFuente);
        }

        public static void ColARNro(this GridEX grid, string key, string nombre, int ancho, string formato, int tamFuente = 8)
        {
            grid.RootTable.Columns[key].CellStyle.TextAlignment = TextAlignment.Far;
            grid.RootTable.Columns[key].FormatString = formato;
            ColPropiedadesComun(grid, key, nombre, ancho, tamFuente);
        }

        public static void ColIcon(this GridEX grid, int posicion, string key, string nombre, Image imagen, int ancho = 40)
        {
            ColIconPropiedadesComun(grid, key, nombre, imagen, ancho);
            grid.RootTable.Columns[nombre].Position = posicion;
        }

        public static void ColIcon(this GridEX grid, string key, string nombre, Image imagen, int ancho = 40)
        {
            ColIconPropiedadesComun(grid, key, nombre, imagen, ancho);
        }
        #endregion

        #region Configuración final
        public static void ConfigFinalCompleto(this GridEX grid, int fijarColumnas = 0)
        {
            grid.GroupByBoxVisible = true;
            grid.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
            grid.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            grid.FilterMode = FilterMode.Automatic;
            grid.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            grid.VisualStyle = VisualStyle.Office2007;
            grid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            grid.AlternatingColors = true;
            grid.RecordNavigator = true;
            grid.AllowEdit = InheritableBoolean.False;
            grid.AllowColumnDrag = true;
            grid.AutomaticSort = true;
            grid.RootTable.HeaderLines = 2;
            grid.RootTable.RowHeight = 20;
            grid.CellSelectionMode = CellSelectionMode.SingleCell;
            grid.RowHeaders = InheritableBoolean.True;
            grid.RowHeaderContent = RowHeaderContent.RowIndex;
            grid.GroupByBoxInfoText = "Arrastre un encabezado de columna aquí para agrupar por esa columna";
            if (fijarColumnas > 0)
                grid.FrozenColumns = fijarColumnas;
        }

        public static void ConfigFinalBasica(this GridEX grid)
        {
            grid.GroupByBoxVisible = false;
            grid.AlternatingColors = true;
            grid.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
            grid.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            grid.FilterMode = FilterMode.Automatic;
            grid.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            grid.VisualStyle = VisualStyle.Office2007;
            grid.AllowEdit = InheritableBoolean.False;
        }

        public static void ConfigFinalDetalle(this GridEX grid)
        {
            grid.GroupByBoxVisible = false;
            grid.AlternatingColors = true;
            grid.FilterMode = FilterMode.None;
            grid.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            grid.VisualStyle = VisualStyle.Office2007;
        }
        #endregion

        #region Ayudas
        public static string CelValorIdView(this GridEX grid)
        {
            return grid.CelValor<string>("IdView");
        }

        public static T CelValor<T>(this GridEX grid, string key)
        {
            return (T)grid.CurrentRow.Cells[key].Value;
        }
        #endregion

        #region Métodos privados
        private static void ColPropiedadesComun(GridEX grid, string key, string nombre, int ancho, int tamFuente)
        {
            grid.RootTable.Columns[key].Caption = nombre;
            grid.RootTable.Columns[key].HeaderAlignment = TextAlignment.Center;
            grid.RootTable.Columns[key].Width = ancho;
        }

        private static void ColIconPropiedadesComun(GridEX grid, string key, string nombre, Image imagen, int ancho)
        {
            grid.RootTable.Columns.Add(key, ColumnType.Image);
            grid.RootTable.Columns[key].Caption = nombre;
            grid.RootTable.Columns[key].HeaderAlignment = TextAlignment.Center;
            grid.RootTable.Columns[key].Image = imagen;
            grid.RootTable.Columns[key].CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center;
            grid.RootTable.Columns[key].Width = ancho;
        }
        #endregion
        #endregion

        #region MultiColumnCombo
        public static void SelecionarPrimero(this MultiColumnCombo combo)
        {
            try
            {
                combo.SelectedIndex = 0;
            }
            catch { }
        }
        #endregion
    }
}
