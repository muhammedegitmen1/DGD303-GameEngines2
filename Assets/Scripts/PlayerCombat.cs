using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint; 
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public float attackRate = 2f; 
    public LayerMask enemyLayers; 

    private float nextAttackTime = 0f;
    public Animator animator;
    
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            // Fire1: Left Click or Left Ctrl
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // for animation
        animator.SetTrigger("Attack");

        // find enemies at attack point
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // hit them all
        foreach (Collider2D enemy in hitEnemies)
        {
            // give damage
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    // for visualisation
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}