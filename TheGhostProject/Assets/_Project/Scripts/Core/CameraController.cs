using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target; 
    [SerializeField] private float smoothSpeed = 0.125f;

    [Header("Level Bounds")]
    [SerializeField] private float minX; 
    [SerializeField] private float maxX; 

    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
        {
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float targetX = target.position.x;

        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 desiredPosition = new Vector3(clampedX, transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
