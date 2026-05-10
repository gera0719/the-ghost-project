using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target; // A Player Transform-ja
    [SerializeField] private float smoothSpeed = 0.125f;

    [Header("Level Bounds")]
    [SerializeField] private float minX; // A pálya bal széle
    [SerializeField] private float maxX; // A pálya jobb széle

    private Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        // Azonnal a célponthoz ugrunk, hogy ne legyen "beúszás" az elején
        if (target != null)
        {
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Kiszámoljuk a cél pozíciót (csak X tengelyen mozgunk)
        float targetX = target.position.x;

        // 2. Korlátozzuk az értéket a pálya szélei között
        // A határértékeket a pálya szélessége és a kamera látószöge alapján kell belőni
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 desiredPosition = new Vector3(clampedX, transform.position.y, transform.position.z);

        // 3. Simított mozgás (opcionális, de szebb)
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
