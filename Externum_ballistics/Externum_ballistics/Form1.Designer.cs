namespace Externum_ballistics
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Параметры снаряда");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Параметры расчета");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Оптимизация");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Прямая задача");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Обратная задача");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Расчёт", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            this.FileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jsonToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьПараметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InitialСonditions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.начальныеУсловияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Optimisation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Calculation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.начатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остановитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.внутренняяБаллистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.jsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.formsPlot4 = new ScottPlot.FormsPlot();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.FileMenu.SuspendLayout();
            this.InitialСonditions.SuspendLayout();
            this.Optimisation.SuspendLayout();
            this.Calculation.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // FileMenu
            // 
            this.FileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.изменитьПараметрыToolStripMenuItem});
            this.FileMenu.Name = "contextMenuStrip2";
            this.FileMenu.Size = new System.Drawing.Size(194, 48);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jsonToolStripMenuItem1,
            this.xMLToolStripMenuItem});
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            // 
            // jsonToolStripMenuItem1
            // 
            this.jsonToolStripMenuItem1.Name = "jsonToolStripMenuItem1";
            this.jsonToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.jsonToolStripMenuItem1.Text = "Json";
            this.jsonToolStripMenuItem1.Click += new System.EventHandler(this.jsonToolStripMenuItem1_Click);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.xMLToolStripMenuItem.Text = "XML";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.xMLToolStripMenuItem_Click);
            // 
            // изменитьПараметрыToolStripMenuItem
            // 
            this.изменитьПараметрыToolStripMenuItem.Name = "изменитьПараметрыToolStripMenuItem";
            this.изменитьПараметрыToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.изменитьПараметрыToolStripMenuItem.Text = "Изменить параметры";
            this.изменитьПараметрыToolStripMenuItem.Click += new System.EventHandler(this.изменитьПараметрыToolStripMenuItem_Click);
            // 
            // InitialСonditions
            // 
            this.InitialСonditions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.начальныеУсловияToolStripMenuItem,
            this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem});
            this.InitialСonditions.Name = "InitialСonditions";
            this.InitialСonditions.Size = new System.Drawing.Size(332, 48);
            // 
            // начальныеУсловияToolStripMenuItem
            // 
            this.начальныеУсловияToolStripMenuItem.Name = "начальныеУсловияToolStripMenuItem";
            this.начальныеУсловияToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.начальныеУсловияToolStripMenuItem.Text = "Начальные условия снаряда";
            this.начальныеУсловияToolStripMenuItem.Click += new System.EventHandler(this.начальныеУсловияToolStripMenuItem_Click);
            // 
            // начальныеПараметрыРеактивногоДвигателяToolStripMenuItem
            // 
            this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem.Name = "начальныеПараметрыРеактивногоДвигателяToolStripMenuItem";
            this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem.Size = new System.Drawing.Size(331, 22);
            this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem.Text = "Начальные параметры реактивного двигателя";
            this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem.Click += new System.EventHandler(this.начальныеПараметрыРеактивногоДвигателяToolStripMenuItem_Click);
            // 
            // Optimisation
            // 
            this.Optimisation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem});
            this.Optimisation.Name = "Optimisation";
            this.Optimisation.Size = new System.Drawing.Size(355, 48);
            // 
            // оптимизацияВнешнебаллистическихПараметровToolStripMenuItem
            // 
            this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem.Name = "оптимизацияВнешнебаллистическихПараметровToolStripMenuItem";
            this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem.Size = new System.Drawing.Size(354, 22);
            this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem.Text = "Оптимизация внешнебаллистических параметров";
            this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem.Click += new System.EventHandler(this.оптимизацияВнешнебаллистическихПараметровToolStripMenuItem_Click);
            // 
            // Calculation
            // 
            this.Calculation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.начатьToolStripMenuItem,
            this.остановитьToolStripMenuItem,
            this.внутренняяБаллистикаToolStripMenuItem});
            this.Calculation.Name = "contextMenuStrip1";
            this.Calculation.Size = new System.Drawing.Size(206, 70);
            // 
            // начатьToolStripMenuItem
            // 
            this.начатьToolStripMenuItem.Name = "начатьToolStripMenuItem";
            this.начатьToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.начатьToolStripMenuItem.Text = "Внешняя баллистика";
            this.начатьToolStripMenuItem.Click += new System.EventHandler(this.начатьToolStripMenuItem_Click);
            // 
            // остановитьToolStripMenuItem
            // 
            this.остановитьToolStripMenuItem.Name = "остановитьToolStripMenuItem";
            this.остановитьToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.остановитьToolStripMenuItem.Text = "Остановить";
            // 
            // внутренняяБаллистикаToolStripMenuItem
            // 
            this.внутренняяБаллистикаToolStripMenuItem.Name = "внутренняяБаллистикаToolStripMenuItem";
            this.внутренняяБаллистикаToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.внутренняяБаллистикаToolStripMenuItem.Text = "Внутренняя Баллистика";
            this.внутренняяБаллистикаToolStripMenuItem.Click += new System.EventHandler(this.внутренняяБаллистикаToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1538, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.сохранитьToolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(133, 22);
            this.OpenFile.Text = "Открыть";
            // 
            // сохранитьToolStripMenuItem1
            // 
            this.сохранитьToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jsonToolStripMenuItem});
            this.сохранитьToolStripMenuItem1.Name = "сохранитьToolStripMenuItem1";
            this.сохранитьToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.сохранитьToolStripMenuItem1.Text = "Сохранить";
            // 
            // jsonToolStripMenuItem
            // 
            this.jsonToolStripMenuItem.Name = "jsonToolStripMenuItem";
            this.jsonToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.jsonToolStripMenuItem.Text = "Json";
            this.jsonToolStripMenuItem.Click += new System.EventHandler(this.jsonToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.справкаToolStripMenuItem1});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // справкаToolStripMenuItem1
            // 
            this.справкаToolStripMenuItem1.Name = "справкаToolStripMenuItem1";
            this.справкаToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.справкаToolStripMenuItem1.Text = "Просмотр справки";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(479, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1014, 696);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.formsPlot4);
            this.tabPage1.Controls.Add(this.formsPlot3);
            this.tabPage1.Controls.Add(this.formsPlot2);
            this.tabPage1.Controls.Add(this.formsPlot1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1006, 668);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Графики";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // formsPlot4
            // 
            this.formsPlot4.Location = new System.Drawing.Point(513, 336);
            this.formsPlot4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(497, 308);
            this.formsPlot4.TabIndex = 3;
            // 
            // formsPlot3
            // 
            this.formsPlot3.Location = new System.Drawing.Point(7, 336);
            this.formsPlot3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(497, 308);
            this.formsPlot3.TabIndex = 2;
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(505, 6);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(497, 308);
            this.formsPlot2.TabIndex = 1;
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(7, 6);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(497, 308);
            this.formsPlot1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1006, 668);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Таблица переменных";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25,
            this.Column26,
            this.Column27,
            this.Column28,
            this.Column29,
            this.Column30,
            this.Column31,
            this.Column32});
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1006, 656);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "t";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "X";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Y";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Z";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "V";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "teta";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "psi";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "omega";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "q";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "a";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "T";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "ro";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "g";
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Sm";
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Mah";
            this.Column15.Name = "Column15";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "m";
            this.Column16.Name = "Column16";
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Cx";
            this.Column17.Name = "Column17";
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Cy";
            this.Column18.Name = "Column18";
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Cz";
            this.Column19.Name = "Column19";
            // 
            // Column20
            // 
            this.Column20.HeaderText = "d";
            this.Column20.Name = "Column20";
            // 
            // Column21
            // 
            this.Column21.HeaderText = "L";
            this.Column21.Name = "Column21";
            // 
            // Column22
            // 
            this.Column22.HeaderText = "Ix";
            this.Column22.Name = "Column22";
            // 
            // Column23
            // 
            this.Column23.HeaderText = "mx";
            this.Column23.Name = "Column23";
            // 
            // Column24
            // 
            this.Column24.HeaderText = "P";
            this.Column24.Name = "Column24";
            // 
            // Column25
            // 
            this.Column25.HeaderText = "t_delta";
            this.Column25.Name = "Column25";
            // 
            // Column26
            // 
            this.Column26.HeaderText = "t1";
            this.Column26.Name = "Column26";
            // 
            // Column27
            // 
            this.Column27.HeaderText = "alfa";
            this.Column27.Name = "Column27";
            // 
            // Column28
            // 
            this.Column28.HeaderText = "beta";
            this.Column28.Name = "Column28";
            // 
            // Column29
            // 
            this.Column29.HeaderText = "sigma";
            this.Column29.Name = "Column29";
            // 
            // Column30
            // 
            this.Column30.HeaderText = "Iz";
            this.Column30.Name = "Column30";
            // 
            // Column31
            // 
            this.Column31.HeaderText = "Mza";
            this.Column31.Name = "Column31";
            // 
            // Column32
            // 
            this.Column32.HeaderText = "Column32";
            this.Column32.Name = "Column32";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1006, 668);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Визуализация";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 27);
            this.treeView1.Name = "treeView1";
            treeNode1.ContextMenuStrip = this.FileMenu;
            treeNode1.Name = "Узел5";
            treeNode1.Text = "Параметры снаряда";
            treeNode2.ContextMenuStrip = this.InitialСonditions;
            treeNode2.Name = "Узел4";
            treeNode2.Text = "Параметры расчета";
            treeNode3.ContextMenuStrip = this.Optimisation;
            treeNode3.Name = "Узел1";
            treeNode3.Text = "Оптимизация";
            treeNode4.Name = "Узел0";
            treeNode4.Text = "Прямая задача";
            treeNode5.Name = "Узел7";
            treeNode5.Text = "Обратная задача";
            treeNode6.ContextMenuStrip = this.Calculation;
            treeNode6.Name = "Узел6";
            treeNode6.Text = "Расчёт";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode6});
            this.treeView1.Size = new System.Drawing.Size(449, 175);
            this.treeView1.TabIndex = 2;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(24, 208);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(449, 511);
            this.propertyGrid1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(483, 729);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Очистить графики";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1538, 803);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Внешняя баллистика";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FileMenu.ResumeLayout(false);
            this.InitialСonditions.ResumeLayout(false);
            this.Optimisation.ResumeLayout(false);
            this.Calculation.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private ScottPlot.FormsPlot formsPlot1;
        private TreeView treeView1;
        private ContextMenuStrip Calculation;
        private ToolStripMenuItem начатьToolStripMenuItem;
        private ToolStripMenuItem остановитьToolStripMenuItem;
        private ToolStripMenuItem OpenFile;
        private ToolStripMenuItem сохранитьToolStripMenuItem1;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem1;
        private PropertyGrid propertyGrid1;
        private ContextMenuStrip FileMenu;
        private TabPage tabPage2;
        private DataGridView dataGridView1;
        private ToolStripMenuItem загрузитьToolStripMenuItem;
        private ToolStripMenuItem jsonToolStripMenuItem;
        private ToolStripMenuItem jsonToolStripMenuItem1;
        private ToolStripMenuItem xMLToolStripMenuItem;
        private ContextMenuStrip InitialСonditions;
        private ToolStripMenuItem начальныеУсловияToolStripMenuItem;
        private ToolStripMenuItem начальныеПараметрыРеактивногоДвигателяToolStripMenuItem;
        private TabPage tabPage3;
        private ScottPlot.FormsPlot formsPlot4;
        private ScottPlot.FormsPlot formsPlot3;
        private ScottPlot.FormsPlot formsPlot2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column21;
        private DataGridViewTextBoxColumn Column22;
        private DataGridViewTextBoxColumn Column23;
        private DataGridViewTextBoxColumn Column24;
        private DataGridViewTextBoxColumn Column25;
        private DataGridViewTextBoxColumn Column26;
        private DataGridViewTextBoxColumn Column27;
        private DataGridViewTextBoxColumn Column28;
        private DataGridViewTextBoxColumn Column29;
        private DataGridViewTextBoxColumn Column30;
        private DataGridViewTextBoxColumn Column31;
        private DataGridViewTextBoxColumn Column32;
        private Button button1;
        private ToolStripMenuItem изменитьПараметрыToolStripMenuItem;
        private ToolStripMenuItem внутренняяБаллистикаToolStripMenuItem;
        private ContextMenuStrip Optimisation;
        private ToolStripMenuItem оптимизацияВнешнебаллистическихПараметровToolStripMenuItem;
    }
}