using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Awale
{
    [Serializable]
    public struct AIStruct
    {
        public double[] NN;

        public AIStruct(double[,] nn)
        {
            NN = new double[72];
            int c = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    NN[c] = nn[j, i];
                    c++;
                }
            }
        }

        public double[,] GetNN()
        {
            var nn = new double[12, 6];
            int c = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    nn[j, i] = NN[c];
                    c++;
                }
            }
            return nn;
        }
    }
}
