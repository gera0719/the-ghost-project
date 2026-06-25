using UnityEngine;
using UnityEngine.UI;

public class CablePuzzle : MonoBehaviour
{
    [Header("Cable visuals")]
    [SerializeField] private GameObject wireRed;
    [SerializeField] private GameObject wireBlue;
    [SerializeField] private GameObject wireGreen;
    [SerializeField] private GameObject wireYellow;

    private string selectedColor = "";
    private int pairsFixed = 0;

    private bool isRedFixed = false;
    private bool isBlueFixed = false;
    private bool isGreenFixed = false;
    private bool isYellowFixed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectLeftCable(string color)
    {
        if (color == "Red" && isRedFixed) return;
        if (color == "Blue" && isBlueFixed) return;
        if (color == "Green" && isGreenFixed) return;
        if (color == "Yellow" && isYellowFixed) return;

        selectedColor = color;
        Debug.Log("Left cable selected: " + color);
    }

    public void ConnectRightCable(string color)
    {
        if (string.IsNullOrEmpty(selectedColor))
        {
            Debug.Log("Select a left cable first!");
            return;
        }

        if (selectedColor == color)
        {
            Debug.Log("<color=green>[PUZZLE]:</color> " + color + " cable connected successfully!");
            pairsFixed++;

            if (color == "Red") { wireRed.SetActive(true); isRedFixed = true; }
            if (color == "Blue") { wireBlue.SetActive(true); isBlueFixed = true; }
            if (color == "Green") { wireGreen.SetActive(true); isGreenFixed = true; }
            if (color == "Yellow") { wireYellow.SetActive(true); isYellowFixed = true; }

            selectedColor = "";

            if (pairsFixed >= 4)
            {
                FinishPuzzle();
            }
        }
        else
        {
            Debug.Log("<color=red>[PUZZLE]:</color> Wrong pairing! Resetting selection.");
            selectedColor = "";
        }
    }

    private void FinishPuzzle()
    {
        Debug.Log("Puzzle finished!");

        pairsFixed = 0;
        isRedFixed = false;
        isBlueFixed = false;
        isGreenFixed = false;
        isYellowFixed = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.CloseTerminalPuzzle();
        }

        gameObject.SetActive(false);
    }
}
