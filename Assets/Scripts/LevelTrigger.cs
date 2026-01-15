using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call the function in GameManager
            GameManager.instance.OpenWinPanel();
        }
    }
}