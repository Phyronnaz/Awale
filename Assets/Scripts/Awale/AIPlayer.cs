using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Awale
{
    public class AIPlayer : MonoBehaviour
    {
        public InputField size;
        public InputField generations;
        public static Trainer Trainer;

        void Start()
        {
            //InvokeRepeating("Play", 0, 0.00002f);
        }

        public void Train()
        {
            Trainer = new Trainer(int.Parse(size.text), AI.GetRandom());
            Trainer.Train(int.Parse(generations.text));
        }

        public void Play()
        {
            if (Trainer == null)
            {
                Trainer = new Trainer(int.Parse(size.text), AI.GetRandom());
            }
            if (!Trainer.GetBestIA().PlayMove(AwaleRenderer.Game, AwaleRenderer.playerToPlay))
            {
                CancelInvoke();
                Debug.Break();
            }
            else
            {
                AwaleRenderer.playerToPlay = 1 - AwaleRenderer.playerToPlay;
                //if (AwaleRenderer.Game.GetWinner() != -1)
                //{
                //    AwaleRenderer.Game = new Game();
                //    AwaleRenderer.playerToPlay = 0;
                //    trainer = new Trainer(int.Parse(size.text), AI.GetRandom());
                //}
            }
        }

    }
}
