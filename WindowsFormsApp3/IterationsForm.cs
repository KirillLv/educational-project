using System;
using System.Windows.Forms;

namespace GoldenSectionMinimization
{
    public partial class IterationsForm : Form
    {
        public DataGridView DataGrid { get; private set; }

        public IterationsForm()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            // Добавляем столбцы в DataGridView
            this.DataGrid.Columns.Add("Iteration", "Iteration");
            this.DataGrid.Columns.Add("Interval", "Interval [a, b]");
            this.DataGrid.Columns.Add("Interval Length", "Interval Length");
            this.DataGrid.Columns.Add("c", "c");
            this.DataGrid.Columns.Add("f(c)", "f(c)");
            this.DataGrid.Columns.Add("d", "d");
            this.DataGrid.Columns.Add("f(d)", "f(d)");

            // Настройка ширины столбцов
            this.DataGrid.Columns[0].Width = 80;
            this.DataGrid.Columns[1].Width = 120;
            this.DataGrid.Columns[2].Width = 120;
            this.DataGrid.Columns[3].Width = 80;
            this.DataGrid.Columns[4].Width = 80;
            this.DataGrid.Columns[5].Width = 80;
            this.DataGrid.Columns[6].Width = 80;
        }

        private void InitializeComponent()
        {
            this.DataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGrid
            // 
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Location = new System.Drawing.Point(12, 12);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.Size = new System.Drawing.Size(760, 400);
            this.DataGrid.TabIndex = 0;
            // 
            // IterationsForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.DataGrid);
            this.Name = "IterationsForm";
            this.Text = "Iterations";
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
