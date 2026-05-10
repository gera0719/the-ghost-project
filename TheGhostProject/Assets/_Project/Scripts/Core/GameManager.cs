using UnityEngine;
using UnityEngine.SceneManagement;
using GhostProject.Core;

using UnityObject = UnityEngine.GameObject;
using CoreObject = GhostProject.Core.GameObject;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private UnityObject puzzleUI;
    public static GameManager Instance { get; private set; }

    private GhostProject.Core.Game gameLogic;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameLogic = new GhostProject.Core.Game();
        gameLogic.state = GameState.RUNNING;
    }

    public void OpenTerminalPuzzle()
    {
        if (puzzleUI != null)
        {
            puzzleUI.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("[GameManager] Puzzle activated.");
        }
        else
        {
            Debug.LogError("[GameManager] No Puzzle UI set!");
        }
    }

    public void CloseTerminalPuzzle()
    {
        if (puzzleUI != null)
        {
            puzzleUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void PlayerDied()
    {
        Debug.Log("[GameManager] Player is dead. Restarting sector...");
        gameLogic.state = GameState.GAME_OVER; 
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}