using UnityEngine;

public class SmartPatrolAI : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB;
    public float speed = 2f;

    public Transform groundDetector;
    public float checkDistance = 1f;

    private Vector3 currentTarget;

    void Start()
    {
        // Set initial target to Point B
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
            // Reached A or B! Turn around
            SwitchTarget();
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