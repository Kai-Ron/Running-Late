using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != parent)
        {
            transform.parent = parent;
        }
    }
}
