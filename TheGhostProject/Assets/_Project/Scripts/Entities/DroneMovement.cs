using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newX = transform.position.x + Mathf.Sin(Time.time * 2f) * 0.01f;
        transform.position = new Vector2(newX, transform.position.y);

        if (Mathf.Sin(Time.time * 2f) > 0) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }
}
