using UnityEngine;

public class CardScript : MonoBehaviour
{
    public Transform hand, reader;
    public bool hovering;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hovering && transform.position != reader.position)
        {
            transform.position = transform.position + ((reader.position - transform.position)*(Time.deltaTime/speed));
        }
        else if(hovering == false && transform.position != hand.position)
        {
            transform.position = transform.position + ((hand.position - transform.position)*(Time.deltaTime/speed));
        }
    }

    public void OnEnterHover()
    {
        if(hovering == false)
        {
            hovering = true;
        }
    }

    public void OnExitHover()
    {
        if(hovering == true)
        {
            hovering = false;
        }
    }
}
