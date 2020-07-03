using PRESENTER.alm;
using PRESENTER.com;
using PRESENTER.reg;
using PRESENTER.ven;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using PRESENTER.com.Reporte;

namespace PRESENTER.adm
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }
        #region Metodos Privados
        private void rmSesion_ItemClick(object sender, EventArgs e)
        {
            Login login = new Login();
            RadialMenuItem item = sender as RadialMenuItem;
            if (item != null && (!string.IsNullOrEmpty(item.Text)))
            {
                switch (item.Name)
                {
                    case "btnCerrarSesion":
                        {
                            this.Hide();
                            FP_Administracion.Select();
                            login.Show();
                            break;
                        }

                    case "btnSalir":
                        {
                            _Salir();
                            break;
                        }

                    case "btnAbout":
                        {
                            break;
                        }
                }
            }
        }
        private void _Salir()
        {
            //this.Close();
            Application.ExitThread();
        }
        #endregion
        #region Eventos

        #region Administracion
        private void Metro_Usuarios_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = Metro_Usuarios.Name;
            F1_Usuarios frm = new F1_Usuarios();
            frm.Show();
        }


        private void Metro_Roles_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = Metro_Roles.Name;
            F1_Roles frm = new F1_Roles();
            frm.Show();
        }

        private void CerrarSesion_Click(object sender, EventArgs e)
        {
            rmSesion.IsOpen = true;
            rmSesion.MenuLocation = new Point(this.Width / 2, this.Height / 3);
        }
        private void Metro_Libreria_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = Metro_Libreria.Name;
            F1_Libreria frm = new F1_Libreria();
            frm.Show();
        }
        #endregion
        #region Registro
        private void Metro_Clientes_Click(object sender, EventArgs e)
        {
            F1_Clientes frm = new F1_Clientes();
            frm.Show();
        }

        private void Metro_Productos_Click(object sender, EventArgs e)
        {
            F1_Productos frm = new F1_Productos();
            frm.Show();
        }
        private void Metro_Precio_Click(object sender, EventArgs e)
        {
            f1_Precio frm = new f1_Precio();
            frm.Show();
        }
        #endregion
        #region Inventario
        private void Metro_Transformacion_Click_1(object sender, EventArgs e)
        {
            F1_Transformacion frm = new F1_Transformacion();
            frm.Show();
        }


        private void btInvSucursal_Click(object sender, EventArgs e)
        {
            F1_Sucursal frm = new F1_Sucursal();
            frm.Show();
        }

        private void btInvTraspaso_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = btInvTraspaso.Name;
            F1_Traspaso1 frm = new F1_Traspaso1();
            frm.Show();
        }

        private void btInvAlmacen_Click(object sender, EventArgs e)
        {
            F1_Almacen frm = new F1_Almacen();
            frm.Show();
        }

        private void btn_TipoAlmacen_Click(object sender, EventArgs e)
        {
            F1_TipoAlmacen frm = new F1_TipoAlmacen();
            frm.Show();
        }

        private void btnReporteKardex_Click(object sender, EventArgs e)
        {
            F1_ReporteKardex frm = new F1_ReporteKardex();
            frm.Show();
        }
        #endregion
        #region Compras
        private void Metro_Proveedor_Click(object sender, EventArgs e)
        {
            F1_Proveedor frm = new F1_Proveedor();
            frm.Show();
        }
        private void Metro_CompraIngreso_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = Metro_CompraIngreso.Name;
            F1_CompraIngreso frm = new F1_CompraIngreso();
            frm.Show();
        }
        
        private void Metro_Transformacion_Click(object sender, EventArgs e)
        {
            FI_Seleccion frm = new FI_Seleccion();
            frm.Show();
        }

        private void Metro_Compra_Click(object sender, EventArgs e)
        {
            F1_Compra frm = new F1_Compra();
            frm.Show();
        }
        private void Metro_Reporte_CompraIngreso_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = Metro_Reporte_CompraIngreso.Name;
            F2_CompraIngreso frm = new F2_CompraIngreso();
            frm.Show();
        }
        private void metroMetro_Reporte_CompraIngresoCriterio_Click(object sender, EventArgs e)
        {
            MODEL.ModeloF1._NombreProg = Metro_Reporte_CompraIngresoCriterio.Name;
            F2_CompraIngresoCriterio frm = new F2_CompraIngresoCriterio();
            frm.Show();
        }
        #endregion
        #region Ventas
        private void btnVentas_Click(object sender, EventArgs e)
        {
            F1_Ventas frm = new F1_Ventas();
            frm.Show();
        }

        #endregion

        #endregion

       
    }
}
