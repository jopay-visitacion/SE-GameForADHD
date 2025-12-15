using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public GameObject PauseButtn;
    public GameObject PauseMenu;
  public void OpenGame()
   {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
   }

  public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    } 
    public void OpenCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        PauseButtn.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        PauseButtn.SetActive(true);
    }
}
