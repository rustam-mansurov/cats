namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.кодРайонаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.названиеDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.лесистостьDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.широтаРайонаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.долготаРайонаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.узлыслеваDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.узлысправаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.районыBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.rayon1DataSet = new WindowsFormsApp1.Rayon1DataSet();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.кодУзлаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.широтаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.долготаDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.узлыBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.районыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.районыTableAdapter = new WindowsFormsApp1.Rayon1DataSetTableAdapters.РайоныTableAdapter();
            this.узлыBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.узлыTableAdapter = new WindowsFormsApp1.Rayon1DataSetTableAdapters.УзлыTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.районыBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rayon1DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.узлыBindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.районыBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.узлыBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gMapControl1
            // 
            this.gMapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(12, 12);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(945, 609);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.кодРайонаDataGridViewTextBoxColumn,
            this.названиеDataGridViewTextBoxColumn,
            this.лесистостьDataGridViewTextBoxColumn,
            this.широтаРайонаDataGridViewTextBoxColumn,
            this.долготаРайонаDataGridViewTextBoxColumn,
            this.узлыслеваDataGridViewTextBoxColumn,
            this.узлысправаDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.районыBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(657, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.Size = new System.Drawing.Size(300, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // кодРайонаDataGridViewTextBoxColumn
            // 
            this.кодРайонаDataGridViewTextBoxColumn.DataPropertyName = "Код района";
            this.кодРайонаDataGridViewTextBoxColumn.HeaderText = "Код района";
            this.кодРайонаDataGridViewTextBoxColumn.Name = "кодРайонаDataGridViewTextBoxColumn";
            // 
            // названиеDataGridViewTextBoxColumn
            // 
            this.названиеDataGridViewTextBoxColumn.DataPropertyName = "Название";
            this.названиеDataGridViewTextBoxColumn.HeaderText = "Название";
            this.названиеDataGridViewTextBoxColumn.Name = "названиеDataGridViewTextBoxColumn";
            // 
            // лесистостьDataGridViewTextBoxColumn
            // 
            this.лесистостьDataGridViewTextBoxColumn.DataPropertyName = "Лесистость, %";
            this.лесистостьDataGridViewTextBoxColumn.HeaderText = "Лесистость, %";
            this.лесистостьDataGridViewTextBoxColumn.Name = "лесистостьDataGridViewTextBoxColumn";
            // 
            // широтаРайонаDataGridViewTextBoxColumn
            // 
            this.широтаРайонаDataGridViewTextBoxColumn.DataPropertyName = "ШиротаРайона";
            this.широтаРайонаDataGridViewTextBoxColumn.HeaderText = "ШиротаРайона";
            this.широтаРайонаDataGridViewTextBoxColumn.Name = "широтаРайонаDataGridViewTextBoxColumn";
            // 
            // долготаРайонаDataGridViewTextBoxColumn
            // 
            this.долготаРайонаDataGridViewTextBoxColumn.DataPropertyName = "ДолготаРайона";
            this.долготаРайонаDataGridViewTextBoxColumn.HeaderText = "ДолготаРайона";
            this.долготаРайонаDataGridViewTextBoxColumn.Name = "долготаРайонаDataGridViewTextBoxColumn";
            // 
            // узлыслеваDataGridViewTextBoxColumn
            // 
            this.узлыслеваDataGridViewTextBoxColumn.DataPropertyName = "Узлыслева";
            this.узлыслеваDataGridViewTextBoxColumn.HeaderText = "Узлыслева";
            this.узлыслеваDataGridViewTextBoxColumn.Name = "узлыслеваDataGridViewTextBoxColumn";
            // 
            // узлысправаDataGridViewTextBoxColumn
            // 
            this.узлысправаDataGridViewTextBoxColumn.DataPropertyName = "Узлысправа";
            this.узлысправаDataGridViewTextBoxColumn.HeaderText = "Узлысправа";
            this.узлысправаDataGridViewTextBoxColumn.Name = "узлысправаDataGridViewTextBoxColumn";
            // 
            // районыBindingSource1
            // 
            this.районыBindingSource1.DataMember = "Районы";
            this.районыBindingSource1.DataSource = this.bindingSource1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = this.rayon1DataSet;
            this.bindingSource1.Position = 0;
            // 
            // rayon1DataSet
            // 
            this.rayon1DataSet.DataSetName = "Rayon1DataSet";
            this.rayon1DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView3
            // 
            this.dataGridView3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.кодУзлаDataGridViewTextBoxColumn,
            this.широтаDataGridViewTextBoxColumn,
            this.долготаDataGridViewTextBoxColumn});
            this.dataGridView3.DataSource = this.узлыBindingSource1;
            this.dataGridView3.Location = new System.Drawing.Point(657, 179);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 10;
            this.dataGridView3.Size = new System.Drawing.Size(300, 219);
            this.dataGridView3.TabIndex = 3;
            // 
            // кодУзлаDataGridViewTextBoxColumn
            // 
            this.кодУзлаDataGridViewTextBoxColumn.DataPropertyName = "Код узла";
            this.кодУзлаDataGridViewTextBoxColumn.HeaderText = "Код узла";
            this.кодУзлаDataGridViewTextBoxColumn.Name = "кодУзлаDataGridViewTextBoxColumn";
            // 
            // широтаDataGridViewTextBoxColumn
            // 
            this.широтаDataGridViewTextBoxColumn.DataPropertyName = "Широта";
            this.широтаDataGridViewTextBoxColumn.HeaderText = "Широта";
            this.широтаDataGridViewTextBoxColumn.Name = "широтаDataGridViewTextBoxColumn";
            // 
            // долготаDataGridViewTextBoxColumn
            // 
            this.долготаDataGridViewTextBoxColumn.DataPropertyName = "Долгота";
            this.долготаDataGridViewTextBoxColumn.HeaderText = "Долгота";
            this.долготаDataGridViewTextBoxColumn.Name = "долготаDataGridViewTextBoxColumn";
            // 
            // узлыBindingSource1
            // 
            this.узлыBindingSource1.DataMember = "Узлы";
            this.узлыBindingSource1.DataSource = this.bindingSource1;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(635, 554);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 38);
            this.button2.TabIndex = 7;
            this.button2.Text = "Маршрут";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox1.Location = new System.Drawing.Point(742, 541);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(77, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "55000000";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox2.Location = new System.Drawing.Point(843, 540);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(77, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "52000000";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox3.Location = new System.Drawing.Point(742, 569);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(77, 20);
            this.textBox3.TabIndex = 10;
            this.textBox3.Text = "56000000";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox4.Location = new System.Drawing.Point(843, 569);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(77, 20);
            this.textBox4.TabIndex = 11;
            this.textBox4.Text = "53000000";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Location = new System.Drawing.Point(134, 541);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 58);
            this.panel1.TabIndex = 12;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox1.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(469, 513);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 86);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите слои";
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(14, 27);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 23);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Подложка";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 23);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Цветограмма";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // районыBindingSource
            // 
            this.районыBindingSource.DataMember = "Районы";
            this.районыBindingSource.DataSource = this.bindingSource1;
            // 
            // районыTableAdapter
            // 
            this.районыTableAdapter.ClearBeforeFill = true;
            // 
            // узлыBindingSource
            // 
            this.узлыBindingSource.DataMember = "Узлы";
            this.узлыBindingSource.DataSource = this.bindingSource1;
            // 
            // узлыTableAdapter
            // 
            this.узлыTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 633);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gMapControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.районыBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rayon1DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.узлыBindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.районыBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.узлыBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Rayon1DataSet rayon1DataSet;
        private System.Windows.Forms.BindingSource районыBindingSource;
        private Rayon1DataSetTableAdapters.РайоныTableAdapter районыTableAdapter;
        private System.Windows.Forms.BindingSource узлыBindingSource;
        private Rayon1DataSetTableAdapters.УзлыTableAdapter узлыTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn кодРайонаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn названиеDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn лесистостьDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn широтаРайонаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn долготаРайонаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn узлыслеваDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn узлысправаDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource районыBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn кодУзлаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn широтаDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn долготаDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource узлыBindingSource1;
    }
}

