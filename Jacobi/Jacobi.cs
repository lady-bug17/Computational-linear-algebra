using System;
using System.IO;
using System.Linq;

namespace Task2
{
    class Jacobi
    {
        public static bool IsConvergent(Matrix m)
        {
            for (int i = 0; i < m.m; i++)
            {
                double sum = m.matrix[i].Sum(x => Math.Abs(x)) - Math.Abs(m.matrix[i][i]);
                if (Math.Abs(m.matrix[i][i]) <= sum)
                {
                    return false;
                }
            }
            return true;
        }

        public static Vector CalculateJacobi(Matrix a, Vector b, Vector x, Matrix iterations)
        {
            Vector result = new Vector(b.size);
            double maxDifference = 0;
            for (int i = 0; i < a.n; i++)
            {
                result.elements[i] = b.elements[i] / a.matrix[i][i];
                for (int j = 0; j < a.m; j++)
                {
                    if (i != j)
                    {
                        result.elements[i] -= a.matrix[i][j] * x.elements[j] / a.matrix[i][i];
                    }
                }
            }
            for (int i = 0; i < b.size; i++)
            {
                maxDifference = Math.Max(maxDifference, Math.Abs(x.elements[i] - result.elements[i]));
            }
            if (maxDifference > 1e-3)
            {
                x = result;
                iterations.matrix.Add(result.elements);
                iterations.m = a.m;
                iterations.n++;
                result = CalculateJacobi(a, b, x, iterations);
            }
            return result;
        }
    }
}
