using UnityEngine;

public class TerminalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject puzzleUI;

    public void Interact()
    {
        Debug.Log("[Terminal] Interaction initiated!");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OpenTerminalPuzzle();
        }
        else
        {
            // HA EZT LÁTOD A CONSOLE-BAN, MEG VAN A HIBA!
            Debug.LogError("[Terminal] Critical: GameManager couldn't be found in the scene!");
        }
    }
}
