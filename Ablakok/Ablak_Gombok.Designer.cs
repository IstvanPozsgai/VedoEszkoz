namespace VédőEszköz
{
    partial class Ablak_Gombok
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Gombok));
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtId = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Ablaknév = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.GombNév = new System.Windows.Forms.ComboBox();
            this.GombFelirat = new System.Windows.Forms.TextBox();
            this.Láthatóság = new System.Windows.Forms.CheckBox();
            this.Törölt = new System.Windows.Forms.CheckBox();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.Új_adat = new System.Windows.Forms.Button();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSugó = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSzervezet = new System.Windows.Forms.Label();
            this.ChkSzervezet = new System.Windows.Forms.CheckedListBox();
            this.SzervezetMinden = new System.Windows.Forms.Button();
            this.SzervezetSemmi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.Tábla.Location = new System.Drawing.Point(12, 273);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1486, 239);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 220;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Ablaknév, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.GombNév, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.GombFelirat, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Láthatóság, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Törölt, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(993, 255);
            this.tableLayoutPanel1.TabIndex = 221;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 20);
            this.label5.TabIndex = 220;
            this.label5.Text = "Id:";
            // 
            // TxtId
            // 
            this.TxtId.Enabled = false;
            this.TxtId.Location = new System.Drawing.Point(173, 3);
            this.TxtId.MaxLength = 10;
            this.TxtId.Name = "TxtId";
            this.TxtId.Size = new System.Drawing.Size(109, 26);
            this.TxtId.TabIndex = 219;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Location = new System.Drawing.Point(3, 35);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(82, 20);
            this.Label2.TabIndex = 212;
            this.Label2.Text = "Ablak név:";
            // 
            // Ablaknév
            // 
            this.Ablaknév.FormattingEnabled = true;
            this.Ablaknév.Location = new System.Drawing.Point(173, 38);
            this.Ablaknév.Name = "Ablaknév";
            this.Ablaknév.Size = new System.Drawing.Size(607, 28);
            this.Ablaknév.TabIndex = 228;
            this.Ablaknév.SelectionChangeCommitted += new System.EventHandler(this.Ablaknév_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(3, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 225;
            this.label1.Text = "Gomb Funkció Leírás:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Location = new System.Drawing.Point(3, 70);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(86, 20);
            this.Label4.TabIndex = 214;
            this.Label4.Text = "Gomb név:";
            // 
            // GombNév
            // 
            this.GombNév.FormattingEnabled = true;
            this.GombNév.Location = new System.Drawing.Point(173, 73);
            this.GombNév.Name = "GombNév";
            this.GombNév.Size = new System.Drawing.Size(607, 28);
            this.GombNév.TabIndex = 226;
            this.GombNév.SelectedIndexChanged += new System.EventHandler(this.GombNév_SelectedIndexChanged);
            // 
            // GombFelirat
            // 
            this.GombFelirat.Location = new System.Drawing.Point(173, 108);
            this.GombFelirat.MaxLength = 255;
            this.GombFelirat.Multiline = true;
            this.GombFelirat.Name = "GombFelirat";
            this.GombFelirat.Size = new System.Drawing.Size(805, 72);
            this.GombFelirat.TabIndex = 229;
            // 
            // Láthatóság
            // 
            this.Láthatóság.AutoSize = true;
            this.Láthatóság.BackColor = System.Drawing.Color.Lime;
            this.Láthatóság.Location = new System.Drawing.Point(173, 186);
            this.Láthatóság.Name = "Láthatóság";
            this.Láthatóság.Size = new System.Drawing.Size(109, 24);
            this.Láthatóság.TabIndex = 222;
            this.Láthatóság.Text = "Láthatóság";
            this.Láthatóság.UseVisualStyleBackColor = false;
            // 
            // Törölt
            // 
            this.Törölt.AutoSize = true;
            this.Törölt.BackColor = System.Drawing.Color.Lime;
            this.Törölt.Location = new System.Drawing.Point(173, 221);
            this.Törölt.Name = "Törölt";
            this.Törölt.Size = new System.Drawing.Size(68, 24);
            this.Törölt.TabIndex = 221;
            this.Törölt.Text = "Törölt";
            this.Törölt.UseVisualStyleBackColor = false;
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));

            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Rögzít.Location = new System.Drawing.Point(3, 190);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(44, 44);
            this.Alap_Rögzít.TabIndex = 206;
            this.toolTip1.SetToolTip(this.Alap_Rögzít, "Rögzítés");
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Alap_Rögzít_Click);
            // 
            // Új_adat
            // 
   
            this.Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_adat.Location = new System.Drawing.Point(3, 53);
            this.Új_adat.Name = "Új_adat";
            this.Új_adat.Size = new System.Drawing.Size(44, 44);
            this.Új_adat.TabIndex = 209;
            this.toolTip1.SetToolTip(this.Új_adat, "Új adat");
            this.Új_adat.UseVisualStyleBackColor = true;
            this.Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // BtnFrissít
            // 

            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFrissít.Location = new System.Drawing.Point(153, 103);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(45, 44);
            this.BtnFrissít.TabIndex = 215;
            this.toolTip1.SetToolTip(this.BtnFrissít, "Frissítés");
            this.BtnFrissít.UseVisualStyleBackColor = true;
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // BtnExcel
            // 
        
            this.BtnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcel.Location = new System.Drawing.Point(153, 53);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(45, 44);
            this.BtnExcel.TabIndex = 218;
            this.toolTip1.SetToolTip(this.BtnExcel, "Excel kimenet készítés");
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // BtnSugó
            // 
            this.BtnSugó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSugó.Location = new System.Drawing.Point(162, 3);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 44);
            this.BtnSugó.TabIndex = 219;
            this.toolTip1.SetToolTip(this.BtnSugó, "Online sugó megjelenítése");
            this.BtnSugó.UseVisualStyleBackColor = true;
            this.BtnSugó.Click += new System.EventHandler(this.BtnSugó_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.Controls.Add(this.BtnFrissít, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.Új_adat, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Alap_Rögzít, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.BtnExcel, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.BtnSugó, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1288, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(210, 237);
            this.tableLayoutPanel2.TabIndex = 222;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.ChkSzervezet, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1011, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(271, 237);
            this.tableLayoutPanel3.TabIndex = 229;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SzervezetMinden);
            this.panel1.Controls.Add(this.SzervezetSemmi);
            this.panel1.Controls.Add(this.lblSzervezet);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 44);
            this.panel1.TabIndex = 230;
            // 
            // lblSzervezet
            // 
            this.lblSzervezet.AutoSize = true;
            this.lblSzervezet.Location = new System.Drawing.Point(3, 7);
            this.lblSzervezet.Name = "lblSzervezet";
            this.lblSzervezet.Size = new System.Drawing.Size(80, 20);
            this.lblSzervezet.TabIndex = 225;
            this.lblSzervezet.Text = "Szervezet";
            // 
            // ChkSzervezet
            // 
            this.ChkSzervezet.CheckOnClick = true;
            this.ChkSzervezet.FormattingEnabled = true;
            this.ChkSzervezet.Location = new System.Drawing.Point(3, 53);
            this.ChkSzervezet.Name = "ChkSzervezet";
            this.ChkSzervezet.Size = new System.Drawing.Size(265, 172);
            this.ChkSzervezet.TabIndex = 226;
            // 
            // SzervezetMinden
            // 
            this.SzervezetMinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
    
            this.SzervezetMinden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetMinden.Location = new System.Drawing.Point(166, 0);
            this.SzervezetMinden.Name = "SzervezetMinden";
            this.SzervezetMinden.Size = new System.Drawing.Size(45, 44);
            this.SzervezetMinden.TabIndex = 227;
            this.toolTip1.SetToolTip(this.SzervezetMinden, "Minden kijeölése");
            this.SzervezetMinden.UseVisualStyleBackColor = true;
            this.SzervezetMinden.Click += new System.EventHandler(this.SzervezetMinden_Click);
            // 
            // SzervezetSemmi
            // 
            this.SzervezetSemmi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            this.SzervezetSemmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetSemmi.Location = new System.Drawing.Point(217, 0);
            this.SzervezetSemmi.Name = "SzervezetSemmi";
            this.SzervezetSemmi.Size = new System.Drawing.Size(45, 44);
            this.SzervezetSemmi.TabIndex = 226;
            this.toolTip1.SetToolTip(this.SzervezetSemmi, "Minden kijelölést megszüntet");
            this.SzervezetSemmi.UseVisualStyleBackColor = true;
            this.SzervezetSemmi.Click += new System.EventHandler(this.SzervezetSemmi_Click);
            // 
            // Ablak_Gombok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1510, 523);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Gombok";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gombok beállításai";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Anyagok_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Új_adat;
        internal System.Windows.Forms.Button Alap_Rögzít;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button BtnFrissít;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button BtnExcel;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.TextBox TxtId;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox Láthatóság;
        private System.Windows.Forms.ComboBox Ablaknév;
        private System.Windows.Forms.ComboBox GombNév;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox GombFelirat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        internal System.Windows.Forms.Label lblSzervezet;
        internal System.Windows.Forms.CheckedListBox ChkSzervezet;
        private System.Windows.Forms.CheckBox Törölt;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button SzervezetMinden;
        internal System.Windows.Forms.Button SzervezetSemmi;
        internal System.Windows.Forms.Button BtnSugó;
    }
}