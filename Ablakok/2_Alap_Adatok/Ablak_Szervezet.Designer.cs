namespace VédőEszköz
{
    partial class Ablak_Szervezet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Szervezet));
            this.Új_adat = new System.Windows.Forms.Button();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.KötésiOsztály = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Új_adat
            // 
            this.Új_adat.BackgroundImage = global::VédőEszköz.Properties.Resources.Ok_gyűjtemény;
            this.Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_adat.Location = new System.Drawing.Point(3, 3);
            this.Új_adat.Name = "Új_adat";
            this.Új_adat.Size = new System.Drawing.Size(44, 44);
            this.Új_adat.TabIndex = 209;
            this.toolTip1.SetToolTip(this.Új_adat, "Új elem/Kijelölt elem módosítása");
            this.Új_adat.UseVisualStyleBackColor = true;
            this.Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // BtnFrissít
            // 
            this.BtnFrissít.BackgroundImage = global::VédőEszköz.Properties.Resources.frissít_gyűjtemény;
            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFrissít.Location = new System.Drawing.Point(58, 3);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(45, 44);
            this.BtnFrissít.TabIndex = 215;
            this.toolTip1.SetToolTip(this.BtnFrissít, "Frissítés");
            this.BtnFrissít.UseVisualStyleBackColor = true;
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.BackgroundImage = global::VédőEszköz.Properties.Resources.Excel_gyűjtő;
            this.BtnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcel.Location = new System.Drawing.Point(113, 3);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(45, 44);
            this.BtnExcel.TabIndex = 218;
            this.toolTip1.SetToolTip(this.BtnExcel, "Excel kimenet készítés");
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(10, 80);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(933, 250);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 222;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.Controls.Add(this.Új_adat, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnFrissít, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnExcel, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(165, 62);
            this.tableLayoutPanel2.TabIndex = 224;
            // 
            // Ablak_Szervezet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(955, 342);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Szervezet";
            this.Text = "Szervezet Törzs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Szervezet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Új_adat;
        internal System.Windows.Forms.Button BtnFrissít;
        internal System.Windows.Forms.Button BtnExcel;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.BindingSource KötésiOsztály;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}