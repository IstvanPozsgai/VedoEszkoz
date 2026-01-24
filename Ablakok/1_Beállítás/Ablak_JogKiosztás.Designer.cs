using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VédőEszköz
{

    public partial class Ablak_JogKiosztás : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_JogKiosztás));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Frissít = new System.Windows.Forms.Button();
            this.SzervezetMinden = new System.Windows.Forms.Button();
            this.SzervezetSemmi = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.BtnSugó = new System.Windows.Forms.Button();
            this.Btn_MindenMasol = new System.Windows.Forms.Button();
            this.JogTörlés = new System.Windows.Forms.Button();
            this.MindenGomb = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CmbGombId = new System.Windows.Forms.ComboBox();
            this.CmbGombok = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CmbAblakId = new System.Windows.Forms.ComboBox();
            this.CmbAblak = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LstChkSzervezet = new System.Windows.Forms.CheckedListBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Felhasználók = new System.Windows.Forms.ComboBox();
            this.DolgozóNév = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::VédőEszköz.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Frissít.Location = new System.Drawing.Point(7, 3);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(45, 45);
            this.Frissít.TabIndex = 223;
            this.ToolTip1.SetToolTip(this.Frissít, "Frissíti a táblázatot");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // SzervezetMinden
            // 
            this.SzervezetMinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SzervezetMinden.BackgroundImage = global::VédőEszköz.Properties.Resources.mndent_kijelöl;
            this.SzervezetMinden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetMinden.Location = new System.Drawing.Point(398, 9);
            this.SzervezetMinden.Name = "SzervezetMinden";
            this.SzervezetMinden.Size = new System.Drawing.Size(45, 44);
            this.SzervezetMinden.TabIndex = 104;
            this.ToolTip1.SetToolTip(this.SzervezetMinden, "Minden kijeölése");
            this.SzervezetMinden.UseVisualStyleBackColor = true;
            this.SzervezetMinden.Click += new System.EventHandler(this.SzervezetMinden_Click);
            // 
            // SzervezetSemmi
            // 
            this.SzervezetSemmi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SzervezetSemmi.BackgroundImage = global::VédőEszköz.Properties.Resources.üres_lista;
            this.SzervezetSemmi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SzervezetSemmi.Location = new System.Drawing.Point(449, 9);
            this.SzervezetSemmi.Name = "SzervezetSemmi";
            this.SzervezetSemmi.Size = new System.Drawing.Size(45, 44);
            this.SzervezetSemmi.TabIndex = 103;
            this.ToolTip1.SetToolTip(this.SzervezetSemmi, "Minden kijelölést megszüntet");
            this.SzervezetSemmi.UseVisualStyleBackColor = true;
            this.SzervezetSemmi.Click += new System.EventHandler(this.SzervezetSemmi_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Rögzít.Location = new System.Drawing.Point(7, 196);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.Rögzít, "Rögzíti az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // BtnSugó
            // 
            this.BtnSugó.BackgroundImage = global::VédőEszköz.Properties.Resources.Help_Support;
            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSugó.Location = new System.Drawing.Point(1497, 3);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 45);
            this.BtnSugó.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnSugó, "Online sugó megjelenítése");
            this.BtnSugó.UseVisualStyleBackColor = true;
            // 
            // Btn_MindenMasol
            // 
            this.Btn_MindenMasol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_MindenMasol.Location = new System.Drawing.Point(7, 54);
            this.Btn_MindenMasol.Name = "Btn_MindenMasol";
            this.Btn_MindenMasol.Size = new System.Drawing.Size(45, 45);
            this.Btn_MindenMasol.TabIndex = 224;
            this.ToolTip1.SetToolTip(this.Btn_MindenMasol, "Rögzíti az adatokat");
            this.Btn_MindenMasol.UseVisualStyleBackColor = true;
            this.Btn_MindenMasol.Click += new System.EventHandler(this.Btn_MindenMasol_Click);
            // 
            // JogTörlés
            // 
            this.JogTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.JogTörlés.Location = new System.Drawing.Point(5, 100);
            this.JogTörlés.Name = "JogTörlés";
            this.JogTörlés.Size = new System.Drawing.Size(45, 45);
            this.JogTörlés.TabIndex = 225;
            this.ToolTip1.SetToolTip(this.JogTörlés, "A felhasználó összs jogosultságának törlése");
            this.JogTörlés.UseVisualStyleBackColor = true;
            this.JogTörlés.Click += new System.EventHandler(this.JogTörlés_Click);
            // 
            // MindenGomb
            // 
            this.MindenGomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MindenGomb.Location = new System.Drawing.Point(3, 3);
            this.MindenGomb.Name = "MindenGomb";
            this.MindenGomb.Size = new System.Drawing.Size(45, 45);
            this.MindenGomb.TabIndex = 226;
            this.ToolTip1.SetToolTip(this.MindenGomb, "Az ablakhoz tartozó összes gomb jogosultság beállítása");
            this.MindenGomb.UseVisualStyleBackColor = true;
            this.MindenGomb.Click += new System.EventHandler(this.MindenGomb_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 20);
            this.Label1.TabIndex = 87;
            this.Label1.Text = "Felhasználónév:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LstChkSzervezet, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.BtnSugó, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1555, 315);
            this.tableLayoutPanel1.TabIndex = 99;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.CmbGombId);
            this.panel3.Controls.Add(this.CmbGombok);
            this.panel3.Location = new System.Drawing.Point(303, 68);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(688, 244);
            this.panel3.TabIndex = 223;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.MindenGomb);
            this.panel4.Location = new System.Drawing.Point(237, 170);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(447, 65);
            this.panel4.TabIndex = 230;
            // 
            // CmbGombId
            // 
            this.CmbGombId.FormattingEnabled = true;
            this.CmbGombId.Location = new System.Drawing.Point(4, 37);
            this.CmbGombId.Name = "CmbGombId";
            this.CmbGombId.Size = new System.Drawing.Size(121, 28);
            this.CmbGombId.TabIndex = 229;
            this.CmbGombId.SelectionChangeCommitted += new System.EventHandler(this.CmbGombId_SelectionChangeCommitted);
            // 
            // CmbGombok
            // 
            this.CmbGombok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbGombok.FormattingEnabled = true;
            this.CmbGombok.Location = new System.Drawing.Point(3, 3);
            this.CmbGombok.Name = "CmbGombok";
            this.CmbGombok.Size = new System.Drawing.Size(681, 28);
            this.CmbGombok.Sorted = true;
            this.CmbGombok.TabIndex = 227;
            this.CmbGombok.SelectionChangeCommitted += new System.EventHandler(this.CmbGombok_SelectionChangeCommitted);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CmbAblakId);
            this.panel2.Controls.Add(this.CmbAblak);
            this.panel2.Location = new System.Drawing.Point(3, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(294, 244);
            this.panel2.TabIndex = 223;
            // 
            // CmbAblakId
            // 
            this.CmbAblakId.FormattingEnabled = true;
            this.CmbAblakId.Location = new System.Drawing.Point(4, 37);
            this.CmbAblakId.Name = "CmbAblakId";
            this.CmbAblakId.Size = new System.Drawing.Size(121, 28);
            this.CmbAblakId.TabIndex = 103;
            this.CmbAblakId.SelectionChangeCommitted += new System.EventHandler(this.CmbAblakId_SelectionChangeCommitted);
            // 
            // CmbAblak
            // 
            this.CmbAblak.FormattingEnabled = true;
            this.CmbAblak.Location = new System.Drawing.Point(3, 3);
            this.CmbAblak.Name = "CmbAblak";
            this.CmbAblak.Size = new System.Drawing.Size(288, 28);
            this.CmbAblak.Sorted = true;
            this.CmbAblak.TabIndex = 101;
            this.CmbAblak.SelectionChangeCommitted += new System.EventHandler(this.CmbAblak_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.JogTörlés);
            this.panel1.Controls.Add(this.Btn_MindenMasol);
            this.panel1.Controls.Add(this.Frissít);
            this.panel1.Controls.Add(this.Rögzít);
            this.panel1.Location = new System.Drawing.Point(1497, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(55, 244);
            this.panel1.TabIndex = 225;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 228;
            this.label2.Text = "Gombok";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SzervezetMinden);
            this.groupBox2.Controls.Add(this.SzervezetSemmi);
            this.groupBox2.Location = new System.Drawing.Point(997, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 59);
            this.groupBox2.TabIndex = 226;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Szervezet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 99;
            this.label4.Text = "Ablak";
            // 
            // LstChkSzervezet
            // 
            this.LstChkSzervezet.CheckOnClick = true;
            this.LstChkSzervezet.FormattingEnabled = true;
            this.LstChkSzervezet.Location = new System.Drawing.Point(997, 68);
            this.LstChkSzervezet.Name = "LstChkSzervezet";
            this.LstChkSzervezet.Size = new System.Drawing.Size(494, 235);
            this.LstChkSzervezet.TabIndex = 98;
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
            this.Tábla.Location = new System.Drawing.Point(12, 359);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1555, 124);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 221;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Felhasználók
            // 
            this.Felhasználók.FormattingEnabled = true;
            this.Felhasználók.Location = new System.Drawing.Point(142, 6);
            this.Felhasználók.Name = "Felhasználók";
            this.Felhasználók.Size = new System.Drawing.Size(223, 28);
            this.Felhasználók.TabIndex = 103;
            this.Felhasználók.SelectionChangeCommitted += new System.EventHandler(this.Felhasználók_SelectionChangeCommitted);
            // 
            // DolgozóNév
            // 
            this.DolgozóNév.AutoSize = true;
            this.DolgozóNév.Location = new System.Drawing.Point(371, 12);
            this.DolgozóNév.Name = "DolgozóNév";
            this.DolgozóNév.Size = new System.Drawing.Size(49, 20);
            this.DolgozóNév.TabIndex = 222;
            this.DolgozóNév.Text = "<< >>";
            // 
            // Ablak_JogKiosztás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1574, 495);
            this.Controls.Add(this.DolgozóNév);
            this.Controls.Add(this.Felhasználók);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_JogKiosztás";
            this.Text = "Felhasználók jogkiosztása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_JogKiosztás_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal ToolTip ToolTip1;
        internal Button BtnSugó;
        internal Button Rögzít;
        internal Label Label1;
        private TableLayoutPanel tableLayoutPanel1;
        internal Label label4;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private ComboBox Felhasználók;
        internal Label DolgozóNév;
        internal Button Frissít;
        private GroupBox groupBox2;
        internal Button SzervezetMinden;
        internal Button SzervezetSemmi;
        private ComboBox CmbAblak;
        private CheckedListBox LstChkSzervezet;
        private ComboBox CmbGombok;
        internal Button Btn_MindenMasol;
        internal Label label2;
        private Panel panel1;
        internal Button JogTörlés;
        private Panel panel3;
        private Panel panel2;
        private ComboBox CmbGombId;
        private ComboBox CmbAblakId;
        private Panel panel4;
        internal Button MindenGomb;
    }
}