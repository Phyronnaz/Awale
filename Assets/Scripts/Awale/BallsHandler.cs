using UnityEngine;

public class BallsHandler : MonoBehaviour
{

    void Update()
    {
        foreach (var r in transform.GetComponentsInChildren<Rigidbody>())
        {
            r.AddForce(transform.position - r.transform.position, ForceMode.Force);
        }
    }

    public GameObject GetBall()
    {
        return transform.GetChild(transform.childCount - 1).gameObject;
    }

    public void AddBall(GameObject ball)
    {
        ball.transform.SetParent(transform);
    }
}
