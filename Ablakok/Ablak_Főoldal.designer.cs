using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace VédőEszköz
{
    public partial class Ablak_Főoldal : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Főoldal));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.ProgramAdatokMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.ablakokBeállításaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gombokBeállításaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HibanaploMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.felhasználókLétrehozásaTörléseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jogosultságKiosztásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.működésiAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alapAdatokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Anyag = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Dolgozó = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Szervezet = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Járandóság = new System.Windows.Forms.ToolStripMenuItem();
            this.könyvelésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raktárakKözöttiKönyvelésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.készletSelejtezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BizonylatMenu = new System.Windows.Forms.ToolStripSeparator();
            this.BizonylatMenü = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DolgozóiKiadásMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lekérdezésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.területiIgényekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KiosztásiNyomtatványMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LekérdezésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dolgozóiLekérdezésekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Figyelmeztetés = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Menükinyitás = new System.Windows.Forms.Button();
            this.Verzió_Váltás = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Rejtett = new System.Windows.Forms.GroupBox();
            this.TároltVerzió = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Képkeret = new System.Windows.Forms.PictureBox();
            this.MenuStrip.SuspendLayout();
            this.Rejtett.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Képkeret)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgramAdatokMenü,
            this.alapAdatokToolStripMenuItem,
            this.könyvelésekToolStripMenuItem,
            this.lekérdezésekToolStripMenuItem,
            this.vedoToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MenuStrip.Size = new System.Drawing.Size(656, 28);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "MenuStrip1";
            this.MenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip_ItemClicked);
            this.MenuStrip.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MenuStrip_MouseDoubleClick);
            // 
            // ProgramAdatokMenü
            // 
            this.ProgramAdatokMenü.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ablakokBeállításaToolStripMenuItem,
            this.gombokBeállításaToolStripMenuItem,
            this.HibanaploMenu,
            this.toolStripSeparator37,
            this.felhasználókLétrehozásaTörléseToolStripMenuItem,
            this.jogosultságKiosztásToolStripMenuItem,
            this.toolStripSeparator38,
            this.működésiAdatokToolStripMenuItem});
            this.ProgramAdatokMenü.Name = "ProgramAdatokMenü";
            this.ProgramAdatokMenü.Size = new System.Drawing.Size(98, 24);
            this.ProgramAdatokMenü.Text = "Beállítások";
            // 
            // ablakokBeállításaToolStripMenuItem
            // 
            this.ablakokBeállításaToolStripMenuItem.Name = "ablakokBeállításaToolStripMenuItem";
            this.ablakokBeállításaToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.ablakokBeállításaToolStripMenuItem.Text = "Ablakok beállítása";
            this.ablakokBeállításaToolStripMenuItem.Click += new System.EventHandler(this.AblakokBeállításaToolStripMenuItem_Click);
            // 
            // gombokBeállításaToolStripMenuItem
            // 
            this.gombokBeállításaToolStripMenuItem.Name = "gombokBeállításaToolStripMenuItem";
            this.gombokBeállításaToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.gombokBeállításaToolStripMenuItem.Text = "Gombok beállítása";
            this.gombokBeállításaToolStripMenuItem.Click += new System.EventHandler(this.GombokBeállításaToolStripMenuItem_Click);
            // 
            // HibanaploMenu
            // 
            this.HibanaploMenu.Name = "HibanaploMenu";
            this.HibanaploMenu.Size = new System.Drawing.Size(311, 24);
            this.HibanaploMenu.Text = "Hibanapló";
            this.HibanaploMenu.Click += new System.EventHandler(this.HibanaploMenu_Click);
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new System.Drawing.Size(308, 6);
            // 
            // felhasználókLétrehozásaTörléseToolStripMenuItem
            // 
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Name = "felhasználókLétrehozásaTörléseToolStripMenuItem";
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Text = "Felhasználók létrehozása törlése";
            this.felhasználókLétrehozásaTörléseToolStripMenuItem.Click += new System.EventHandler(this.FelhasználókLétrehozásaTörléseToolStripMenuItem_Click);
            // 
            // jogosultságKiosztásToolStripMenuItem
            // 
            this.jogosultságKiosztásToolStripMenuItem.Name = "jogosultságKiosztásToolStripMenuItem";
            this.jogosultságKiosztásToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.jogosultságKiosztásToolStripMenuItem.Text = "Jogosultság kiosztás";
            this.jogosultságKiosztásToolStripMenuItem.Click += new System.EventHandler(this.JogosultságKiosztásToolStripMenuItem_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(308, 6);
            // 
            // működésiAdatokToolStripMenuItem
            // 
            this.működésiAdatokToolStripMenuItem.Name = "működésiAdatokToolStripMenuItem";
            this.működésiAdatokToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.működésiAdatokToolStripMenuItem.Text = "&Működési Adatok";
            // 
            // alapAdatokToolStripMenuItem
            // 
            this.alapAdatokToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Anyag,
            this.ToolStripMenuItem_Dolgozó,
            this.ToolStripMenuItem_Szervezet,
            this.ToolStripMenuItem_Járandóság});
            this.alapAdatokToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.alapAdatokToolStripMenuItem.Name = "alapAdatokToolStripMenuItem";
            this.alapAdatokToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.alapAdatokToolStripMenuItem.Text = "&Alap adatok";
            // 
            // ToolStripMenuItem_Anyag
            // 
            this.ToolStripMenuItem_Anyag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItem_Anyag.Name = "ToolStripMenuItem_Anyag";
            this.ToolStripMenuItem_Anyag.Size = new System.Drawing.Size(205, 24);
            this.ToolStripMenuItem_Anyag.Text = "Anyag törzs";
            // 
            // ToolStripMenuItem_Dolgozó
            // 
            this.ToolStripMenuItem_Dolgozó.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItem_Dolgozó.Name = "ToolStripMenuItem_Dolgozó";
            this.ToolStripMenuItem_Dolgozó.Size = new System.Drawing.Size(205, 24);
            this.ToolStripMenuItem_Dolgozó.Text = "Dolgozó Törzs";
            this.ToolStripMenuItem_Dolgozó.Click += new System.EventHandler(this.ToolStripMenuItem_Dolgozó_Click);
            // 
            // ToolStripMenuItem_Szervezet
            // 
            this.ToolStripMenuItem_Szervezet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItem_Szervezet.Name = "ToolStripMenuItem_Szervezet";
            this.ToolStripMenuItem_Szervezet.Size = new System.Drawing.Size(205, 24);
            this.ToolStripMenuItem_Szervezet.Text = "Szervezet Törzs";
            this.ToolStripMenuItem_Szervezet.Click += new System.EventHandler(this.ToolStripMenuItem_Szervezet_Click);
            // 
            // ToolStripMenuItem_Járandóság
            // 
            this.ToolStripMenuItem_Járandóság.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItem_Járandóság.Name = "ToolStripMenuItem_Járandóság";
            this.ToolStripMenuItem_Járandóság.Size = new System.Drawing.Size(205, 24);
            this.ToolStripMenuItem_Járandóság.Text = "Járandóság Törzs";
            this.ToolStripMenuItem_Járandóság.Click += new System.EventHandler(this.ToolStripMenuItem_Járandóság_Click);
            // 
            // könyvelésekToolStripMenuItem
            // 
            this.könyvelésekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.raktárakKözöttiKönyvelésToolStripMenuItem,
            this.készletSelejtezésToolStripMenuItem,
            this.BizonylatMenu,
            this.BizonylatMenü,
            this.toolStripSeparator3,
            this.DolgozóiKiadásMenuItem});
            this.könyvelésekToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.könyvelésekToolStripMenuItem.Name = "könyvelésekToolStripMenuItem";
            this.könyvelésekToolStripMenuItem.Size = new System.Drawing.Size(109, 24);
            this.könyvelésekToolStripMenuItem.Text = "&Könyvelések";
            // 
            // raktárakKözöttiKönyvelésToolStripMenuItem
            // 
            this.raktárakKözöttiKönyvelésToolStripMenuItem.Name = "raktárakKözöttiKönyvelésToolStripMenuItem";
            this.raktárakKözöttiKönyvelésToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.raktárakKözöttiKönyvelésToolStripMenuItem.Text = "&Raktárak közötti könyvelés";
            // 
            // készletSelejtezésToolStripMenuItem
            // 
            this.készletSelejtezésToolStripMenuItem.Name = "készletSelejtezésToolStripMenuItem";
            this.készletSelejtezésToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.készletSelejtezésToolStripMenuItem.Text = "Készlet selejtezés";
            // 
            // BizonylatMenu
            // 
            this.BizonylatMenu.Name = "BizonylatMenu";
            this.BizonylatMenu.Size = new System.Drawing.Size(264, 6);
            // 
            // BizonylatMenü
            // 
            this.BizonylatMenü.Name = "BizonylatMenü";
            this.BizonylatMenü.Size = new System.Drawing.Size(267, 24);
            this.BizonylatMenü.Text = "Bizonylat nyomtatás";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(264, 6);
            // 
            // DolgozóiKiadásMenuItem
            // 
            this.DolgozóiKiadásMenuItem.Name = "DolgozóiKiadásMenuItem";
            this.DolgozóiKiadásMenuItem.Size = new System.Drawing.Size(267, 24);
            this.DolgozóiKiadásMenuItem.Text = "&Dolgozói Kiadás";
            // 
            // lekérdezésekToolStripMenuItem
            // 
            this.lekérdezésekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.területiIgényekToolStripMenuItem,
            this.KiosztásiNyomtatványMenuItem,
            this.LekérdezésToolStripMenuItem,
            this.dolgozóiLekérdezésekToolStripMenuItem});
            this.lekérdezésekToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lekérdezésekToolStripMenuItem.Name = "lekérdezésekToolStripMenuItem";
            this.lekérdezésekToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.lekérdezésekToolStripMenuItem.Text = "&Lekérdezések";
            // 
            // területiIgényekToolStripMenuItem
            // 
            this.területiIgényekToolStripMenuItem.Name = "területiIgényekToolStripMenuItem";
            this.területiIgényekToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.területiIgényekToolStripMenuItem.Text = "Területi igények";
            // 
            // KiosztásiNyomtatványMenuItem
            // 
            this.KiosztásiNyomtatványMenuItem.Name = "KiosztásiNyomtatványMenuItem";
            this.KiosztásiNyomtatványMenuItem.Size = new System.Drawing.Size(271, 24);
            this.KiosztásiNyomtatványMenuItem.Text = "Kiosztási Nyomtatvány";
            // 
            // LekérdezésToolStripMenuItem
            // 
            this.LekérdezésToolStripMenuItem.Name = "LekérdezésToolStripMenuItem";
            this.LekérdezésToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.LekérdezésToolStripMenuItem.Text = "Raktár készlet Lekérdezés ";
            // 
            // dolgozóiLekérdezésekToolStripMenuItem
            // 
            this.dolgozóiLekérdezésekToolStripMenuItem.Name = "dolgozóiLekérdezésekToolStripMenuItem";
            this.dolgozóiLekérdezésekToolStripMenuItem.Size = new System.Drawing.Size(271, 24);
            this.dolgozóiLekérdezésekToolStripMenuItem.Text = "Dolgozói Lekérdezések";
            // 
            // vedoToolStripMenuItem
            // 
            this.vedoToolStripMenuItem.Name = "vedoToolStripMenuItem";
            this.vedoToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.vedoToolStripMenuItem.Text = "Vedo";
            this.vedoToolStripMenuItem.Click += new System.EventHandler(this.VedoToolStripMenuItem_Click);
            // 
            // Figyelmeztetés
            // 
            this.Figyelmeztetés.BackColor = System.Drawing.Color.LightSalmon;
            this.Figyelmeztetés.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Figyelmeztetés.Location = new System.Drawing.Point(12, 41);
            this.Figyelmeztetés.Name = "Figyelmeztetés";
            this.Figyelmeztetés.Size = new System.Drawing.Size(136, 86);
            this.Figyelmeztetés.TabIndex = 25;
            this.Figyelmeztetés.Text = "A program karbantartás miatt ki fogja léptetni!";
            this.Figyelmeztetés.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Figyelmeztetés.Visible = false;
            // 
            // Menükinyitás
            // 
            this.Menükinyitás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Menükinyitás.Location = new System.Drawing.Point(409, 23);
            this.Menükinyitás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Menükinyitás.Name = "Menükinyitás";
            this.Menükinyitás.Size = new System.Drawing.Size(45, 45);
            this.Menükinyitás.TabIndex = 29;
            this.ToolTip1.SetToolTip(this.Menükinyitás, "Admin felület");
            this.Menükinyitás.UseVisualStyleBackColor = true;
            this.Menükinyitás.Click += new System.EventHandler(this.Menükinyitás_Click);
            // 
            // Verzió_Váltás
            // 
            this.Verzió_Váltás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Verzió_Váltás.Location = new System.Drawing.Point(338, 23);
            this.Verzió_Váltás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Verzió_Váltás.Name = "Verzió_Váltás";
            this.Verzió_Váltás.Size = new System.Drawing.Size(45, 45);
            this.Verzió_Váltás.TabIndex = 26;
            this.ToolTip1.SetToolTip(this.Verzió_Váltás, "Aktuális verziót állítja be a verzió számnak");
            this.Verzió_Váltás.UseVisualStyleBackColor = true;
            this.Verzió_Váltás.Click += new System.EventHandler(this.Verzió_Váltás_Click);
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Rejtett
            // 
            this.Rejtett.BackColor = System.Drawing.Color.Peru;
            this.Rejtett.Controls.Add(this.Menükinyitás);
            this.Rejtett.Controls.Add(this.TároltVerzió);
            this.Rejtett.Controls.Add(this.Label5);
            this.Rejtett.Controls.Add(this.Verzió_Váltás);
            this.Rejtett.Location = new System.Drawing.Point(13, 132);
            this.Rejtett.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Rejtett.Name = "Rejtett";
            this.Rejtett.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Rejtett.Size = new System.Drawing.Size(473, 84);
            this.Rejtett.TabIndex = 31;
            this.Rejtett.TabStop = false;
            this.Rejtett.Visible = false;
            // 
            // TároltVerzió
            // 
            this.TároltVerzió.Location = new System.Drawing.Point(160, 23);
            this.TároltVerzió.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TároltVerzió.Name = "TároltVerzió";
            this.TároltVerzió.Size = new System.Drawing.Size(148, 26);
            this.TároltVerzió.TabIndex = 28;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Label5.Location = new System.Drawing.Point(8, 29);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(144, 20);
            this.Label5.TabIndex = 27;
            this.Label5.Text = "Tárolt Verzió szám:";
            // 
            // Képkeret
            // 
            this.Képkeret.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Képkeret.Location = new System.Drawing.Point(0, 29);
            this.Képkeret.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Képkeret.Name = "Képkeret";
            this.Képkeret.Size = new System.Drawing.Size(656, 272);
            this.Képkeret.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Képkeret.TabIndex = 32;
            this.Képkeret.TabStop = false;
            // 
            // Ablak_Főoldal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(656, 299);
            this.Controls.Add(this.Rejtett);
            this.Controls.Add(this.Figyelmeztetés);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.Képkeret);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Főoldal";
            this.Text = "Villamos Nyilvántartások";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.A_Főoldal_FormClosing);
            this.Load += new System.EventHandler(this.Ablak_Főoldal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.A_Főoldal_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.A_Főoldal_KeyUp);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.Rejtett.ResumeLayout(false);
            this.Rejtett.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Képkeret)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal MenuStrip MenuStrip;
        internal ToolStripMenuItem ProgramAdatokMenü;
        internal Label Figyelmeztetés;
        internal ToolTip ToolTip1;
        internal Timer Timer1;
        private ToolStripMenuItem ablakokBeállításaToolStripMenuItem;
        private ToolStripMenuItem felhasználókLétrehozásaTörléseToolStripMenuItem;
        private ToolStripMenuItem gombokBeállításaToolStripMenuItem;
        private ToolStripMenuItem jogosultságKiosztásToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator37;
        private ToolStripSeparator toolStripSeparator38;
        private ToolStripMenuItem HibanaploMenu;
        internal GroupBox Rejtett;
        internal Button Menükinyitás;
        internal TextBox TároltVerzió;
        internal Label Label5;
        internal Button Verzió_Váltás;
        private ToolStripMenuItem működésiAdatokToolStripMenuItem;
        private ToolStripMenuItem alapAdatokToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItem_Anyag;
        private ToolStripMenuItem ToolStripMenuItem_Dolgozó;
        private ToolStripMenuItem ToolStripMenuItem_Szervezet;
        private ToolStripMenuItem ToolStripMenuItem_Járandóság;
        internal PictureBox Képkeret;
        private ToolStripMenuItem könyvelésekToolStripMenuItem;
        private ToolStripMenuItem raktárakKözöttiKönyvelésToolStripMenuItem;
        private ToolStripMenuItem készletSelejtezésToolStripMenuItem;
        private ToolStripSeparator BizonylatMenu;
        private ToolStripMenuItem BizonylatMenü;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem DolgozóiKiadásMenuItem;
        private ToolStripMenuItem lekérdezésekToolStripMenuItem;
        private ToolStripMenuItem területiIgényekToolStripMenuItem;
        private ToolStripMenuItem KiosztásiNyomtatványMenuItem;
        private ToolStripMenuItem LekérdezésToolStripMenuItem;
        private ToolStripMenuItem dolgozóiLekérdezésekToolStripMenuItem;
        private ToolStripMenuItem vedoToolStripMenuItem;
    }
}