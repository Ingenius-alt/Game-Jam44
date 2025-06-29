using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopTrigger : MonoBehaviour
{
    public string SceneName = "RepairShop";
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Movement>())
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
            Time.timeScale = 0.0f;
        }
    }
}
