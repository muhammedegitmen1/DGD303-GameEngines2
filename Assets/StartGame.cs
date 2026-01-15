using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject gameManager;
    
    public void Starter()
    {
        gameManager.SetActive(true);
        this.gameObject.SetActive(false);
    }


    public void Exit()
    {
        Application.Quit();
    }
    
}
