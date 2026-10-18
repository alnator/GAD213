using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private SettingsMenu settingsMenu;
    //private bool isPaused= false;

    void Start()
    {
        Time.timeScale =1f;
       pausePanel.SetActive(false);
    }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.IsGameOver())
            {
                return;
            }
            if (GameManager.Instance.IsPaused())
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }
    void PauseGame()
    {
        GameManager.Instance.SetPaused();       
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        // CursorLocker();
    }
     public void ResumeGame()
    {
        GameManager.Instance.SetPlaying();
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        //settingsMenu.CloseSettings();
        // CursorLocker();
        
    }
    //     void CursorLocker()
    // {

    //         if(GameManager.Instance.IsPlaying())
    //         {
    //         Cursor.lockState = CursorLockMode.Locked;
    //         Cursor.visible = false;
    //         }
    //         if (GameManager.Instance.IsPaused())
    //         {
    //             Cursor.lockState = CursorLockMode.None;
    //             Cursor.visible = true;
    //         }
    // }
    public void MainMenuButton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale =1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}