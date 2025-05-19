using UnityEngine;

public class SeamusMovement : MonoBehaviour
{
    Rigidbody rb;
    public int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(Vector3.forward * speed);
    }

    public void SlowDown()
    {
        rb.linearVelocity = Vector3.zero;
    }

    public void TurnLeft()
    {
        rb.transform.Rotate(0, 90, 0);
    }
}
