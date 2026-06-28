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
            Debug.LogError("[Terminal] Critical: GameManager couldn't be found in the scene!");
        }
    }
}
