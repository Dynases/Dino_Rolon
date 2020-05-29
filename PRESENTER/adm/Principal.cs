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

namespace PRESENTER.adm
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Metro_Clientes_Click(object sender, EventArgs e)
        {
            F1_Clientes frm = new F1_Clientes();
            frm.Show();
        }

        private void Metro_Proveedor_Click(object sender, EventArgs e)
        {
            F1_Proveedor frm = new F1_Proveedor();
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

        private void Metro_CompraIngreso_Click(object sender, EventArgs e)
        {
            F1_CompraIngreso frm = new F1_CompraIngreso();
            frm.Show();
        }

        private void Metro_Transformacion_Click(object sender, EventArgs e)
        {
            FI_Seleccion frm = new FI_Seleccion();
            frm.Show();
        }

        private void Metro_Transformacion_Click_1(object sender, EventArgs e)
        {
            F1_Transformacion frm = new F1_Transformacion();
            frm.Show();
        }

        private void Metro_Compra_Click(object sender, EventArgs e)
        {
            F1_Compra frm = new F1_Compra();
            frm.Show();
        }

        private void btInvSucursal_Click(object sender, EventArgs e)
        {
            F1_Sucursal frm = new F1_Sucursal();
            frm.Show();
        }

        private void btInvTraspaso_Click(object sender, EventArgs e)
        {
            F1_Traspaso frm = new F1_Traspaso();
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

        private void btnVentas_Click(object sender, EventArgs e)
        {
            F1_Ventas frm = new F1_Ventas();
            frm.Show();
        }

        private void btnReporteKardex_Click(object sender, EventArgs e)
        {
            F1_ReporteKardex frm = new F1_ReporteKardex();
            frm.Show();
        }

        private void Metro_Usuarios_Click(object sender, EventArgs e)
        {
            F1_Usuarios frm = new F1_Usuarios();
            frm._NombreProg = Metro_Usuarios.Name;
            frm.Show();
        }

        private void Metro_Roles_Click(object sender, EventArgs e)
        {
            F1_Roles frm = new F1_Roles();
            frm._NombreProg = Metro_Roles.Name;
            frm.Show();
        }
    }
}
