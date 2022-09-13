using System;
using System.Collections.Generic;
using System.IO;

namespace Task2
{
    class Gauss
    {
        public static Vector ParseMatrix(Matrix m)
        {
            Vector v = new Vector(m.n);
            for (int i = 0; i < m.n; i++)
            {
                v.elements.Add(m.matrix[i][m.m - 1]);
                m.matrix[i].RemoveRange(m.m - 1, 1);
            }
            m.m -= 1;
            return v;
        }

        public static Matrix RowReduction(Matrix a, Vector b)
        {
            Matrix decompined = new Matrix(a.n, a.m);
            for (int i = 0; i < a.m; i++)
            {
                if (Math.Abs(a.matrix[i][i]) <= 0.00001)
                {
                    a.SwapRows(i, FindMaxElement(a, i));
                }
                //Matrix t = CreateMatrixTi(i, a);
                //a = t * a;
                //b = t * b;
                List<double> t = CreateTi(i, a);
                a.MultiplyLeft(t, i);
                b.MultiplyLeft(t, i);
                int elem = 0;
                for (int j = i; j < a.n; j++)
                {
                    decompined.matrix[j][i] = t[elem];
                    elem++;
                }
            }
            for (int i = 0; i < a.n; i++)
            {
                for (int j = i+1; j < a.n; j++)
                {
                    decompined.matrix[i][j] = a.matrix[i][j];
                }
            }
            return decompined;
        }

        //public static Matrix CreateMatrixTi(int column, Matrix a)
        //{
        //    Matrix result = new Matrix(a.n, a.m);
        //    for (int i = column + 1; i < a.n; i++)
        //    {
        //        result.matrix[i][column] = - a.matrix[i][column] / a.matrix[column][column];
        //    }
        //    for (int i = 0; i < a.m; i++)
        //    {
        //        result.matrix[i][i] = 1;
        //    }
        //    result.matrix[column][column] = 1 / a.matrix[column][column];
        //    return result;
        //}

        public static List<double> CreateTi(int column, Matrix a)
        {
            List<double> result = new List<double>();
            result.Add(1 / a.matrix[column][column]);
            for (int i = column + 1; i < a.n; i++)
            {
                result.Add(-a.matrix[i][column] / a.matrix[column][column]);
            }
            return result;
        }

        public static Vector ReverseWay(Matrix a, Vector v)
        {
            Vector result = new Vector(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result.elements.Add(v.elements[i]);
            }
            for (int i = v.size-1; i >= 0; i--)
            {
                for (int j = i + 1; j < v.size; j++)
                {
                    result.elements[i] -= a.matrix[i][j] * result.elements[j];
                }
            }
            return result;
        }
        
        private static int FindMaxElement(Matrix a, int index)
        {
            int res = index + 1;
            for (int i = index + 2; i < a.n; i++)
            {
                if (Math.Abs(a.matrix[res][index]) < Math.Abs(a.matrix[i][index]))
                {
                    res = i;
                }
            }
            if (Math.Abs(a.matrix[res][index]) < 1e-6)
            {
                throw new InvalidDataException("Degenerate matrix.");
            }
            return res;
        }

        public static Matrix FindInverseMatrix(Matrix decompined)
        {
            Matrix inverse = new Matrix(decompined.n, decompined.m);
            Matrix T = new Matrix(decompined.n, decompined.m);
            Matrix A = new Matrix(decompined.n, decompined.m);
            for (int i = 0; i < T.n; i++)
            {
                T.matrix[i][i] = 1;
                A.matrix[i][i] = 1;
            }
            for (int i = 0; i < T.n; i++)
            {
                for (int j = i + 1; j < T.n; j++)
                {
                    A.matrix[i][j] = decompined.matrix[i][j];
                }
            }
            for (int i = 0; i < A.n; i++)
            {
                List<double> t = new List<double>();
                for (int j = i; j < A.n; j++)
                {
                    t.Add(decompined.matrix[j][i]);
                }
                T.MultiplyLeft(t, i);
            }
            for (int i = 0; i < A.n; i++)
            {
                Vector b = new Vector(A.n);
                for (int j = 0; j < b.size; j++)
                {
                    b.elements.Add(0);
                }
                b.elements[i] = 1;
                b = T * b;
                Vector ans = ReverseWay(A, b);
                for (int j = 0; j < A.n; j++)
                {
                    inverse.matrix[j][i] = ans.elements[j];
                }
            }
            return inverse;
        }

    }
}
