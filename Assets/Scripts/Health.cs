using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

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

    void Die()
    {
        // if player dies restart level
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // if its not player destroy
        else
        {
            Destroy(gameObject);
        }
    }
}