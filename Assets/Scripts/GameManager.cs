using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern for easy access

    [Header("Level Management")]
    public GameObject[] levelObjects;   
    public Transform[] spawnPoints;     // PLayer Spawn Points for each level
    public int currentLevelIndex = 0;

    [Header("Player References")]
    public GameObject player;
    public Health playerHealth;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject finishPanel;

    void Awake()
    {
        // Set up the singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        Time.timeScale = 1;
        
        // Disable all levels first
        foreach (GameObject level in levelObjects)
        {
            level.SetActive(false);
        }

        // Activate only the first level
        if (levelObjects.Length > 0)
        {
            levelObjects[0].SetActive(true);
            MovePlayerToSpawn(0);
        }
    }

    public void OpenWinPanel()
    {
        Debug.LogWarning("index level: " + currentLevelIndex);
        Time.timeScale = 0.001f;

        if (currentLevelIndex < 3)
        {
            winPanel.SetActive(true);
        }
        else
        {
            finishPanel.SetActive(true);
        }
        
        
    }
    
    public void OpenLosePanel()
    {
        Time.timeScale = 0.001f;
        losePanel.SetActive(true);
    }

    public void ReplayFromStart()
    {
        finishPanel.SetActive(false);
        levelObjects[currentLevelIndex].SetActive(false);
        
        currentLevelIndex = 0;
        levelObjects[0].SetActive(true);
        
        MovePlayerToSpawn(currentLevelIndex);
        if (playerHealth != null)
        {
            playerHealth.HealFull();
        }
        Time.timeScale = 1;
    }
    
    public void PlayAgain()
    {
        levelObjects[currentLevelIndex].GetComponent<SceneReset>().ResetAllObjects();
        MovePlayerToSpawn(currentLevelIndex);
            
        // Heal the player
        if (playerHealth != null)
        {
            playerHealth.HealFull(); // We need to add this method to Health.cs
        }
        
        Time.timeScale = 1f;
        losePanel.SetActive(false);
        
    }
    
    public void NextLevel()
    {
        //Check if we have more levels
        if (currentLevelIndex < levelObjects.Length)
        {
            currentLevelIndex++;
            
            // Activate next level
            if (currentLevelIndex < levelObjects.Length)
            {
                levelObjects[currentLevelIndex -1].SetActive(false);
                levelObjects[currentLevelIndex].SetActive(true);
            }
            
            // Move player to new spawn point
            MovePlayerToSpawn(currentLevelIndex);
            
            // Heal the player
            if (playerHealth != null)
            {
                playerHealth.HealFull(); // We need to add this method to Health.cs
            }
            
            winPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.Log("Game Finished! You beat all levels.");
            // Show Win Screen
            Time.timeScale = 0.001f;
            finishPanel.SetActive(true);
        }
    }

    void MovePlayerToSpawn(int index)
    {
        if (spawnPoints[index] != null)
        {
            player.transform.position = spawnPoints[index].position;
        }
    }
}