using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class HealthUI : MonoBehaviour
{
    public Health playerHealth; // Reference to the Player's Health script
    
    public int numOfHearts = 3; // Maximum number of hearts
    public Image[] hearts;      // Array to hold the hearts
    public Sprite fullHeart;    
    public Sprite emptyHeart;  

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Check if this heart index is within the current health limit
            if (i < playerHealth.currentHealth)
            {
                // Draw a full heart
                hearts[i].sprite = fullHeart;
            }
            else
            {
                // Draw an empty heart
                hearts[i].sprite = emptyHeart;
            }

            // Ensure we don't display more hearts than the player's max health cap
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}