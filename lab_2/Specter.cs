using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
using System.Numerics;

namespace lab_2
{
    public class Specter
    {
        
        public (List<double> specterA, List<double> specterF) dpf(PointPairList points)
        {
            List<double> specterA = new List<double>();
            List<double> specterF = new List<double>();
            double Acj = 0, Asj = 0;
            for (int j = 0; j <= 100 - 1 / 2; j++)
            {
                double sum = 0;
                for (int i = 0; i <= points.Count - 1 ; i++)
                {
                    sum += points[i].Y * Math.Cos(2 * Math.PI * j * i / points.Count);
                }
                Acj = 2.0 / points.Count * sum;

                sum = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    sum += points[i].Y * Math.Sin(2 * Math.PI * j * i / points.Count);
                }
                Asj = 2 * sum / points.Count;
                specterA.Add(Math.Sqrt(Math.Pow(Acj, 2) + Math.Pow(Asj,2)));
                specterF.Add(Math.Atan2(Asj, Acj));
            }
            return (specterA: specterA, specterF: specterF);
        }
        public PointPairList undpf(List<double> specterA, List<double> specterF )
        {
            PointPairList undpfPoints = new PointPairList();
            double sum = 0, n = 256;
            for (int i = 0; i <= n -1  ; i++)
            {
                sum = 0;
                for (int j = 1; j <= 100 - 1  ; j++)
                {
                    
                    sum += specterA[j] * Math.Cos((2 * Math.PI * j * i / n)   - specterF[j]);
                }
                undpfPoints.Add(i, sum);
            }
            return undpfPoints;
        }

        public PointPairList undpfParam(List<double> specterA, List<double> specterF)
        {
            PointPairList undpfPoints = new PointPairList();
            double sum = 0, n = 256;
            for (int i = 0; i <= n - 1; i++)
            {
                sum = 0;
                for (int j = 1; j <= 100 - 1; j++)
                {

                    sum += 2 * specterA[j] * Math.Cos((2 * Math.PI * j * i / n) - specterF[j]);
                }
                undpfPoints.Add(i, sum);
            }
            return undpfPoints;
        }
    }
    public class Filter
    {
        public List<double> specterA;
        public List<double> specterF;
        public void filterMethod(int a, int b, List<double> specterA, List<double> specterF)
        {
            this.specterA = new List<double>(specterA);
            this.specterF = new List<double>(specterF);
            for (int i = 0; i < specterA.Count; i++)
            {
                if (i <= a || i >= b)
                {
                    this.specterA[i] = 0;
                    this.specterF[i] = 0;
                }

            }
        }
    }
}
