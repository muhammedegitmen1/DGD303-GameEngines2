using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Getting damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        // debug in console for now
        Debug.Log(gameObject.name + " Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void HealFull()
    {
        currentHealth = maxHealth;
        Debug.Log("Player healed to full health!");
    }

    void Die()
    {
        // if player dies restart level
        if (gameObject.CompareTag("Player"))
        {
            //restart level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // if its not player destroy
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}