using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public float attackRange;
    public float attackCooldown; 
    
    public Transform attackPoint; 
    public LayerMask playerLayer; 

    private float nextAttackTime = 0f;
    private Transform player;

    void Start()
    {
        // Find the player automatically by Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No player object found");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if player is in range AND cooldown is finished
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
        
        //Debug.LogWarning("atk time:"  + nextAttackTime);
        
    }

    void Attack()
    {
        // 1. Detect player in range of the attack point
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        // 2. Apply damage
        foreach (Collider2D playerCollider in hitPlayers)
        {
            Health playerHealth = playerCollider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Enemy attacked the player!");
            }
        }
    }

    // Visualize the attack range in Editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}