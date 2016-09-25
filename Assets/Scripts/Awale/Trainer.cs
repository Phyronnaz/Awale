using System.Collections.Generic;

namespace Assets.Scripts.Awale
{
    public class Trainer
    {
        private List<AI> players = new List<AI>();

        public Trainer(int size, AI ia)
        {
            players.Add(ia);
            while (players.Count < size)
            {
                players.Add(AI.GetRandom());
            }
        }

        public void Train(int generation)
        {
            for (int i = 0; i < generation; i++)
            {
                foreach (var p in players)
                {
                    p.Score = 0;
                    foreach (var ia in players)
                    {
                        if (p != ia)
                        {
                            p.Score += Compare(p, ia);
                        }
                    }
                }
                players.Sort((a, b) => a.Score.CompareTo(b.Score));

                for (int k = 0; k < players.Count / 2; k++)
                {
                    players[players.Count - 1 - k] = players[k].GetChild((double)1 / generation);
                }
            }
        }

        public static int Compare(AI a, AI b)
        {
            var g = new Game();
            var p = 0;
            while (g.GetWinner() == -1)
            {
                if (p == 0)
                {
                    if (!a.PlayMove(g, p))
                    {
                        UnityEngine.Debug.LogError("Play error");
                        break;
                    }
                }
                else
                {
                    if (!b.PlayMove(g, p))
                    {
                        UnityEngine.Debug.LogError("Play error");
                        break;
                    }
                }
                p = 1 - p;
            }

            if (g.GetWinner() == 0)
            {
                return 1;
            }
            else if (g.GetWinner() == 1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public AI GetBestIA()
        {
            return players[0];
        }

        public void SetBestAI(AI ai)
        {
            players[0] = ai;
        }
    }
}
