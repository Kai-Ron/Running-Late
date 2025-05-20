using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour
{
    public Image image;
    public Color on, off;
    public float delay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(OpenScene());
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

    public void End()
    {
        StartCoroutine(ChangeScene());
    }

    public IEnumerator ChangeScene()
    {

        yield return new WaitForSeconds(52.0f);

        image.color = on;

        SceneManager.LoadScene(1);
    }

    public IEnumerator OpenScene()
    {
        float time = 0;

        while(delay > time)
        {
            float ratio = time/delay;
            image.color = Color.Lerp(on, off, ratio);
            time += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        image.enabled = false;
    }
}
