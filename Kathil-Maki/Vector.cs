using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task2
{
    class Vector
    {
        public int size;
        public List<double> elements;

        public Vector()
        {
            size = 0;
            elements = new List<double>();
        }

        public Vector(int n)
        {
            this.size = n;
            this.elements = new List<double>();
        }

        public Vector(String fileName)
        {
            this.elements = new List<double>();
            this.ReadFromFile(fileName);
        }

        public Vector(List<double> elems)
        {
            this.size = elems.Count;
            elements = new List<double>();
            for (int i = 0; i < size; i++)
            {
                elements.Add(elems[i]);
            }
        }

        public void ReadFromFile(string fileName)
        {
            try
            {
                this.elements = File.ReadAllText(fileName).Split(' ').Select(i => double.Parse(i)).ToList();
                this.size = this.elements.Count();
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

        public void RandomizeVector()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < this.size; i++)
            {
                this.elements.Add(random.Next(5, 11));
            }
        }

        public void MultiplyLeft(List<double> t, int column)
        {
            List<double> result = new List<double>();
            for (int i = column; i < size; i++)
            {
                double sum = 0;
                if (i != column)
                {
                    sum = this.elements[i];
                }
                sum += t[i - column] * elements[column];
                result.Add(sum);
            }
            for (int i = column; i < size; i++)
            {
                elements[i] = result[i - column];
            }
        }

        public void AddToDataGridView(DataGridView dataGridView)
        {
            dataGridView.ColumnCount = 1;
            dataGridView.RowCount = size;
            dataGridView.Columns[0].Name = "1";
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Aqua;
            for (int i = 0; i < size; i++)
            {
                dataGridView.RowHeadersWidth = 50;
                dataGridView.Rows[i].HeaderCell.Value = i.ToString();
                dataGridView.Rows[i].HeaderCell.Style.BackColor = Color.Aqua;
            }
            for (int i = 0; i < size; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = Math.Round(elements[i], 3);
            }
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            if (m.m == v.size)
            {
                Vector result = new Vector(v.size);
                for (int i = 0; i < m.n; i++)
                {
                    double element = 0;
                    for (int j = 0; j < v.size; j++)
                    {
                        element += m.matrix[i][j] * v.elements[j];
                    }
                    result.elements.Add(element);
                }
                return result;
            }
            else
            {
                throw new InvalidDataException("Invalid data.");
            }
        }
    }
}
