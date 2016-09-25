using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Awale
{
    public class GraphicalRenderer : AwaleRenderer
    {
        BallsHandler[] ballsHandlers = new BallsHandler[14];

        protected override void StartDelegate()
        {
            for (int i = 0; i < 12; i++)
            {
                ballsHandlers[i] = GameObject.Find("Balls " + i.ToString()).GetComponent<BallsHandler>();
            }
            ballsHandlers[12] = GameObject.Find("Player0Score").GetComponent<BallsHandler>();
            ballsHandlers[13] = GameObject.Find("Player1Score").GetComponent<BallsHandler>();
            Game = new Game();
            Game.MoveDelegate = MoveDelegate;
        }

        protected override void UpdateDelegate()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.layer == 9)
                    {
                        if (Game.PlayMove(int.Parse(hit.transform.name.Substring(6)), playerToPlay))
                        {
                            playerToPlay = 1 - playerToPlay;
                        }
                    }
                }
            }
        }

        void MoveDelegate(int from, int to, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                ballsHandlers[to].AddBall(ballsHandlers[from].GetBall());
            }
        }
    }
}

