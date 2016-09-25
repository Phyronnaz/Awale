namespace Assets.Scripts.Awale
{

    public delegate void MoveDelegate(int a, int b, int c);
    public class Game
    {
        public int[] Board = new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };

        private int[] scores = new int[2];
        private int currentPlayer = 0;
        private int moveCount = 0;

        public MoveDelegate MoveDelegate;

        public int GetCount(int index)
        {
            return Board[index % 12];
        }

        public int GetScore(int player)
        {
            return scores[player % 2];
        }

        public int GetWinner()
        {
            if (moveCount > 250 || scores[0] > 24 || scores[1] > 24 || scores[0] + scores[1] == 48)
            {
                if (scores[0] > scores[1])
                {
                    return 0;
                }
                else if (scores[1] > scores[0])
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return -1;
            }
        }

        public bool PlayMove(int index, int player)
        {
            /*
             * Wrong player
             */
            if (player != currentPlayer)
            {
                return false;
            }
            /*
             * Wrong side
             */
            if ((index > 5 && player == 0) || (index < 6 && player == 1))
            {
                return false;
            }
            /*
             * Empty move
             */
            if (Board[index] == 0)
            {
                return false;
            }
            /*
             * Starve
             */
            if (IsEmpty(1 - player) && Board[index] + index % 6 < 6)
            {
                if (CanFeed(1 - player))
                {
                    return false;
                }
                else
                {
                    //Eat all
                    for (int k = player * 6; k < (player + 1) * 6; k++)
                    {
                        Move(k, 12 + player, Board[k]);
                    }
                }
            }
            /*
             * Play
             */
            var i = index;
            while (Board[index] != 0)
            {
                i++;
                if (i % 12 == index % 12)
                {
                    i++;
                }
                Move(index, i % 12, 1);
            }
            /*
             * Check if we are going to starve other player
             */
            bool willStarve = false;
            if ((player == 0 && i % 12 > 5) || (player == 1 && i % 12 < 6))
            {
                willStarve = true;
                for (int k = (1 - player) * 6; k < (1 - player + 1) * 6; k++)
                {
                    willStarve = willStarve && (Board[k] == 0 || ((Board[k] == 2 || Board[k] == 3) && k <= i % 12));
                }
            }
            /*
             * Eat
             */
            if (!willStarve)
            {
                while (i != index && (Board[i % 12] == 2 || Board[i % 12] == 3) && ((player == 0 && i % 12 > 5) || (player == 1 && i % 12 < 6)))
                {
                    Move(i % 12, player + 12, Board[i % 12]);
                    i--;
                }
            }

            currentPlayer = 1 - currentPlayer;
            moveCount++;
            return true;
        }

        private void Move(int from, int to, int amount)
        {
            if (to < 12)
            {
                Board[from] -= amount;
                Board[to] += amount;
            }
            else
            {
                Board[from] -= amount;
                scores[to - 12] += amount;
            }
            if (MoveDelegate != null)
            {
                MoveDelegate(from, to, amount);
            }
        }

        private bool IsEmpty(int player)
        {
            bool empty = true;
            for (int i = player * 6; i < (player + 1) * 6; i++)
            {
                empty = empty && Board[i] == 0;
            }
            return empty;
        }

        private bool CanFeed(int playerToFeed)
        {
            bool canFeed = false;
            for (int i = (1 - playerToFeed) * 6; i < (1 - playerToFeed + 1) * 6; i++)
            {
                canFeed = canFeed || (Board[i] + i % 6 > 5);
            }
            return canFeed;
        }
    }
}
