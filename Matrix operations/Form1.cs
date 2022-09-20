using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Matrix m1 = new Matrix("m1.txt");
        private Matrix m2 = new Matrix("m2.txt");
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");
            comboBox1.Items.Add("*");
            m1.AddToDataGridView(dataGridView1);
            m2.AddToDataGridView(dataGridView2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem.ToString() == "+")
                {
                    Matrix result = m1 + m2;
                    result.AddToDataGridView(dataGridView3);
                }
                else if (comboBox1.SelectedItem.ToString() == "-")
                {
                    Matrix result = m1 - m2;
                    result.AddToDataGridView(dataGridView3);
                }
                else if (comboBox1.SelectedItem.ToString() == "*")
                {
                    Matrix result = m1 * m2;
                    result.AddToDataGridView(dataGridView3);
                }
            }
            catch(InvalidDataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
