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
        Debug.Log(gameObject.name + " resetlendi.");
    }
}
