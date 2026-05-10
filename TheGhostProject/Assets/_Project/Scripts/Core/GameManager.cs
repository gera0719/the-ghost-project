using UnityEngine;
using UnityEngine.SceneManagement;
using GhostProject.Core;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GhostProject.Core.Game gameLogic;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameLogic = new GhostProject.Core.Game();
        gameLogic.state = GameState.RUNNING;
    }

    public void PlayerDied()
    {
        Debug.Log("[GameManager] Player is dead. Restarting sector...");
        gameLogic.state = GameState.GAME_OVER; 
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}