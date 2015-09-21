namespace CompuLinERP.WIN
{
    partial class SalesPrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesPrice));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmbLoca = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.DGPhysical = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txtToItem = new System.Windows.Forms.TextBox();
            this.txtFromItem = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAllLocations = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkZeroValues = new System.Windows.Forms.CheckBox();
            this.cmdPOST = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cmbLocaTo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbLocaFrom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGPhysical)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.cmbLoca);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1078, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(160, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(260, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // cmbLoca
            // 
            this.cmbLoca.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLoca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoca.FormattingEnabled = true;
            this.cmbLoca.Location = new System.Drawing.Point(534, 27);
            this.cmbLoca.Name = "cmbLoca";
            this.cmbLoca.Size = new System.Drawing.Size(262, 21);
            this.cmbLoca.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(468, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.DGPhysical);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtToItem);
            this.groupBox2.Controls.Add(this.txtFromItem);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1078, 427);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(800, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "F2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(420, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "F2";
            // 
            // DGPhysical
            // 
            this.DGPhysical.AllowUserToResizeRows = false;
            this.DGPhysical.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGPhysical.Location = new System.Drawing.Point(22, 39);
            this.DGPhysical.Name = "DGPhysical";
            this.DGPhysical.Size = new System.Drawing.Size(1038, 382);
            this.DGPhysical.TabIndex = 5;
            this.DGPhysical.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGPhysical_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(816, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "<< Press Enter >>";
            // 
            // txtToItem
            // 
            this.txtToItem.Location = new System.Drawing.Point(534, 13);
            this.txtToItem.Name = "txtToItem";
            this.txtToItem.Size = new System.Drawing.Size(260, 20);
            this.txtToItem.TabIndex = 4;
            this.txtToItem.TextChanged += new System.EventHandler(this.txtToItem_TextChanged);
            this.txtToItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToItem_KeyDown);
            this.txtToItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToItem_KeyPress);
            // 
            // txtFromItem
            // 
            this.txtFromItem.Location = new System.Drawing.Point(160, 13);
            this.txtFromItem.Name = "txtFromItem";
            this.txtFromItem.Size = new System.Drawing.Size(260, 20);
            this.txtFromItem.TabIndex = 3;
            this.txtFromItem.TextChanged += new System.EventHandler(this.txtFromItem_TextChanged);
            this.txtFromItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromItem_KeyDown);
            this.txtFromItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromItem_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(445, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "To Item Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "From Item Code";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAllLocations);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.chkZeroValues);
            this.groupBox3.Controls.Add(this.cmdPOST);
            this.groupBox3.Location = new System.Drawing.Point(12, 529);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1078, 62);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // chkAllLocations
            // 
            this.chkAllLocations.AutoSize = true;
            this.chkAllLocations.Location = new System.Drawing.Point(22, 39);
            this.chkAllLocations.Name = "chkAllLocations";
            this.chkAllLocations.Size = new System.Drawing.Size(220, 17);
            this.chkAllLocations.TabIndex = 10;
            this.chkAllLocations.Text = "Update Sales Prices to ALL LOCATIONS";
            this.chkAllLocations.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(898, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Display - Price List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(756, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 24);
            this.button2.TabIndex = 8;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkZeroValues
            // 
            this.chkZeroValues.AutoSize = true;
            this.chkZeroValues.Location = new System.Drawing.Point(22, 18);
            this.chkZeroValues.Name = "chkZeroValues";
            this.chkZeroValues.Size = new System.Drawing.Size(121, 17);
            this.chkZeroValues.TabIndex = 6;
            this.chkZeroValues.Text = "Update Zero Values";
            this.chkZeroValues.UseVisualStyleBackColor = true;
            // 
            // cmdPOST
            // 
            this.cmdPOST.Location = new System.Drawing.Point(261, 19);
            this.cmdPOST.Name = "cmdPOST";
            this.cmdPOST.Size = new System.Drawing.Size(123, 24);
            this.cmdPOST.TabIndex = 7;
            this.cmdPOST.Text = "Update to Live System";
            this.cmdPOST.UseVisualStyleBackColor = true;
            this.cmdPOST.Click += new System.EventHandler(this.cmdPOST_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.cmbLocaTo);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cmbLocaFrom);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(28, 597);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(965, 52);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(814, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 24);
            this.button3.TabIndex = 8;
            this.button3.Text = "Transfer Now";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmbLocaTo
            // 
            this.cmbLocaTo.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLocaTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocaTo.FormattingEnabled = true;
            this.cmbLocaTo.Location = new System.Drawing.Point(558, 19);
            this.cmbLocaTo.Name = "cmbLocaTo";
            this.cmbLocaTo.Size = new System.Drawing.Size(222, 21);
            this.cmbLocaTo.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(478, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "location to ";
            // 
            // cmbLocaFrom
            // 
            this.cmbLocaFrom.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLocaFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocaFrom.FormattingEnabled = true;
            this.cmbLocaFrom.Location = new System.Drawing.Point(243, 19);
            this.cmbLocaFrom.Name = "cmbLocaFrom";
            this.cmbLocaFrom.Size = new System.Drawing.Size(229, 21);
            this.cmbLocaFrom.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transfer sales prices of  ALL ITEMS in";
            // 
            // SalesPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1104, 661);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SalesPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Price";
            this.Load += new System.EventHandler(this.StockAdjustments_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGPhysical)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cmbLoca;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtToItem;
        private System.Windows.Forms.TextBox txtFromItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DGPhysical;
        private System.Windows.Forms.CheckBox chkZeroValues;
        private System.Windows.Forms.Button cmdPOST;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkAllLocations;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbLocaTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbLocaFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
    }
}