using UnityEngine;
using GhostProject.Core;

public class HazardTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[Hazard] Player collided with hazard!");
            GameManager.Instance.PlayerDied();
        }
    }
}