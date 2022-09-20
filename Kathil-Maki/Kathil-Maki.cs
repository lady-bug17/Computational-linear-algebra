using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task2
{
    class Сuthill_McKee
    {
        public static List<int> FindPeripheralVertex(Matrix m)
        {
            int iter = 0;
            Matrix copy = m;
            Matrix prevCopy = copy;
            for (int i = 0; i < m.n; i++)
            {
                for (int j = 0; j < m.n; j++)
                {
                    while (copy.matrix[i][j] == 0)
                    {
                        prevCopy = copy;
                        copy = Matrix.MultiplyByOrRule(copy, m);
                        iter++;
                    }
                }
            }
            List<int> result = new List<int>();
            for (int i = 0; i < m.n; i++)
            {
                for (int j = 0; j < m.n; j++)
                {
                    if(prevCopy.matrix[i][j] == 0)
                    {
                        result.Add(i);
                        break;
                    }
                }
            }
            return result;
        }

        public static List<int> CalculateСuthillMcKee(Matrix m, int peripheralVertex)
        {
            List<bool> used = new List<bool>();
            List<int> result = new List<int>();
            List<int> vertecesDegree = new List<int>();
            for (int i = 0; i < m.n; i++)
            {
                int degree = 0;
                for (int j = 0; j < m.n; j++)
                {
                    if (m.matrix[i][j] == 1)
                    {
                        degree++;
                    }
                }
                vertecesDegree.Add(degree - 1);
            }
            for (int i = 0; i < m.n; i++)
            {
                used.Add(false);
            }
            used[peripheralVertex] = true;
            result.Add(peripheralVertex);
            List<int> last = result;
            Dictionary<int, int> l = new Dictionary<int, int>();
            while(result.Count != m.n)
            {
                List<KeyValuePair<int, int>> need = new List<KeyValuePair<int, int>>();
                foreach (var vertex in last)
                {
                    for (int i = 0; i < m.n; i++)
                    {
                        if (m.matrix[vertex][i] == 1 && !used[i])
                        {
                            need.Add(new KeyValuePair<int, int>(i, vertecesDegree[i]));
                            used[i] = true;
                        }
                    }
                }
                need.Sort((x, y) => x.Value.CompareTo(y.Value));
                foreach(var p in need)
                {
                    result.Add(p.Key);
                }
                last = need.Select(x => x.Key).ToList();
                
            }
            return result;
        }
    }
}
