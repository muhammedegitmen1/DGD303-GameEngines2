using UnityEngine;
using System.Collections;

public class CollapsingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;   // Time before falling
    public float destroyDelay = 3.0f; // Time to destroy after falling

    private Rigidbody2D rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Ensure platform stays in air initially
        rb.bodyType = RigidbodyType2D.Kinematic;
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
        
        Destroy(transform.parent.gameObject, destroyDelay);
    }
}