using UnityEngine;

public class TimeController : MonoBehaviour
{
    bool isPaused = true;

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 0.2f;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

}
