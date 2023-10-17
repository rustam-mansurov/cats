using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VneshBallistic;

namespace Vnesh_ballistic
{
    public partial class InterfaceDBForm : Form
    {
        public InterfaceDB iDB;
        public InterfaceDBForm()
        {
            InitializeComponent();
            iDB = new InterfaceDB();
            //connect
            string dir = Application.StartupPath + "\\Data";
            string file = "\\dblogin.txt";
            iDB.DBconnect(dir + file);
            //Grid
            dataGridView1.ColumnCount = 11;
            dataGridView1.Columns[0].Width = 30; dataGridView1.Columns[0].Name = "id";
            dataGridView1.Columns[1].Width = 80; dataGridView1.Columns[1].Name = "Название";
            dataGridView1.Columns[2].Width = 80; dataGridView1.Columns[2].Name = "Статус"; 
            dataGridView1.Columns[3].Width = 150; dataGridView1.Columns[3].Name = "Параметры"; dataGridView1.Columns[3].Visible = false;//!!!
            dataGridView1.Columns[4].Width = 80; dataGridView1.Columns[4].Name = "Дата создания"; 
            dataGridView1.Columns[5].Width = 80; dataGridView1.Columns[5].Name = "Дата изменения"; 
            dataGridView1.Columns[6].Width = 50; dataGridView1.Columns[6].Name = "Автор"; 
            dataGridView1.Columns[7].Width = 80; dataGridView1.Columns[7].Name = "Дата активации"; 
            dataGridView1.Columns[8].Width = 80; dataGridView1.Columns[8].Name = "Дата деактивации"; 
            dataGridView1.Columns[9].Width = 80; dataGridView1.Columns[9].Name = "Приоритет"; 
            dataGridView1.Columns[10].Width = 100; dataGridView1.Columns[10].Name = "Комментарий"; 
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int task_id = Convert.ToInt32(textBox1.Text);
            await iDB.LoadtaskparamsFromDB(task_id);
            this.Cursor = Cursors.Default;
            System.Windows.Forms.MessageBox.Show("Данные из БД успешно считаны.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void InterfaceDBForm_Shown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            await iDB.LoadtaskFromDB();
            //Grid
            dataGridView1.Rows.Clear();
            dataGridView1.RowCount = iDB.task_list.Count;
            outball_task task = new outball_task();
            for(int i = 0; i < iDB.task_list.Count; i++)
            {
                task = iDB.task_list[i];
                dataGridView1.Rows[i].Cells[0].Value = task.id;
                dataGridView1.Rows[i].Cells[1].Value = task.name;
                dataGridView1.Rows[i].Cells[2].Value = task.get_status(task.status);
                dataGridView1.Rows[i].Cells[3].Value = task.Params;
                dataGridView1.Rows[i].Cells[4].Value = task.createdAt;
                dataGridView1.Rows[i].Cells[5].Value = task.updatedAt;
                dataGridView1.Rows[i].Cells[6].Value = task.authorId;
                dataGridView1.Rows[i].Cells[7].Value = task.activeFrom;
                dataGridView1.Rows[i].Cells[8].Value = task.activeTill;
                dataGridView1.Rows[i].Cells[9].Value = task.priority;
                dataGridView1.Rows[i].Cells[10].Value = task.memo;
            }
            this.Cursor = Cursors.Default;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[3].Value != null)
                richTextBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }
    }

}
