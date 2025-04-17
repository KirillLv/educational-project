using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApp3;

namespace GoldenSectionMinimization
{
    public partial class MainForm : Form
    {
        private TextBox txtA;
        private TextBox txtB;
        private Button btnCalculate;
        private Chart chart1;
        private TextBox txtPrecision;
        private ComboBox cmbFunction;

        public MainForm()
        {
            InitializeComponent();

            txtA.Text = "-5";
            txtB.Text = "5";
            txtPrecision.Text = "0.001";
            cmbFunction.Items.AddRange(new object[]
            {
                "((1-x^3)/x)+1",
                "x^2 + 3*x + 2",
                "sin(x) + cos(2*x)"
            });
            cmbFunction.SelectedIndex = 0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(txtA.Text);
                double b = double.Parse(txtB.Text);
                double precision = double.Parse(txtPrecision.Text);
                string selectedFunction = cmbFunction.SelectedItem.ToString();

                Func<double, double> function = x => FunctionEvaluator.EvaluateFunction(selectedFunction, x);

                IterationsForm iterForm = new IterationsForm();
                iterForm.Show();

                double min = GoldenSection.GoldenSectionSearch(function, a, b, precision, iterForm);

                FunctionPlotter.PlotFunction(chart1, function, a, b);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtPrecision = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbFunction = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(53, 80);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(100, 20);
            this.txtA.TabIndex = 0;
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(160, 80);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(100, 20);
            this.txtB.TabIndex = 1;
            // 
            // txtPrecision
            // 
            this.txtPrecision.Location = new System.Drawing.Point(53, 110);
            this.txtPrecision.Name = "txtPrecision";
            this.txtPrecision.Size = new System.Drawing.Size(100, 20);
            this.txtPrecision.TabIndex = 2;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(53, 140);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(100, 30);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(292, 50);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(540, 400);
            this.chart1.TabIndex = 4;
            // 
            // cmbFunction
            // 
            this.cmbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunction.Location = new System.Drawing.Point(53, 50);
            this.cmbFunction.Name = "cmbFunction";
            this.cmbFunction.Size = new System.Drawing.Size(200, 21);
            this.cmbFunction.TabIndex = 5;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(870, 650);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtPrecision);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.cmbFunction);
            this.Name = "MainForm";
            this.Text = "Golden Section Minimization";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}