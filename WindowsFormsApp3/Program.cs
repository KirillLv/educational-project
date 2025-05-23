﻿using System;
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
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private ComboBox cmbFunction;


        public MainForm()
        {
            InitializeComponent(); // Должен быть первым!

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

            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
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
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (chart1.ChartAreas.Count == 0) return;

            var chartArea = chart1.ChartAreas[0];
            var xAxis = chartArea.AxisX;
            var yAxis = chartArea.AxisY;

            try
            {
                // Коэффициент масштабирования (меньше 1 для увеличения)
                double zoomFactor = 0.8;

                if (xAxis.ScaleView.IsZoomed || yAxis.ScaleView.IsZoomed)
                {
                    // Если уже есть масштабирование - увеличиваем дальше
                    double xMin = xAxis.ScaleView.ViewMinimum;
                    double xMax = xAxis.ScaleView.ViewMaximum;
                    double yMin = yAxis.ScaleView.ViewMinimum;
                    double yMax = yAxis.ScaleView.ViewMaximum;

                    double xCenter = (xMin + xMax) / 2;
                    double yCenter = (yMin + yMax) / 2;
                    double xRadius = (xMax - xMin) / 2 * zoomFactor;
                    double yRadius = (yMax - yMin) / 2 * zoomFactor;

                    xAxis.ScaleView.Zoom(xCenter - xRadius, xCenter + xRadius);
                    yAxis.ScaleView.Zoom(yCenter - yRadius, yCenter + yRadius);
                }
                else
                {
                    // Первоначальное масштабирование
                    double xMin = xAxis.Minimum;
                    double xMax = xAxis.Maximum;
                    double yMin = yAxis.Minimum;
                    double yMax = yAxis.Maximum;

                    double xCenter = (xMin + xMax) / 2;
                    double yCenter = (yMin + yMax) / 2;
                    double xRadius = (xMax - xMin) / 2 * zoomFactor;
                    double yRadius = (yMax - yMin) / 2 * zoomFactor;

                    xAxis.ScaleView.Zoom(xCenter - xRadius, xCenter + xRadius);
                    yAxis.ScaleView.Zoom(yCenter - yRadius, yCenter + yRadius);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при увеличении: {ex.Message}");
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (chart1.ChartAreas.Count == 0) return;

            var chartArea = chart1.ChartAreas[0];
            var xAxis = chartArea.AxisX;
            var yAxis = chartArea.AxisY;

            try
            {
                // Коэффициент масштабирования (больше 1 для уменьшения)
                double zoomFactor = 1.2;

                if (xAxis.ScaleView.IsZoomed || yAxis.ScaleView.IsZoomed)
                {
                    // Если уже есть масштабирование - уменьшаем дальше
                    double xMin = xAxis.ScaleView.ViewMinimum;
                    double xMax = xAxis.ScaleView.ViewMaximum;
                    double yMin = yAxis.ScaleView.ViewMinimum;
                    double yMax = yAxis.ScaleView.ViewMaximum;

                    double xCenter = (xMin + xMax) / 2;
                    double yCenter = (yMin + yMax) / 2;
                    double xRadius = (xMax - xMin) / 2 * zoomFactor;
                    double yRadius = (yMax - yMin) / 2 * zoomFactor;

                    // Проверка, чтобы не выйти за пределы исходного диапазона
                    if (xRadius > (xAxis.Maximum - xAxis.Minimum) / 2 ||
                        yRadius > (yAxis.Maximum - yAxis.Minimum) / 2)
                    {
                        // Если уменьшаем слишком сильно - сбрасываем масштаб
                        xAxis.ScaleView.ZoomReset();
                        yAxis.ScaleView.ZoomReset();
                    }
                    else
                    {
                        xAxis.ScaleView.Zoom(xCenter - xRadius, xCenter + xRadius);
                        yAxis.ScaleView.Zoom(yCenter - yRadius, yCenter + yRadius);
                    }
                }
                else
                {
                    // Если нет масштабирования - просто сбрасываем (ничего не делаем)
                    // или можно установить небольшое начальное уменьшение
                    double xMin = xAxis.Minimum;
                    double xMax = xAxis.Maximum;
                    double yMin = yAxis.Minimum;
                    double yMax = yAxis.Maximum;

                    double xCenter = (xMin + xMax) / 2;
                    double yCenter = (yMin + yMax) / 2;
                    double xRadius = (xMax - xMin) / 2 * zoomFactor;
                    double yRadius = (yMax - yMin) / 2 * zoomFactor;
                    xAxis.ScaleView.Zoom(xCenter - xRadius, xCenter + xRadius);
                    yAxis.ScaleView.Zoom(yCenter - yRadius, yCenter + yRadius);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при уменьшении: {ex.Message}");
            }
        }




        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtPrecision = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbFunction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(88, 126);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(100, 20);
            this.txtA.TabIndex = 0;
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(88, 177);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(100, 20);
            this.txtB.TabIndex = 1;
            // 
            // txtPrecision
            // 
            this.txtPrecision.Location = new System.Drawing.Point(54, 246);
            this.txtPrecision.Name = "txtPrecision";
            this.txtPrecision.Size = new System.Drawing.Size(100, 20);
            this.txtPrecision.TabIndex = 2;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(51, 292);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(137, 45);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(292, 50);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(540, 400);
            this.chart1.TabIndex = 4;
            // 
            // cmbFunction
            // 
            this.cmbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunction.Location = new System.Drawing.Point(55, 50);
            this.cmbFunction.Name = "cmbFunction";
            this.cmbFunction.Size = new System.Drawing.Size(200, 21);
            this.cmbFunction.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(56, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Выберите функции:\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(50, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Введите диапазон:\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(50, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "A:\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(50, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "B:\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(50, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Введите точность:\r\n";
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(304, 427);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btnZoomIn.TabIndex = 11;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(429, 427);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(75, 23);
            this.btnZoomOut.TabIndex = 12;
            this.btnZoomOut.Text = "-";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(870, 650);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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