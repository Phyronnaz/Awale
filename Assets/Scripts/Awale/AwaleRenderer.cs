using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Awale
{
    public class AwaleRenderer : MonoBehaviour
    {
        public static Game Game;
        public static int playerToPlay = 0;
        Text[] scores = new Text[2];

        GameObject player0Indicator;
        GameObject player1Indicator;

        protected virtual void StartDelegate()
        {
        }

        protected virtual void UpdateDelegate()
        {
        }

        protected void Start()
        {
            scores[0] = GameObject.Find("ScorePlayer0").GetComponentInChildren<Text>();
            scores[1] = GameObject.Find("ScorePlayer1").GetComponentInChildren<Text>();
            player0Indicator = GameObject.Find("Player0Indicator");
            player1Indicator = GameObject.Find("Player1Indicator");
            StartDelegate();
        }

        protected void Update()
        {
            scores[0].text = Game.GetScore(0).ToString();
            scores[1].text = Game.GetScore(1).ToString();
            player0Indicator.SetActive(playerToPlay == 0);
            player1Indicator.SetActive(playerToPlay == 1);
            UpdateDelegate();
        }
    }
}
