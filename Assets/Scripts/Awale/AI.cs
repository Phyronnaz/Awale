using System;

namespace Assets.Scripts.Awale
{
    public class AI
    {
        public static Random random = new Random(0);
        public int Score = 0;
        private double[,] nn = new double[12, 6];

        public AI(double[,] nn)
        {
            this.nn = nn;
        }

        public AI(AIStruct a)
        {
            nn = a.GetNN();
        }

        public bool PlayMove(Game g, int player)
        {
            var b = g.Board;
            var r = new double[6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    r[i] = Math.Tanh(nn[j, i] * b[(j + player * 6) % 12] / 24);
                }
            }
            var loop = true;
            int c = 0;
            while (loop)
            {
                var m = r[0];
                var k = 0;
                for (int i = 1; i < 6; i++)
                {
                    if (r[i] > m)
                    {
                        m = r[i];
                        k = i;
                    }
                }
                loop = !g.PlayMove(k + player * 6, player);
                r[k] = -10000000;
                if (c == 7)
                {
                    return false;
                }
                c++;
            }
            return true;
        }

        public AI GetChild(double variation)
        {
            var n = new double[12, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    n[j, i] = nn[j, i] + (random.NextDouble() * 2 - 1) * variation;
                }
            }
            return new AI(n);
        }

        public static AI GetRandom()
        {
            var n = new double[12, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    n[j, i] = random.NextDouble() * 2 - 1;
                }
            }
            return new AI(n);
        }

        public AIStruct GetAIStruct()
        {
            return new AIStruct(nn);
        }
    }
}
