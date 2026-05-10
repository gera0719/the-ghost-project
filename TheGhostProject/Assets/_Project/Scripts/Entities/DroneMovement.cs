using UnityEngine;

public class DronePatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float range = 3f;
    public float speed = 1f;
    public string patrolType = "horizontal";

    private Vector3 startPos;
    private float baseScale = 0.46f;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * range;

        if (patrolType == "vertical")
        {
            transform.position = startPos + new Vector3(0, offset, 0);
        }
        else
        {
            transform.position = startPos + new Vector3(offset, 0, 0);

            if (offset > 0.1f) transform.localScale = new Vector3(baseScale, baseScale, 1f);
            else if (offset < -0.1f) transform.localScale = new Vector3(-baseScale, baseScale, 1f);
        }
    }
}
