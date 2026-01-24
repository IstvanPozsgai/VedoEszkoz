using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VédőEszköz
{
    public partial class AblakBejelentkezés : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AblakBejelentkezés));
            this.lblVerzió = new System.Windows.Forms.Label();
            this.Btnlekérdezés = new System.Windows.Forms.Button();
            this.lblProgramnév = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnBelépés = new System.Windows.Forms.Button();
            this.BtnMégse = new System.Windows.Forms.Button();
            this.BtnJelszóMódosítás = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Label3 = new System.Windows.Forms.Label();
            this.CmbUserName = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.Súgó = new System.Windows.Forms.Button();
            this.Timer_kilép = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVerzió
            // 
            this.lblVerzió.BackColor = System.Drawing.Color.Bisque;
            this.lblVerzió.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVerzió.Location = new System.Drawing.Point(451, 30);
            this.lblVerzió.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerzió.Name = "lblVerzió";
            this.lblVerzió.Size = new System.Drawing.Size(163, 27);
            this.lblVerzió.TabIndex = 38;
            this.lblVerzió.Text = "Verzió: 20.04.19";
            this.lblVerzió.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btnlekérdezés
            // 
            this.Btnlekérdezés.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Btnlekérdezés.Location = new System.Drawing.Point(324, 5);
            this.Btnlekérdezés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnlekérdezés.Name = "Btnlekérdezés";
            this.Btnlekérdezés.Size = new System.Drawing.Size(152, 50);
            this.Btnlekérdezés.TabIndex = 1;
            this.Btnlekérdezés.Text = "Csak Lekérdezésre";
            this.Btnlekérdezés.UseVisualStyleBackColor = true;
            this.Btnlekérdezés.Click += new System.EventHandler(this.Btnlekérdezés_Click);
            // 
            // lblProgramnév
            // 
            this.lblProgramnév.BackColor = System.Drawing.Color.Bisque;
            this.lblProgramnév.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblProgramnév.Location = new System.Drawing.Point(224, 30);
            this.lblProgramnév.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgramnév.Name = "lblProgramnév";
            this.lblProgramnév.Size = new System.Drawing.Size(208, 27);
            this.lblProgramnév.TabIndex = 31;
            this.lblProgramnév.Text = "Villamos Nyilvántartások";
            this.lblProgramnév.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.Coral;
            this.GroupBox2.Controls.Add(this.pictureBox1);
            this.GroupBox2.Controls.Add(this.tableLayoutPanel2);
            this.GroupBox2.Controls.Add(this.tableLayoutPanel1);
            this.GroupBox2.Controls.Add(this.Súgó);
            this.GroupBox2.Controls.Add(this.lblVerzió);
            this.GroupBox2.Controls.Add(this.lblProgramnév);
            this.GroupBox2.Location = new System.Drawing.Point(13, 14);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox2.Size = new System.Drawing.Size(675, 242);
            this.GroupBox2.TabIndex = 40;
            this.GroupBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel2.Controls.Add(this.BtnBelépés, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnMégse, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Btnlekérdezés, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnJelszóMódosítás, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 166);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(649, 65);
            this.tableLayoutPanel2.TabIndex = 44;
            // 
            // BtnBelépés
            // 
            this.BtnBelépés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnBelépés.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnBelépés.Location = new System.Drawing.Point(484, 5);
            this.BtnBelépés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBelépés.Name = "BtnBelépés";
            this.BtnBelépés.Size = new System.Drawing.Size(152, 50);
            this.BtnBelépés.TabIndex = 2;
            this.BtnBelépés.Text = "Belépés";
            this.BtnBelépés.UseVisualStyleBackColor = true;
            this.BtnBelépés.Click += new System.EventHandler(this.BtnBelépés_Click);
            // 
            // BtnMégse
            // 
            this.BtnMégse.Location = new System.Drawing.Point(4, 5);
            this.BtnMégse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnMégse.Name = "BtnMégse";
            this.BtnMégse.Size = new System.Drawing.Size(152, 50);
            this.BtnMégse.TabIndex = 3;
            this.BtnMégse.Text = "Mégse";
            this.BtnMégse.UseVisualStyleBackColor = true;
            this.BtnMégse.Click += new System.EventHandler(this.BtnMégse_Click);
            // 
            // BtnJelszóMódosítás
            // 
            this.BtnJelszóMódosítás.Location = new System.Drawing.Point(164, 5);
            this.BtnJelszóMódosítás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnJelszóMódosítás.Name = "BtnJelszóMódosítás";
            this.BtnJelszóMódosítás.Size = new System.Drawing.Size(152, 50);
            this.BtnJelszóMódosítás.TabIndex = 4;
            this.BtnJelszóMódosítás.Text = "Jelszó Módosítás";
            this.BtnJelszóMódosítás.UseVisualStyleBackColor = true;
            this.BtnJelszóMódosítás.Click += new System.EventHandler(this.BtnJelszóMódosítás_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.53659F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.46341F));
            this.tableLayoutPanel1.Controls.Add(this.Label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmbUserName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtPassword, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(224, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(438, 90);
            this.tableLayoutPanel1.TabIndex = 43;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(4, 0);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(128, 20);
            this.Label3.TabIndex = 41;
            this.Label3.Text = "Felhasználó név:";
            // 
            // CmbUserName
            // 
            this.CmbUserName.DropDownHeight = 160;
            this.CmbUserName.DropDownWidth = 215;
            this.CmbUserName.FormattingEnabled = true;
            this.CmbUserName.IntegralHeight = false;
            this.CmbUserName.ItemHeight = 20;
            this.CmbUserName.Location = new System.Drawing.Point(150, 5);
            this.CmbUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbUserName.Name = "CmbUserName";
            this.CmbUserName.Size = new System.Drawing.Size(272, 28);
            this.CmbUserName.Sorted = true;
            this.CmbUserName.TabIndex = 0;
            this.CmbUserName.SelectedIndexChanged += new System.EventHandler(this.CmbUserName_SelectedIndexChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(4, 39);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(58, 20);
            this.Label4.TabIndex = 42;
            this.Label4.Text = "Jelszó:";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(150, 44);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(215, 26);
            this.TxtPassword.TabIndex = 1;
            this.TxtPassword.UseSystemPasswordChar = true;
            // 
            // Súgó
            // 
            this.Súgó.BackgroundImage = global::VédőEszköz.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(621, 17);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(40, 40);
            this.Súgó.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // Timer_kilép
            // 
            this.Timer_kilép.Interval = 10000;
            this.Timer_kilép.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // AblakBejelentkezés
            // 
            this.AcceptButton = this.BtnBelépés;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(700, 267);
            this.Controls.Add(this.GroupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AblakBejelentkezés";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bejelentkezés";
            this.Load += new System.EventHandler(this.AblakBejelentkezés_Load);
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
        internal Label lblVerzió;
        internal Label lblProgramnév;
        internal Button Btnlekérdezés;
        internal GroupBox GroupBox2;
        internal TextBox TxtPassword;
        internal Label Label4;
        internal Label Label3;
        internal Button BtnMégse;
        internal Button BtnJelszóMódosítás;
        private Button BtnBelépés;
        internal ComboBox CmbUserName;
        internal Button Súgó;
        internal Timer Timer_kilép;
        private ToolTip toolTip1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox pictureBox1;
    }
}