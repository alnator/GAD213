using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string scene;
   public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }
    public void LeaveGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
