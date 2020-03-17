﻿using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_ReporteKardex : MODEL.ModeloF1
    {
        public F1_ReporteKardex()
        {
            InitializeComponent();
            this.MP_CargarAlmacenes();
        }

        #region Variables de Entorno


        #endregion

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacenes, almacenes);

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion

        #region Eventos

        private void F1_ReporteKardex_Load(object sender, EventArgs e)
        {
            this.PanelContenidoBuscar.Visible = false;
            this.PanelMenu.Visible = false;
            this.SuperTabBuscar.Visible = false;
            this.TxtNombreUsu.Visible = false;
            this.SuperTalRegistro.Text = "REPORTES";
            this.PanelInferior.Visible = false;
        }

        private void BtKardexGeneral_Click(object sender, EventArgs e)
        {

        }

        #endregion        
    }
}
