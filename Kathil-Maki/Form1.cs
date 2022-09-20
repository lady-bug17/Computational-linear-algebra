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
            //m.AddToDataGridView(dataGridView1);
            //v.AddToDataGridView(dataGridView2);
            
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
                List<int> res = Сuthill_McKee.FindPeripheralVertex(m);
                dataGridView1.RowCount = m.m;
                for (int i = 0; i < res.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = res[i] + 1;
                }
                var res69 = Сuthill_McKee.CalculateСuthillMcKee(m, 4);
                dataGridView2.RowCount = m.m;
                for (int i = 0; i < res69.Count; i++)
                {
                    dataGridView2.Rows[i].Cells[0].Value = res69[i] + 1;
                }
                Matrix dublicate = new Matrix(m.n, m.m);
                for (int i = 0; i < m.m; i++)
                {
                    for (int j = 0; j < m.m; j++)
                    {
                        dublicate.matrix[i][j] = m.matrix[res69[i]][res69[j]];
                    }
                }
                dublicate.AddToDataGridView(dataGridView4);
                m.AddToDataGridView(dataGridView3);
                //using (StreamWriter outputFile = new StreamWriter("f.txt"))
                //{
                //    for (int i = 0; i < m.n; i++)
                //    {
                //        for (int j = 0; j < m.m; j++)
                //        {
                //            outputFile.Write(dublicate.matrix[i][j]);
                //            outputFile.Write(" ");
                //        }
                //        outputFile.WriteLine();
                //    }
                //}
                Vector data = new Vector("v.txt");
                Matrix decomposed = Gauss.RowReduction(m, v);
                Matrix inverse = Gauss.FindInverseMatrix(decomposed);
                Vector result = inverse * v;
                result.AddToDataGridView(dataGridView5);
                Vector dub = new Vector(data.elements);
                for (int i = 0; i < m.n; i++)
                {
                    dub.elements[i] = data.elements[res69[i]];
                }
                //using (StreamWriter outputFile = new StreamWriter("g.txt"))
                //{
                //    for (int i = 0; i < m.n; i++)
                //    {
                //        for (int j = 0; j < m.m; j++)
                //        {
                //            outputFile.Write(dublicate.matrix[i][j]);
                //            outputFile.Write(" ");
                //        }
                //        outputFile.WriteLine();
                //    }
                //}
                decomposed = Gauss.RowReduction(dublicate, dub);
                result = Gauss.ReverseWay(dublicate, dub);
                //using (StreamWriter outputFile = new StreamWriter("f.txt"))
                //{
                //    outputFile.WriteLine();
                //    for (int i = 0; i < m.n; i++)
                //    {
                //        outputFile.Write(result.elements[i]);
                //        outputFile.Write(" ");
                //    }
                //    outputFile.WriteLine();

                //}

                Vector ans = new Vector(result.elements);
                for (int i = 0; i < m.n; i++)
                {
                    ans.elements[res69[i]] = result.elements[i];
                }
                ans.AddToDataGridView(dataGridView5);
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
