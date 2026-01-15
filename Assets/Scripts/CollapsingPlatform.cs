using UnityEngine;
using System.Collections;

public class CollapsingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;   // Time before falling
    public float destroyDelay = 3.0f; // Time to destroy after falling

    private Rigidbody2D rb;
    private bool isFalling = false;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
        ResetPlatform();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Trigger fall only if Player touches from above
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            if (collision.transform.position.y > transform.position.y)
            {
                StartCoroutine(Fall());
            }
        }
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);

        // Enable physics to make it fall
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 2.5f; 
        
        //Destroy(transform.parent.gameObject, destroyDelay);
        Invoke(nameof(DeactivatePlatform), destroyDelay);
    }

    void DeactivatePlatform()
    {
         rb.bodyType = RigidbodyType2D.Static;
         rb.gravityScale = 0f;
         isFalling = false;
    }
    
    public void ResetPlatform()
    {
        StopAllCoroutines();
        isFalling = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        rb.gravityScale = 0f; 
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;

        gameObject.SetActive(true);
    }
    
}