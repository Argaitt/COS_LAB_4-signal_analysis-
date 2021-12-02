using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace lab_2
{
    class Correlator
    {
        public double getCorrelationCoef(PointPairList signal1, PointPairList signal2)
        {
            if (signal1.Count != signal2.Count)
            {
                throw new Exception("signal's count not equal");
            }
            double sums1s2 = 0, sums1 = 0, sums2 = 0, sums1sqr = 0, sums2sqr = 0;
            for (int i = 0; i < signal1.Count; i++)
            {
                sums1s2 += signal1[i].Y * signal2[i].Y;
                sums1 += signal1[i].Y;
                sums2 += signal2[i].Y;
                sums1sqr += Math.Pow(signal1[i].Y, 2);
                sums2sqr += Math.Pow(signal2[i].Y, 2);
            }


            return (signal1.Count * sums1s2 - sums1 * sums2) / Math.Sqrt(Math.Abs(signal1.Count * sums1sqr - sums1sqr) * Math.Abs(signal1.Count * sums2sqr - sums2sqr));
        }
        public PointPairList getCorrelationFunc(PointPairList signal1, PointPairList signal2)
        {
            PointPairList correlationFunc = new PointPairList();
            double sum;
            for (int i = 0; i < signal1.Count; i++)
            {
                sum = 0;
                for (int j = 0; j < signal1.Count; j++)
                {
                    if (i + j < signal1.Count)
                    {
                        sum += signal1[i].Y * signal2[i + j].Y;
                    }
                    else
                    {
                        sum += signal1[i].Y * signal2[i + j - signal1.Count].Y;
                    }
                    
                }
                correlationFunc.Add(i, sum / signal1.Count);
            }
            return correlationFunc;
        }
        public PointPairList getFastCorrelationFunc(PointPairList signal1, PointPairList signal2)
        {
            Specter specter = new Specter();
            List<double> specter1A = specter.dpf(signal1).specterA;
            List<double> specter2A = specter.dpf(signal2).specterA;
            List<double> specter1F = specter.dpf(signal1).specterF;
            List<double> specter2F = specter.dpf(signal2).specterF;
            List<double> multF = new List<double>();
            List<double> multA = new List<double>();
            for (int i = 0; i < specter1A.Count; i++)
            {
                multA.Add(specter1A[i] * specter2A[i]);
                multF.Add(specter1F[i] * specter2F[i]);
            }
            return specter.undpf(multA, multF);
        }
    }
}
