using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParallelCalc;

namespace Task2
{
    public partial class Form1 : Form
    {
        private Matrix m = new Matrix("m.txt");
        private Vector v = new Vector("v.txt");
        //Matrix m = new Matrix(5, 5);
        //Vector v = new Vector(5);

        public Form1()
        {
            //m.RandomizeMatrix();
            //v.RandomizeVector();
            InitializeComponent();
            m.AddToDataGridView(dataGridView1);
            v.AddToDataGridView(dataGridView2);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int threadNumber = 100;
                int n = 10;
                double[,] m1 = new double[m.n,m.m];
                for (int i = 0; i < m.n; i++)
                {
                    for (int j = 0; j < m.m; j++)
                    {
                        m1[i, j] = m.matrix[i][j];
                    }
                }
                double[] x = new double[n];
                for (int j = 0; j < m.m; j++)
                {
                    x[j] = v.elements[j];
                } 
                var watch = Stopwatch.StartNew();
                ParallelGauss.GaussMethod(m1, x);
                watch.Stop();
                Console.WriteLine($"Linear execution time: {watch.ElapsedMilliseconds} ms");
                watch = Stopwatch.StartNew();
                ParallelGauss.ThreadedGauss(m1, x, threadNumber);
                watch.Stop();
                Console.WriteLine($"Threaded execution time: {watch.ElapsedMilliseconds} ms");
                Matrix decomposed = Gauss.RowReduction(m, v);
                decomposed.AddToDataGridView(dataGridView4);
                Matrix inverse = Gauss.FindInverseMatrix(decomposed);
                inverse.AddToDataGridView(dataGridView3);
                Vector data = new Vector("v.txt");
                Vector result = inverse * data;
                result.AddToDataGridView(dataGridView5);
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
