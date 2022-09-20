using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Drawing;


namespace WindowsFormsApp1
{
    class Matrix
    {
        public int n { get; set; }
        public int m { get; set; }
        public List<List<double>> matrix { get; set; }

        public Matrix()
        {
            this.n = 0;
            this.m = 0;
            this.matrix = new List<List<double>>();
        }

        public Matrix(int n, int m)
        {
            this.n = n;
            this.m = m;
            this.matrix = new List<List<double>>(n) { };
            for (int i = 0; i < n; i++)
            {
                List<double> row = new List<double>();
                for (int j = 0; j < m; j++)
                {
                    row.Add(0);
                }
                this.matrix.Add(row);
            }
        }

        public Matrix(int n, int m, List<double> elements)
        {
            this.n = n;
            this.m = m;
            this.matrix = new List<List<double>>(n) { };
            for (int i = 0; i < n; i++)
            {
                List<double> row = new List<double>();
                for (int j = 0; j < m; j++)
                {
                    row.Add(elements[i * j + j]);
                }
                this.matrix.Add(row);
            }
        }

        public Matrix(int n, int m, List<List<double>> elements)
        {
            this.n = n;
            this.m = m;
            this.matrix = new List<List<double>>();
            for (int i = 0; i < n; i++)
            {
                matrix.Add(elements[i]);
            }
        }

        public Matrix(Matrix matrix)
        {
            this.n = matrix.n;
            this.m = matrix.m;
            this.matrix = matrix.matrix;
        }

        public Matrix(String fileName)
        {
            this.matrix = new List<List<double>>();
            this.ReadFromFile(fileName);
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.n == m2.n && m1.m == m2.m)
            {
                Matrix result = new Matrix(m1.n, m1.m);
                for (int i = 0; i < m1.n; i++)
                {
                    for (int j = 0; j < m1.m; j++)
                    {
                        result.matrix[i][j] = m1.matrix[i][j] + m2.matrix[i][j];
                    }
                }
                return result;
            }
            else
            {
                throw new InvalidDataException("Invalid data.");
            }
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.n == m2.n && m1.m == m2.m)
            {
                Matrix result = new Matrix(m1.n, m1.m);
                for (int i = 0; i < m1.n; i++)
                {
                    for (int j = 0; j < m1.m; j++)
                    {
                        result.matrix[i][j] = m1.matrix[i][j] - m2.matrix[i][j];
                    }
                }
                return result;
            }
            else
            {
                throw new InvalidDataException("Invalid data.");
            }
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.m == m2.n)
            {
                Matrix result = new Matrix(m1.n, m1.m);
                for (int i = 0; i < m1.n; i++)
                {
                    for (int j = 0; j < m2.m; j++)
                    {
                        for (int k = 0; k < m1.n; k++)
                        {
                            result.matrix[i][j] += m1.matrix[i][k] * m2.matrix[k][j];
                        }
                    }
                }
                return result;
            }
            else
            {
                throw new InvalidDataException("Invalid data.");
            }
        }

        public void RandomizeMatrix()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    this.matrix[i][j] = random.NextDouble();
                }
            }
        }

        public void ReadFromFile(String fileName)
        {
            try
            {
                this.matrix = File.ReadAllLines(fileName)
                   .Select(l => l.Split(' ').Select(i => double.Parse(i)).ToList())
                   .ToList();
                this.n = this.matrix.Count();
                this.m = this.matrix[0].Count();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddToDataGridView(DataGridView dataGridView)
        {
            dataGridView.ColumnCount = m;
            dataGridView.RowCount = n;
            for (int i = 0; i < m; i++)
            {
                dataGridView.Columns[i].Name = i.ToString();
                dataGridView.EnableHeadersVisualStyles = false;
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
            }
            for (int i = 0; i < n; i++)
            {
                dataGridView.RowHeadersWidth = 50;
                dataGridView.Rows[i].HeaderCell.Value = i.ToString();
                dataGridView.Rows[i].HeaderCell.Style.BackColor = Color.Aqua;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = matrix[i][j];
                }
            }
        }
    }
}