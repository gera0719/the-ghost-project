using UnityEngine;
using UnityEngine.SceneManagement;
using GhostProject.Core;

using UnityObject = UnityEngine.GameObject;
using CoreObject = GhostProject.Core.GameObject;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private UnityObject puzzleUI;
    [Header("Managers")]
    [SerializeField] private WorldManager worldManager;
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

            Debug.Log("[GameManager] Puzzle completed successfully. Transitioning to next sector...");

            if (worldManager != null)
            {
                worldManager.NextSector();
            }
            else
            {
                Debug.LogError("[GameManager] WorldManager reference is missing in the Inspector!");
            }
        }
    }

    public void PlayerDied()
    {
        Debug.Log("[GameManager] Player is dead. Reloading CURRENT sector...");
        gameLogic.state = GameState.GAME_OVER;

        if (worldManager != null)
        {
            gameLogic.state = GameState.RUNNING;

            worldManager.ReloadCurrentSector();
        }
        else
        {
            Debug.LogError("[GameManager] WorldManager reference is missing! Falling back to Scene reload.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}