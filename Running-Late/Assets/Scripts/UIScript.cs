using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnSkip();
        }
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnSkip()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
