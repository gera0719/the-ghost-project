using UnityEngine;
using GhostProject.Core; // [cite: 166]

public class HazardTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[Hazard] A játékos savba vagy lézerbe lépett!");
            GameManager.Instance.PlayerDied();
        }
    }
}