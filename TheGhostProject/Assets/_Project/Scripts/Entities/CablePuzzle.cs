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
        selectedColor = color;
    }

    public void ConnectRightCable(string color)
    {
        if (selectedColor == color)
        {
            Debug.Log(color + " cable connected!");
            pairsFixed++;
            selectedColor = "";

            if (pairsFixed >= 4) FinishPuzzle();
        }
        else
        {
            Debug.Log("Wrong pairing!");
        }
        if (color == "Red") wireRed.SetActive(true);
        if (color == "Blue") wireBlue.SetActive(true);
        if (color == "Green") wireGreen.SetActive(true);
        if (color == "Yellow") wireYellow.SetActive(true);

        if (pairsFixed >= 4) FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        Debug.Log("Puzzle finished!");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CloseTerminalPuzzle();
        }
    }
}
