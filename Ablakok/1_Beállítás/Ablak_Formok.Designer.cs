namespace VédőEszköz
{
    partial class Ablak_Formok
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.Új_adat = new System.Windows.Forms.Button();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.BtnSugó = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MenüFelirat = new System.Windows.Forms.ComboBox();
            this.Törölt = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.MenüNév = new System.Windows.Forms.ComboBox();
            this.Ablaknév = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Láthatóság = new System.Windows.Forms.CheckBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel2.Controls.Add(this.Alap_Rögzít, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.Új_adat, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.BtnExcel, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.BtnSugó, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(744, 7);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(210, 222);
            this.tableLayoutPanel2.TabIndex = 225;
            // 
            // BtnFrissít
            // 
            this.BtnFrissít.BackgroundImage = global::VédőEszköz.Properties.Resources.frissít_gyűjtemény;
            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFrissít.Location = new System.Drawing.Point(153, 103);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(45, 44);
            this.BtnFrissít.TabIndex = 215;
            this.toolTip1.SetToolTip(this.BtnFrissít, "Frissítés");
            this.BtnFrissít.UseVisualStyleBackColor = true;
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.BackgroundImage = global::VédőEszköz.Properties.Resources.Ok_gyűjtemény;
            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_Rögzít.Location = new System.Drawing.Point(3, 153);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(44, 44);
            this.Alap_Rögzít.TabIndex = 206;
            this.toolTip1.SetToolTip(this.Alap_Rögzít, "Rögzítés");
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Alap_Rögzít_Click);
            // 
            // Új_adat
            // 
            this.Új_adat.BackgroundImage = global::VédőEszköz.Properties.Resources.New_gyűjtemény;
            this.Új_adat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Új_adat.Location = new System.Drawing.Point(3, 53);
            this.Új_adat.Name = "Új_adat";
            this.Új_adat.Size = new System.Drawing.Size(44, 44);
            this.Új_adat.TabIndex = 209;
            this.toolTip1.SetToolTip(this.Új_adat, "Új adat");
            this.Új_adat.UseVisualStyleBackColor = true;
            this.Új_adat.Click += new System.EventHandler(this.Új_adat_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.BackgroundImage = global::VédőEszköz.Properties.Resources.Excel_gyűjtő;
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
            this.BtnSugó.BackgroundImage = global::VédőEszköz.Properties.Resources.Help_Support;
            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSugó.Location = new System.Drawing.Point(153, 3);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 44);
            this.BtnSugó.TabIndex = 219;
            this.toolTip1.SetToolTip(this.BtnSugó, "Excel kimenet készítés");
            this.BtnSugó.UseVisualStyleBackColor = true;
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
            this.TxtId.Location = new System.Drawing.Point(105, 3);
            this.TxtId.MaxLength = 10;
            this.TxtId.Name = "TxtId";
            this.TxtId.Size = new System.Drawing.Size(202, 26);
            this.TxtId.TabIndex = 219;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 225;
            this.label1.Text = "Menü felirat:";
            // 
            // MenüFelirat
            // 
            this.MenüFelirat.FormattingEnabled = true;
            this.MenüFelirat.Location = new System.Drawing.Point(105, 38);
            this.MenüFelirat.Name = "MenüFelirat";
            this.MenüFelirat.Size = new System.Drawing.Size(607, 28);
            this.MenüFelirat.TabIndex = 227;
            // 
            // Törölt
            // 
            this.Törölt.AutoSize = true;
            this.Törölt.BackColor = System.Drawing.Color.Lime;
            this.Törölt.Location = new System.Drawing.Point(105, 178);
            this.Törölt.Name = "Törölt";
            this.Törölt.Size = new System.Drawing.Size(68, 24);
            this.Törölt.TabIndex = 221;
            this.Törölt.Text = "Törölt";
            this.Törölt.UseVisualStyleBackColor = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Location = new System.Drawing.Point(3, 105);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(82, 20);
            this.Label2.TabIndex = 212;
            this.Label2.Text = "Ablak név:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Location = new System.Drawing.Point(3, 70);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(82, 20);
            this.Label4.TabIndex = 214;
            this.Label4.Text = "Menü név:";
            // 
            // MenüNév
            // 
            this.MenüNév.FormattingEnabled = true;
            this.MenüNév.Location = new System.Drawing.Point(105, 73);
            this.MenüNév.Name = "MenüNév";
            this.MenüNév.Size = new System.Drawing.Size(607, 28);
            this.MenüNév.TabIndex = 226;
            // 
            // Ablaknév
            // 
            this.Ablaknév.FormattingEnabled = true;
            this.Ablaknév.Location = new System.Drawing.Point(105, 108);
            this.Ablaknév.Name = "Ablaknév";
            this.Ablaknév.Size = new System.Drawing.Size(607, 28);
            this.Ablaknév.TabIndex = 228;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MenüFelirat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Törölt, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.MenüNév, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Ablaknév, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Láthatóság, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(726, 214);
            this.tableLayoutPanel1.TabIndex = 224;
            // 
            // Láthatóság
            // 
            this.Láthatóság.AutoSize = true;
            this.Láthatóság.BackColor = System.Drawing.Color.Lime;
            this.Láthatóság.Location = new System.Drawing.Point(105, 143);
            this.Láthatóság.Name = "Láthatóság";
            this.Láthatóság.Size = new System.Drawing.Size(109, 24);
            this.Láthatóság.TabIndex = 222;
            this.Láthatóság.Text = "Láthatóság";
            this.Láthatóság.UseVisualStyleBackColor = false;
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
            this.Tábla.Location = new System.Drawing.Point(12, 235);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(942, 280);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 223;
            // 
            // Ablak_Formok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 523);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Formok";
            this.Text = "Ablak_Formok";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        internal System.Windows.Forms.Button BtnExcel;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button BtnFrissít;
        internal System.Windows.Forms.Button Új_adat;
        internal System.Windows.Forms.Button Alap_Rögzít;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox TxtId;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox MenüFelirat;
        private System.Windows.Forms.CheckBox Törölt;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.ComboBox MenüNév;
        private System.Windows.Forms.ComboBox Ablaknév;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox Láthatóság;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.Button BtnSugó;
    }
}