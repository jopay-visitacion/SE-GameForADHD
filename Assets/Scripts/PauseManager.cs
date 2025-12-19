using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseUI;


    public void Pause()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseUI.SetActive(false);
    }
}
