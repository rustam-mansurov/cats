namespace Vnesh_ballistic
{
    partial class TrajectoryGraphicsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart_Oxy = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_Oxz = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1_psy = new System.Windows.Forms.Label();
            this.label1_teta = new System.Windows.Forms.Label();
            this.label1_D = new System.Windows.Forms.Label();
            this.label1_V = new System.Windows.Forms.Label();
            this.label1_z = new System.Windows.Forms.Label();
            this.label1_y = new System.Windows.Forms.Label();
            this.label1_x = new System.Windows.Forms.Label();
            this.label1_t = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Oxy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Oxz)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_Oxy
            // 
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            chartArea3.Name = "ChartArea1";
            this.chart_Oxy.ChartAreas.Add(chartArea3);
            this.chart_Oxy.Location = new System.Drawing.Point(2, 0);
            this.chart_Oxy.Name = "chart_Oxy";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Blue;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_Oxy.Series.Add(series3);
            this.chart_Oxy.Size = new System.Drawing.Size(591, 287);
            this.chart_Oxy.TabIndex = 3;
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title3.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title3.Name = "Title1";
            title3.Text = "    Проекция на Oxy: дальность (x) - высота (y)";
            this.chart_Oxy.Titles.Add(title3);
            // 
            // chart_Oxz
            // 
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea4.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            chartArea4.AxisY.IsLabelAutoFit = false;
            chartArea4.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea4.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            chartArea4.Name = "ChartArea1";
            this.chart_Oxz.ChartAreas.Add(chartArea4);
            this.chart_Oxz.Location = new System.Drawing.Point(11, 285);
            this.chart_Oxz.Name = "chart_Oxz";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Blue;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_Oxz.Series.Add(series4);
            this.chart_Oxz.Size = new System.Drawing.Size(582, 287);
            this.chart_Oxz.TabIndex = 4;
            title4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title4.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title4.Name = "Title1";
            title4.Text = "       Проекция на Oxz: дальность (x) - боковое отклонение (z)";
            this.chart_Oxz.Titles.Add(title4);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1_psy);
            this.groupBox1.Controls.Add(this.label1_teta);
            this.groupBox1.Controls.Add(this.label1_D);
            this.groupBox1.Controls.Add(this.label1_V);
            this.groupBox1.Controls.Add(this.label1_z);
            this.groupBox1.Controls.Add(this.label1_y);
            this.groupBox1.Controls.Add(this.label1_x);
            this.groupBox1.Controls.Add(this.label1_t);
            this.groupBox1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(583, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 248);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры снаряда:";
            // 
            // label1_psy
            // 
            this.label1_psy.AutoSize = true;
            this.label1_psy.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_psy.Location = new System.Drawing.Point(13, 212);
            this.label1_psy.Name = "label1_psy";
            this.label1_psy.Size = new System.Drawing.Size(21, 19);
            this.label1_psy.TabIndex = 7;
            this.label1_psy.Text = "ψ";
            // 
            // label1_teta
            // 
            this.label1_teta.AutoSize = true;
            this.label1_teta.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_teta.Location = new System.Drawing.Point(13, 185);
            this.label1_teta.Name = "label1_teta";
            this.label1_teta.Size = new System.Drawing.Size(18, 19);
            this.label1_teta.TabIndex = 6;
            this.label1_teta.Text = "θ";
            // 
            // label1_D
            // 
            this.label1_D.AutoSize = true;
            this.label1_D.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_D.Location = new System.Drawing.Point(13, 131);
            this.label1_D.Name = "label1_D";
            this.label1_D.Size = new System.Drawing.Size(54, 19);
            this.label1_D.TabIndex = 5;
            this.label1_D.Text = "label1";
            // 
            // label1_V
            // 
            this.label1_V.AutoSize = true;
            this.label1_V.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_V.Location = new System.Drawing.Point(13, 158);
            this.label1_V.Name = "label1_V";
            this.label1_V.Size = new System.Drawing.Size(54, 19);
            this.label1_V.TabIndex = 4;
            this.label1_V.Text = "label5";
            // 
            // label1_z
            // 
            this.label1_z.AutoSize = true;
            this.label1_z.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_z.Location = new System.Drawing.Point(13, 104);
            this.label1_z.Name = "label1_z";
            this.label1_z.Size = new System.Drawing.Size(54, 19);
            this.label1_z.TabIndex = 3;
            this.label1_z.Text = "label4";
            // 
            // label1_y
            // 
            this.label1_y.AutoSize = true;
            this.label1_y.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_y.Location = new System.Drawing.Point(13, 77);
            this.label1_y.Name = "label1_y";
            this.label1_y.Size = new System.Drawing.Size(54, 19);
            this.label1_y.TabIndex = 2;
            this.label1_y.Text = "label3";
            // 
            // label1_x
            // 
            this.label1_x.AutoSize = true;
            this.label1_x.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_x.Location = new System.Drawing.Point(13, 50);
            this.label1_x.Name = "label1_x";
            this.label1_x.Size = new System.Drawing.Size(54, 19);
            this.label1_x.TabIndex = 1;
            this.label1_x.Text = "label2";
            // 
            // label1_t
            // 
            this.label1_t.AutoSize = true;
            this.label1_t.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1_t.Location = new System.Drawing.Point(13, 23);
            this.label1_t.Name = "label1_t";
            this.label1_t.Size = new System.Drawing.Size(54, 19);
            this.label1_t.TabIndex = 0;
            this.label1_t.Text = "label1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(629, 536);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Пуск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // TrajectoryGraphicsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(781, 577);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart_Oxz);
            this.Controls.Add(this.chart_Oxy);
            this.Name = "TrajectoryGraphicsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_dopolnit";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_dopolnit_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Oxy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Oxz)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Oxy;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Oxz;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1_V;
        private System.Windows.Forms.Label label1_z;
        private System.Windows.Forms.Label label1_y;
        private System.Windows.Forms.Label label1_x;
        private System.Windows.Forms.Label label1_t;
        private System.Windows.Forms.Label label1_psy;
        private System.Windows.Forms.Label label1_teta;
        private System.Windows.Forms.Label label1_D;
        private System.Windows.Forms.Button button1;
    }
}