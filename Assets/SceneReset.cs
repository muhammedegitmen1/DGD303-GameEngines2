using UnityEngine;
using System.Collections.Generic;

public class SceneReset : MonoBehaviour
{
    public List<CollapsingPlatform> platforms = new List<CollapsingPlatform>();
    public List<SmartPatrolAI> enemies = new List<SmartPatrolAI>(); 
    
    void OnEnable()
    {
        ResetAllObjects();
    }

    public void ResetAllObjects()
    {
        foreach (CollapsingPlatform platform in platforms)
        {
            platform.ResetPlatform();
        }
        
        foreach (SmartPatrolAI enemy in enemies)
        {
            enemy.ResetEnemy();
        }
        Debug.Log(gameObject.name + " resetlendi.");
    }
}
