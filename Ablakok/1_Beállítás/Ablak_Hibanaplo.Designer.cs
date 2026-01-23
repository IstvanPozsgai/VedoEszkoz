namespace VédőEszköz
{
    partial class Ablak_Hibanaplo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Hibanaplo));
            this.Hibanaplo_Tablazat = new Zuby.ADGV.AdvancedDataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Részletek = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Hibanaplo_Tablazat
            // 
            this.Hibanaplo_Tablazat.AllowUserToAddRows = false;
            this.Hibanaplo_Tablazat.AllowUserToDeleteRows = false;
            this.Hibanaplo_Tablazat.AllowUserToOrderColumns = true;
            this.Hibanaplo_Tablazat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Hibanaplo_Tablazat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Hibanaplo_Tablazat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Hibanaplo_Tablazat.DefaultCellStyle = dataGridViewCellStyle2;
            this.Hibanaplo_Tablazat.FilterAndSortEnabled = true;
            this.Hibanaplo_Tablazat.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Hibanaplo_Tablazat.Location = new System.Drawing.Point(2, 50);
            this.Hibanaplo_Tablazat.Margin = new System.Windows.Forms.Padding(2);
            this.Hibanaplo_Tablazat.MaxFilterButtonImageHeight = 23;
            this.Hibanaplo_Tablazat.Name = "Hibanaplo_Tablazat";
            this.Hibanaplo_Tablazat.ReadOnly = true;
            this.Hibanaplo_Tablazat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Hibanaplo_Tablazat.RowHeadersWidth = 51;
            this.Hibanaplo_Tablazat.RowTemplate.Height = 24;
            this.Hibanaplo_Tablazat.Size = new System.Drawing.Size(754, 455);
            this.Hibanaplo_Tablazat.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Hibanaplo_Tablazat.TabIndex = 0;
            this.Hibanaplo_Tablazat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Hibanaplo_Tablazat_CellClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Részletek, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 45);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Részletek
            // 
            this.Részletek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));

            this.Részletek.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Részletek.Location = new System.Drawing.Point(3, 3);
            this.Részletek.Name = "Részletek";
            this.Részletek.Size = new System.Drawing.Size(40, 39);
            this.Részletek.TabIndex = 191;
            this.toolTip1.SetToolTip(this.Részletek, "Hiba részletei");
            this.Részletek.UseVisualStyleBackColor = true;
            this.Részletek.Click += new System.EventHandler(this.Részletek_Click);
            // 
            // Ablak_Hibanaplo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Hibanaplo_Tablazat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Ablak_Hibanaplo";
            this.Text = "Hibanapló";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Hibanaplo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView Hibanaplo_Tablazat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button Részletek;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}