using UnityEngine;

public class SmartPatrolAI : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB;
    public float speed = 2f;

    public Transform groundDetector;
    public float checkDistance = 1f;

    private Vector3 currentTarget;

    public Animator anim;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Health healthScript;
    
    void Awake() // Start yerine Awake kullanmak daha güvenli
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        healthScript = GetComponent<Health>();
    }
    
    public void ResetEnemy()
    {
        gameObject.SetActive(true);

        transform.position = startPosition;
        transform.rotation = startRotation;

        if (healthScript != null)
        {
            healthScript.ResetHealth();
        }

        currentTarget = pointB.position;
    }
    
    void Update()
    {
        // 1. Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // 2. CLIFF CHECK
        // Check ground strictly downwards from the detector
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, checkDistance);

        if (groundInfo.collider == false)
        {
            // Cliff detected! Force turn around immediately
            SwitchTarget();
        }
        // 3. TARGET CHECK
        // Check if we reached the patrol point
        else if (Vector2.Distance(transform.position, currentTarget) < 0.2f)
        {
            SwitchTarget();
        }
        
        anim.SetBool("isWalking", true);
    }
    
    public void StopMoving(bool stop)
    {
        if(stop)
        {
            speed = 0;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = 2f; // Varsayılan hızın neyse ona döndür
            anim.SetBool("isWalking", true);
        }
    }

    void SwitchTarget()
    {
        // Determine which point we were going to, and swap it
        // Also rotate the character to face the new direction
        
        if (currentTarget == pointB.position)
        {
            currentTarget = pointA.position;
            transform.eulerAngles = new Vector3(0, -180, 0); // Face Left
        }
        else
        {
            currentTarget = pointB.position;
            transform.eulerAngles = new Vector3(0, 0, 0); // Face Right
        }
    }
    
    // Visualization for Debugging
    private void OnDrawGizmos()
    {
        if (groundDetector != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundDetector.position, groundDetector.position + Vector3.down * checkDistance);
        }
    }
}