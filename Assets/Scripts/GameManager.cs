using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        Win,
        Lose
    }
    private GameState currentState = GameState.Playing;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if(Instance != null && Instance !=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance =this;
    }
    public void SetPlaying()
    {
        currentState = GameState.Playing;
    }
    public void SetPaused()
    {
        currentState = GameState.Paused;
    }
    public void SetWin()
    {
        currentState = GameState.Win;
    }
    public void SetLose()
    {
        currentState = GameState.Lose;
    }
    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
        
    }
    public bool IsPaused()
    {
         return currentState == GameState.Paused;
    }
    public bool IsLose()
    {
        return currentState == GameState.Lose;
    }
    public bool IsWin()
    {
        return currentState == GameState.Win;
    }
    public bool IsGameOver()
    {
        return currentState == GameState.Win || currentState == GameState.Lose;
    }
    void OnDestroy()
{
    if(Instance == this)
    {
        Instance = null;
    }
}
}