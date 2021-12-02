using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace lab_2
{
   
    class Signal
    {
        public PointPairList generateSignal(int B1, int B2, int N)
        {
            double sum = 0;
            Random rnd = new Random();
            PointPairList signalPoints = new PointPairList();
            for (int i = 0; i < N; i++)
            {
                sum = 0;
                for (int j = 50; j < 70; j++)
                {

                    sum += Math.Pow(-1, rnd.Next(0, 1)) * B2 * Math.Sin(2 * Math.PI * i * j / N);
                }
                signalPoints.Add(i, B1 * Math.Sin(2 * Math.PI * i / N) + sum, sum);
            }
            return signalPoints;
        }
    }
}
