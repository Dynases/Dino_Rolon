namespace MODEL
{
    partial class ModeloF2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeloF2));
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMin = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnMax = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnClose = new Bunifu.Framework.UI.BunifuImageButton();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.LblSubtitulo = new System.Windows.Forms.Label();
            this.PanelInferior = new System.Windows.Forms.Panel();
            this.BubbleBarUsuario = new DevComponents.DotNetBar.BubbleBar();
            this.BubbleBarTabUsuario = new DevComponents.DotNetBar.BubbleBarTab(this.components);
            this.TxtNombreUsu = new System.Windows.Forms.TextBox();
            this.PanelNavegacion = new System.Windows.Forms.Panel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.LblPaginacion = new System.Windows.Forms.Label();
            this.GPanel_Criterio = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PanelMenu = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.BtnExportar = new DevComponents.DotNetBar.ButtonX();
            this.BtnGenerar = new DevComponents.DotNetBar.ButtonX();
            this.BtnAtras = new DevComponents.DotNetBar.ButtonX();
            this.reflectionLabelLogo = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.PanelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.PanelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleBarUsuario)).BeginInit();
            this.PanelNavegacion.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.PanelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelSuperior
            // 
            this.PanelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.PanelSuperior.Controls.Add(this.PictureBox1);
            this.PanelSuperior.Controls.Add(this.btnMin);
            this.PanelSuperior.Controls.Add(this.btnMax);
            this.PanelSuperior.Controls.Add(this.btnClose);
            this.PanelSuperior.Controls.Add(this.LblTitulo);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Location = new System.Drawing.Point(0, 0);
            this.PanelSuperior.Name = "PanelSuperior";
            this.PanelSuperior.Size = new System.Drawing.Size(921, 27);
            this.PanelSuperior.TabIndex = 3;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(3, 1);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(31, 26);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 6;
            this.PictureBox1.TabStop = false;
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMin.ErrorImage = null;
            this.btnMin.Image = ((System.Drawing.Image)(resources.GetObject("btnMin.Image")));
            this.btnMin.ImageActive = null;
            this.btnMin.Location = new System.Drawing.Point(861, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(20, 27);
            this.btnMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnMin.TabIndex = 3;
            this.btnMin.TabStop = false;
            this.btnMin.Zoom = 10;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.btnMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMax.ErrorImage = null;
            this.btnMax.Image = ((System.Drawing.Image)(resources.GetObject("btnMax.Image")));
            this.btnMax.ImageActive = null;
            this.btnMax.Location = new System.Drawing.Point(881, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(20, 27);
            this.btnMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnMax.TabIndex = 4;
            this.btnMax.TabStop = false;
            this.btnMax.Zoom = 10;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.ErrorImage = null;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageActive = null;
            this.btnClose.Location = new System.Drawing.Point(901, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 27);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnClose.TabIndex = 5;
            this.btnClose.TabStop = false;
            this.btnClose.Zoom = 10;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LblTitulo
            // 
            this.LblTitulo.AutoSize = true;
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo.ForeColor = System.Drawing.SystemColors.Control;
            this.LblTitulo.Location = new System.Drawing.Point(47, 4);
            this.LblTitulo.Name = "LblTitulo";
            this.LblTitulo.Size = new System.Drawing.Size(63, 17);
            this.LblTitulo.TabIndex = 0;
            this.LblTitulo.Text = "NOMBRE";
            // 
            // LblSubtitulo
            // 
            this.LblSubtitulo.BackColor = System.Drawing.Color.White;
            this.LblSubtitulo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblSubtitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSubtitulo.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSubtitulo.ForeColor = System.Drawing.Color.Black;
            this.LblSubtitulo.Location = new System.Drawing.Point(0, 99);
            this.LblSubtitulo.Name = "LblSubtitulo";
            this.LblSubtitulo.Size = new System.Drawing.Size(921, 36);
            this.LblSubtitulo.TabIndex = 69;
            this.LblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelInferior
            // 
            this.PanelInferior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(65)))));
            this.PanelInferior.Controls.Add(this.BubbleBarUsuario);
            this.PanelInferior.Controls.Add(this.TxtNombreUsu);
            this.PanelInferior.Controls.Add(this.PanelNavegacion);
            this.PanelInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelInferior.Location = new System.Drawing.Point(0, 471);
            this.PanelInferior.Name = "PanelInferior";
            this.PanelInferior.Size = new System.Drawing.Size(921, 28);
            this.PanelInferior.TabIndex = 70;
            // 
            // BubbleBarUsuario
            // 
            this.BubbleBarUsuario.Alignment = DevComponents.DotNetBar.eBubbleButtonAlignment.Bottom;
            this.BubbleBarUsuario.AntiAlias = true;
            // 
            // 
            // 
            this.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1;
            this.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3;
            this.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3;
            this.BubbleBarUsuario.Dock = System.Windows.Forms.DockStyle.Right;
            this.BubbleBarUsuario.ImageSizeNormal = new System.Drawing.Size(24, 24);
            this.BubbleBarUsuario.Location = new System.Drawing.Point(729, 0);
            this.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight;
            this.BubbleBarUsuario.Name = "BubbleBarUsuario";
            this.BubbleBarUsuario.SelectedTab = this.BubbleBarTabUsuario;
            this.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black;
            this.BubbleBarUsuario.Size = new System.Drawing.Size(50, 28);
            this.BubbleBarUsuario.TabIndex = 27;
            this.BubbleBarUsuario.Tabs.Add(this.BubbleBarTabUsuario);
            this.BubbleBarUsuario.TabsVisible = false;
            this.BubbleBarUsuario.Text = "BubbleBar5";
            // 
            // BubbleBarTabUsuario
            // 
            this.BubbleBarTabUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.BubbleBarTabUsuario.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
            this.BubbleBarTabUsuario.DarkBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.BubbleBarTabUsuario.LightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BubbleBarTabUsuario.Name = "BubbleBarTabUsuario";
            this.BubbleBarTabUsuario.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Blue;
            this.BubbleBarTabUsuario.Text = "BubbleBarTab3";
            this.BubbleBarTabUsuario.TextColor = System.Drawing.Color.Black;
            // 
            // TxtNombreUsu
            // 
            this.TxtNombreUsu.Dock = System.Windows.Forms.DockStyle.Right;
            this.TxtNombreUsu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNombreUsu.Location = new System.Drawing.Point(779, 0);
            this.TxtNombreUsu.Multiline = true;
            this.TxtNombreUsu.Name = "TxtNombreUsu";
            this.TxtNombreUsu.Size = new System.Drawing.Size(142, 28);
            this.TxtNombreUsu.TabIndex = 25;
            this.TxtNombreUsu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PanelNavegacion
            // 
            this.PanelNavegacion.Controls.Add(this.bunifuCustomLabel1);
            this.PanelNavegacion.Controls.Add(this.LblPaginacion);
            this.PanelNavegacion.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelNavegacion.Location = new System.Drawing.Point(0, 0);
            this.PanelNavegacion.Name = "PanelNavegacion";
            this.PanelNavegacion.Size = new System.Drawing.Size(318, 28);
            this.PanelNavegacion.TabIndex = 26;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(3, 6);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(115, 15);
            this.bunifuCustomLabel1.TabIndex = 0;
            this.bunifuCustomLabel1.Text = "Cantidad de filas";
            // 
            // LblPaginacion
            // 
            this.LblPaginacion.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblPaginacion.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPaginacion.ForeColor = System.Drawing.Color.White;
            this.LblPaginacion.Location = new System.Drawing.Point(202, 0);
            this.LblPaginacion.Name = "LblPaginacion";
            this.LblPaginacion.Size = new System.Drawing.Size(116, 28);
            this.LblPaginacion.TabIndex = 24;
            this.LblPaginacion.Text = "0/0";
            this.LblPaginacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GPanel_Criterio
            // 
            this.GPanel_Criterio.BackColor = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.GPanel_Criterio.DisabledBackColor = System.Drawing.Color.Empty;
            this.GPanel_Criterio.Dock = System.Windows.Forms.DockStyle.Left;
            this.GPanel_Criterio.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GPanel_Criterio.Location = new System.Drawing.Point(0, 135);
            this.GPanel_Criterio.Name = "GPanel_Criterio";
            this.GPanel_Criterio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GPanel_Criterio.Size = new System.Drawing.Size(346, 336);
            // 
            // 
            // 
            this.GPanel_Criterio.Style.BackColor = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.Style.BackColorGradientAngle = 90;
            this.GPanel_Criterio.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderBottomWidth = 1;
            this.GPanel_Criterio.Style.BorderColor = System.Drawing.Color.Transparent;
            this.GPanel_Criterio.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderLeftWidth = 1;
            this.GPanel_Criterio.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderRightWidth = 1;
            this.GPanel_Criterio.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.GPanel_Criterio.Style.BorderTopWidth = 1;
            this.GPanel_Criterio.Style.CornerDiameter = 4;
            this.GPanel_Criterio.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.GPanel_Criterio.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.GPanel_Criterio.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.GPanel_Criterio.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.GPanel_Criterio.TabIndex = 352;
            this.GPanel_Criterio.Text = "Filtro de búsqueda";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.LabelMode = MetroFramework.Controls.MetroLabelMode.Selectable;
            this.metroLabel1.Location = new System.Drawing.Point(0, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(100, 23);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Cantidad de filas";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.panel1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel1.Location = new System.Drawing.Point(346, 135);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupPanel1.Size = new System.Drawing.Size(575, 336);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColor = System.Drawing.Color.Transparent;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 353;
            this.groupPanel1.Text = "Reporte";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 308);
            this.panel1.TabIndex = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.White;
            this.PanelMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelMenu.BackgroundImage")));
            this.PanelMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelMenu.Controls.Add(this.BtnExportar);
            this.PanelMenu.Controls.Add(this.BtnGenerar);
            this.PanelMenu.Controls.Add(this.BtnAtras);
            this.PanelMenu.Controls.Add(this.reflectionLabelLogo);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelMenu.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(115)))));
            this.PanelMenu.GradientBottomRight = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
            this.PanelMenu.GradientTopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(136)))), ((int)(((byte)(209)))));
            this.PanelMenu.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(136)))), ((int)(((byte)(209)))));
            this.PanelMenu.Location = new System.Drawing.Point(0, 27);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Quality = 10;
            this.PanelMenu.Size = new System.Drawing.Size(921, 72);
            this.PanelMenu.TabIndex = 4;
            // 
            // BtnExportar
            // 
            this.BtnExportar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnExportar.BackColor = System.Drawing.Color.Transparent;
            this.BtnExportar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.BtnExportar.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnExportar.Enabled = false;
            this.BtnExportar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExportar.Image = global::MODEL.Properties.Resources.EXCEL;
            this.BtnExportar.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.BtnExportar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.BtnExportar.Location = new System.Drawing.Point(144, 0);
            this.BtnExportar.Name = "BtnExportar";
            this.BtnExportar.Size = new System.Drawing.Size(78, 72);
            this.BtnExportar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnExportar.TabIndex = 76;
            this.BtnExportar.Text = "EXPORTAR";
            this.BtnExportar.TextColor = System.Drawing.Color.White;
            this.BtnExportar.Visible = false;
            // 
            // BtnGenerar
            // 
            this.BtnGenerar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnGenerar.BackColor = System.Drawing.Color.Transparent;
            this.BtnGenerar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.BtnGenerar.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnGenerar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGenerar.Image = global::MODEL.Properties.Resources.PRINT;
            this.BtnGenerar.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.BtnGenerar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.BtnGenerar.Location = new System.Drawing.Point(72, 0);
            this.BtnGenerar.Name = "BtnGenerar";
            this.BtnGenerar.Size = new System.Drawing.Size(72, 72);
            this.BtnGenerar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnGenerar.TabIndex = 75;
            this.BtnGenerar.Text = "GENERAR";
            this.BtnGenerar.TextColor = System.Drawing.Color.White;
            // 
            // BtnAtras
            // 
            this.BtnAtras.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnAtras.BackColor = System.Drawing.Color.Transparent;
            this.BtnAtras.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.BtnAtras.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnAtras.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAtras.Image = global::MODEL.Properties.Resources.atras;
            this.BtnAtras.ImageFixedSize = new System.Drawing.Size(48, 48);
            this.BtnAtras.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.BtnAtras.Location = new System.Drawing.Point(0, 0);
            this.BtnAtras.Name = "BtnAtras";
            this.BtnAtras.Size = new System.Drawing.Size(72, 72);
            this.BtnAtras.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnAtras.TabIndex = 74;
            this.BtnAtras.Text = "SALIR";
            this.BtnAtras.TextColor = System.Drawing.Color.White;
            this.BtnAtras.Click += new System.EventHandler(this.BtnAtras_Click);
            // 
            // reflectionLabelLogo
            // 
            this.reflectionLabelLogo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.reflectionLabelLogo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionLabelLogo.Dock = System.Windows.Forms.DockStyle.Right;
            this.reflectionLabelLogo.Location = new System.Drawing.Point(776, 0);
            this.reflectionLabelLogo.Name = "reflectionLabelLogo";
            this.reflectionLabelLogo.Size = new System.Drawing.Size(145, 72);
            this.reflectionLabelLogo.TabIndex = 70;
            this.reflectionLabelLogo.Text = "<b><font size=\"+20\">RO<font color=\"#B02B2C\">LON</font></font></b>";
            // 
            // ModeloF2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(921, 499);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.GPanel_Criterio);
            this.Controls.Add(this.PanelInferior);
            this.Controls.Add(this.LblSubtitulo);
            this.Controls.Add(this.PanelMenu);
            this.Controls.Add(this.PanelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ModeloF2";
            this.Text = "ModeloF2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ModeloF2_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModeloF2_MouseMove);
            this.PanelSuperior.ResumeLayout(false);
            this.PanelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.PanelInferior.ResumeLayout(false);
            this.PanelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BubbleBarUsuario)).EndInit();
            this.PanelNavegacion.ResumeLayout(false);
            this.PanelNavegacion.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.PanelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel PanelSuperior;
        protected System.Windows.Forms.PictureBox PictureBox1;
        protected Bunifu.Framework.UI.BunifuImageButton btnMin;
        protected Bunifu.Framework.UI.BunifuImageButton btnMax;
        protected Bunifu.Framework.UI.BunifuImageButton btnClose;
        protected System.Windows.Forms.Label LblTitulo;
        protected Bunifu.Framework.UI.BunifuGradientPanel PanelMenu;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabelLogo;
        protected System.Windows.Forms.Label LblSubtitulo;
        public System.Windows.Forms.Panel PanelInferior;
        protected DevComponents.DotNetBar.BubbleBar BubbleBarUsuario;
        protected DevComponents.DotNetBar.BubbleBarTab BubbleBarTabUsuario;
        protected System.Windows.Forms.TextBox TxtNombreUsu;
        private System.Windows.Forms.Panel PanelNavegacion;
        protected DevComponents.DotNetBar.ButtonX BtnExportar;
        protected DevComponents.DotNetBar.ButtonX BtnGenerar;
        protected DevComponents.DotNetBar.ButtonX BtnAtras;
        protected System.Windows.Forms.Label LblPaginacion;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        public DevComponents.DotNetBar.Controls.GroupPanel GPanel_Criterio;
        public DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        public System.Windows.Forms.Panel panel1;
    }
}