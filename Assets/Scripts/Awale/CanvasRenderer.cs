using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Awale
{
    public class CanvasRenderer : AwaleRenderer
    {
        Button[] buttons = new Button[12];
        Text[] texts = new Text[12];

        protected override void StartDelegate()
        {
            for (int i = 0; i < 12; i++)
            {
                var x = i;
                buttons[i] = GameObject.Find(i.ToString()).GetComponent<Button>();
                buttons[i].onClick.AddListener(() => PlayMove(x, x < 6 ? 0 : 1));
                texts[i] = buttons[i].GetComponentInChildren<Text>();
            }
            Game = new Game();
        }

        protected override void UpdateDelegate()
        {
            if (Game != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    texts[i].text = Game.GetCount(i).ToString();
                }
            }
        }

        void PlayMove(int index, int player)
        {
            if (Game.PlayMove(index, player))
            {
                playerToPlay = 1 - player;
            }
            print(Game.GetWinner());
        }
    }
}
