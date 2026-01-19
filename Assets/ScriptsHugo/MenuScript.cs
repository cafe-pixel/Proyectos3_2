using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;
    public AudioClip menuMusic;

    private void Start()
    {
        AudioManager.Instance.ChangeMusic(menuMusic);
    }
    
    public void PlayGame()
    {
        AudioManager.Instance.StopMusic();
        SceneManager.LoadSceneAsync(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
