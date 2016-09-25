using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{

    public float speed = 100f;

    void Update()
    {
        transform.RotateAround(Vector3.up * 2, Vector3.up, -Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        transform.RotateAround(Vector3.up * 2, transform.right, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        transform.GetChild(0).Translate(Vector3.forward * Input.mouseScrollDelta.y * Time.deltaTime * speed);
    }
}
