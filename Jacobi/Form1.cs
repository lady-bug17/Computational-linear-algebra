using System;
using System.IO;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        private Matrix m = new Matrix("m.txt");
        private Vector v = new Vector("v.txt");
        private Vector approximationVector = new Vector("approx.txt");

        public Form1()
        {
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
            if (Jacobi.IsConvergent(m))
            {
                Matrix iterations = new Matrix();
                Vector result = Jacobi.CalculateJacobi(m, v, approximationVector, iterations);
                result.AddToDataGridView(dataGridView4);
                iterations.AddToDataGridView(dataGridView3);
            }
            else
            { 
                MessageBox.Show("Matrix is not convergent");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
