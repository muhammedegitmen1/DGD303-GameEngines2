using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA; // Start point
    public Transform posB; // End point
    public float speed = 3.0f;

    private Vector3 targetPos;

    void Start()
    {
        // Start moving towards B
        targetPos = posB.position;
    }

    void Update()
    {
        // Check distance to target to switch direction
        if (Vector2.Distance(transform.position, posA.position) < 0.1f)
        {
            targetPos = posB.position;
        }
        else if (Vector2.Distance(transform.position, posB.position) < 0.1f)
        {
            targetPos = posA.position;
        }

        // Move 
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
    
    // Makes the player move with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}